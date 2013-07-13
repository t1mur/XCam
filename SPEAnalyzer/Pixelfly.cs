using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices; // For DllImport
using System.Windows.Forms;
using System.IO;
namespace XCamera
{
    // singleton class
    // This class wraps the uniform_SDK.dll 
    // 
    sealed unsafe class Pixelfly
    {
        CamInfo camInfo ;
        byte[] camdata = new byte[10000];
        public const int NBuffer = 4;
        private int nextBufferID = 0;
        List<UInt16[]> buffers = new List<UInt16[]>();
        string cameraName = "PIXELFLY";
        int boardNumber = 0;
        int camID=0;
        private CameraMode cameraMode;
        //int mode = 2; //0=video, 1=async/QEfastshutter, 2=doubleShutter
        int subMode = 0; // sensicam only
        //int trig = 0; // 0=software, 1=hardware-rising, 2=hardware falling
        int roix1 = 1; // fixed for pixelfly  //1392x1024
        int roix2 = 43; // fixed for pixelfly
        int roiy1 = 1; // fixed for pixelfly
        int roiy2 = 33; // fixed for pixelfly
        int hbin = 1;// 1 or 2
        int vbin = 1;// 1 or 2
        //int gain = 0; // 0 or 1
        int delay = 0; // sensicam only
        int exptime = 50; //async in us, video in ms
        uint timeout = 1; // I think in ms

        private static bool initialized = false;


        public List<SingleImage> images;


        [DllImport ("uniform_SDK",EntryPoint="SELECT_CAMERA",CallingConvention= CallingConvention.Cdecl)]
        private static extern unsafe int SELECT_CAMERA(
            [MarshalAs(UnmanagedType.LPStr)] string cameraName,
            int board_number,
            ref int camID,
            [MarshalAs(UnmanagedType.LPArray)] byte[] camData
            );

        [DllImport("uniform_SDK", EntryPoint = "SETUP_CAMERA", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int SETUP_CAMERA(
            int camID,
            [MarshalAs(UnmanagedType.LPArray)] byte[] camdata,
            int mode, int submode, int trigger, int roix1, 
            int roix2, int roiy1, int roiy2, int hbin, int vbin, int gain, int delay , int exptime
             );

        [DllImport("uniform_SDK", EntryPoint = "SNAP", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int SNAP
            (int camID,
            [MarshalAs(UnmanagedType.LPArray)] byte[] camdata,
            int mode);

        [DllImport("uniform_SDK", EntryPoint = "GETIMAGE", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int GETIMAGE(
            int camID,
            [MarshalAs(UnmanagedType.LPArray)] byte[] camdata,
            uint timeout);

        [DllImport("uniform_SDK", EntryPoint = "STOP_CCD", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int STOP_CCD(
            int camID, 
            [MarshalAs(UnmanagedType.LPArray)] byte[] camdata
            );

        [DllImport("uniform_SDK", EntryPoint = "CLOSE_CAMERA", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int CLOSE_CAMERA(
            int camID,
            [MarshalAs(UnmanagedType.LPArray)] byte[] camdata
            );

        
        [DllImport("uniform_SDK", EntryPoint = "GETCAMINFO", CallingConvention = CallingConvention.Cdecl)]
        private static extern unsafe int GETCAMINFO
            (int camID,
            [MarshalAs(UnmanagedType.LPArray)] byte[] camdata,
            out int color,
            out int shutter, out int x, out int y, out int ele_temperature, out int ccd_temperature);


        public Pixelfly()
        {
            Console.WriteLine("Puxelfly()");
            if (initialized) throw new Exception("Initialized before. There is a BUG in your program.");
            images = new List<SingleImage>();
            for (int i = 0; i < 1; i++) images.Add(null);
            for (int i = 0; i < NBuffer; i++) buffers.Add(new UInt16[1392 * 1024]);
            initialized = true;
        }

        public int CameraInitializeCamera()
        {
            Console.WriteLine("CameraInitializeCamera");
            camInfo = new CamInfo();
            int err = SELECT_CAMERA(cameraName, boardNumber, ref camID, camdata);
            if (err != 0)
            {
                MessageBox.Show(PixelFlyError.getErrorString(err));
                return err;
            }
            //MessageBox.Show("CamID=" + camID);
            parseCamData();

            return err;
        }

        public int CameraSetupCamera(CameraMode mode, CameraTrigger trig, CameraGain gain, int exposureTime)
        {
            Console.WriteLine("CameraSetupCamera");
            this.exptime = exposureTime;
            cameraMode = mode;
            int err = SETUP_CAMERA(camID,
                camdata, mode.Value, subMode, trig.Value,
                roix1, roix2, roiy1, roiy2, hbin, vbin, gain.Value, delay, exptime
              );
            if (err != 0)
            {
                MessageBox.Show("SETUP_CAMERA:" + PixelFlyError.getErrorString(err));
                return err;
            }
            parseCamData();

            /*
            MessageBox.Show(
                ", color=" + camInfo.color +
                ", shutter=" + camInfo.shutter +
                ", xMax=" + camInfo.x_max +
                ", yMax=" + camInfo.y_max + "\n" +
                ", gain=" + camInfo.setting.gain +
                ", imgx=" + camInfo.setting.imgx +
                ", imgy=" + camInfo.setting.imgy +
                ", submode=" + camInfo.setting.submode
            );*/
            return err;
        }

        public int CameraSnapCamera() // asking the camera to take image
        {
            Console.WriteLine("CameraSnapCamera");
            int err = SNAP(camID, camdata, 0);
            if (err != 0)
            {
                MessageBox.Show("SETUP_CAMERA:" + PixelFlyError.getErrorString(err));
                return err;
            }
            nextBufferID = 0;
            return err;
        }

        public int CameraGetImage()
        {
            Console.WriteLine("CameragetImageCamera");
            int bufferID = nextBufferID;
            nextBufferID++;
            if (bufferID >= NBuffer) throw new Exception("Buffer ID must be smalled than NBuffer=" + NBuffer);
            int err = GETIMAGE(camID, camdata, timeout);
            Console.WriteLine("GetImage error =" + err);
            if (err != 0)
            {
                //MessageBox.Show("GETIMAGE:" + PixelFlyError.getErrorString(err));
                nextBufferID--;
                return err;
            }
            UInt16 * ptr = (UInt16*) camInfo.pictptr;
            UInt16[] buffer = buffers[bufferID];
            for (int i = 0; i < 1024 * 1392; i++)
            {
                buffer[i] = *(ptr + i);
            }

            if (cameraMode == CameraMode.DoubleShutter) // there is second frame for double shutter
            {
                UInt16* ptr2 = (UInt16*)(new IntPtr(camInfo.pictptr.ToInt32() + 1024 * 1392 * 2)).ToPointer();
                buffer = buffers[bufferID + 1];
                nextBufferID++;
                for (int i = 0; i < 1024 * 1392; i++)
                {
                    buffer[i] = *(ptr2 + i);
                }
            }
           
            return err;
        }

        private SingleImage getImage(int bufferID)
        {
            if (bufferID >= NBuffer) throw new Exception("Buffer ID must be smalled than NBuffer=" + NBuffer);
            SingleImage image = new SingleImage(1024, 1392);
            UInt16[] buffer = buffers[bufferID];
            for (int i = 0; i < 1024; i++)
            {
                for (int j = 0; j < 1392; j++)
                {
                    image.data[i, j] = buffer[i*1392+j];
                }
            }
            return image;
        }

        public List<SingleImage> getImages()
        {
            List<SingleImage> result = new List<SingleImage>();
            for (int i = 0; i < nextBufferID; i++)
            {
                result.Add(getImage(i));
            }
            return result;
        }

        // Added function wrapper for stopping CCD - try to solve GETIMAGE error
        public int CameraStopCCD()
        {
            int err = STOP_CCD(camID, camdata);
            if (err != 0)
            {
                MessageBox.Show("STOP_CCD:" + PixelFlyError.getErrorString(err));
                return err;
            }
            return err;
        }
        
        public int CameraCloseCamera()
        {
            int err = CLOSE_CAMERA(camID, camdata);
            if (err != 0)
            {
                MessageBox.Show("CLOSE_CAMERA:" + PixelFlyError.getErrorString(err));
                return err;
            }
            return err;
        }

        private void parseCamData()
        {
            //Parsing the data
            MemoryStream stream = new MemoryStream(camdata);
            BinaryReader br = new BinaryReader(stream);
            camInfo.initOK = br.ReadBoolean();
            br.ReadChars(3); // BOOL is 4 bytes
            camInfo.boardnum = br.ReadInt32();
            camInfo.camName = "";
            string s = "";
            for (int i = 0; i < 20; i++)
            {
                byte b = br.ReadByte();
                s += b + " ";
                if (b > 0) camInfo.camName += Convert.ToChar(b);
            }
            camInfo.color = br.ReadInt32();
            camInfo.shutter = br.ReadInt32();
            camInfo.x_max = br.ReadInt32();
            camInfo.y_max = br.ReadInt32();
            camInfo.pictptr = (IntPtr)br.ReadInt32();

            // camsetting
            CamSetting setting = new CamSetting();
            setting.setupOK = br.ReadBoolean();
            br.ReadChars(3);
            setting.mode = br.ReadInt32();
            setting.trig = br.ReadInt32();
            setting.exptime = br.ReadInt32();
            setting.hbin = br.ReadInt32();
            setting.vbin = br.ReadInt32();
            setting.delay = br.ReadInt32();
            setting.gain = br.ReadInt32();
            setting.roi.left = br.ReadInt32();
            setting.roi.top = br.ReadInt32();
            setting.roi.right = br.ReadInt32();
            setting.roi.bottom = br.ReadInt32();
            setting.imgx = br.ReadInt32();
            setting.imgy = br.ReadInt32();
            setting.submode = br.ReadInt32();
            camInfo.setting = setting;
        }
    }

    [Serializable]
    public class CameraMode
    {
        public const int N = 3;
        public int type;
        public static string[] names = { "Video", "Normal","Double Shutter" };
        public static CameraMode[] allModes;
        public static readonly CameraMode Video = new CameraMode(0);
        public static readonly CameraMode Async = new CameraMode(1);
        public static readonly CameraMode DoubleShutter = new CameraMode(2);

        public CameraMode() {}
        private CameraMode(int type) 
        { 
            this.type = type;
            if (allModes == null) allModes = new CameraMode[N];
            allModes[type] = this;
        }
        public string Name() { if (type < N)  { return names[type];} else  return "No name";}
        public int Value { get{return type;}}
        public static bool operator ==(CameraMode cm1, CameraMode cm2) {return (cm1.type==cm2.type);}
        public static bool operator !=(CameraMode cm1, CameraMode cm2) {return (cm1.type!=cm2.type);}
        public override bool  Equals(object obj)
        {
            if (obj==null) return false;
         	if (obj.GetType() == this.GetType())
                 return (((CameraMode) obj) == this);
            return false;
        }
        public override int GetHashCode()   {return 1;}
        public override string ToString() { return names[type]; }

    }

    [Serializable]
    public class CameraTrigger
    {
        public const int N = 3;
        public int type;
        public static string[] names = { "Software", "Hardware Rising", "Hardware Falling" };
        public static CameraTrigger[] allTriggers;
        public static readonly CameraTrigger Software = new CameraTrigger(0);
        public static readonly CameraTrigger HardwareRising = new CameraTrigger(1);
        public static readonly CameraTrigger HardwareFalling = new CameraTrigger(2);

        public CameraTrigger() { }
        private CameraTrigger(int type) { 
            this.type = type;
            if (allTriggers == null) allTriggers = new CameraTrigger[N];
            allTriggers[type] = this;
        }
        public string Name() { if (type < N) { return names[type]; } else  return "No name"; }
        public int Value { get { return type; } }
        public static bool operator ==(CameraTrigger cm1, CameraTrigger cm2) { return (cm1.type == cm2.type); }
        public static bool operator !=(CameraTrigger cm1, CameraTrigger cm2) { return (cm1.type != cm2.type); }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() == this.GetType())
                return (((CameraTrigger)obj) == this);
            return false;
        }
        public override int GetHashCode() { return 1; }
        public override string ToString() { return names[type]; }

    }

    [Serializable]
    public class CameraGain
    {
        public const int N = 2;
        public int type;
        public static string[] names = { "Normal", "Extended" };
        public static CameraGain[] allGains;
        public static readonly CameraGain Normal = new CameraGain(0);
        public static readonly CameraGain Extended = new CameraGain(1);

        public CameraGain() { }
        private CameraGain(int type) { 
            this.type = type;
            if (allGains == null) allGains = new CameraGain[N];
            allGains[type] = this;

        }
        public string Name() { if (type < N) { return names[type]; } else  return "No name"; }
        public int Value { get { return type; } }
        public static bool operator ==(CameraGain cm1, CameraGain cm2) { return (cm1.type == cm2.type); }
        public static bool operator !=(CameraGain cm1, CameraGain cm2) { return (cm1.type != cm2.type); }
        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            if (obj.GetType() == this.GetType())
                return (((CameraGain)obj) == this);
            return false;
        }
        public override int GetHashCode() { return 1; }
        public override string ToString() { return names[type]; }
    }


    public struct Rect
    {
        public int left;
        public int top;
        public int right;
        public int bottom;
    }  

    public struct CamSetting
    {
        public Boolean setupOK;
        public int mode;
        public int trig;
        public int exptime;
        public int hbin;
        public int vbin;
        public int delay; //sensicam only
        public int gain;
        public Rect roi;		//current setting of ROI
        public int imgx;		// the X resolution of image
        public int imgy;		// the X resolution of image
        public int submode;
    }

    public struct CamInfo
    {
        public Boolean initOK;   //initialization status; TRUE if initialized sucessfully
	    public int boardnum;
	    public string camName;
	    public int  color;
	    public int shutter; 
	    public int x_max;
	    public int y_max;
	    public IntPtr pictptr; //pointing to image data, alternating for pixelfly
	    public CamSetting setting;

	    //pixelfly exclusive
	    public int camhandle;

    	public int bufnr;	//

    	public int PixLib;
    	public int SenLib;
    }
}
