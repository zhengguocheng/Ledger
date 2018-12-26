namespace Ledgers
{
    partial class UcLedgerClientList 
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
            //if (disposing && (components != null))  commented because we dont need to dispose ClientList because it is loaded again and again.
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn7 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn8 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn9 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn10 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn11 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.grdClients = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.cbShowFilter = new Telerik.WinControls.UI.RadCheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.drpSelGrp = new Telerik.WinControls.UI.RadDropDownList();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLedger = new System.Windows.Forms.Panel();
            this.btnAccounts = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccounts)).BeginInit();
            this.SuspendLayout();
            // 
            // grdClients
            // 
            this.grdClients.AutoScroll = true;
            this.grdClients.BackColor = System.Drawing.SystemColors.Control;
            this.grdClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClients.ForeColor = System.Drawing.Color.Black;
            this.grdClients.Location = new System.Drawing.Point(0, 77);
            this.grdClients.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdClients
            // 
            this.grdClients.MasterTemplate.AllowAddNewRow = false;
            this.grdClients.MasterTemplate.AllowCellContextMenu = false;
            this.grdClients.MasterTemplate.AllowDeleteRow = false;
            this.grdClients.MasterTemplate.AllowDragToGroup = false;
            this.grdClients.MasterTemplate.AllowEditRow = false;
            this.grdClients.MasterTemplate.AutoGenerateColumns = false;
            this.grdClients.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewTextBoxColumn1.FieldName = "ID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "ID";
            gridViewTextBoxColumn1.Width = 46;
            gridViewTextBoxColumn2.FieldName = "Reference";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Reference";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "Reference";
            gridViewTextBoxColumn2.Width = 91;
            gridViewTextBoxColumn3.FieldName = "Client_Name";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Client Name";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.Name = "Client_Name";
            gridViewTextBoxColumn3.Width = 155;
            gridViewTextBoxColumn4.FieldName = "Trading_Name";
            gridViewTextBoxColumn4.HeaderText = "Trading Name";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.Name = "colTradingName";
            gridViewTextBoxColumn4.Width = 103;
            gridViewTextBoxColumn5.FieldName = "Post_Code";
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "Post Code";
            gridViewTextBoxColumn5.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn5.Name = "Post_Code";
            gridViewTextBoxColumn5.Width = 82;
            gridViewTextBoxColumn6.FieldName = "Company_Number";
            gridViewTextBoxColumn6.HeaderText = "Company No.";
            gridViewTextBoxColumn6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn6.Name = "colCompanyNo";
            gridViewTextBoxColumn6.Width = 76;
            gridViewTextBoxColumn7.FieldName = "EmailAddress1";
            gridViewTextBoxColumn7.HeaderText = "Email 1";
            gridViewTextBoxColumn7.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn7.Name = "colEmail1";
            gridViewTextBoxColumn7.Width = 86;
            gridViewTextBoxColumn8.FieldName = "EmailAddress2";
            gridViewTextBoxColumn8.HeaderText = "Email 2";
            gridViewTextBoxColumn8.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn8.Name = "colEmail2";
            gridViewTextBoxColumn8.Width = 86;
            gridViewTextBoxColumn9.FieldName = "FaxNo";
            gridViewTextBoxColumn9.HeaderText = "Fax No.";
            gridViewTextBoxColumn9.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn9.Name = "colFaxNo";
            gridViewTextBoxColumn9.Width = 66;
            gridViewTextBoxColumn10.FieldName = "UTR";
            gridViewTextBoxColumn10.FormatString = "";
            gridViewTextBoxColumn10.HeaderText = "UTR";
            gridViewTextBoxColumn10.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn10.Name = "UTR";
            gridViewTextBoxColumn10.Width = 61;
            gridViewTextBoxColumn11.FieldName = "National_Insurance";
            gridViewTextBoxColumn11.FormatString = "";
            gridViewTextBoxColumn11.HeaderText = "National Insurance";
            gridViewTextBoxColumn11.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn11.Name = "National_Insurance";
            gridViewTextBoxColumn11.Width = 76;
            this.grdClients.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6,
            gridViewTextBoxColumn7,
            gridViewTextBoxColumn8,
            gridViewTextBoxColumn9,
            gridViewTextBoxColumn10,
            gridViewTextBoxColumn11});
            this.grdClients.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdClients.MasterTemplate.EnableFiltering = true;
            this.grdClients.MasterTemplate.EnableGrouping = false;
            this.grdClients.MasterTemplate.MultiSelect = true;
            this.grdClients.MasterTemplate.ShowFilteringRow = false;
            this.grdClients.Name = "grdClients";
            this.grdClients.ReadOnly = true;
            // 
            // 
            // 
            this.grdClients.RootElement.ForeColor = System.Drawing.Color.Black;
            this.grdClients.ShowGroupPanel = false;
            this.grdClients.Size = new System.Drawing.Size(939, 265);
            this.grdClients.TabIndex = 0;
            this.grdClients.Text = "radGridView1";
            this.grdClients.SelectionChanged += new System.EventHandler(this.grdClients_SelectionChanged);
            this.grdClients.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdClients_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 44);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 33);
            this.panel1.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.cbShowFilter);
            this.flowLayoutPanel2.Controls.Add(this.label1);
            this.flowLayoutPanel2.Controls.Add(this.drpSelGrp);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(939, 33);
            this.flowLayoutPanel2.TabIndex = 12;
            // 
            // cbShowFilter
            // 
            this.cbShowFilter.Location = new System.Drawing.Point(2, 2);
            this.cbShowFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cbShowFilter.Name = "cbShowFilter";
            this.cbShowFilter.Size = new System.Drawing.Size(91, 18);
            this.cbShowFilter.TabIndex = 0;
            this.cbShowFilter.Text = "Show All Filter";
            this.cbShowFilter.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.cbShowFilter_ToggleStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(105, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Filter by Group:";
            // 
            // drpSelGrp
            // 
            this.drpSelGrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpSelGrp.Location = new System.Drawing.Point(188, 2);
            this.drpSelGrp.Margin = new System.Windows.Forms.Padding(2);
            this.drpSelGrp.MaxDropDownItems = 15;
            this.drpSelGrp.Name = "drpSelGrp";
            this.drpSelGrp.ShowImageInEditorArea = true;
            this.drpSelGrp.Size = new System.Drawing.Size(159, 21);
            this.drpSelGrp.TabIndex = 10;
            this.drpSelGrp.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.drpSelGrp_SelectedIndexChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.pnlLedger);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(939, 44);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // pnlLedger
            // 
            this.pnlLedger.Controls.Add(this.btnAccounts);
            this.pnlLedger.Location = new System.Drawing.Point(3, 3);
            this.pnlLedger.Name = "pnlLedger";
            this.pnlLedger.Size = new System.Drawing.Size(325, 32);
            this.pnlLedger.TabIndex = 8;
            // 
            // btnAccounts
            // 
            this.btnAccounts.Location = new System.Drawing.Point(3, 3);
            this.btnAccounts.Name = "btnAccounts";
            this.btnAccounts.Size = new System.Drawing.Size(68, 24);
            this.btnAccounts.TabIndex = 1;
            this.btnAccounts.Text = "Accounts";
            this.btnAccounts.Click += new System.EventHandler(this.btnAccounts_Click);
            // 
            // UcLedgerClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdClients);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcLedgerClientList";
            this.Size = new System.Drawing.Size(939, 342);
            this.Load += new System.EventHandler(this.UcClientList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAccounts)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdClients;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Telerik.WinControls.UI.RadCheckBox cbShowFilter;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadDropDownList drpSelGrp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Panel pnlLedger;
        private Telerik.WinControls.UI.RadButton btnAccounts;
    }
}
