using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;



namespace XCamera
{
    /// <summary>
    /// This class represents an image with Single precission
    /// </summary>
    public class SingleImage
    {
        const double replacezero = 100; // replacement value in case pixel value is zero
        // avoids log(0) problems

        public Single[,] data; //[y,x]
        private Bitmap relativeBitmap;
        private Bitmap absoluteBitmap;
        private int binning = 0;
        private Rectangle roi = new Rectangle(0,0,13,10); // absolute offset
        private Size maxBitmapSize = new Size(10, 10);
        public ImageViewType prefferedViewType = ImageViewType.Relative;

        private SingleImage()
        {
        }
        
        public SingleImage(int y, int x)
        {
            data = new Single[y, x];
        }

        //--- Generate relative scaled image ---//
        public void generateRelativeScaleBitmap(Single start, Single end)
        {
            Console.WriteLine("Generating Relative bitmap");
            //find the scale
            Single max = Single.MinValue; // Set default max and min values
            Single min = Single.MaxValue;
            Single[] sorter = new Single[yLength * xLength];
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    sorter[y * xLength + x] = data[y, x]; // Put all values in a 1D array for later sorting
                    if (max < data[y, x]) 
                        max = data[y, x];
                    if (min > data[y, x]) min = data[y, x];
                }
            }
            Array.Sort(sorter);
            //Console.WriteLine("Max=" + max + ", min=" + min);
            double area = yLength * xLength;
            min = sorter[(int) (start * area)]; // Determine min, max values
            max = sorter[(int) (end * area)];
            //Console.WriteLine("Maxs=" + max + ", mins=" + min);
            if (min > 4000) min = 4000; 
            Single scaling = (Single) 255.0 / ((Single)(max-min)); // Scale to 0-255 
            int startY = roi.Y; int endY = roi.Y + roi.Height; // absolute
            int startX = roi.X; int endX = roi.X + roi.Width; // absolute
            if (binning <= 0) binning = 1;
            int bXLength = (endX - startX) / binning + 1;
            int bYLength = (endY - startY) / binning + 1;
            byte[,] bdata = new byte[bYLength, bXLength];
            int by = 0;
            for (int y = startY; y < endY; y += binning)
            {
                int bx = 0;
                for (int x = startX; x < endX; x += binning) // Iterate over pixels in ROI with binning
                {
                    Single valS = 0; byte val = 0;
                    for (int ybin = 0; ybin < binning; ybin++)
                    {
                        for (int xbin = 0; xbin < binning; xbin++)
                        {
                            valS += data[y + ybin, x + xbin] - min; // Load value with minimum subtracted
                        }
                    }
                    valS = (scaling * valS / (binning * binning)); // Scale to 0-255 divided by binning
                    val = (byte)valS;
                    if (valS > 255) val = 255;
                    if (valS < 0) val = 0; // Why are these even necessary?
                    bdata[by, bx] = val; // Produce byte-sized image array
                    bx++;
                }
                by++;
            }

            relativeBitmap = UnsafeBitmap.generateBitmap(bdata); // Generate scaled bitmap
            Console.WriteLine("Done Generating Relative bitmap");
        }

        //--- Generate absolute scaled image ---//
        public void generateAbsoluteScaleBitmap(Single start, Single end)
        {
            Console.WriteLine("Generating Absolute bitmap");
            //find the scale
            Single max = Single.MinValue;
            Single min = Single.MaxValue;
            min = start;
            max = (Single) end;
            Single scaling = (Single)255.0 / ((Single)(max - min));
            int startY = roi.Y; int endY = roi.Y + roi.Height; // absolute
            int startX = roi.X; int endX = roi.X + roi.Width; // absolute
            if (binning <= 0) binning = 1;
            int bXLength = (endX - startX)/binning + 1;
            int bYLength = (endY - startY)/binning + 1;
            byte[,] bdata = new byte[bYLength, bXLength];
            int by = 0;
            for (int y = startY; y < endY; y+=binning)
            {
                int bx = 0;
                for (int x = startX; x < endX; x+=binning)
                {
                    Single valS = 0; byte val = 0;
                    for (int ybin = 0; ybin < binning; ybin++)
                    {
                        for (int xbin = 0; xbin < binning; xbin++)
                        {
                            valS += data[y + ybin, x + xbin] - min;
                        }
                    }
                    valS = (scaling * valS / (binning * binning));
                    val = (byte)valS;
                    if (valS > 255) val = 255;
                    if (valS < 0) val = 0;
                    bdata[by, bx] = val;
                    bx++;
                }
                by++;
            }
            Console.WriteLine("Done Generating Absolute bitmap");
            absoluteBitmap = UnsafeBitmap.generateBitmap(bdata);
        }

        private void eraseBitmapBuffer()
        {
            relativeBitmap = null;
            absoluteBitmap = null;
        }

        public int Binning
        {
            get { return binning; }
            set { binning = value; eraseBitmapBuffer(); }
        }
        public Size MaxBitmapSize
        {
            get { return maxBitmapSize; }
            set { maxBitmapSize = value; eraseBitmapBuffer(); }
        }

        public Rectangle getFullROI()
        {
            return new Rectangle(0, 0, xLength, yLength);
        }

        public Rectangle fromRelativeToAbsolute(Rectangle recIn)
        // Relative: rectangle drawn in ROI
        // Absolute: actual pixels in image
        {
            int bin = Binning;
            Rectangle rec =  new Rectangle(
                roi.X+  recIn.X * bin, 
                roi.Y + recIn.Y * bin, 
                recIn.Width * bin, 
                recIn.Height*bin); // Take into account smaller ROI
            if (rec.X < 0) rec.X = 0;
            if (rec.Y < 0) rec.Y = 0;
            if (rec.X >= xLength) rec.X = xLength - 1;
            if (rec.Y >= yLength) rec.Y = yLength - 1;
            if (rec.Width < 0) rec.Width = 0;
            if (rec.Height < 0) rec.Height = 0;
            if (rec.Width + rec.X > xLength) rec.Width = xLength-rec.X;
            if (rec.Height + rec.Y > yLength) rec.Height = yLength-rec.Y;
            return rec; // Rectangle as it would appear in full image
        }

        public Rectangle fromAbsoluteToRelative(Rectangle recIn)
        // Reverse of from RelativeToAbsolute
        {
            int bin = Binning;
            Rectangle rec = new Rectangle(
                (recIn.X -roi.X) / bin,
                (recIn.Y-roi.Y) / bin,
                recIn.Width / bin,
                recIn.Height / bin);
            return fixROI(rec);
        }
        public Point fromRelativeToAbsolutePoint(Point p)
        {
            Point ret = new Point(roi.X + p.X * binning, roi.Y + p.Y*binning);
            if (ret.X < 0) ret.X = 0;
            if (ret.Y < 0) ret.Y = 0;
            if (ret.X >= xLength) ret.X = xLength - 1;
            if (ret.Y >= yLength) ret.Y = yLength - 1;
            return ret;
        }
        private Rectangle fixROI(Rectangle recIn)
        {
            Rectangle rec = recIn;
            if (rec.X < 0) rec.X = 0;
            if (rec.Y < 0) rec.Y = 0;
            if (rec.X >= xLength) rec.X = xLength - 1;
            if (rec.Y >= yLength) rec.Y = xLength - 1;
            if (rec.Width < 0) rec.Width = 0;
            if (rec.Height < 0) rec.Height = 0;
            if (rec.Width + rec.X > xLength) rec.Width = xLength - rec.X;
            if (rec.Height + rec.Y > yLength) rec.Height = xLength - rec.Y;
            return rec;
        }
        public void setRelativeROI(Rectangle rec)
        {
            setAbsoluteROI(fromRelativeToAbsolute(rec));
        }
        public void setAbsoluteROI(Rectangle rec)
        {
            if (rec.Width == 0) return; if (rec.Height ==0) return;
            roi = fixROI(rec);
            // calculating binning
            double binX = ((double)roi.Width /  (double)maxBitmapSize.Width);
            double binY = ((double)roi.Height / (double)maxBitmapSize.Height);
            double binD = Math.Max(binX, binY);
            int bin = (int) Math.Ceiling(binD);
            Binning = bin;
            roi.Width = (roi.Width / binning) * binning;
            roi.Height = (roi.Height / binning) * binning;
        }
        public Rectangle getROI()
        {
            return new Rectangle(roi.Location, roi.Size);
        }

        public Bitmap getBitmap595()
        {
            if (relativeBitmap == null) generateRelativeScaleBitmap(0.05f,0.95f);
            return relativeBitmap;
        }
        public Bitmap getBitmap0135()
        {
            if (absoluteBitmap == null) generateAbsoluteScaleBitmap(0,1.35f);
            return absoluteBitmap;
        }
        public Bitmap getBitmap(ImageViewType type)
        {
            if (type == null) type = ImageViewType.Relative;
            ImageViewType theType = prefferedViewType;
            if (prefferedViewType == ImageViewType.Default) theType = type;
            if (theType == ImageViewType.Default) theType = ImageViewType.Relative;
            if (theType == ImageViewType.Absolute) return getBitmap0135();
            if (theType == ImageViewType.Relative) return getBitmap595();
            return null;
        }
        public Bitmap getBitmapMust(ImageViewType type)
        {
            if (type == ImageViewType.Default) return getBitmap(null);
            if (type == ImageViewType.Absolute) return getBitmap0135();
            if (type == ImageViewType.Relative) return getBitmap595();
            return null;
        }


        public Single getNCount(Rectangle recIn)
        // Calculate N count for current SingleImage instance
        {
            int bin = Binning;
            Rectangle rec = fromRelativeToAbsolute(recIn); // Figure out NCount region in actual image
            Single result = 0;
            Single normcount = 0;

            normcount = getNorm(recIn);

            for (int x = rec.X; x < rec.X + rec.Width; x++)
            {
                for (int y = rec.Y; y < rec.Y + rec.Height; y++)
                {

                    if (data[y, x] > 0) result += (Single)(-Math.Log(data[y, x]) - normcount);
                    // For pixels where probe-with-atoms image is totally dark, set replacement value
                }
            }
            return result;
        }

        public Single getNorm(Rectangle recIn)
        // Get normalized count by averaging along perimeter specified by recIn
        {
            Rectangle rec = fromRelativeToAbsolute(recIn);
            Single edgecount = 0; // Total optical density along perimeter
            Single edgepixels; // Total number of pixels along perimeter
            Single edgewidth = 20;

            edgepixels = 0;
            //edgepixels = edgewidth * (rec.Width-2) + edgewidth * (rec.Height-2); // Total number of points along edge

            //if (edgepixels <= 0) edgepixels = 1; // Avoid errors with dividing by zero or boxes that are too small

            for (int x = rec.X + 1; x < rec.X + rec.Width-1; x++)
            { // Count along two segments of perimeter in x-direction
                for (int n = 0; n  < edgewidth; n++)
                {
                    if (data[rec.Y + n, x] > 0)
                    {
                        edgecount -= (Single)Math.Log(data[rec.Y + n, x]);
                        edgepixels++;
                    }
                    if (data[rec.Y + rec.Height - 1 - n, x] > 0)
                    {
                        edgecount -= (Single)Math.Log(data[rec.Y + rec.Height - 1 - n, x]);
                        edgepixels++;
                    }
                }
            }
            for (int y = rec.Y + 1; y < rec.Y + rec.Height - 1; y++)
            { // Count along two elements of perimeter in y-direction
                for (int n = 0; n < edgewidth; n++)
                {
                    if (data[y, rec.X + n] > 0)
                    {
                        edgecount -= (Single)Math.Log(data[y, rec.X + n]);
                        edgepixels++;
                    }
                    if (data[y, rec.X + rec.Width - 1 - n] > 0)
                    {
                        edgecount -= (Single)Math.Log(data[y, rec.X + rec.Width - 1 - n]);
                        edgepixels++;
                    }
                }
            }
            // Sum counts from edge of box

            return edgecount / edgepixels; // Return average OD per pixel
        }


        public Single getNCountMinBackground(Rectangle atomIn, Rectangle backgroundIn)
        {
            Single result = 0;
            Single atomCount = getNCount(atomIn);
            Single backCount = 0;
            int bin = Binning;
            Rectangle atom = fromRelativeToAbsolute(atomIn);
            Rectangle background = fromRelativeToAbsolute(backgroundIn);
            int backSize = 0;
            for (int x = background.X; x < background.X + background.Width; x++)
            {
                for (int y = background.Y; y < background.Y + background.Height; y++)
                {
                    if (atom.Contains(x, y)) continue;
                    backSize++;
                    if (data[y, x] > 0)
                        backCount -= (Single)Math.Log(data[y, x]);
                }
            }
            result = atomCount;
            if (backSize > 0)
                result = atomCount - backCount / backSize * atom.Width * atom.Height;
                // Normalize for background by average ( backCount / backSize ) times area ( atom.Width * atom.Height)
            return result;
        }
        
        public Single getDataRelativePoint(Point p) {
            p = fromRelativeToAbsolutePoint(p);
            return data[p.Y, p.X]; 
        }

        public void substract(SingleImage di)
        // Subtract background give by SingleImage di from current SingleImage instance
        {
            if (xLength != di.xLength) throw new Exception("x length is different");
            if (yLength != di.yLength) throw new Exception("y length is different");
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    data[y, x] -= di.data[y, x];
                }
            }
        }
        public void divide(SingleImage di)
        // Divide current SingleImage instance by SingleImage di
        {
            if (xLength != di.xLength) throw new Exception("x length is different");
            if (yLength != di.yLength) throw new Exception("y length is different"); // Check for compatibility of dimensions
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    di.data[y, x] = Math.Abs(di.data[y, x]);
                    data[y, x] = Math.Abs(data[y, x]);
                    if (di.data[y, x] > 0 && data[y, x] > 0)
                        data[y, x] /= di.data[y, x];
                    else if (data[y, x] == 0 && di.data[y, x] > 0)
                        data[y, x] = (Single)0.1 / di.data[y,x]; 
                        // If probe-with-atoms has pixel value 0, change to pixel value 1 and divide by background to get absorption
                    else
                        data[y, x] = 0; 
                        // If probe-without-atoms has pixel value 0, make corresponding absorption value also 0
                        // These pixels will not be counted
                }
            }
        }
/*        public void reverseDivide(SingleImage di)
        // Divide SingleImage di by current SingleImage instance
        {
            if (xLength != di.xLength) throw new Exception("x length is different");
            if (yLength != di.yLength) throw new Exception("y length is different");
            for (int y = 0; y < yLength; y++)
            {
                for (int x = 0; x < xLength; x++)
                {
                    if (data[y, x] > 0)
                        data[y, x] = di.data[y, x]/ data[y,x];
                }
            }
        }   */

        public int xLength
        {
            get { if (data != null) return data.GetLength(1); else return 0; }
        }
        public int yLength
        {
            get { if (data != null) return data.GetLength(0); else return 0; }
        }

        public SingleImage getSubSet(int startY, int startX, int endY, int endX)
        {
            SingleImage result = new SingleImage();
            result.data = new Single[endY - startY + 1, endX - startX + 1];
            for (int y = startY; y <= endY; y++)
            {
                for (int x = startX; x <= endX; x++)
                {
                    result.data[y-startY,x-startX] = data[y, x];
                }
            }
            return result;
        }
        public SingleImage returnCopy()
        {
            return getSubSet(0, 0, this.yLength -1, this.xLength-1);
        }

        public static SingleImage loadXRaw(string fileName)
        {
            SingleImage result = null;
            StreamReader sr = new StreamReader(fileName);
            Single[,] data = new Single[1100, 1500];
            int row=0;
            bool firstTime = true;
            int NCol =0 ;
            int NRow = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                string[] vals = line.Split(' ');
                if (firstTime)
                {
                    firstTime = false;
                    NCol = vals.Length - 1;
                }
                if (vals.Length < 2) break;
                for (int col = 0; col < vals.Length-1; col++)
                {
                    try
                    {
                        data[row, col] = Single.Parse(vals[col]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("row=" + row + ", col=" + col + ", string=" + vals[col]);
                        throw e;
                    }
                }
                row++;
            }
            NRow = row;
            sr.Close();

            result = new SingleImage(NRow, NCol);
            for (int i = 0; i < NRow; i++)
            {
                for (int j = 0; j < NCol; j++)
                {
                    result.data[i, j] = data[i, j];
                }
            }
            return result;
        }

        public static List<SingleImage> loadSPE(string fileName)
        {
            List<SingleImage> result = new List<SingleImage>();


            FileStream fs;
            BinaryReader br;
            try
            {
                fs = File.OpenRead(fileName);
                br = new BinaryReader(fs);
            }
            catch (Exception ee)
            {
                throw new Exception("Error in opening file: " + ee.Message);
            }
            fs.Position = 42;
            short cx = br.ReadInt16();  // The number of pixel in x direction

            fs.Position = 656;
            short cy = br.ReadInt16();  // The number of pixel in y direction

            fs.Position = 1446;
            Int32 numOfFrame = br.ReadInt32(); // THe number of frame

            //loading the data
            fs.Position = 4100;
            for (int i = 0; i < numOfFrame; i++)
            {
                SingleImage si = new SingleImage(cy, cx);
                result.Add(si);
                Single[,] data = si.data;
                for (int y = 0; y < cy; y++)
                {
                    for (int x = 0; x < cx; x++)
                    {
                        data[y, x] = (Single)br.ReadUInt16();
                    }
                }
            }
            br.Close();
            fs.Close();
            return result;
        }
        
        public static List<SingleImage> loadXRawN(string fileName)
        {
            List<SingleImage> result = new List<SingleImage>();
            if (fileName[fileName.Length - 1] == '0') fileName = fileName.Substring(0, fileName.Length - 1);
            FileInfo fi = new FileInfo(fileName);
            if (fi.Exists) result.Add(loadXRaw(fileName));
            int i = 0;
            while ((new FileInfo(fileName + i)).Exists)
            {
                result.Add(loadXRaw(fileName + i));
                i++;
            }
            return result;
        }



        public static void saveXRawN(string filePath, List<SingleImage> images, int bin)
        {
            if ((images == null) || (images.Count == 0) || (images[0] == null)) return;
            saveXRawNROI(filePath, images, images[0].getFullROI(), bin);
        }

        public static void saveXRawNROI(string filePath, List<SingleImage> images, Rectangle roiIn, int bin)
        {
            if ((images == null) || (images.Count == 0) || (images[0] == null)) return;
            if (bin <= 0) bin = 1;
            if (bin >= 10) bin = 10;
            for (int i = 0; i < images.Count; i++)
            {
                TextWriter writer = new StreamWriter(filePath + i);
                Rectangle roi = images[i].fixROI(roiIn);
                for (int j = roi.Y; j <= roi.Y + roi.Height - bin; j += bin)
                {
                    for (int k = roi.X; k <= roi.X + roi.Width - bin; k += bin)
                    {
                        float val = 0;
                        for (int jin = 0; jin < bin; jin++)
                        {
                            for (int kin = 0; kin < bin; kin++)
                            {
                                val += images[i].data[j + jin, k + kin];
                            }
                        }
                        writer.Write(val + " ");
                    }
                    writer.WriteLine();
                }
                writer.Close();
            }
        }

        public static void saveTIFFN(string filePath, List<SingleImage> images)
        {
            ImageCodecInfo info = null; 

            foreach(ImageCodecInfo ice in ImageCodecInfo.GetImageEncoders())
            {
                if(ice.MimeType=="image/tiff") info=ice; 
            }

            System.Drawing.Imaging.Encoder enc = System.Drawing.Imaging.Encoder.SaveFlag;
            System.Drawing.Imaging.Encoder encb = System.Drawing.Imaging.Encoder.ColorDepth; 
            EncoderParameters ep = new EncoderParameters(2);
            ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.MultiFrame);
            ep.Param[1] = new EncoderParameter(encb, (long)24); 
                           

            Bitmap pages = null;

            if ((images == null) || (images.Count == 0) || (images[0] == null)) return;

            for (int i = 0; i < images.Count; i++)
            {
                Bitmap bm = UnsafeBitmap.generateBitmap(images[i].data);
                if(i==0) 
                {
                    pages = bm;
                    pages.Save(filePath, info, ep);
                } else {
                    ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.FrameDimensionPage);
                    pages.SaveAdd(bm, ep);
                }
                if(i==(images.Count-1))
                {
                    ep.Param[0] = new EncoderParameter(enc, (long)EncoderValue.Flush);
                    pages.SaveAdd(ep);
                }
            }
                

        }

        public static void saveImage(string filePath, SingleImage image)
        {
            if ((image == null)) return;
            saveImageROI(filePath, image, image.getFullROI());
        }
        public static void saveImageROI(string filePath, SingleImage image, Rectangle roi)
        {
            if ((image == null)) return;
            roi = image.fixROI(roi);
            TextWriter writer = new StreamWriter(filePath);
            for (int j = roi.Y; j < roi.Y + roi.Height; j++)
            {
                for (int k = roi.X; k < roi.X + roi.Width; k++)
                {
                    writer.Write(image.data[j, k] + " ");
                }
                writer.WriteLine();
            }
            writer.Close();
        }

        public static void writeGarbage(BinaryWriter writer, int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                writer.Write('x');
            }
        }
        public static void saveRawSPE(string filePath, List<SingleImage> images,  int bin)
        {
            if ((images == null) || (images.Count == 0) || (images[0] == null)) return;
            saveRawRoiSPE(filePath, images, images[0].getFullROI(), bin);
        }
        public static void saveRawRoiSPE(string filePath, List<SingleImage> images, Rectangle roiIn, int bin)
        {
            if ((images == null) || (images.Count == 0) || (images[0] == null)) return;

            FileStream fs = File.Create(filePath);
            BinaryWriter writer = new BinaryWriter(fs);
            writeGarbage(writer, 42); // now at position 42
            writer.Write((Int16)roiIn.Width); // now at 44
            writeGarbage(writer, 64); // now at position 108
            writer.Write((Int16) 3); // now at 110
            writeGarbage(writer, 546); // now at position 656
            writer.Write((Int16) roiIn.Height); // now at 658
            writeGarbage(writer, 788); // now at position 1446
            writer.Write((Int32)images.Count); // now at 1450
            writeGarbage(writer, 2650); // now at position 4100

            bin = 1;
            for (int i = 0; i < images.Count; i++)
            {
                Rectangle roi = images[i].fixROI(roiIn);
                for (int j = roi.Y; j <= roi.Y + roi.Height - bin; j += bin)
                {
                    for (int k = roi.X; k <= roi.X + roi.Width - bin; k += bin)
                    {
                        UInt16 val = 0;
                        for (int jin = 0; jin < bin; jin++)
                        {
                            for (int kin = 0; kin < bin; kin++)
                            {
                                val += (UInt16) images[i].data[j + jin, k + kin];
                            }
                        }
                        writer.Write(val);
                    }
                }
            }
            writer.Close();
            fs.Close();
        }
    }
}
