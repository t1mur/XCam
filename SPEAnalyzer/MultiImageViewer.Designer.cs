namespace XCamera
{
    partial class MultiImageViewer
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.label1 = new System.Windows.Forms.Label();
            this.imageIDNUP = new System.Windows.Forms.NumericUpDown();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.setROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fullROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.xLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.yLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.iLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.inputCountGB = new System.Windows.Forms.GroupBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.countLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button595 = new System.Windows.Forms.Button();
            this.button0135 = new System.Windows.Forms.Button();
            this.fullROIButton = new System.Windows.Forms.Button();
            this.crossSectionViewer = new XCamera.CrossSectionViewer();
            ((System.ComponentModel.ISupportInitialize)(this.imageIDNUP)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossSectionViewer)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Image ID";
            // 
            // imageIDNUP
            // 
            this.imageIDNUP.Location = new System.Drawing.Point(56, 0);
            this.imageIDNUP.Maximum = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.imageIDNUP.Name = "imageIDNUP";
            this.imageIDNUP.Size = new System.Drawing.Size(49, 20);
            this.imageIDNUP.TabIndex = 1;
            this.imageIDNUP.ValueChanged += new System.EventHandler(this.imageIDNUP_ValueChanged);
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.Color.DarkGray;
            this.pictureBox.Location = new System.Drawing.Point(0, 97);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(507, 239);
            this.pictureBox.TabIndex = 2;
            this.pictureBox.TabStop = false;
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // contextMenuStrip
            // 
            this.contextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.setROIToolStripMenuItem,
            this.fullROIToolStripMenuItem,
            this.crossSectionToolStripMenuItem});
            this.contextMenuStrip.Name = "contextMenuStrip";
            this.contextMenuStrip.Size = new System.Drawing.Size(151, 70);
            // 
            // setROIToolStripMenuItem
            // 
            this.setROIToolStripMenuItem.Name = "setROIToolStripMenuItem";
            this.setROIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setROIToolStripMenuItem.Text = "Set ROI";
            this.setROIToolStripMenuItem.Click += new System.EventHandler(this.setROIToolStripMenuItem_Click);
            // 
            // fullROIToolStripMenuItem
            // 
            this.fullROIToolStripMenuItem.Name = "fullROIToolStripMenuItem";
            this.fullROIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.fullROIToolStripMenuItem.Text = "Full ROI";
            this.fullROIToolStripMenuItem.Click += new System.EventHandler(this.fullROIToolStripMenuItem_Click);
            // 
            // crossSectionToolStripMenuItem
            // 
            this.crossSectionToolStripMenuItem.Name = "crossSectionToolStripMenuItem";
            this.crossSectionToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.crossSectionToolStripMenuItem.Text = "Cross Section";
            this.crossSectionToolStripMenuItem.Click += new System.EventHandler(this.crossSectionToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xLabel,
            this.toolStripStatusLabel1,
            this.yLabel,
            this.toolStripStatusLabel2,
            this.iLabel,
            this.toolStripStatusLabel4});
            this.statusStrip.Location = new System.Drawing.Point(0, 685);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(829, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 3;
            this.statusStrip.Text = "statusStrip";
            // 
            // xLabel
            // 
            this.xLabel.Name = "xLabel";
            this.xLabel.Size = new System.Drawing.Size(33, 17);
            this.xLabel.Text = "x = 0";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // yLabel
            // 
            this.yLabel.Name = "yLabel";
            this.yLabel.Size = new System.Drawing.Size(33, 17);
            this.yLabel.Text = "y = 0";
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // iLabel
            // 
            this.iLabel.Name = "iLabel";
            this.iLabel.Size = new System.Drawing.Size(31, 17);
            this.iLabel.Text = "I = 0";
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // inputCountGB
            // 
            this.inputCountGB.BackColor = System.Drawing.Color.Black;
            this.inputCountGB.Location = new System.Drawing.Point(129, -1);
            this.inputCountGB.Name = "inputCountGB";
            this.inputCountGB.Size = new System.Drawing.Size(252, 92);
            this.inputCountGB.TabIndex = 4;
            this.inputCountGB.TabStop = false;
            this.inputCountGB.Text = "Input Count Rectangle";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.countLabel,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 663);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(829, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 5;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // countLabel
            // 
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(34, 17);
            this.countLabel.Text = "N = 0";
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel5.Text = "|";
            // 
            // button595
            // 
            this.button595.Location = new System.Drawing.Point(3, 26);
            this.button595.Name = "button595";
            this.button595.Size = new System.Drawing.Size(58, 20);
            this.button595.TabIndex = 6;
            this.button595.Text = "5% - 95%";
            this.button595.UseVisualStyleBackColor = true;
            this.button595.Click += new System.EventHandler(this.button595_Click);
            // 
            // button0135
            // 
            this.button0135.Location = new System.Drawing.Point(3, 47);
            this.button0135.Name = "button0135";
            this.button0135.Size = new System.Drawing.Size(58, 20);
            this.button0135.TabIndex = 7;
            this.button0135.Text = "0 - 1.35";
            this.button0135.UseVisualStyleBackColor = true;
            this.button0135.Click += new System.EventHandler(this.button0135_Click);
            // 
            // fullROIButton
            // 
            this.fullROIButton.Location = new System.Drawing.Point(3, 69);
            this.fullROIButton.Name = "fullROIButton";
            this.fullROIButton.Size = new System.Drawing.Size(58, 20);
            this.fullROIButton.TabIndex = 8;
            this.fullROIButton.Text = "Full ROI";
            this.fullROIButton.UseVisualStyleBackColor = true;
            this.fullROIButton.Click += new System.EventHandler(this.fullROIButton_Click);
            // 
            // crossSectionViewer
            // 
            this.crossSectionViewer.BackColor = System.Drawing.Color.Coral;
            this.crossSectionViewer.Location = new System.Drawing.Point(303, 401);
            this.crossSectionViewer.MySize = new System.Drawing.Size(300, 200);
            this.crossSectionViewer.Name = "crossSectionViewer";
            this.crossSectionViewer.Size = new System.Drawing.Size(135, 174);
            this.crossSectionViewer.TabIndex = 9;
            this.crossSectionViewer.TabStop = false;
            // 
            // MultiImageViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.fullROIButton);
            this.Controls.Add(this.button0135);
            this.Controls.Add(this.button595);
            this.Controls.Add(this.crossSectionViewer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.inputCountGB);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.imageIDNUP);
            this.Controls.Add(this.label1);
            this.Name = "MultiImageViewer";
            this.Size = new System.Drawing.Size(829, 707);
            this.Load += new System.EventHandler(this.MultiImageViewer_Load);
            ((System.ComponentModel.ISupportInitialize)(this.imageIDNUP)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.crossSectionViewer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown imageIDNUP;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel xLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel yLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.ToolStripStatusLabel iLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.GroupBox inputCountGB;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel countLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.Button button595;
        private System.Windows.Forms.Button button0135;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem setROIToolStripMenuItem;
        private System.Windows.Forms.Button fullROIButton;
        private System.Windows.Forms.ToolStripMenuItem fullROIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossSectionToolStripMenuItem;
        private CrossSectionViewer crossSectionViewer;
    }
}
