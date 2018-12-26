namespace DMS.UserControls
{
    partial class UcExcelSheet
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
            this.components = new System.ComponentModel.Container();
            this.ledgerGrid1 = new DMS.CustomClasses.LedgerGrid();
            this.chkHideVat = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExport = new Telerik.WinControls.UI.RadButton();
            this.btnUnlock = new Telerik.WinControls.UI.RadButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.chkLockSheet = new System.Windows.Forms.CheckBox();
            this.btnDeleteRow = new Telerik.WinControls.UI.RadButton();
            this.btnInsertRow = new Telerik.WinControls.UI.RadButton();
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
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUnlock)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInsertRow)).BeginInit();
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
            this.ledgerGrid1.Location = new System.Drawing.Point(0, 134);
            this.ledgerGrid1.Name = "ledgerGrid1";
            this.ledgerGrid1.RowHeaderContextMenuStrip = null;
            this.ledgerGrid1.Script = null;
            this.ledgerGrid1.SheetTabContextMenuStrip = null;
            this.ledgerGrid1.SheetTabControlNewButtonVisible = true;
            this.ledgerGrid1.SheetTabControlWidth = 60;
            this.ledgerGrid1.SheetTabNewButtonVisible = true;
            this.ledgerGrid1.SheetTabWidth = 60;
            this.ledgerGrid1.Size = new System.Drawing.Size(946, 306);
            this.ledgerGrid1.TabIndex = 0;
            this.ledgerGrid1.Text = "ledgerGrid1";
            // 
            // chkHideVat
            // 
            this.chkHideVat.AutoSize = true;
            this.chkHideVat.Location = new System.Drawing.Point(246, 62);
            this.chkHideVat.Name = "chkHideVat";
            this.chkHideVat.Size = new System.Drawing.Size(67, 17);
            this.chkHideVat.TabIndex = 2;
            this.chkHideVat.Text = "Hide Vat";
            this.chkHideVat.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExport);
            this.groupBox1.Controls.Add(this.btnUnlock);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtPeriod);
            this.groupBox1.Controls.Add(this.chkLockSheet);
            this.groupBox1.Controls.Add(this.btnDeleteRow);
            this.groupBox1.Controls.Add(this.btnInsertRow);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.drpNominalCode);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Controls.Add(this.chkHideVat);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 134);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Information";
            // 
            // btnExport
            // 
            this.btnExport.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExport.Location = new System.Drawing.Point(399, 93);
            this.btnExport.Name = "btnExport";
            // 
            // 
            // 
            this.btnExport.RootElement.AccessibleDescription = null;
            this.btnExport.RootElement.AccessibleName = null;
            this.btnExport.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnExport.Size = new System.Drawing.Size(74, 24);
            this.btnExport.TabIndex = 83;
            this.btnExport.Text = "Export";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnUnlock
            // 
            this.btnUnlock.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUnlock.Location = new System.Drawing.Point(479, 93);
            this.btnUnlock.Name = "btnUnlock";
            // 
            // 
            // 
            this.btnUnlock.RootElement.AccessibleDescription = null;
            this.btnUnlock.RootElement.AccessibleName = null;
            this.btnUnlock.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnUnlock.Size = new System.Drawing.Size(74, 24);
            this.btnUnlock.TabIndex = 82;
            this.btnUnlock.Text = "Unloack";
            this.btnUnlock.Click += new System.EventHandler(this.btnUnlock_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 63);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 81;
            this.label1.Text = "Period:";
            // 
            // txtPeriod
            // 
            this.txtPeriod.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtPeriod.Location = new System.Drawing.Point(82, 60);
            this.txtPeriod.Name = "txtPeriod";
            // 
            // 
            // 
            this.txtPeriod.RootElement.AccessibleDescription = null;
            this.txtPeriod.RootElement.AccessibleName = null;
            this.txtPeriod.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 100, 20);
            this.txtPeriod.RootElement.StretchVertically = true;
            this.txtPeriod.Size = new System.Drawing.Size(142, 20);
            this.txtPeriod.TabIndex = 1;
            // 
            // chkLockSheet
            // 
            this.chkLockSheet.AutoSize = true;
            this.chkLockSheet.Location = new System.Drawing.Point(319, 62);
            this.chkLockSheet.Name = "chkLockSheet";
            this.chkLockSheet.Size = new System.Drawing.Size(81, 17);
            this.chkLockSheet.TabIndex = 3;
            this.chkLockSheet.Text = "Lock Sheet";
            this.chkLockSheet.UseVisualStyleBackColor = true;
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDeleteRow.Location = new System.Drawing.Point(161, 93);
            this.btnDeleteRow.Name = "btnDeleteRow";
            // 
            // 
            // 
            this.btnDeleteRow.RootElement.AccessibleDescription = null;
            this.btnDeleteRow.RootElement.AccessibleName = null;
            this.btnDeleteRow.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnDeleteRow.Size = new System.Drawing.Size(72, 24);
            this.btnDeleteRow.TabIndex = 6;
            this.btnDeleteRow.Text = "Delete Row";
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // btnInsertRow
            // 
            this.btnInsertRow.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnInsertRow.Location = new System.Drawing.Point(82, 93);
            this.btnInsertRow.Name = "btnInsertRow";
            // 
            // 
            // 
            this.btnInsertRow.RootElement.AccessibleDescription = null;
            this.btnInsertRow.RootElement.AccessibleName = null;
            this.btnInsertRow.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnInsertRow.Size = new System.Drawing.Size(72, 24);
            this.btnInsertRow.TabIndex = 5;
            this.btnInsertRow.Text = "Insert Row";
            this.btnInsertRow.Click += new System.EventHandler(this.btnInsertRow_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSearch.Location = new System.Drawing.Point(238, 93);
            this.btnSearch.Name = "btnSearch";
            // 
            // 
            // 
            this.btnSearch.RootElement.AccessibleDescription = null;
            this.btnSearch.RootElement.AccessibleName = null;
            this.btnSearch.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnSearch.Size = new System.Drawing.Size(74, 24);
            this.btnSearch.TabIndex = 7;
            this.btnSearch.Text = "Search";
            // 
            // drpNominalCode
            // 
            this.drpNominalCode.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.drpNominalCode.Location = new System.Drawing.Point(82, 25);
            this.drpNominalCode.Name = "drpNominalCode";
            // 
            // 
            // 
            this.drpNominalCode.RootElement.AccessibleDescription = null;
            this.drpNominalCode.RootElement.AccessibleName = null;
            this.drpNominalCode.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
            this.drpNominalCode.RootElement.StretchVertically = true;
            this.drpNominalCode.Size = new System.Drawing.Size(311, 20);
            this.drpNominalCode.TabIndex = 0;
            this.drpNominalCode.Text = "radDropDownList1";
            this.drpNominalCode.TextChanged += new System.EventHandler(this.drpNominalCode_TextChanged);
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnClear.Location = new System.Drawing.Point(319, 93);
            this.btnClear.Name = "btnClear";
            // 
            // 
            // 
            this.btnClear.RootElement.AccessibleDescription = null;
            this.btnClear.RootElement.AccessibleName = null;
            this.btnClear.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnClear.Size = new System.Drawing.Size(74, 24);
            this.btnClear.TabIndex = 8;
            this.btnClear.Text = "Clear";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(5, 27);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 13);
            this.label11.TabIndex = 70;
            this.label11.Text = "Nominal Code:";
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnSave.Location = new System.Drawing.Point(6, 93);
            this.btnSave.Name = "btnSave";
            // 
            // 
            // 
            this.btnSave.RootElement.AccessibleDescription = null;
            this.btnSave.RootElement.AccessibleName = null;
            this.btnSave.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnSave.Size = new System.Drawing.Size(68, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.MouseClick += new System.Windows.Forms.MouseEventHandler(this.btnSave_MouseClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTotals);
            this.panel1.Controls.Add(this.btnLedgerRpt);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 440);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(946, 40);
            this.panel1.TabIndex = 5;
            // 
            // lblTotals
            // 
            this.lblTotals.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTotals.AutoSize = true;
            this.lblTotals.ForeColor = System.Drawing.Color.Black;
            this.lblTotals.Location = new System.Drawing.Point(678, 11);
            this.lblTotals.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTotals.Name = "lblTotals";
            this.lblTotals.Size = new System.Drawing.Size(36, 13);
            this.lblTotals.TabIndex = 78;
            this.lblTotals.Text = "Totals";
            // 
            // btnLedgerRpt
            // 
            this.btnLedgerRpt.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnLedgerRpt.Location = new System.Drawing.Point(86, 6);
            this.btnLedgerRpt.Name = "btnLedgerRpt";
            // 
            // 
            // 
            this.btnLedgerRpt.RootElement.AccessibleDescription = null;
            this.btnLedgerRpt.RootElement.AccessibleName = null;
            this.btnLedgerRpt.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnLedgerRpt.Size = new System.Drawing.Size(84, 24);
            this.btnLedgerRpt.TabIndex = 1;
            this.btnLedgerRpt.Text = "Page Report";
            this.btnLedgerRpt.Click += new System.EventHandler(this.btnLedgerRpt_Click);
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBack.Location = new System.Drawing.Point(6, 6);
            this.btnBack.Name = "btnBack";
            // 
            // 
            // 
            this.btnBack.RootElement.AccessibleDescription = null;
            this.btnBack.RootElement.AccessibleName = null;
            this.btnBack.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnBack.Size = new System.Drawing.Size(74, 24);
            this.btnBack.TabIndex = 0;
            this.btnBack.Text = "Go Back";
            // 
            // timer1
            // 
            this.timer1.Interval = 1500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // UcExcelSheet
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ledgerGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcExcelSheet";
            this.Size = new System.Drawing.Size(946, 480);
            this.Load += new System.EventHandler(this.UcExcelSheet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUnlock)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInsertRow)).EndInit();
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

        private CustomClasses.LedgerGrid ledgerGrid1;
        private System.Windows.Forms.CheckBox chkHideVat;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private System.Windows.Forms.Label label11;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadDropDownList drpNominalCode;
        private Telerik.WinControls.UI.RadButton btnSearch;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnBack;
        private Telerik.WinControls.UI.RadButton btnInsertRow;
        private Telerik.WinControls.UI.RadButton btnLedgerRpt;
        private Telerik.WinControls.UI.RadButton btnDeleteRow;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.CheckBox chkLockSheet;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadTextBox txtPeriod;
        private System.Windows.Forms.Timer timer1;
        private Telerik.WinControls.UI.RadButton btnUnlock;
        private Telerik.WinControls.UI.RadButton btnExport;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
    }
}
