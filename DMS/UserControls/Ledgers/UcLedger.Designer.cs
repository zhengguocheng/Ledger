namespace DMS
{
    partial class UcLedger
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
            Telerik.WinControls.UI.GridViewDateTimeColumn gridViewDateTimeColumn1 = new Telerik.WinControls.UI.GridViewDateTimeColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdItems = new Telerik.WinControls.UI.RadGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnGoBack = new Telerik.WinControls.UI.RadButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtOpeningBal = new Telerik.WinControls.UI.RadTextBox();
            this.txtDebit = new Telerik.WinControls.UI.RadTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtClient = new Telerik.WinControls.UI.RadTextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtCredit = new Telerik.WinControls.UI.RadTextBox();
            this.txtTo = new Telerik.WinControls.UI.RadTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtFrom = new Telerik.WinControls.UI.RadTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGoBack)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningBal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdItems);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1127, 423);
            this.panel1.TabIndex = 2;
            // 
            // grdItems
            // 
            this.grdItems.AutoScroll = true;
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItems.Location = new System.Drawing.Point(0, 0);
            this.grdItems.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdItems
            // 
            this.grdItems.MasterTemplate.AddNewRowPosition = Telerik.WinControls.UI.SystemRowPosition.Bottom;
            this.grdItems.MasterTemplate.AllowCellContextMenu = false;
            this.grdItems.MasterTemplate.AllowDragToGroup = false;
            this.grdItems.MasterTemplate.AutoGenerateColumns = false;
            this.grdItems.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewDateTimeColumn1.CustomFormat = "d";
            gridViewDateTimeColumn1.DataEditFormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn1.FieldName = "Date";
            gridViewDateTimeColumn1.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            gridViewDateTimeColumn1.FormatString = "{0:dd/MM/yyyy}";
            gridViewDateTimeColumn1.HeaderText = "Date";
            gridViewDateTimeColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewDateTimeColumn1.Name = "colDate";
            gridViewDateTimeColumn1.Width = 99;
            gridViewTextBoxColumn1.FieldName = "Reference";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Reference";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "colReference";
            gridViewTextBoxColumn1.Width = 130;
            gridViewTextBoxColumn2.FieldName = "Depit";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Debit";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.MaxWidth = 100;
            gridViewTextBoxColumn2.MinWidth = 100;
            gridViewTextBoxColumn2.Name = "colDebit";
            gridViewTextBoxColumn2.Width = 100;
            gridViewTextBoxColumn3.FieldName = "Credit";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Credit";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.MaxWidth = 100;
            gridViewTextBoxColumn3.MinWidth = 100;
            gridViewTextBoxColumn3.Name = "colCredit";
            gridViewTextBoxColumn3.Width = 100;
            gridViewTextBoxColumn4.FieldName = "Balance";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "Balance";
            gridViewTextBoxColumn4.MaxWidth = 100;
            gridViewTextBoxColumn4.MinWidth = 100;
            gridViewTextBoxColumn4.Name = "colBalance";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 100;
            gridViewComboBoxColumn1.DisplayMember = null;
            gridViewComboBoxColumn1.FieldName = "AnalysisCodeID";
            gridViewComboBoxColumn1.HeaderText = "Analysis Code";
            gridViewComboBoxColumn1.MinWidth = 100;
            gridViewComboBoxColumn1.Name = "colAnalysisCodeID";
            gridViewComboBoxColumn1.ValueMember = null;
            gridViewComboBoxColumn1.Width = 102;
            gridViewTextBoxColumn5.FieldName = "Details";
            gridViewTextBoxColumn5.HeaderText = "Detail";
            gridViewTextBoxColumn5.MaxWidth = 150;
            gridViewTextBoxColumn5.MinWidth = 150;
            gridViewTextBoxColumn5.Name = "colDetails";
            gridViewTextBoxColumn5.Width = 150;
            gridViewTextBoxColumn6.FieldName = "Notes";
            gridViewTextBoxColumn6.FormatString = "";
            gridViewTextBoxColumn6.HeaderText = "Notes";
            gridViewTextBoxColumn6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn6.Name = "colNotes";
            gridViewTextBoxColumn6.Width = 332;
            gridViewTextBoxColumn7.FieldName = "ID";
            gridViewTextBoxColumn7.FormatString = "";
            gridViewTextBoxColumn7.HeaderText = "ID";
            gridViewTextBoxColumn7.IsVisible = false;
            gridViewTextBoxColumn7.Name = "colID";
            gridViewTextBoxColumn7.Width = 32;
            this.grdItems.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewComboBoxColumn1,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7});
            this.grdItems.MasterTemplate.EnableGrouping = false;
            this.grdItems.Name = "grdItems";
            this.grdItems.ShowGroupPanel = false;
            this.grdItems.Size = new System.Drawing.Size(1127, 317);
            this.grdItems.TabIndex = 1;
            this.grdItems.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdItems_CellEndEdit);
            this.grdItems.CellValidating += new Telerik.WinControls.UI.CellValidatingEventHandler(this.grdItems_CellValidating);
            this.grdItems.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.grdItems_CurrentRowChanged);
            this.grdItems.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.grdItems_UserDeletingRow);
            this.grdItems.DefaultValuesNeeded += new Telerik.WinControls.UI.GridViewRowEventHandler(this.grdItems_DefaultValuesNeeded);
            this.grdItems.SortChanged += new Telerik.WinControls.UI.GridViewCollectionChangedEventHandler(this.grdItems_SortChanged);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnSave);
            this.panel3.Controls.Add(this.btnGoBack);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 317);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1127, 106);
            this.panel3.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(8, 76);
            this.btnSave.Margin = new System.Windows.Forms.Padding(2);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(74, 22);
            this.btnSave.TabIndex = 5;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(86, 76);
            this.btnGoBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(74, 22);
            this.btnGoBack.TabIndex = 4;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.txtOpeningBal);
            this.groupBox1.Controls.Add(this.txtDebit);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.txtClient);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.txtCredit);
            this.groupBox1.Controls.Add(this.txtTo);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtFrom);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1127, 74);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Details";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(342, 45);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(62, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Total Debit:";
            // 
            // txtOpeningBal
            // 
            this.txtOpeningBal.Location = new System.Drawing.Point(105, 41);
            this.txtOpeningBal.Name = "txtOpeningBal";
            this.txtOpeningBal.Size = new System.Drawing.Size(218, 20);
            this.txtOpeningBal.TabIndex = 11;
            this.txtOpeningBal.TabStop = false;
            // 
            // txtDebit
            // 
            this.txtDebit.Location = new System.Drawing.Point(406, 41);
            this.txtDebit.Name = "txtDebit";
            this.txtDebit.ReadOnly = true;
            this.txtDebit.Size = new System.Drawing.Size(218, 20);
            this.txtDebit.TabIndex = 1;
            this.txtDebit.TabStop = false;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(7, 45);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(92, 13);
            this.label6.TabIndex = 10;
            this.label6.Text = "Opening Balance:";
            // 
            // txtClient
            // 
            this.txtClient.Location = new System.Drawing.Point(105, 16);
            this.txtClient.Name = "txtClient";
            this.txtClient.ReadOnly = true;
            this.txtClient.Size = new System.Drawing.Size(218, 20);
            this.txtClient.TabIndex = 9;
            this.txtClient.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(7, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(36, 13);
            this.label5.TabIndex = 8;
            this.label5.Text = "Client:";
            // 
            // txtCredit
            // 
            this.txtCredit.Location = new System.Drawing.Point(743, 41);
            this.txtCredit.Name = "txtCredit";
            this.txtCredit.ReadOnly = true;
            this.txtCredit.Size = new System.Drawing.Size(218, 20);
            this.txtCredit.TabIndex = 3;
            this.txtCredit.TabStop = false;
            // 
            // txtTo
            // 
            this.txtTo.Location = new System.Drawing.Point(743, 13);
            this.txtTo.Name = "txtTo";
            this.txtTo.ReadOnly = true;
            this.txtTo.Size = new System.Drawing.Size(218, 20);
            this.txtTo.TabIndex = 7;
            this.txtTo.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(675, 16);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(23, 13);
            this.label4.TabIndex = 6;
            this.label4.Text = "To:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(673, 45);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Total Credit:";
            // 
            // txtFrom
            // 
            this.txtFrom.Location = new System.Drawing.Point(406, 13);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.ReadOnly = true;
            this.txtFrom.Size = new System.Drawing.Size(218, 20);
            this.txtFrom.TabIndex = 5;
            this.txtFrom.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(342, 16);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(33, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "From:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1127, 28);
            this.panel2.TabIndex = 3;
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(2, 2);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 22);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Row";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UcLedger
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcLedger";
            this.Size = new System.Drawing.Size(1127, 451);
            this.Load += new System.EventHandler(this.UcTaskList_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnGoBack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOpeningBal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtFrom)).EndInit();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadTextBox txtCredit;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtDebit;
        private System.Windows.Forms.Panel panel3;
        private Telerik.WinControls.UI.RadButton btnGoBack;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadTextBox txtOpeningBal;
        private System.Windows.Forms.Label label6;
        private Telerik.WinControls.UI.RadTextBox txtClient;
        private System.Windows.Forms.Label label5;
        private Telerik.WinControls.UI.RadTextBox txtTo;
        private System.Windows.Forms.Label label4;
        private Telerik.WinControls.UI.RadTextBox txtFrom;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadButton btnSave;

    }
}
