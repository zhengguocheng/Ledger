namespace DMS
{
    partial class UcLedgerReport
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdItems = new Telerik.WinControls.UI.RadGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.chkDayBooks = new System.Windows.Forms.CheckBox();
            this.drpNominalCode = new Telerik.WinControls.UI.RadDropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.btnPrint = new Telerik.WinControls.UI.RadButton();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.txtEndPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.txtStPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnReport = new Telerik.WinControls.UI.RadButton();
            this.radLabel1 = new Telerik.WinControls.UI.RadLabel();
            this.dpEndDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.radLabel3 = new Telerik.WinControls.UI.RadLabel();
            this.dpStDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.txtYearEnd = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClientName = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.chkSummaryLedger = new System.Windows.Forms.CheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpNominalCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpStDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 156);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 328);
            this.panel1.TabIndex = 2;
            // 
            // grdItems
            // 
            this.grdItems.AutoScroll = true;
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItems.EnableHotTracking = false;
            this.grdItems.HideSelection = true;
            this.grdItems.Location = new System.Drawing.Point(0, 0);
            this.grdItems.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdItems
            // 
            this.grdItems.MasterTemplate.AllowAddNewRow = false;
            this.grdItems.MasterTemplate.AllowCellContextMenu = false;
            this.grdItems.MasterTemplate.AllowDragToGroup = false;
            this.grdItems.MasterTemplate.AllowEditRow = false;
            this.grdItems.MasterTemplate.AutoGenerateColumns = false;
            this.grdItems.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            this.grdItems.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdItems.MasterTemplate.EnableGrouping = false;
            this.grdItems.MasterTemplate.ShowFilteringRow = false;
            this.grdItems.Name = "grdItems";
            this.grdItems.ShowGroupPanel = false;
            this.grdItems.Size = new System.Drawing.Size(783, 328);
            this.grdItems.TabIndex = 1;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 156);
            this.panel2.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.chkSummaryLedger);
            this.groupBox1.Controls.Add(this.chkDayBooks);
            this.groupBox1.Controls.Add(this.drpNominalCode);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnPrint);
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Controls.Add(this.txtEndPeriod);
            this.groupBox1.Controls.Add(this.txtStPeriod);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnReport);
            this.groupBox1.Controls.Add(this.radLabel1);
            this.groupBox1.Controls.Add(this.dpEndDate);
            this.groupBox1.Controls.Add(this.radLabel3);
            this.groupBox1.Controls.Add(this.dpStDate);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.txtYearEnd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 156);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Information";
            // 
            // chkDayBooks
            // 
            this.chkDayBooks.AutoSize = true;
            this.chkDayBooks.Location = new System.Drawing.Point(78, 101);
            this.chkDayBooks.Name = "chkDayBooks";
            this.chkDayBooks.Size = new System.Drawing.Size(132, 17);
            this.chkDayBooks.TabIndex = 91;
            this.chkDayBooks.Text = "Show Day Books Only";
            this.chkDayBooks.UseVisualStyleBackColor = true;
            // 
            // drpNominalCode
            // 
            this.drpNominalCode.Location = new System.Drawing.Point(78, 75);
            this.drpNominalCode.Name = "drpNominalCode";
            this.drpNominalCode.Size = new System.Drawing.Size(228, 20);
            this.drpNominalCode.TabIndex = 89;
            this.drpNominalCode.Text = "radDropDownList1";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(1, 79);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 90;
            this.label11.Text = "Nominal Code:";
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(339, 127);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(80, 24);
            this.btnPrint.TabIndex = 88;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnExport
            // 
            this.btnExport.Location = new System.Drawing.Point(253, 127);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(80, 24);
            this.btnExport.TabIndex = 87;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(425, 101);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(115, 17);
            this.chkAll.TabIndex = 86;
            this.chkAll.Text = "All Dates && Periods";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // txtEndPeriod
            // 
            this.txtEndPeriod.Location = new System.Drawing.Point(539, 75);
            this.txtEndPeriod.Name = "txtEndPeriod";
            this.txtEndPeriod.NullText = "End Period";
            this.txtEndPeriod.Size = new System.Drawing.Size(114, 20);
            this.txtEndPeriod.TabIndex = 85;
            this.txtEndPeriod.TabStop = false;
            // 
            // txtStPeriod
            // 
            this.txtStPeriod.Location = new System.Drawing.Point(425, 75);
            this.txtStPeriod.Name = "txtStPeriod";
            this.txtStPeriod.NullText = "Start Period";
            this.txtStPeriod.Size = new System.Drawing.Size(110, 20);
            this.txtStPeriod.TabIndex = 83;
            this.txtStPeriod.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(345, 79);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Select Period:";
            // 
            // btnReport
            // 
            this.btnReport.Location = new System.Drawing.Point(78, 127);
            this.btnReport.Name = "btnReport";
            this.btnReport.Size = new System.Drawing.Size(80, 24);
            this.btnReport.TabIndex = 81;
            this.btnReport.Text = "Show Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // radLabel1
            // 
            this.radLabel1.Location = new System.Drawing.Point(345, 45);
            this.radLabel1.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel1.Name = "radLabel1";
            this.radLabel1.Size = new System.Drawing.Size(54, 18);
            this.radLabel1.TabIndex = 80;
            this.radLabel1.Text = "End Date:";
            // 
            // dpEndDate
            // 
            this.dpEndDate.Location = new System.Drawing.Point(425, 45);
            this.dpEndDate.Name = "dpEndDate";
            this.dpEndDate.Size = new System.Drawing.Size(228, 20);
            this.dpEndDate.TabIndex = 79;
            this.dpEndDate.TabStop = false;
            this.dpEndDate.Text = "03/09/2015";
            this.dpEndDate.Value = new System.DateTime(2015, 9, 3, 15, 21, 19, 443);
            // 
            // radLabel3
            // 
            this.radLabel3.Location = new System.Drawing.Point(1, 45);
            this.radLabel3.Margin = new System.Windows.Forms.Padding(2);
            this.radLabel3.Name = "radLabel3";
            this.radLabel3.Size = new System.Drawing.Size(59, 18);
            this.radLabel3.TabIndex = 78;
            this.radLabel3.Text = "Start Date:";
            // 
            // dpStDate
            // 
            this.dpStDate.Location = new System.Drawing.Point(78, 45);
            this.dpStDate.Name = "dpStDate";
            this.dpStDate.Size = new System.Drawing.Size(228, 20);
            this.dpStDate.TabIndex = 77;
            this.dpStDate.TabStop = false;
            this.dpStDate.Text = "03/09/2015";
            this.dpStDate.Value = new System.DateTime(2015, 9, 3, 15, 21, 19, 443);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(167, 127);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 24);
            this.btnBack.TabIndex = 76;
            this.btnBack.Text = "Go Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtYearEnd
            // 
            this.txtYearEnd.Location = new System.Drawing.Point(425, 15);
            this.txtYearEnd.Name = "txtYearEnd";
            this.txtYearEnd.ReadOnly = true;
            this.txtYearEnd.Size = new System.Drawing.Size(228, 20);
            this.txtYearEnd.TabIndex = 5;
            this.txtYearEnd.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(345, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Year End:";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(78, 15);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.ReadOnly = true;
            this.txtClientName.Size = new System.Drawing.Size(228, 20);
            this.txtClientName.TabIndex = 3;
            this.txtClientName.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(1, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Client Name:";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Please select the folder for importing all files in it.";
            // 
            // chkSummaryLedger
            // 
            this.chkSummaryLedger.AutoSize = true;
            this.chkSummaryLedger.Location = new System.Drawing.Point(548, 101);
            this.chkSummaryLedger.Name = "chkSummaryLedger";
            this.chkSummaryLedger.Size = new System.Drawing.Size(105, 17);
            this.chkSummaryLedger.TabIndex = 92;
            this.chkSummaryLedger.Text = "Summary Ledger";
            this.chkSummaryLedger.UseVisualStyleBackColor = true;
            this.chkSummaryLedger.CheckedChanged += new System.EventHandler(this.chkSummaryLedger_CheckedChanged);
            // 
            // UcLedgerReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcLedgerReport";
            this.Size = new System.Drawing.Size(783, 484);
            this.Load += new System.EventHandler(this.UcLedgerReport_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpNominalCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnPrint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radLabel3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpStDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadTextBox txtYearEnd;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtClientName;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnBack;
        private Telerik.WinControls.UI.RadButton btnReport;
        private Telerik.WinControls.UI.RadLabel radLabel1;
        private Telerik.WinControls.UI.RadDateTimePicker dpEndDate;
        private Telerik.WinControls.UI.RadLabel radLabel3;
        private Telerik.WinControls.UI.RadDateTimePicker dpStDate;
        private System.Windows.Forms.CheckBox chkAll;
        private Telerik.WinControls.UI.RadTextBox txtEndPeriod;
        private Telerik.WinControls.UI.RadTextBox txtStPeriod;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadButton btnExport;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private Telerik.WinControls.UI.RadButton btnPrint;
        private Telerik.WinControls.UI.RadDropDownList drpNominalCode;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.CheckBox chkDayBooks;
        private System.Windows.Forms.CheckBox chkSummaryLedger;

    }
}
