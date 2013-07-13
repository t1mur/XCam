using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace XCamera
{
    public partial class LoadController : UserControl
    {
        private List<SingleImage> rawImages;
        protected SingleImage finalImage;
        public DateTime lastImageTime;

        public LoadController()
        {
            InitializeComponent();
            InitializeToolTip();
            //BaseInitializeComponent2();
        }
        protected void BaseInitializeComponent2()
        {
            rawMIV.setPictureBoxSize(new Size(348, 256));
            finalMIV.setPictureBoxSize(new Size(696, 512));
            rawMIV.Binning = 4;
            finalMIV.Binning = 2;
            rawMIV.imageViewType = ImageViewType.Relative;
            finalMIV.imageViewType = ImageViewType.Absolute;
        }

        private void InitializeToolTip()
        {
        }

        protected void processAndDisplay(List<SingleImage> rawImages) 
        {

        }

        protected void loadAndDisplayXRAW(FileInfo file)
        {
            if (file == null) return;
            string fileName = file.FullName;
            bool isSPE = false;
            if (fileName.IndexOf("spe") > 0) isSPE = true;
            try
            {
                if (!isSPE)
                {
                    rawImages = SingleImage.loadXRawN(fileName);
                }
                else
                {
                    rawImages = SingleImage.loadSPE(fileName);
                }
                displayImages(rawImages);
            }
            catch (Exception e)
            {
                MessageBox.Show("Error at loading images. " + e.Message);
                return;
            }

        }

        protected void displayImages(List<SingleImage> images)
        {
            this.rawImages = images;
            SingleImage atomImage;
            SingleImage atomImage2;
            SingleImage backGroundImage;
            SingleImage backGroundImage2;
            if (images == null) return;
            if (images.Count == 0) return;
            if (images.Count == 1)
            {
                finalImage = images[0];
                finalMIV.displayImages(finalImage);
            }
            else if (images.Count == 3)
            {
                // there are 3 images
                if (images.Count < 3)
                {
                    Console.WriteLine("LoadController.displayImages. Not enough images");
                    return;
                }
                atomImage = images[0].returnCopy();
                backGroundImage = images[1].returnCopy();
                atomImage.substract(images[2]);
                backGroundImage.substract(images[2]);
                atomImage.divide(backGroundImage);
                finalImage = atomImage;
                finalImage.prefferedViewType = ImageViewType.Absolute;
                rawMIV.displayImages(images);
                finalMIV.displayImages(finalImage);
            }
            else if (images.Count == 4)
            {
                // there are 4 images
                if (images.Count < 4) return;
                atomImage = images[0].returnCopy();
                backGroundImage = images[1].returnCopy();
                atomImage.substract(images[2]);
                backGroundImage.substract(images[3]);
                atomImage.divide(backGroundImage);
                finalImage = atomImage;
                finalImage.prefferedViewType = ImageViewType.Absolute;
                rawMIV.displayImages(images);
                finalMIV.displayImages(finalImage);
            }
            else if (images.Count >= 6)
            {
                atomImage = images[0].returnCopy();
                atomImage2 = images[1].returnCopy();
                backGroundImage = images[2].returnCopy();
                backGroundImage2 = images[3].returnCopy();

                atomImage.substract(images[4]);
                atomImage2.substract(images[5]);
                backGroundImage.substract(images[4]);
                backGroundImage2.substract(images[5]);
                atomImage.divide(backGroundImage);
                atomImage2.divide(backGroundImage2);

                finalImage = atomImage;
                finalImage.prefferedViewType = ImageViewType.Absolute;
                rawMIV.displayImages(images);
                finalMIV.displayImages(atomImage);
                finalMIV.displayImages(atomImage2);
            }
            else
            {
                finalMIV.displayImages(images);
                return;
            }

        }



        private void LoadController_Load(object sender, EventArgs e)
        {

        }

        private void rawMIV_Load(object sender, EventArgs e)
        {

        }
    }
}
