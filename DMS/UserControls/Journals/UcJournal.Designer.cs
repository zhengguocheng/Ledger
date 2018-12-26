namespace DMS.UserControls
{
    partial class UcJournal
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
            this.ledgerGrid1 = new DMS.CustomClasses.JournalsGrid();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnDeleteRow = new Telerik.WinControls.UI.RadButton();
            this.btnInsertRow = new Telerik.WinControls.UI.RadButton();
            this.btnSearch = new Telerik.WinControls.UI.RadButton();
            this.btnClear = new Telerik.WinControls.UI.RadButton();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTotals = new System.Windows.Forms.Label();
            this.btnLedgerRpt = new Telerik.WinControls.UI.RadButton();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.txtClientName = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtYearEnd = new Telerik.WinControls.UI.RadTextBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInsertRow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLedgerRpt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearEnd)).BeginInit();
            this.SuspendLayout();
            // 
            // ledgerGrid1
            // 
            this.ledgerGrid1.BackColor = System.Drawing.Color.White;
            this.ledgerGrid1.ColumnHeaderContextMenuStrip = null;
            this.ledgerGrid1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ledgerGrid1.LeadHeaderContextMenuStrip = null;
            this.ledgerGrid1.Location = new System.Drawing.Point(0, 107);
            this.ledgerGrid1.Name = "ledgerGrid1";
            this.ledgerGrid1.RowHeaderContextMenuStrip = null;
            this.ledgerGrid1.Script = null;
            this.ledgerGrid1.SheetTabContextMenuStrip = null;
            this.ledgerGrid1.SheetTabControlWidth = 60;
            this.ledgerGrid1.Size = new System.Drawing.Size(946, 333);
            this.ledgerGrid1.TabIndex = 0;
            this.ledgerGrid1.Text = "ledgerGrid1";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtYearEnd);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.btnDeleteRow);
            this.groupBox1.Controls.Add(this.btnInsertRow);
            this.groupBox1.Controls.Add(this.btnSearch);
            this.groupBox1.Controls.Add(this.btnClear);
            this.groupBox1.Controls.Add(this.btnSave);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(946, 107);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Payment Information";
            // 
            // btnDeleteRow
            // 
            this.btnDeleteRow.Location = new System.Drawing.Point(161, 74);
            this.btnDeleteRow.Name = "btnDeleteRow";
            this.btnDeleteRow.Size = new System.Drawing.Size(72, 24);
            this.btnDeleteRow.TabIndex = 76;
            this.btnDeleteRow.Text = "Delete Row";
            this.btnDeleteRow.Click += new System.EventHandler(this.btnDeleteRow_Click);
            // 
            // btnInsertRow
            // 
            this.btnInsertRow.Location = new System.Drawing.Point(82, 74);
            this.btnInsertRow.Name = "btnInsertRow";
            this.btnInsertRow.Size = new System.Drawing.Size(72, 24);
            this.btnInsertRow.TabIndex = 75;
            this.btnInsertRow.Text = "Insert Row";
            this.btnInsertRow.Click += new System.EventHandler(this.btnInsertRow_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(238, 74);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(74, 24);
            this.btnSearch.TabIndex = 74;
            this.btnSearch.Text = "Search";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(319, 74);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(74, 24);
            this.btnClear.TabIndex = 72;
            this.btnClear.Text = "Clear";
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(6, 74);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(68, 24);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
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
            this.lblTotals.Location = new System.Drawing.Point(697, 11);
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(5, 22);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 83;
            this.label1.Text = "Client Name:";
            // 
            // txtClientName
            // 
            this.txtClientName.Location = new System.Drawing.Point(82, 19);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.ReadOnly = true;
            this.txtClientName.Size = new System.Drawing.Size(311, 20);
            this.txtClientName.TabIndex = 82;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(5, 48);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 85;
            this.label2.Text = "Year End:";
            // 
            // txtYearEnd
            // 
            this.txtYearEnd.Location = new System.Drawing.Point(82, 45);
            this.txtYearEnd.Name = "txtYearEnd";
            this.txtYearEnd.ReadOnly = true;
            this.txtYearEnd.Size = new System.Drawing.Size(311, 20);
            this.txtYearEnd.TabIndex = 84;
            // 
            // UcJournal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ledgerGrid1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.groupBox1);
            this.Name = "UcJournal";
            this.Size = new System.Drawing.Size(946, 480);
            this.Load += new System.EventHandler(this.UcExcelSheet_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDeleteRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnInsertRow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSearch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnClear)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLedgerRpt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearEnd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomClasses.JournalsGrid ledgerGrid1;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadButton btnClear;
        private Telerik.WinControls.UI.RadButton btnSearch;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnBack;
        private Telerik.WinControls.UI.RadButton btnInsertRow;
        private Telerik.WinControls.UI.RadButton btnLedgerRpt;
        private Telerik.WinControls.UI.RadButton btnDeleteRow;
        private System.Windows.Forms.Label lblTotals;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtYearEnd;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadTextBox txtClientName;
    }
}
