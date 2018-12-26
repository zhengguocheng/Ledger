namespace DMS.UserControls.Popups
{
    partial class UcSupplierPopup
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
            this.drpDescription = new Telerik.WinControls.UI.RadDropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.drpNomCode = new Telerik.WinControls.UI.RadDropDownList();
            this.lblNomCode = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpDescription)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpNomCode)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(85, 68);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(62, 20);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // drpDescription
            // 
            this.drpDescription.Location = new System.Drawing.Point(85, 12);
            this.drpDescription.Name = "drpDescription";
            this.drpDescription.Size = new System.Drawing.Size(314, 20);
            this.drpDescription.TabIndex = 0;
            this.drpDescription.Text = "radDropDownList1";
            this.drpDescription.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drpVATCode_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(4, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(63, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "Description:";
            // 
            // drpNomCode
            // 
            this.drpNomCode.Location = new System.Drawing.Point(85, 38);
            this.drpNomCode.Name = "drpNomCode";
            this.drpNomCode.Size = new System.Drawing.Size(314, 20);
            this.drpNomCode.TabIndex = 1;
            this.drpNomCode.Text = "radDropDownList1";
            // 
            // lblNomCode
            // 
            this.lblNomCode.AutoSize = true;
            this.lblNomCode.ForeColor = System.Drawing.Color.Black;
            this.lblNomCode.Location = new System.Drawing.Point(4, 40);
            this.lblNomCode.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblNomCode.Name = "lblNomCode";
            this.lblNomCode.Size = new System.Drawing.Size(76, 13);
            this.lblNomCode.TabIndex = 76;
            this.lblNomCode.Text = "Nominal Code:";
            // 
            // UcSupplierPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.drpNomCode);
            this.Controls.Add(this.lblNomCode);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.drpDescription);
            this.Controls.Add(this.label11);
            this.Name = "UcSupplierPopup";
            this.Size = new System.Drawing.Size(425, 100);
            this.Load += new System.EventHandler(this.UcNominalCodePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpDescription)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpNomCode)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList drpDescription;
        private System.Windows.Forms.Label label11;
        private Telerik.WinControls.UI.RadButton btnOk;
        private Telerik.WinControls.UI.RadDropDownList drpNomCode;
        private System.Windows.Forms.Label lblNomCode;
    }
}
