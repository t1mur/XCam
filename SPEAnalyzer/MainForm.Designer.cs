namespace XCamera
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabPages = new System.Windows.Forms.TabControl();
            this.pixelFlyTabPage = new System.Windows.Forms.TabPage();
            this.pixelFlyController1 = new XCamera.PixelFlyController();
            this.loadTabPage = new System.Windows.Forms.TabPage();
            this.manualLoadController = new XCamera.ManualLoadController();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.multiImageViewer1 = new XCamera.MultiImageViewer();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.statusStrip1.SuspendLayout();
            this.tabPages.SuspendLayout();
            this.pixelFlyTabPage.SuspendLayout();
            this.loadTabPage.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 933);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.statusStrip1.Size = new System.Drawing.Size(1382, 22);
            this.statusStrip1.TabIndex = 9;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(172, 17);
            this.toolStripStatusLabel1.Text = "Widagdo Setiawan - October 2006";
            this.toolStripStatusLabel1.ToolTipText = "Omoikane Zero - August 2006";
            this.toolStripStatusLabel1.Click += new System.EventHandler(this.toolStripStatusLabel1_Click);
            // 
            // tabPages
            // 
            this.tabPages.Controls.Add(this.pixelFlyTabPage);
            this.tabPages.Controls.Add(this.loadTabPage);
            this.tabPages.Controls.Add(this.tabPage1);
            this.tabPages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPages.Location = new System.Drawing.Point(0, 0);
            this.tabPages.Margin = new System.Windows.Forms.Padding(0);
            this.tabPages.Name = "tabPages";
            this.tabPages.SelectedIndex = 0;
            this.tabPages.Size = new System.Drawing.Size(1382, 933);
            this.tabPages.TabIndex = 11;
            // 
            // pixelFlyTabPage
            // 
            this.pixelFlyTabPage.Controls.Add(this.pixelFlyController1);
            this.pixelFlyTabPage.Location = new System.Drawing.Point(4, 22);
            this.pixelFlyTabPage.Name = "pixelFlyTabPage";
            this.pixelFlyTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.pixelFlyTabPage.Size = new System.Drawing.Size(1374, 907);
            this.pixelFlyTabPage.TabIndex = 2;
            this.pixelFlyTabPage.Text = "PixelFly";
            this.pixelFlyTabPage.UseVisualStyleBackColor = true;
            // 
            // pixelFlyController1
            // 
            this.pixelFlyController1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pixelFlyController1.Location = new System.Drawing.Point(3, 3);
            this.pixelFlyController1.Margin = new System.Windows.Forms.Padding(0);
            this.pixelFlyController1.Name = "pixelFlyController1";
            this.pixelFlyController1.Size = new System.Drawing.Size(1368, 901);
            this.pixelFlyController1.TabIndex = 0;
            // 
            // loadTabPage
            // 
            this.loadTabPage.Controls.Add(this.manualLoadController);
            this.loadTabPage.Location = new System.Drawing.Point(4, 22);
            this.loadTabPage.Name = "loadTabPage";
            this.loadTabPage.Padding = new System.Windows.Forms.Padding(3);
            this.loadTabPage.Size = new System.Drawing.Size(1374, 907);
            this.loadTabPage.TabIndex = 1;
            this.loadTabPage.Text = "Load";
            this.loadTabPage.UseVisualStyleBackColor = true;
            // 
            // manualLoadController
            // 
            this.manualLoadController.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manualLoadController.Location = new System.Drawing.Point(3, 3);
            this.manualLoadController.Margin = new System.Windows.Forms.Padding(0);
            this.manualLoadController.Name = "manualLoadController";
            this.manualLoadController.Size = new System.Drawing.Size(1368, 901);
            this.manualLoadController.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.multiImageViewer1);
            this.tabPage1.Controls.Add(this.textBox1);
            this.tabPage1.Controls.Add(this.button1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(1374, 907);
            this.tabPage1.TabIndex = 3;
            this.tabPage1.Text = "sand box";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // multiImageViewer1
            // 
            this.multiImageViewer1.Binning = 1;
            this.multiImageViewer1.Location = new System.Drawing.Point(515, 11);
            this.multiImageViewer1.Name = "multiImageViewer1";
            this.multiImageViewer1.Size = new System.Drawing.Size(980, 774);
            this.multiImageViewer1.TabIndex = 4;
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(8, 44);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(498, 322);
            this.textBox1.TabIndex = 3;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(8, 17);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(106, 21);
            this.button1.TabIndex = 2;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1382, 955);
            this.Controls.Add(this.tabPages);
            this.Controls.Add(this.statusStrip1);
            this.Name = "MainForm";
            this.Text = "XCamera v0.4";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tabPages.ResumeLayout(false);
            this.pixelFlyTabPage.ResumeLayout(false);
            this.loadTabPage.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TabControl tabPages;
        private System.Windows.Forms.TabPage loadTabPage;
        private ManualLoadController manualLoadController;
        private System.Windows.Forms.TabPage pixelFlyTabPage;
        private PixelFlyController pixelFlyController1;
        private System.Windows.Forms.TabPage tabPage1;
        private MultiImageViewer multiImageViewer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button1;
    }
}

