namespace DMS
{
    partial class UcCreateYearEndFolder
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtYearEnd = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.dpEnd = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dpStart = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpStart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtYearEnd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dpEnd);
            this.groupBox1.Controls.Add(this.dpStart);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(350, 190);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // txtYearEnd
            // 
            this.txtYearEnd.Enabled = false;
            this.txtYearEnd.Location = new System.Drawing.Point(74, 15);
            this.txtYearEnd.Name = "txtYearEnd";
            this.txtYearEnd.Size = new System.Drawing.Size(150, 20);
            this.txtYearEnd.TabIndex = 97;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(4, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 96;
            this.label1.Text = "Year End:";
            // 
            // dpEnd
            // 
            this.dpEnd.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dpEnd.Location = new System.Drawing.Point(74, 84);
            this.dpEnd.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dpEnd.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dpEnd.Name = "dpEnd";
            this.dpEnd.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dpEnd.Size = new System.Drawing.Size(150, 20);
            this.dpEnd.TabIndex = 95;
            this.dpEnd.TabStop = false;
            this.dpEnd.Text = "radDateTimePicker2";
            this.dpEnd.Value = new System.DateTime(2014, 7, 12, 12, 38, 23, 590);
            // 
            // dpStart
            // 
            this.dpStart.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dpStart.Location = new System.Drawing.Point(74, 48);
            this.dpStart.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dpStart.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dpStart.Name = "dpStart";
            this.dpStart.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dpStart.Size = new System.Drawing.Size(150, 20);
            this.dpStart.TabIndex = 94;
            this.dpStart.TabStop = false;
            this.dpStart.Text = "radDateTimePicker1";
            this.dpStart.Value = new System.DateTime(2014, 7, 12, 12, 38, 23, 590);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(146, 119);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(74, 119);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 21);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Create";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 52);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(33, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "From:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(4, 87);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(23, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "To:";
            // 
            // UcCreateYearEndFolder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcCreateYearEndFolder";
            this.Size = new System.Drawing.Size(350, 190);
            this.Load += new System.EventHandler(this.UcAccountChart_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dpEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpStart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.TextBox txtYearEnd;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDateTimePicker dpEnd;
        private Telerik.WinControls.UI.RadDateTimePicker dpStart;
    }
}
