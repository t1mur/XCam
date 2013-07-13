using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;


namespace XCamera
{
    public partial class MainForm : Form
    {

        public static MainForm instance = null;
        //Pixelfly pf;
        public MainForm()
        {
            instance = this;
            InitializeComponent();
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            MessageBox.Show("Closing");
            Console.WriteLine("Closing the MainForm");
            if (PixelFlyController.cameraThread != null)
                PixelFlyController.cameraThread.Abort();
            if (PixelFlyController.instance != null)
            {
                PixelFlyGenerator.instance.abort = true;
                PixelFlyController.instance.disableTheCamera();
            }
            base.OnClosing(e);
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            /*
             * 
             * THIS IS WORKING FINE. Try restarting the computer
            int err;


            pf = new Pixelfly();
            err = pf.CameraInitializeCamera();
            textBox1.Text = "Initialize=" + PixelFlyError.getErrorString(err) + "\r\n";
            err = pf.CameraSetupCamera(CameraMode.DoubleShutter, CameraTrigger.HardwareRising, CameraGain.Normal);
            textBox1.Text += "Setup=" + PixelFlyError.getErrorString(err) + "\r\n";


            DateTime lastTime = DateTime.Now;
            DateTime now = DateTime.Now;
            List<SingleImage> images = new List<SingleImage>();
            images.Add(null);
            images.Add(null);


            err = pf.CameraSnapCamera();
            now = DateTime.Now;
            textBox1.Text += "Snap took:" + (now.Ticks - lastTime.Ticks) / 10000 + "ms   ";
            lastTime = DateTime.Now;

            err = pf.CameraGetImage();
            textBox1.Text += "GetImage=" + PixelFlyError.getErrorString(err) + "\r\n";
            now = DateTime.Now;
            textBox1.Text += "CameraGetImage took:" + (now.Ticks - lastTime.Ticks) / 10000 + "ms   ";
            lastTime = DateTime.Now;

            images = pf.getImages();
            now = DateTime.Now;
            textBox1.Text += "GetImage took:" + (now.Ticks - lastTime.Ticks) / 10000 + "ms   ";
            lastTime = DateTime.Now;

            multiImageViewer1.displayImages(images);
            now = DateTime.Now;
            textBox1.Text += "Display took:" + (now.Ticks - lastTime.Ticks) / 10000 + "ms\r\n";
            lastTime = DateTime.Now;

            System.GC.Collect();
            System.GC.Collect();
             */

        }



    }
}