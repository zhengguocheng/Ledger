namespace DMS.UserControls
{
    partial class UcEdit
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
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.txtFolderName = new Telerik.WinControls.UI.RadTextBox();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.drpTemplateType = new Telerik.WinControls.UI.RadDropDownList();
            this.lblTemplate = new Telerik.WinControls.UI.RadLabel();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpTemplateType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(4, 5);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(40, 16);
            this.radLabel3.TabIndex = 13;
            this.radLabel3.Text = "Name:";
            // 
            // txtFolderName
            // 
            this.txtFolderName.Location = new System.Drawing.Point(65, 3);
            this.txtFolderName.Margin = new System.Windows.Forms.Padding(2);
            this.txtFolderName.Name = "txtFolderName";
            this.txtFolderName.Size = new System.Drawing.Size(242, 20);
            this.txtFolderName.TabIndex = 12;
            this.txtFolderName.TabStop = false;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(65, 75);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 20);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // drpTemplateType
            // 
            this.drpTemplateType.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpTemplateType.Location = new System.Drawing.Point(65, 38);
            this.drpTemplateType.Margin = new System.Windows.Forms.Padding(2);
            this.drpTemplateType.MaxDropDownItems = 15;
            this.drpTemplateType.Name = "drpTemplateType";
            this.drpTemplateType.ShowImageInEditorArea = true;
            this.drpTemplateType.Size = new System.Drawing.Size(242, 21);
            this.drpTemplateType.TabIndex = 15;
            // 
            // lblTemplate
            // 
            this.lblTemplate.Location = new System.Drawing.Point(4, 38);
            this.lblTemplate.Margin = new System.Windows.Forms.Padding(2);
            this.lblTemplate.Name = "lblTemplate";
            this.lblTemplate.Size = new System.Drawing.Size(57, 16);
            this.lblTemplate.TabIndex = 14;
            this.lblTemplate.Text = "Template:";
            // 
            // UcEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.lblTemplate);
            this.Controls.Add(this.drpTemplateType);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.txtFolderName);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcEdit";
            this.Size = new System.Drawing.Size(334, 105);
            this.Load += new System.EventHandler(this.UcEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFolderName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpTemplateType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadTextBox txtFolderName;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadDropDownList drpTemplateType;
        private Telerik.WinControls.UI.RadLabel lblTemplate;
    }
}
