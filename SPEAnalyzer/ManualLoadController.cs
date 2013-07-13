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
    public partial class ManualLoadController : LoadController
    {
        private FileInfo lastFile;
        public ManualLoadController()
        {
            InitializeComponent();
            BaseInitializeComponent2();

        }

        private void browseButton_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog.SelectedPath;
                directoryBox.Text = folderName;
            }
            listFilesButton_Click(this, null);
        }

        private void listFilesButton_Click(object sender, EventArgs e)
        {
            fileListBox.Items.Clear();
            if (directoryBox == null) return;
            if (directoryBox.Text.Length == 0) return;
            if (directoryBox.Text.Substring(directoryBox.Text.Length - 1, 1) != "\\")
            {
                directoryBox.Text += "\\";
            }
            DirectoryInfo di;
            try
            {
                di = new DirectoryInfo(directoryBox.Text);
            }
            catch (Exception ee)
            {
                MessageBox.Show("Directory " + directoryBox.Text + " cannot be opened because of" + ee.Message);
                return;
            }
            if (di.Exists == false)
            {
                MessageBox.Show("The directory " + di.FullName + " does not exist.");
                return;
            }

            FileInfo[] files ;
            files = di.GetFiles("*.xr??0");
            fileListBox.Items.AddRange(files);
            files = di.GetFiles("*.*spe*");
            fileListBox.Items.AddRange(files);
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            if (fileListBox.SelectedItems == null) return;
            foreach (Object o in fileListBox.SelectedItems)
            {
                if (o.GetType() != (new FileInfo("c:\\x.x")).GetType()) return;
                FileInfo file = (FileInfo)o;
                loadAndDisplayXRAW(file);
                lastFile = file;
            }
            listFilesButton_Click(null, null);
        }

        private void finalMIV_Load(object sender, EventArgs e)
        {

        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (lastFile == null) return;
            List<SingleImage> images = SingleImage.loadXRawN(lastFile.FullName);
            SingleImage.saveXRawN(
                lastFile.FullName.Substring(0,lastFile.FullName.Length-1) + "bin", images, (int)binNUP.Value);
        }
    }
}
