namespace XCamera
{
    partial class MultiImageViewer2
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
            this.button595 = new System.Windows.Forms.Button();
            this.iLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripStatusLabel4 = new System.Windows.Forms.ToolStripStatusLabel();
            this.fullROIButton = new System.Windows.Forms.Button();
            this.toolStripStatusLabel2 = new System.Windows.Forms.ToolStripStatusLabel();
            this.button0135 = new System.Windows.Forms.Button();
            this.toolStripStatusLabel5 = new System.Windows.Forms.ToolStripStatusLabel();
            this.countLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.inputCountGB = new System.Windows.Forms.GroupBox();
            this.imageIDNUP = new System.Windows.Forms.NumericUpDown();
            this.setROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.contextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.fullROIToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.crossSectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label1 = new System.Windows.Forms.Label();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.yLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.xLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.cursorLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.labelNCount = new System.Windows.Forms.Label();
            this.pictureBox = new XCamera.GenericControl();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageIDNUP)).BeginInit();
            this.contextMenuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // button595
            // 
            this.button595.Location = new System.Drawing.Point(3, 26);
            this.button595.Name = "button595";
            this.button595.Size = new System.Drawing.Size(58, 20);
            this.button595.TabIndex = 16;
            this.button595.TabStop = false;
            this.button595.Text = "5% - 95%";
            this.button595.UseVisualStyleBackColor = true;
            this.button595.Click += new System.EventHandler(this.button595_Click);
            // 
            // iLabel
            // 
            this.iLabel.AutoSize = false;
            this.iLabel.Name = "iLabel";
            this.iLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.iLabel.Size = new System.Drawing.Size(300, 17);
            this.iLabel.Text = "I=000000000000000000000000000000000000000000000";
            this.iLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusLabel4
            // 
            this.toolStripStatusLabel4.Name = "toolStripStatusLabel4";
            this.toolStripStatusLabel4.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel4.Text = "|";
            // 
            // fullROIButton
            // 
            this.fullROIButton.Location = new System.Drawing.Point(3, 69);
            this.fullROIButton.Name = "fullROIButton";
            this.fullROIButton.Size = new System.Drawing.Size(58, 20);
            this.fullROIButton.TabIndex = 18;
            this.fullROIButton.TabStop = false;
            this.fullROIButton.Text = "Full ROI";
            this.fullROIButton.UseVisualStyleBackColor = true;
            this.fullROIButton.Click += new System.EventHandler(this.fullROIButton_Click);
            // 
            // toolStripStatusLabel2
            // 
            this.toolStripStatusLabel2.Name = "toolStripStatusLabel2";
            this.toolStripStatusLabel2.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel2.Text = "|";
            // 
            // button0135
            // 
            this.button0135.Location = new System.Drawing.Point(3, 47);
            this.button0135.Name = "button0135";
            this.button0135.Size = new System.Drawing.Size(58, 20);
            this.button0135.TabIndex = 17;
            this.button0135.TabStop = false;
            this.button0135.Text = "0 - 1.35";
            this.button0135.UseVisualStyleBackColor = true;
            this.button0135.Click += new System.EventHandler(this.button0135_Click);
            // 
            // toolStripStatusLabel5
            // 
            this.toolStripStatusLabel5.Name = "toolStripStatusLabel5";
            this.toolStripStatusLabel5.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel5.Text = "|";
            // 
            // countLabel
            // 
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(34, 17);
            this.countLabel.Text = "N = 0";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.countLabel,
            this.toolStripStatusLabel5});
            this.statusStrip1.Location = new System.Drawing.Point(0, 617);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(840, 22);
            this.statusStrip1.SizingGrip = false;
            this.statusStrip1.TabIndex = 15;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // inputCountGB
            // 
            this.inputCountGB.BackColor = System.Drawing.Color.Black;
            this.inputCountGB.Location = new System.Drawing.Point(132, 1);
            this.inputCountGB.Name = "inputCountGB";
            this.inputCountGB.Size = new System.Drawing.Size(252, 92);
            this.inputCountGB.TabIndex = 14;
            this.inputCountGB.TabStop = false;
            this.inputCountGB.Text = "Input Count Rectangle";
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
            this.imageIDNUP.TabIndex = 11;
            this.imageIDNUP.TabStop = false;
            this.imageIDNUP.ValueChanged += new System.EventHandler(this.imageIDNUP_ValueChanged);
            // 
            // setROIToolStripMenuItem
            // 
            this.setROIToolStripMenuItem.Name = "setROIToolStripMenuItem";
            this.setROIToolStripMenuItem.Size = new System.Drawing.Size(150, 22);
            this.setROIToolStripMenuItem.Text = "Set ROI";
            this.setROIToolStripMenuItem.Click += new System.EventHandler(this.setROIToolStripMenuItem_Click);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(0, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 13);
            this.label1.TabIndex = 10;
            this.label1.Text = "Image ID";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(11, 17);
            this.toolStripStatusLabel1.Text = "|";
            // 
            // yLabel
            // 
            this.yLabel.AutoSize = false;
            this.yLabel.Name = "yLabel";
            this.yLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.yLabel.Size = new System.Drawing.Size(51, 17);
            this.yLabel.Text = "y=00000";
            this.yLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // xLabel
            // 
            this.xLabel.AutoSize = false;
            this.xLabel.Name = "xLabel";
            this.xLabel.Overflow = System.Windows.Forms.ToolStripItemOverflow.Never;
            this.xLabel.Size = new System.Drawing.Size(51, 17);
            this.xLabel.Text = "x=00000";
            this.xLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.xLabel,
            this.toolStripStatusLabel1,
            this.yLabel,
            this.toolStripStatusLabel2,
            this.iLabel,
            this.toolStripStatusLabel4,
            this.cursorLabel});
            this.statusStrip.Location = new System.Drawing.Point(0, 639);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(840, 22);
            this.statusStrip.SizingGrip = false;
            this.statusStrip.TabIndex = 13;
            this.statusStrip.Text = "statusStrip";
            // 
            // cursorLabel
            // 
            this.cursorLabel.BackColor = System.Drawing.Color.LightGreen;
            this.cursorLabel.Name = "cursorLabel";
            this.cursorLabel.Size = new System.Drawing.Size(67, 17);
            this.cursorLabel.Text = "Cursor Label";
            this.cursorLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelNCount
            // 
            this.labelNCount.AutoSize = true;
            this.labelNCount.Font = new System.Drawing.Font("Arial Black", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelNCount.Location = new System.Drawing.Point(411, 9);
            this.labelNCount.Name = "labelNCount";
            this.labelNCount.Size = new System.Drawing.Size(61, 68);
            this.labelNCount.TabIndex = 20;
            this.labelNCount.Text = "0";
            // 
            // pictureBox
            // 
            this.pictureBox.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.pictureBox.Cursor = System.Windows.Forms.Cursors.Cross;
            this.pictureBox.Location = new System.Drawing.Point(3, 99);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(431, 131);
            this.pictureBox.TabIndex = 19;
            this.pictureBox.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.pictureBox_PreviewKeyDown);
            this.pictureBox.Click += new System.EventHandler(this.pictureBox_Click);
            this.pictureBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseDown);
            this.pictureBox.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseMove);
            this.pictureBox.Leave += new System.EventHandler(this.pictureBox_Leave);
            this.pictureBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBox_MouseUp);
            // 
            // MultiImageViewer2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button595);
            this.Controls.Add(this.labelNCount);
            this.Controls.Add(this.fullROIButton);
            this.Controls.Add(this.button0135);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.imageIDNUP);
            this.Controls.Add(this.inputCountGB);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip);
            this.Name = "MultiImageViewer2";
            this.Size = new System.Drawing.Size(840, 661);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imageIDNUP)).EndInit();
            this.contextMenuStrip.ResumeLayout(false);
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button595;
        private System.Windows.Forms.ToolStripStatusLabel iLabel;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel4;
        private System.Windows.Forms.Button fullROIButton;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel2;
        private System.Windows.Forms.Button button0135;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel5;
        private System.Windows.Forms.ToolStripStatusLabel countLabel;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.GroupBox inputCountGB;
        private System.Windows.Forms.NumericUpDown imageIDNUP;
        private System.Windows.Forms.ToolStripMenuItem setROIToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem fullROIToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem crossSectionToolStripMenuItem;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.ToolStripStatusLabel yLabel;
        private System.Windows.Forms.ToolStripStatusLabel xLabel;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel cursorLabel;
        private GenericControl pictureBox;
        private System.Windows.Forms.Label labelNCount;
    }
}
