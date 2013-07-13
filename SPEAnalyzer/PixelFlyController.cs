using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace XCamera
{
    public delegate void DelegatePictureIsTaken();
    public delegate void DelegateThereIsAnError(string error);
    public partial class PixelFlyController : LoadController
    {
        public static Thread cameraThread;
        private Pixelfly pf = null;
        private bool cameraInitialized = false;
        public CameraMode cameraMode = CameraMode.Async;
        public CameraTrigger cameraTrigger = CameraTrigger.HardwareRising;
        private CameraGain cameraGain = CameraGain.Normal;
        private int NImage = 3;
        private int exposureTime = 50;
        public List<SingleImage> images;
        public static PixelFlyController instance = null;
        public static DelegatePictureIsTaken pictureIsTakenDelegate;
        public static DelegateThereIsAnError thereIsAnErrorDelegate;
        private bool autoSave = false;
        public PixelFlyController()
        {
            instance = this;
            InitializeComponent();
            InitializeComponent2();
            new PixelFlyGenerator();
            pf = PixelFlyGenerator.instance.pf;
            BaseInitializeComponent2();
        }
        private void InitializeComponent2()
        {
            enableTakingImageButton.EnabledString = "Taking Images";
            pictureIsTakenDelegate = new DelegatePictureIsTaken(this.pictureIsTaken);
            thereIsAnErrorDelegate = new DelegateThereIsAnError(this.thereIsAnError);

            // camera mode
            for (int i = 0; i < CameraMode.N; i++)
            {
                cameraModeCB.Items.Add(CameraMode.allModes[i]);
            }
            cameraModeCB.SelectedIndex = 1;

           
            // Trigger mode
            for (int i = 0; i < CameraTrigger.N; i++)
            {
                cameraTriggerCB.Items.Add(CameraTrigger.allTriggers[i]);
            }
            cameraTriggerCB.SelectedIndex = 1;

            // gain mode
            for (int i = 0; i < CameraGain.N; i++)
            {
                cameraGainCB.Items.Add(CameraGain.allGains[i]);
            }
            cameraGainCB.SelectedIndex = 0;



        }

        private void enableController(bool enable)
        {
        }


        private void enableTakingImageButton_Click(object sender, EventArgs e)
        {
            if (enableTakingImageButton.MyEnabled) //enabling
            {
                textBox1.Text = "";
                cameraMode = CameraMode.allModes[cameraModeCB.SelectedIndex];
                cameraGain = CameraGain.allGains[cameraGainCB.SelectedIndex];
                cameraTrigger = CameraTrigger.allTriggers[cameraTriggerCB.SelectedIndex];
                NImage = (int)nImagesNUP.Value;
                exposureTime = (int)exposureNUP.Value;

                int err = pf.CameraInitializeCamera();
                if (err != 0)
                {
                    textBox1.Text = "Initialize=" + PixelFlyError.getErrorString(err) + "\r\n";
                    cameraInitialized = false;
                    enableTakingImageButton.MyEnabled = false;
                    return;
                }

                err = pf.CameraSetupCamera(cameraMode, cameraTrigger, cameraGain,(int) exposureTime);
                if (err != 0)
                {
                    textBox1.Text += "Setup=" + PixelFlyError.getErrorString(err) + "\r\n";
                    enableTakingImageButton.MyEnabled = false;
                    return;
                }
                cameraInitialized = true;


                triggerButton.Enabled = false;
                if (cameraMode != CameraMode.Video)
                {
                    if (cameraTrigger == CameraTrigger.Software)
                        triggerButton.Enabled = true;
                }
                //triggerButton.Enabled = true;
                if (triggerButton.Enabled == false)
                {
                    takeManyImagesUsingNewThread();
                }
            }
            else // disabling
            {
                disableTheCamera();
            }
        }

        public void disableTheCamera()
        {
            if (cameraThread != null)   cameraThread.Abort();
            if (cameraInitialized == false) return;
            int err = pf.CameraStopCCD();
            err = pf.CameraCloseCamera();
            if (err != 0) textBox1.Text = "Close=" + PixelFlyError.getErrorString(err) + "\r\n";
            triggerButton.Enabled = false;
            cameraInitialized = false;
        }

        private void takeManyImagesUsingNewThread() 
        {
            if (cameraThread != null) if (cameraThread.IsAlive) cameraThread.Abort();
            cameraThread = new Thread(delegate()
            {
                PixelFlyGenerator.instance.takeManySetsOfImages(NImage);
            });
            cameraThread.Start();
        }

        private void triggerButton_Click(object sender, EventArgs e)
        {
            PixelFlyGenerator.instance.takeOneSetOfImages(NImage);
        }

        private int getLastIndex(string pre, string pos, int startIndex)
        {
            FileInfo fi = new FileInfo(pre + startIndex + pos);
            int fileCounter = startIndex;
            while (fi.Exists)
            {
                fileCounter++;
                fi = new FileInfo(pre + fileCounter + pos);
            }
            return fileCounter;
        }

        private void saveImage()
        {
            //if (images == null) return;
            if (saveDirectoryBox == null) return;
            if (ImgNameBox == null) return;
            if (saveDirectoryBox.Text.Length == 0) return;
            if (saveDirectoryBox.Text.Substring(saveDirectoryBox.Text.Length - 1, 1) != "\\")
            {
                saveDirectoryBox.Text += "\\";
            }
            string directoryName = saveDirectoryBox.Text;
            DirectoryInfo di = new DirectoryInfo(directoryName);
            if (di.Exists == false)
            {
                MessageBox.Show("The directory " + di.FullName + " does not exist.");
                return;
            }
            string fileName = ImgNameBox.Text;
            if (fileName == "") fileName = "image";
            int fileCounter = 0;
            string pre = directoryName + fileName;
            int oldFileCounter = -1;
            while (oldFileCounter != fileCounter)
            {
                oldFileCounter = fileCounter;
                fileCounter = getLastIndex(pre, ".xraw0", fileCounter);
                fileCounter = getLastIndex(pre, ".xod", fileCounter);
                fileCounter = getLastIndex(pre, ".xroi0", fileCounter);
                fileCounter = getLastIndex(pre, ".xodroi", fileCounter);
                fileCounter = getLastIndex(pre, ".XspeRaw", fileCounter);
                fileCounter = getLastIndex(pre, ".XspeRoi", fileCounter);
                //MessageBox.Show(pre + "---" + fileCounter);
                if (di.GetFiles(fileName + fileCounter + "*").Length > 0) fileCounter++;
            }
           
            //saving files ------------------------------

            string filePath = pre + fileCounter;
            if (rawCheck.Checked)
            {
                SingleImage.saveXRawN(filePath + ".xraw", images, 1);
            }
            if (absorptionCheck.Checked)
            {
                if (finalImage != null) SingleImage.saveImage(filePath + ".xod", finalImage);
            }
            Rectangle roi = finalMIV.currentROI;
            if (rawROICheck.Checked)
            {
                SingleImage.saveXRawNROI(filePath + ".xroi", images, roi, 1);
            }
            if (absorptionROICheck.Checked)
            {
                if (finalImage != null) SingleImage.saveImageROI(filePath + ".xodroi", finalImage, roi);
            }
            if (speCheck.Checked)
            {
                SingleImage.saveRawSPE(filePath + ".XspeRaw", images, 1);                
            }
            if (speRoiCheck.Checked)
            {
                SingleImage.saveRawRoiSPE(filePath + ".XspeRoi", images, roi, 1);
            }
            if (TIFFCheck.Checked)
            {
                filePath = directoryName + ImgNameBox.Text+ImgNumBox.Text + ".TIF";
                if (File.Exists(filePath))
                { // Check whether file exists, otherwise save function will overwrite
                    MessageBox.Show("File already exists in directory");
                    return;
                }
                SingleImage.saveTIFFN(filePath, images);
                ImgNumBox.Text = "00"+(Convert.ToUInt16(ImgNumBox.Text)+1);
                // Add 1 to image number counter text-box, e.g. 001 -> 002
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            saveImage();
        }

        /// <summary>
        /// Called by the CameraThread when the picture is taken
        /// </summary>
        public void pictureIsTaken()
        {
            Console.WriteLine("Picture is Taken");
            textBox1.Text += "picture is taken \r\n";
            displayImages(images);
            if (autoSave) saveImage();
        }

        /// <summary>
        /// Called by CameraThread when there is an error
        /// </summary>
        /// <param name="s"></param>
        public void thereIsAnError(string s)
        {
            textBox1.Text += "Error:" + s;
            enableTakingImageButton.MyEnabled = false; //disabling the camera
        }

        private void autoSaveCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            autoSave = autoSaveCheckBox.Checked;
        }

        private void resetButton_Click(object sender, EventArgs e)
        {
            disableTheCamera();
        }


    }
}
