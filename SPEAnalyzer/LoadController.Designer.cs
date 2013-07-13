namespace XCamera
{
    partial class LoadController
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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.infoGroupBox = new System.Windows.Forms.GroupBox();
            this.fileCreationDateBox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fileNameBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rawMIV = new XCamera.MultiImageViewer2();
            this.finalMIV = new XCamera.MultiImageViewer2();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.infoGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.finalMIV);
            this.groupBox3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox3.Location = new System.Drawing.Point(667, 0);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(698, 672);
            this.groupBox3.TabIndex = 9;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Final Picture";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rawMIV);
            this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox2.Location = new System.Drawing.Point(271, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 418);
            this.groupBox2.TabIndex = 10;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Raw Picture";
            // 
            // infoGroupBox
            // 
            this.infoGroupBox.Controls.Add(this.fileCreationDateBox);
            this.infoGroupBox.Controls.Add(this.label2);
            this.infoGroupBox.Controls.Add(this.fileNameBox);
            this.infoGroupBox.Controls.Add(this.label1);
            this.infoGroupBox.Location = new System.Drawing.Point(3, 512);
            this.infoGroupBox.Name = "infoGroupBox";
            this.infoGroupBox.Size = new System.Drawing.Size(258, 210);
            this.infoGroupBox.TabIndex = 12;
            this.infoGroupBox.TabStop = false;
            this.infoGroupBox.Text = "Info";
            // 
            // fileCreationDateBox
            // 
            this.fileCreationDateBox.Location = new System.Drawing.Point(87, 37);
            this.fileCreationDateBox.Name = "fileCreationDateBox";
            this.fileCreationDateBox.ReadOnly = true;
            this.fileCreationDateBox.Size = new System.Drawing.Size(165, 20);
            this.fileCreationDateBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 40);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(72, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Creation Date";
            // 
            // fileNameBox
            // 
            this.fileNameBox.Location = new System.Drawing.Point(87, 17);
            this.fileNameBox.Name = "fileNameBox";
            this.fileNameBox.ReadOnly = true;
            this.fileNameBox.Size = new System.Drawing.Size(165, 20);
            this.fileNameBox.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "File";
            // 
            // rawMIV
            // 
            this.rawMIV.Binning = 1;
            this.rawMIV.Location = new System.Drawing.Point(1, 15);
            this.rawMIV.Name = "rawMIV";
            this.rawMIV.Size = new System.Drawing.Size(385, 383);
            this.rawMIV.TabIndex = 0;
            this.rawMIV.Load += new System.EventHandler(this.rawMIV_Load);
            // 
            // finalMIV
            // 
            this.finalMIV.Binning = 1;
            this.finalMIV.Location = new System.Drawing.Point(1, 15);
            this.finalMIV.Name = "finalMIV";
            this.finalMIV.Size = new System.Drawing.Size(696, 421);
            this.finalMIV.TabIndex = 0;
            // 
            // LoadController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.infoGroupBox);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox3);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "LoadController";
            this.Size = new System.Drawing.Size(1370, 901);
            this.Load += new System.EventHandler(this.LoadController_Load);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.infoGroupBox.ResumeLayout(false);
            this.infoGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ToolTip toolTip1;
        protected MultiImageViewer2 finalMIV;
        protected MultiImageViewer2 rawMIV;
        private System.Windows.Forms.GroupBox infoGroupBox;
        private System.Windows.Forms.TextBox fileCreationDateBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fileNameBox;
        private System.Windows.Forms.Label label1;
    }
}
