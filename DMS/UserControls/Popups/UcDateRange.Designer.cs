namespace DMS.UserControls
{
    partial class UcDateRange
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
            this.dpStart = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.dpEnd = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.dpStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            this.SuspendLayout();
            // 
            // dpStart
            // 
            this.dpStart.Location = new System.Drawing.Point(68, 13);
            this.dpStart.Name = "dpStart";
            this.dpStart.Size = new System.Drawing.Size(208, 20);
            this.dpStart.TabIndex = 0;
            this.dpStart.TabStop = false;
            this.dpStart.Text = "03/09/2015";
            this.dpStart.Value = new System.DateTime(2015, 9, 3, 15, 21, 19, 443);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(6, 15);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(59, 18);
            this.radLabel3.TabIndex = 12;
            this.radLabel3.Text = "Start Date:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(68, 67);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(62, 20);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Okay";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(6, 41);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(54, 18);
            this.radLabel1.TabIndex = 15;
            this.radLabel1.Text = "End Date:";
            // 
            // dpEnd
            // 
            this.dpEnd.Location = new System.Drawing.Point(68, 39);
            this.dpEnd.Name = "dpEnd";
            this.dpEnd.Size = new System.Drawing.Size(208, 20);
            this.dpEnd.TabIndex = 14;
            this.dpEnd.TabStop = false;
            this.dpEnd.Text = "03/09/2015";
            this.dpEnd.Value = new System.DateTime(2015, 9, 3, 15, 21, 19, 443);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(137, 67);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 20);
            this.btnCancel.TabIndex = 16;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // UcDateRange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.radLabel1);
            this.Controls.Add(this.dpEnd);
            this.Controls.Add(this.btnSave);
            this.Controls.Add(this.radLabel3);
            this.Controls.Add(this.dpStart);
            this.Name = "UcDateRange";
            this.Size = new System.Drawing.Size(339, 103);
            this.Load += new System.EventHandler(this.UcDateRange_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dpStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Telerik.WinControls.UI.RadDateTimePicker dpStart;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDateTimePicker dpEnd;
        private Telerik.WinControls.UI.RadButton btnCancel;

    }
}
