namespace XCamera
{
    partial class CrossSectionForm
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
            this.sizeLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.gc = new XCamera.GenericControl();
            this.posLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.sizeLabel,
            this.posLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 309);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(976, 22);
            this.statusStrip1.TabIndex = 0;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // sizeLabel
            // 
            this.sizeLabel.Name = "sizeLabel";
            this.sizeLabel.Size = new System.Drawing.Size(86, 17);
            this.sizeLabel.Text = "x=0, width=100";
            // 
            // gc
            // 
            this.gc.AutoSize = true;
            this.gc.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gc.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.gc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gc.Location = new System.Drawing.Point(0, 0);
            this.gc.Name = "gc";
            this.gc.Size = new System.Drawing.Size(976, 309);
            this.gc.TabIndex = 1;
            // 
            // posLabel
            // 
            this.posLabel.Name = "posLabel";
            this.posLabel.Size = new System.Drawing.Size(49, 17);
            this.posLabel.Text = "posLabel";
            // 
            // CrossSectionForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(976, 331);
            this.Controls.Add(this.gc);
            this.Controls.Add(this.statusStrip1);
            this.Name = "CrossSectionForm";
            this.Text = "CrossSectionForm";
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel sizeLabel;
        private GenericControl gc;
        private System.Windows.Forms.ToolStripStatusLabel posLabel;
    }
}