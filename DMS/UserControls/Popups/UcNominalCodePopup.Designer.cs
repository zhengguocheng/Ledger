namespace DMS.UserControls.Popups
{
    partial class UcNominalCodePopup
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
            this.drpNominalCode = new Telerik.WinControls.UI.RadDropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.btnOk = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.drpNominalCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).BeginInit();
            this.SuspendLayout();
            // 
            // drpNominalCode
            // 
            this.drpNominalCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.drpNominalCode.Location = new System.Drawing.Point(85, 12);
            this.drpNominalCode.Name = "drpNominalCode";
            // 
            // 
            // 
            this.drpNominalCode.RootElement.AccessibleDescription = null;
            this.drpNominalCode.RootElement.AccessibleName = null;
            this.drpNominalCode.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
            this.drpNominalCode.RootElement.StretchVertically = true;
            this.drpNominalCode.Size = new System.Drawing.Size(314, 20);
            this.drpNominalCode.TabIndex = 0;
            this.drpNominalCode.Text = "radDropDownList1";
            this.drpNominalCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.drpNominalCode_KeyUp);
            this.drpNominalCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.drpNominalCode_KeyDown);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(4, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 74;
            this.label11.Text = "Nominal Code:";
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
            // UcNominalCodePopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.drpNominalCode);
            this.Controls.Add(this.label11);
            this.Name = "UcNominalCodePopup";
            this.Size = new System.Drawing.Size(425, 67);
            this.Load += new System.EventHandler(this.UcNominalCodePopup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.drpNominalCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOk)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDropDownList drpNominalCode;
        private System.Windows.Forms.Label label11;
        private Telerik.WinControls.UI.RadButton btnOk;
    }
}
