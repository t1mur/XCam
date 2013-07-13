namespace XCamera
{
    partial class ManualLoadController
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
            this.manualLoadGroupBox = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.binNUP = new System.Windows.Forms.NumericUpDown();
            this.saveButton = new System.Windows.Forms.Button();
            this.loadButton = new System.Windows.Forms.Button();
            this.listFilesButton = new System.Windows.Forms.Button();
            this.fileListBox = new System.Windows.Forms.ListBox();
            this.browseButton = new System.Windows.Forms.Button();
            this.directoryBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.manualLoadGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binNUP)).BeginInit();
            this.SuspendLayout();
            // 
            // finalMIV
            // 
            this.finalMIV.Size = new System.Drawing.Size(696, 658);
            this.finalMIV.Load += new System.EventHandler(this.finalMIV_Load);
            // 
            // manualLoadGroupBox
            // 
            this.manualLoadGroupBox.Controls.Add(this.label4);
            this.manualLoadGroupBox.Controls.Add(this.binNUP);
            this.manualLoadGroupBox.Controls.Add(this.saveButton);
            this.manualLoadGroupBox.Controls.Add(this.loadButton);
            this.manualLoadGroupBox.Controls.Add(this.listFilesButton);
            this.manualLoadGroupBox.Controls.Add(this.fileListBox);
            this.manualLoadGroupBox.Controls.Add(this.browseButton);
            this.manualLoadGroupBox.Controls.Add(this.directoryBox);
            this.manualLoadGroupBox.Controls.Add(this.label3);
            this.manualLoadGroupBox.Location = new System.Drawing.Point(8, 3);
            this.manualLoadGroupBox.Name = "manualLoadGroupBox";
            this.manualLoadGroupBox.Size = new System.Drawing.Size(258, 503);
            this.manualLoadGroupBox.TabIndex = 13;
            this.manualLoadGroupBox.TabStop = false;
            this.manualLoadGroupBox.Text = "Manual Load Setting";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 480);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(22, 13);
            this.label4.TabIndex = 8;
            this.label4.Text = "Bin";
            // 
            // binNUP
            // 
            this.binNUP.Location = new System.Drawing.Point(73, 476);
            this.binNUP.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.binNUP.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.binNUP.Name = "binNUP";
            this.binNUP.Size = new System.Drawing.Size(117, 20);
            this.binNUP.TabIndex = 7;
            this.binNUP.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(196, 475);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(57, 22);
            this.saveButton.TabIndex = 6;
            this.saveButton.Text = "Save";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.saveButton_Click);
            // 
            // loadButton
            // 
            this.loadButton.Location = new System.Drawing.Point(175, 308);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(77, 20);
            this.loadButton.TabIndex = 5;
            this.loadButton.Text = "Load";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // listFilesButton
            // 
            this.listFilesButton.Location = new System.Drawing.Point(170, 39);
            this.listFilesButton.Name = "listFilesButton";
            this.listFilesButton.Size = new System.Drawing.Size(82, 20);
            this.listFilesButton.TabIndex = 4;
            this.listFilesButton.Text = "List Files";
            this.listFilesButton.UseVisualStyleBackColor = true;
            this.listFilesButton.Click += new System.EventHandler(this.listFilesButton_Click);
            // 
            // fileListBox
            // 
            this.fileListBox.FormattingEnabled = true;
            this.fileListBox.Location = new System.Drawing.Point(9, 65);
            this.fileListBox.Name = "fileListBox";
            this.fileListBox.ScrollAlwaysVisible = true;
            this.fileListBox.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.fileListBox.Size = new System.Drawing.Size(241, 238);
            this.fileListBox.TabIndex = 3;
            // 
            // browseButton
            // 
            this.browseButton.Location = new System.Drawing.Point(87, 39);
            this.browseButton.Name = "browseButton";
            this.browseButton.Size = new System.Drawing.Size(77, 20);
            this.browseButton.TabIndex = 2;
            this.browseButton.Text = "Browse";
            this.browseButton.UseVisualStyleBackColor = true;
            this.browseButton.Click += new System.EventHandler(this.browseButton_Click);
            // 
            // directoryBox
            // 
            this.directoryBox.Location = new System.Drawing.Point(87, 13);
            this.directoryBox.Name = "directoryBox";
            this.directoryBox.Size = new System.Drawing.Size(165, 20);
            this.directoryBox.TabIndex = 1;
            this.directoryBox.Text = "C:\\Documents and Settings\\Omoikane\\My Documents\\Upload\\shakeIt";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Directory";
            // 
            // folderBrowserDialog
            // 
            this.folderBrowserDialog.Description = "Select the folder of the SPE files";
            this.folderBrowserDialog.SelectedPath = "P:\\";
            // 
            // ManualLoadController
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.manualLoadGroupBox);
            this.Name = "ManualLoadController";
            this.Size = new System.Drawing.Size(1370, 832);
            this.Controls.SetChildIndex(this.manualLoadGroupBox, 0);
            this.manualLoadGroupBox.ResumeLayout(false);
            this.manualLoadGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.binNUP)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox manualLoadGroupBox;
        private System.Windows.Forms.Button browseButton;
        private System.Windows.Forms.TextBox directoryBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListBox fileListBox;
        private System.Windows.Forms.Button listFilesButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog;
        private System.Windows.Forms.Button loadButton;
        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown binNUP;
    }
}
