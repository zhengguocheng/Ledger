namespace DMS.UserControls
{
    partial class UcAddDocument
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.btnSelectFile = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtDescription = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtFilePath = new Telerik.WinControls.UI.RadTextBox();
            this.txtFileName = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtFileExtention = new Telerik.WinControls.UI.RadTextBox();
            this.chkIsMultiple = new Telerik.WinControls.UI.RadCheckBox();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileExtention)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMultiple)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Title = "Open File";
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(470, 87);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(25, 24);
            this.btnSelectFile.TabIndex = 0;
            this.btnSelectFile.Text = "....";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(100, 251);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 24);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(6, 131);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(78, 18);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.Location = new System.Drawing.Point(100, 130);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            // 
            // 
            // 
            this.txtDescription.RootElement.StretchVertically = true;
            this.txtDescription.Size = new System.Drawing.Size(368, 103);
            this.txtDescription.TabIndex = 1;
            this.txtDescription.TabStop = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(6, 90);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(73, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Select File:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.Enabled = false;
            this.txtFilePath.Location = new System.Drawing.Point(100, 87);
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(368, 24);
            this.txtFilePath.TabIndex = 0;
            this.txtFilePath.TabStop = false;
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(100, 42);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.Size = new System.Drawing.Size(289, 24);
            this.txtFileName.TabIndex = 3;
            this.txtFileName.TabStop = false;
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(6, 45);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(46, 18);
            this.radLabel3.TabIndex = 4;
            this.radLabel3.Text = "Name:";
            // 
            // txtFileExtention
            // 
            this.txtFileExtention.Enabled = false;
            this.txtFileExtention.Location = new System.Drawing.Point(393, 42);
            this.txtFileExtention.Name = "txtFileExtention";
            this.txtFileExtention.Size = new System.Drawing.Size(75, 24);
            this.txtFileExtention.TabIndex = 7;
            this.txtFileExtention.TabStop = false;
            // 
            // chkIsMultiple
            // 
            this.chkIsMultiple.Location = new System.Drawing.Point(100, 3);
            this.chkIsMultiple.Name = "chkIsMultiple";
            this.chkIsMultiple.Size = new System.Drawing.Size(15, 15);
            this.chkIsMultiple.TabIndex = 8;
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(6, 3);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(89, 18);
            this.radLabel4.TabIndex = 5;
            this.radLabel4.Text = "Multiple Files:";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Please select the folder for importing all files in it.";
            // 
            // UcAddDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.chkIsMultiple);
            this.Controls.Add(this.txtFileExtention);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtFilePath);
            this.Name = "UcAddDocument";
            this.Size = new System.Drawing.Size(510, 291);
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFileExtention)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIsMultiple)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadTextBox txtFilePath;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtDescription;
        private Telerik.WinControls.UI.RadLabel radLabel2;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnSelectFile;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private Telerik.WinControls.UI.RadTextBox txtFileName;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtFileExtention;
        private Telerik.WinControls.UI.RadCheckBox chkIsMultiple;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
