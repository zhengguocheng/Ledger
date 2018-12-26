namespace DMS.UserControls
{
    partial class UcClientGroup
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
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.txtGrpName = new Telerik.WinControls.UI.RadTextBox();
            this.btnAddGroup = new Telerik.WinControls.UI.RadButton();
            this.drpSelGrp = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrpName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).BeginInit();
            this.SuspendLayout();
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(7, 64);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(89, 18);
            this.radLabel3.TabIndex = 16;
            this.radLabel3.Text = "Select Group:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 104);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(83, 24);
            this.btnSave.TabIndex = 14;
            this.btnSave.Text = "OK";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(7, 24);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(65, 18);
            this.radLabel1.TabIndex = 17;
            this.radLabel1.Text = "Add New:";
            // 
            // txtGrpName
            // 
            this.txtGrpName.Location = new System.Drawing.Point(97, 18);
            this.txtGrpName.Name = "txtGrpName";
            this.txtGrpName.Size = new System.Drawing.Size(212, 24);
            this.txtGrpName.TabIndex = 19;
            this.txtGrpName.TabStop = false;
            // 
            // btnAddGroup
            // 
            this.btnAddGroup.Location = new System.Drawing.Point(324, 18);
            this.btnAddGroup.Name = "btnAddGroup";
            this.btnAddGroup.Size = new System.Drawing.Size(69, 24);
            this.btnAddGroup.TabIndex = 20;
            this.btnAddGroup.Text = "Add";
            this.btnAddGroup.Click += new System.EventHandler(this.btnAddGroup_Click);
            // 
            // drpSelGrp
            // 
            this.drpSelGrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpSelGrp.Location = new System.Drawing.Point(97, 61);
            this.drpSelGrp.MaxDropDownItems = 15;
            this.drpSelGrp.Name = "drpSelGrp";
            this.drpSelGrp.ShowImageInEditorArea = true;
            this.drpSelGrp.Size = new System.Drawing.Size(212, 25);
            this.drpSelGrp.TabIndex = 9;
            // 
            // UcClientGroup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drpSelGrp);
            this.Controls.Add(this.btnAddGroup);
            this.Controls.Add(this.txtGrpName);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.btnSave);
            this.Name = "UcClientGroup";
            this.Size = new System.Drawing.Size(447, 149);
            this.Load += new System.EventHandler(this.UcClientGroup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGrpName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadTextBox txtGrpName;
        private Telerik.WinControls.UI.RadButton btnAddGroup;
        private Telerik.WinControls.UI.RadDropDownList drpSelGrp;
    }
}
