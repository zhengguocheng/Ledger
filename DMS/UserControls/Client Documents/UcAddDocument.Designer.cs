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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.radLabel4 = new Telerik.WinControls.UI.RadLabel();
            this.chkImportFolder = new Telerik.WinControls.UI.RadCheckBox();
            this.btnSelectFile = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel2 = new Telerik.WinControls.UI.RadLabel();
            this.txtDescription = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtFilePath = new Telerik.WinControls.UI.RadTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.Title = "Open File(s)";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Please select the folder for importing all files in it.";
            // 
            // radLabel4
            // 
            this.radLabel4.Location = new System.Drawing.Point(4, 2);
            this.radLabel4.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel4.Name = "radLabel4";
            this.radLabel4.Size = new System.Drawing.Size(77, 18);
            this.radLabel4.TabIndex = 5;
            this.radLabel4.Text = "Import Folder:";
            // 
            // chkImportFolder
            // 
            this.chkImportFolder.Location = new System.Drawing.Point(98, 2);
            this.chkImportFolder.Margin = new System.Windows.Forms.Padding(2);
            this.chkImportFolder.Name = "chkImportFolder";
            this.chkImportFolder.Size = new System.Drawing.Size(15, 15);
            this.chkImportFolder.TabIndex = 0;
            this.chkImportFolder.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkImportFolder_ToggleStateChanged);
            // 
            // btnSelectFile
            // 
            this.btnSelectFile.Location = new System.Drawing.Point(438, 35);
            this.btnSelectFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectFile.Name = "btnSelectFile";
            this.btnSelectFile.Size = new System.Drawing.Size(19, 20);
            this.btnSelectFile.TabIndex = 3;
            this.btnSelectFile.Text = "....";
            this.btnSelectFile.Click += new System.EventHandler(this.btnSelectFile_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(98, 204);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 20);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radLabel2
            // 
            this.radLabel2.Location = new System.Drawing.Point(4, 82);
            this.radLabel2.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel2.Name = "radLabel2";
            this.radLabel2.Size = new System.Drawing.Size(66, 18);
            this.radLabel2.TabIndex = 2;
            this.radLabel2.Text = "Description:";
            // 
            // txtDescription
            // 
            this.txtDescription.AcceptsReturn = true;
            this.txtDescription.AutoSize = false;
            this.txtDescription.Location = new System.Drawing.Point(98, 82);
            this.txtDescription.Margin = new System.Windows.Forms.Padding(2);
            this.txtDescription.Multiline = true;
            this.txtDescription.Name = "txtDescription";
            this.txtDescription.Size = new System.Drawing.Size(336, 113);
            this.txtDescription.TabIndex = 4;
            this.txtDescription.TabStop = false;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(4, 37);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(64, 18);
            this.radLabel1.TabIndex = 1;
            this.radLabel1.Text = "Select Path:";
            // 
            // txtFilePath
            // 
            this.txtFilePath.AutoSize = false;
            this.txtFilePath.Location = new System.Drawing.Point(98, 35);
            this.txtFilePath.Margin = new System.Windows.Forms.Padding(2);
            this.txtFilePath.Multiline = true;
            this.txtFilePath.Name = "txtFilePath";
            this.txtFilePath.ReadOnly = true;
            this.txtFilePath.Size = new System.Drawing.Size(336, 38);
            this.txtFilePath.TabIndex = 2;
            this.txtFilePath.TabStop = false;
            // 
            // UcAddDocument
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radLabel4);
            this.Controls.Add(this.chkImportFolder);
            this.Controls.Add(this.btnSelectFile);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.radLabel2);
            this.Controls.Add(this.txtDescription);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtFilePath);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcAddDocument";
            this.Size = new System.Drawing.Size(473, 254);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkImportFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFilePath)).EndInit();
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
        private Telerik.WinControls.UI.RadCheckBox chkImportFolder;
        private Telerik.WinControls.UI.RadLabel radLabel4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}
