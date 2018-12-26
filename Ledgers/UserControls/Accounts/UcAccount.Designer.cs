namespace Ledgers
{
    public partial class UcAccount
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
            this.txtDetail = new System.Windows.Forms.TextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtNotes = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtAnalysisCode = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.drpVAT = new Telerik.WinControls.UI.RadDropDownList();
            this.txtAmount = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtNet = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.dpDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.txtRefrence = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtDetail);
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.txtNotes);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.txtAnalysisCode);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.drpVAT);
            this.groupBox1.Controls.Add(this.txtAmount);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtNet);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.dpDate);
            this.groupBox1.Controls.Add(this.btnCancel);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.txtRefrence);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(2);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(2);
            this.groupBox1.Size = new System.Drawing.Size(618, 353);
            this.groupBox1.TabIndex = 40;
            this.groupBox1.TabStop = false;
            // 
            // txtDetail
            // 
            this.txtDetail.Location = new System.Drawing.Point(378, 133);
            this.txtDetail.Margin = new System.Windows.Forms.Padding(2);
            this.txtDetail.Multiline = true;
            this.txtDetail.Name = "txtDetail";
            this.txtDetail.Size = new System.Drawing.Size(214, 72);
            this.txtDetail.TabIndex = 107;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(331, 136);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(37, 13);
            this.label9.TabIndex = 108;
            this.label9.Text = "Detail:";
            // 
            // txtNotes
            // 
            this.txtNotes.Location = new System.Drawing.Point(96, 133);
            this.txtNotes.Margin = new System.Windows.Forms.Padding(2);
            this.txtNotes.Multiline = true;
            this.txtNotes.Name = "txtNotes";
            this.txtNotes.Size = new System.Drawing.Size(187, 72);
            this.txtNotes.TabIndex = 105;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(4, 140);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(38, 13);
            this.label8.TabIndex = 106;
            this.label8.Text = "Notes:";
            // 
            // txtAnalysisCode
            // 
            this.txtAnalysisCode.Location = new System.Drawing.Point(96, 94);
            this.txtAnalysisCode.Margin = new System.Windows.Forms.Padding(2);
            this.txtAnalysisCode.Name = "txtAnalysisCode";
            this.txtAnalysisCode.Size = new System.Drawing.Size(187, 20);
            this.txtAnalysisCode.TabIndex = 103;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(4, 96);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(76, 13);
            this.label6.TabIndex = 104;
            this.label6.Text = "Analysis Code:";
            // 
            // drpVAT
            // 
            this.drpVAT.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.drpVAT.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpVAT.Location = new System.Drawing.Point(378, 56);
            this.drpVAT.Margin = new System.Windows.Forms.Padding(2);
            this.drpVAT.Name = "drpVAT";
            this.drpVAT.ShowImageInEditorArea = true;
            this.drpVAT.Size = new System.Drawing.Size(214, 21);
            this.drpVAT.TabIndex = 102;
            // 
            // txtAmount
            // 
            this.txtAmount.Location = new System.Drawing.Point(96, 57);
            this.txtAmount.Margin = new System.Windows.Forms.Padding(2);
            this.txtAmount.Name = "txtAmount";
            this.txtAmount.Size = new System.Drawing.Size(187, 20);
            this.txtAmount.TabIndex = 100;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(4, 59);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 13);
            this.label4.TabIndex = 101;
            this.label4.Text = "Amount:";
            // 
            // txtNet
            // 
            this.txtNet.Location = new System.Drawing.Point(378, 94);
            this.txtNet.Margin = new System.Windows.Forms.Padding(2);
            this.txtNet.Name = "txtNet";
            this.txtNet.Size = new System.Drawing.Size(214, 20);
            this.txtNet.TabIndex = 96;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(341, 59);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 97;
            this.label2.Text = "VAT:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(341, 97);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 13);
            this.label1.TabIndex = 95;
            this.label1.Text = "Net:";
            // 
            // dpDate
            // 
            this.dpDate.Format = System.Windows.Forms.DateTimePickerFormat.Long;
            this.dpDate.Location = new System.Drawing.Point(378, 20);
            this.dpDate.Margin = new System.Windows.Forms.Padding(2);
            this.dpDate.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dpDate.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dpDate.Name = "dpDate";
            this.dpDate.NullDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dpDate.Size = new System.Drawing.Size(214, 20);
            this.dpDate.TabIndex = 3;
            this.dpDate.TabStop = false;
            this.dpDate.Text = "radDateTimePicker1";
            this.dpDate.Value = new System.DateTime(2014, 6, 24, 0, 0, 0, 0);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(168, 218);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(2);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(68, 21);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(96, 218);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 21);
            this.btnSave.TabIndex = 2;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtRefrence
            // 
            this.txtRefrence.Location = new System.Drawing.Point(96, 20);
            this.txtRefrence.Margin = new System.Windows.Forms.Padding(2);
            this.txtRefrence.Name = "txtRefrence";
            this.txtRefrence.Size = new System.Drawing.Size(187, 20);
            this.txtRefrence.TabIndex = 0;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(4, 22);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(54, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "Refrence:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(341, 22);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(33, 13);
            this.label5.TabIndex = 82;
            this.label5.Text = "Date:";
            // 
            // UcAccount
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.groupBox1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcAccount";
            this.Size = new System.Drawing.Size(618, 353);
            this.Load += new System.EventHandler(this.UcAccountChart_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtRefrence;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnCancel;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.TextBox txtAmount;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtNet;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDateTimePicker dpDate;
        private Telerik.WinControls.UI.RadDropDownList drpVAT;
        private System.Windows.Forms.TextBox txtDetail;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox txtNotes;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtAnalysisCode;
        private System.Windows.Forms.Label label6;
    }
}
