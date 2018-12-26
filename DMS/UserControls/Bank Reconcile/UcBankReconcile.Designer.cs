namespace DMS.UserControls
{
    partial class UcBankReconcile
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.ledgerGrid1 = new DMS.CustomClasses.BankReconcileGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label9 = new System.Windows.Forms.Label();
            this.drpSort = new Telerik.WinControls.UI.RadDropDownList();
            this.btnShowData = new Telerik.WinControls.UI.RadButton();
            this.txtEndPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.chkShowTick = new System.Windows.Forms.RadioButton();
            this.chkShowUntick = new System.Windows.Forms.RadioButton();
            this.chkShowAll = new System.Windows.Forms.RadioButton();
            this.label7 = new System.Windows.Forms.Label();
            this.txtStatementBal = new Telerik.WinControls.UI.RadTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtBalPerSt = new Telerik.WinControls.UI.RadTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtOutRec = new Telerik.WinControls.UI.RadTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtOutPayment = new Telerik.WinControls.UI.RadTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtAcctBal = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dpRecDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.txtStPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.drpNominalCode = new Telerik.WinControls.UI.RadDropDownList();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.label11 = new System.Windows.Forms.Label();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotals = new System.Windows.Forms.Label();
            this.btnLedgerRpt = new Telerik.WinControls.UI.RadButton();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpSort)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatementBal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalPerSt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutRec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutPayment)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAcctBal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpRecDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpNominalCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLedgerRpt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // ledgerGrid1
            // 
            this.ledgerGrid1.BackColor = System.Drawing.Color.White;
            this.ledgerGrid1.ColumnHeaderContextMenuStrip = null;
            this.ledgerGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ledgerGrid1.LeadHeaderContextMenuStrip = null;
            this.ledgerGrid1.Location = new System.Drawing.Point(0, 162);
            this.ledgerGrid1.Name = "ledgerGrid1";
            this.ledgerGrid1.RowHeaderContextMenuStrip = null;
            this.ledgerGrid1.Script = null;
            this.ledgerGrid1.SheetTabContextMenuStrip = null;
            this.ledgerGrid1.SheetTabControlNewButtonVisible = true;
            this.ledgerGrid1.SheetTabControlWidth = 60;
            this.ledgerGrid1.SheetTabNewButtonVisible = true;
            this.ledgerGrid1.SheetTabWidth = 60;
            this.ledgerGrid1.Size = new System.Drawing.Size(946, 284);
            this.ledgerGrid1.TabIndex = 0;
            this.ledgerGrid1.Text = "ledgerGrid1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label9);
            this.groupBox1.Controls.Add(this.drpSort);
            this.groupBox1.Controls.Add(this.btnShowData);
            this.groupBox1.Controls.Add(this.txtEndPeriod);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.chkShowTick);
            this.groupBox1.Controls.Add(this.chkShowUntick);
            this.groupBox1.Controls.Add(this.chkShowAll);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.txtStatementBal);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtBalPerSt);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtOutRec);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtOutPayment);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtAcctBal);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.dpRecDate);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtStPeriod);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.drpNominalCode);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 162);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Information";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(7, 110);
            this.label9.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(106, 13);
            this.label9.TabIndex = 99;
            this.label9.Text = "Select Sort Column:  ";
            // 
            // drpSort
            // 
            this.drpSort.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpSort.Location = new System.Drawing.Point(136, 106);
            this.drpSort.Name = "drpSort";
            this.drpSort.Size = new System.Drawing.Size(126, 20);
            this.drpSort.TabIndex = 98;
            // 
            // btnShowData
            // 
            this.btnShowData.Location = new System.Drawing.Point(10, 132);
            this.btnShowData.Name = "btnShowData";
            this.btnShowData.Size = new System.Drawing.Size(80, 24);
            this.btnShowData.TabIndex = 12;
            this.btnShowData.Text = "Show Data";
            this.btnShowData.Click += new System.EventHandler(this.btnShowData_Click);
            // 
            // txtEndPeriod
            // 
            this.txtEndPeriod.Location = new System.Drawing.Point(474, 76);
            this.txtEndPeriod.Name = "txtEndPeriod";
            this.txtEndPeriod.NullText = "To";
            this.txtEndPeriod.Size = new System.Drawing.Size(39, 20);
            this.txtEndPeriod.TabIndex = 8;
            this.txtEndPeriod.Text = "12";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(453, 80);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(13, 13);
            this.label8.TabIndex = 97;
            this.label8.Text = "--";
            // 
            // chkShowTick
            // 
            this.chkShowTick.AutoSize = true;
            this.chkShowTick.Location = new System.Drawing.Point(708, 78);
            this.chkShowTick.Name = "chkShowTick";
            this.chkShowTick.Size = new System.Drawing.Size(88, 17);
            this.chkShowTick.TabIndex = 10;
            this.chkShowTick.TabStop = true;
            this.chkShowTick.Text = "Show Ticked";
            this.chkShowTick.UseVisualStyleBackColor = true;
            this.chkShowTick.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // chkShowUntick
            // 
            this.chkShowUntick.AutoSize = true;
            this.chkShowUntick.Location = new System.Drawing.Point(797, 78);
            this.chkShowUntick.Name = "chkShowUntick";
            this.chkShowUntick.Size = new System.Drawing.Size(98, 17);
            this.chkShowUntick.TabIndex = 11;
            this.chkShowUntick.TabStop = true;
            this.chkShowUntick.Text = "Show Unticked";
            this.chkShowUntick.UseVisualStyleBackColor = true;
            this.chkShowUntick.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);
            // 
            // chkShowAll
            // 
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Location = new System.Drawing.Point(641, 78);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(66, 17);
            this.chkShowAll.TabIndex = 9;
            this.chkShowAll.TabStop = true;
            this.chkShowAll.Text = "Show All";
            this.chkShowAll.UseVisualStyleBackColor = true;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(7, 80);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(100, 13);
            this.label7.TabIndex = 93;
            this.label7.Text = "Statement Balance:";
            // 
            // txtStatementBal
            // 
            this.txtStatementBal.Location = new System.Drawing.Point(136, 76);
            this.txtStatementBal.Name = "txtStatementBal";
            this.txtStatementBal.Size = new System.Drawing.Size(126, 20);
            this.txtStatementBal.TabIndex = 6;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(639, 52);
            this.label6.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(119, 13);
            this.label6.TabIndex = 91;
            this.label6.Text = "Balance Per Statement:";
            // 
            // txtBalPerSt
            // 
            this.txtBalPerSt.Location = new System.Drawing.Point(775, 48);
            this.txtBalPerSt.Name = "txtBalPerSt";
            this.txtBalPerSt.ReadOnly = true;
            this.txtBalPerSt.Size = new System.Drawing.Size(113, 20);
            this.txtBalPerSt.TabIndex = 5;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(290, 52);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(112, 13);
            this.label5.TabIndex = 89;
            this.label5.Text = "Outstanding Receipts:";
            // 
            // txtOutRec
            // 
            this.txtOutRec.Location = new System.Drawing.Point(408, 48);
            this.txtOutRec.Name = "txtOutRec";
            this.txtOutRec.ReadOnly = true;
            this.txtOutRec.Size = new System.Drawing.Size(213, 20);
            this.txtOutRec.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(7, 52);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(111, 13);
            this.label4.TabIndex = 87;
            this.label4.Text = "Outstanding Payment:";
            // 
            // txtOutPayment
            // 
            this.txtOutPayment.Location = new System.Drawing.Point(136, 48);
            this.txtOutPayment.Name = "txtOutPayment";
            this.txtOutPayment.ReadOnly = true;
            this.txtOutPayment.Size = new System.Drawing.Size(126, 20);
            this.txtOutPayment.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(639, 25);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 13);
            this.label3.TabIndex = 85;
            this.label3.Text = "Account Balance:";
            // 
            // txtAcctBal
            // 
            this.txtAcctBal.Location = new System.Drawing.Point(775, 20);
            this.txtAcctBal.Name = "txtAcctBal";
            this.txtAcctBal.ReadOnly = true;
            this.txtAcctBal.Size = new System.Drawing.Size(113, 20);
            this.txtAcctBal.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(7, 24);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 13);
            this.label2.TabIndex = 83;
            this.label2.Text = "Reconciliation Date:";
            // 
            // dpRecDate
            // 
            this.dpRecDate.Location = new System.Drawing.Point(136, 20);
            this.dpRecDate.Name = "dpRecDate";
            this.dpRecDate.Size = new System.Drawing.Size(126, 20);
            this.dpRecDate.TabIndex = 0;
            this.dpRecDate.TabStop = false;
            this.dpRecDate.Text = "17/12/2015";
            this.dpRecDate.Value = new System.DateTime(2015, 12, 17, 14, 11, 39, 969);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(290, 80);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Period:";
            // 
            // txtStPeriod
            // 
            this.txtStPeriod.Location = new System.Drawing.Point(408, 76);
            this.txtStPeriod.Name = "txtStPeriod";
            this.txtStPeriod.NullText = "From";
            this.txtStPeriod.Size = new System.Drawing.Size(39, 20);
            this.txtStPeriod.TabIndex = 7;
            this.txtStPeriod.Text = "1";
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(173, 132);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 24);
            this.btnSearch.TabIndex = 14;
            this.btnSearch.Text = "Search";
            // 
            // drpNominalCode
            // 
            this.drpNominalCode.Location = new System.Drawing.Point(408, 20);
            this.drpNominalCode.Name = "drpNominalCode";
            this.drpNominalCode.Size = new System.Drawing.Size(213, 20);
            this.drpNominalCode.TabIndex = 1;
            this.drpNominalCode.Text = "radDropDownList1";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(254, 132);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 24);
            this.btnClear.TabIndex = 15;
            this.btnClear.Text = "Clear";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(290, 24);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 70;
            this.label11.Text = "Nominal Code:";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(97, 132);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 24);
            this.btnSave.TabIndex = 13;
            this.btnSave.Text = "Save";
            this.btnSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSave_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTotals);
            this.panel1.Controls.Add(this.btnLedgerRpt);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 446);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 34);
            this.panel1.TabIndex = 5;
            // 
            // lblTotals
            // 
            this.lblTotals.AutoSize = true;
            this.lblTotals.ForeColor = System.Drawing.Color.Black;
            this.lblTotals.Location = new System.Drawing.Point(175, 10);
            this.lblTotals.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.Size = new System.Drawing.Size(36, 13);
            this.lblTotals.TabIndex = 78;
            this.lblTotals.Text = "Totals";
            // 
            // btnLedgerRpt
            // 
            this.btnLedgerRpt.Location = new System.Drawing.Point(86, 6);
            this.btnLedgerRpt.Name = "btnLedgerRpt";
            this.btnLedgerRpt.Size = new System.Drawing.Size(84, 24);
            this.btnLedgerRpt.TabIndex = 76;
            this.btnLedgerRpt.Text = "Page Report";
            this.btnLedgerRpt.Click += new System.EventHandler(this.btnLedgerRpt_Click);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(6, 6);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(74, 24);
            this.btnBack.TabIndex = 75;
            this.btnBack.Text = "Go Back";
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UcBankReconcile
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ledgerGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcBankReconcile";
            this.Size = new System.Drawing.Size(946, 480);
            this.Load += new System.EventHandler(this.UcExcelSheet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpSort)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnShowData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStatementBal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBalPerSt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutRec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOutPayment)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtAcctBal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpRecDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpNominalCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLedgerRpt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomClasses.BankReconcileGrid ledgerGrid1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.Label label11;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadDropDownList drpNominalCode;
        private Telerik.WinControls.UI.RadButton btnSearch;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnBack;
        private Telerik.WinControls.UI.RadButton btnLedgerRpt;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadTextBox txtStPeriod;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.RadioButton chkShowTick;
        private System.Windows.Forms.RadioButton chkShowUntick;
        private System.Windows.Forms.RadioButton chkShowAll;
        private System.Windows.Forms.Label label7;
        private Telerik.WinControls.UI.RadTextBox txtStatementBal;
        private System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadTextBox txtBalPerSt;
        private System.Windows.Forms.Label label5;
        private Telerik.WinControls.UI.RadTextBox txtOutRec;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadTextBox txtOutPayment;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadTextBox txtAcctBal;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadDateTimePicker dpRecDate;
        private Telerik.WinControls.UI.RadTextBox txtEndPeriod;
        private System.Windows.Forms.Label label8;
        private Telerik.WinControls.UI.RadButton btnShowData;
        private System.Windows.Forms.Label label9;
        private Telerik.WinControls.UI.RadDropDownList drpSort;
    }
}
