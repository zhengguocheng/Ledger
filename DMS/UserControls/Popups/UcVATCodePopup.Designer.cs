namespace DMS.UserControls.Popups
{
    partial class UcVATCodePopup
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
            this.drpVATCode = new Telerik.WinControls.UI.RadDropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOk = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.drpVATCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            this.SuspendLayout();
            // 
            // drpVATCode
            // 
            this.drpVATCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.drpVATCode.Location = new System.Drawing.Point(85, 12);
            this.drpVATCode.Name = "drpVATCode";
            // 
            // 
            // 
            this.drpVATCode.RootElement.AccessibleDescription = null;
            this.drpVATCode.RootElement.AccessibleName = null;
            this.drpVATCode.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
            this.drpVATCode.RootElement.StretchVertically = true;
            this.drpVATCode.Size = new System.Drawing.Size(314, 20);
            this.drpVATCode.TabIndex = 0;
            this.drpVATCode.Text = "radDropDownList1";
            this.drpVATCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.drpVATCode_KeyUp);
            this.drpVATCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drpVATCode_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(4, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(59, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "VAT Code:";
            // 
            // btnOk
            // 
            this.btnOk.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(85, 37);
            this.btnOk.Margin = new System.Windows.Forms.Padding(2);
            this.btnOk.Name = "btnOk";
            // 
            // 
            // 
            this.btnOk.RootElement.AccessibleDescription = null;
            this.btnOk.RootElement.AccessibleName = null;
            this.btnOk.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnOk.Size = new System.Drawing.Size(62, 20);
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "OK";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // UcVATCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.drpVATCode);
            this.Controls.Add(this.label11);
            this.Name = "UcVATCodePopup";
            this.Size = new System.Drawing.Size(425, 67);
            this.Load += new System.EventHandler(this.UcNominalCodePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.drpVATCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList drpVATCode;
        private System.Windows.Forms.Label label11;
        private Telerik.WinControls.UI.RadButton btnOk;
    }
}
