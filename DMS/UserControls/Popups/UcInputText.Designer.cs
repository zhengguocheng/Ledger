namespace DMS.UserControls.Popups
{
    partial class UcInputText
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
            this.btnOk = new Telerik.WinControls.UI.RadButton();
            this.label = new Telerik.WinControls.UI.RadLabel();
            this.txtInput = new Telerik.WinControls.UI.RadTextBox();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.btnSelectFolder = new Telerik.WinControls.UI.RadButton();
            this.txtPath = new Telerik.WinControls.UI.RadTextBox();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.label)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFolder)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(67, 66);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(57, 20);
            this.btnOk.TabIndex = 16;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // label
            // 
            this.label.Location = new System.Drawing.Point(4, 15);
            this.label.Margin = new System.Windows.Forms.Padding(2);
            this.label.Name = "label";
            this.label.Size = new System.Drawing.Size(59, 18);
            this.label.TabIndex = 15;
            this.label.Text = "File Name:";
            // 
            // txtInput
            // 
            this.txtInput.Location = new System.Drawing.Point(67, 15);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(211, 20);
            this.txtInput.TabIndex = 17;
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(133, 66);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(57, 20);
            this.btnCancel.TabIndex = 18;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSelectFolder
            // 
            this.btnSelectFolder.Location = new System.Drawing.Point(282, 41);
            this.btnSelectFolder.Margin = new System.Windows.Forms.Padding(2);
            this.btnSelectFolder.Name = "btnSelectFolder";
            this.btnSelectFolder.Size = new System.Drawing.Size(20, 19);
            this.btnSelectFolder.TabIndex = 19;
            this.btnSelectFolder.Text = "...";
            this.btnSelectFolder.Click += new System.EventHandler(this.btnSelectFolder_Click);
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(67, 41);
            this.txtPath.Name = "txtPath";
            this.txtPath.ReadOnly = true;
            this.txtPath.Size = new System.Drawing.Size(211, 20);
            this.txtPath.TabIndex = 20;
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(4, 42);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(31, 18);
            this.radLabel1.TabIndex = 21;
            this.radLabel1.Text = "Path:";
            // 
            // UcInputText
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.btnSelectFolder);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.txtInput);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.label);
            this.Name = "UcInputText";
            this.Size = new System.Drawing.Size(339, 136);
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.label)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSelectFolder)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPath)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnOk;
        private Telerik.WinControls.UI.RadLabel label;
        private Telerik.WinControls.UI.RadTextBox txtInput;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Telerik.WinControls.UI.RadButton btnSelectFolder;
        private Telerik.WinControls.UI.RadTextBox txtPath;
        private Telerik.WinControls.UI.RadLabel radLabel1;
    }
}
