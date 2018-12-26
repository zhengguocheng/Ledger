namespace DMS.Reports
{
    partial class UcRptNextYrOpnBal
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
            this.pnlRpt = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.chkNxtYrOpn = new System.Windows.Forms.RadioButton();
            this.chkPLReserve = new System.Windows.Forms.RadioButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRpt
            // 
            this.pnlRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRpt.Location = new System.Drawing.Point(0, 33);
            this.pnlRpt.Name = "pnlRpt";
            this.pnlRpt.Size = new System.Drawing.Size(901, 427);
            this.pnlRpt.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.chkPLReserve);
            this.panel1.Controls.Add(this.chkNxtYrOpn);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 33);
            this.panel1.TabIndex = 4;
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(63, 22);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // chkNxtYrOpn
            // 
            this.chkNxtYrOpn.AutoSize = true;
            this.chkNxtYrOpn.Checked = true;
            this.chkNxtYrOpn.Location = new System.Drawing.Point(92, 4);
            this.chkNxtYrOpn.Name = "chkNxtYrOpn";
            this.chkNxtYrOpn.Size = new System.Drawing.Size(157, 17);
            this.chkNxtYrOpn.TabIndex = 4;
            this.chkNxtYrOpn.TabStop = true;
            this.chkNxtYrOpn.Text = "Next Year Opening Balance";
            this.chkNxtYrOpn.UseVisualStyleBackColor = true;
            this.chkNxtYrOpn.CheckedChanged += new System.EventHandler(this.chkNxtYrOpn_CheckedChanged);
            // 
            // chkPLReserve
            // 
            this.chkPLReserve.AutoSize = true;
            this.chkPLReserve.Location = new System.Drawing.Point(258, 4);
            this.chkPLReserve.Name = "chkPLReserve";
            this.chkPLReserve.Size = new System.Drawing.Size(81, 17);
            this.chkPLReserve.TabIndex = 5;
            this.chkPLReserve.Text = "PL Reserve";
            this.chkPLReserve.UseVisualStyleBackColor = true;
            // 
            // UcRptNextYrOpnBal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRpt);
            this.Controls.Add(this.panel1);
            this.Name = "UcRptNextYrOpnBal";
            this.Size = new System.Drawing.Size(901, 460);
            this.Load += new System.EventHandler(this.UcRptNextYrOpnBal_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlRpt;
        private System.Windows.Forms.RadioButton chkPLReserve;
        private System.Windows.Forms.RadioButton chkNxtYrOpn;

    }
}
