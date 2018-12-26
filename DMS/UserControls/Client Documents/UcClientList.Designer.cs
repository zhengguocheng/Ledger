namespace DMS.UserControls
{
    partial class UcClientList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing) moved to cs file
        //{
        //    //if (disposing && (components != null))  commented because we dont need to dispose ClientList because it is loaded again and again.
        //    //{
        //    //    components.Dispose();
        //    //}
        //    //base.Dispose(disposing);
        //}

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
            this.pnlDMS = new System.Windows.Forms.Panel();
            this.btnAdd = new Telerik.WinControls.UI.RadButton();
            this.btnUpdate = new Telerik.WinControls.UI.RadButton();
            this.btnDocMerge = new Telerik.WinControls.UI.RadButton();
            this.btnOpenDocument = new Telerik.WinControls.UI.RadButton();
            this.btnMailMerge = new Telerik.WinControls.UI.RadButton();
            this.btnOpenEmail = new Telerik.WinControls.UI.RadButton();
            this.pnlLedger = new System.Windows.Forms.Panel();
            this.btnLedger = new Telerik.WinControls.UI.RadButton();
            this.btnAccount = new Telerik.WinControls.UI.RadButton();
            this.btnRefresh = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.pnlDMS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDocMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenDocument)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMailMerge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenEmail)).BeginInit();
            this.pnlLedger.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            this.SuspendLayout();
            // 
            // grdClients
            // 
            this.grdClients.AutoScroll = true;
            this.grdClients.BackColor = System.Drawing.SystemColors.Control;
            this.grdClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClients.ForeColor = System.Drawing.Color.Black;
            this.grdClients.Location = new System.Drawing.Point(0, 75);
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
            this.grdClients.Size = new System.Drawing.Size(939, 267);
            this.grdClients.TabIndex = 0;
            this.grdClients.Text = "radGridView1";
            this.grdClients.SelectionChanged += new System.EventHandler(this.grdClients_SelectionChanged);
            this.grdClients.CellDoubleClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdClients_CellDoubleClick);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.flowLayoutPanel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 42);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(939, 33);
            this.panel1.TabIndex = 3;
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.btnRefresh);
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
            this.cbShowFilter.Location = new System.Drawing.Point(30, 2);
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
            this.label1.Location = new System.Drawing.Point(133, 3);
            this.label1.Margin = new System.Windows.Forms.Padding(10, 3, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 13);
            this.label1.TabIndex = 11;
            this.label1.Text = "Filter by Group:";
            // 
            // drpSelGrp
            // 
            this.drpSelGrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpSelGrp.Location = new System.Drawing.Point(216, 2);
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
            this.flowLayoutPanel1.Controls.Add(this.pnlDMS);
            this.flowLayoutPanel1.Controls.Add(this.pnlLedger);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(939, 42);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // pnlDMS
            // 
            this.pnlDMS.Controls.Add(this.btnAdd);
            this.pnlDMS.Controls.Add(this.btnUpdate);
            this.pnlDMS.Controls.Add(this.btnDocMerge);
            this.pnlDMS.Controls.Add(this.btnOpenDocument);
            this.pnlDMS.Controls.Add(this.btnMailMerge);
            this.pnlDMS.Controls.Add(this.btnOpenEmail);
            this.pnlDMS.Location = new System.Drawing.Point(3, 3);
            this.pnlDMS.Name = "pnlDMS";
            this.pnlDMS.Size = new System.Drawing.Size(586, 32);
            this.pnlDMS.TabIndex = 7;
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(4, 3);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(68, 24);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.Location = new System.Drawing.Point(78, 3);
            this.btnUpdate.Name = "btnUpdate";
            this.btnUpdate.Size = new System.Drawing.Size(68, 24);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnDocMerge
            // 
            this.btnDocMerge.Location = new System.Drawing.Point(459, 3);
            this.btnDocMerge.Name = "btnDocMerge";
            this.btnDocMerge.Size = new System.Drawing.Size(115, 24);
            this.btnDocMerge.TabIndex = 5;
            this.btnDocMerge.Text = "Document Merge";
            this.btnDocMerge.Click += new System.EventHandler(this.btnDocMerge_Click);
            // 
            // btnOpenDocument
            // 
            this.btnOpenDocument.Location = new System.Drawing.Point(152, 3);
            this.btnOpenDocument.Name = "btnOpenDocument";
            this.btnOpenDocument.Size = new System.Drawing.Size(99, 24);
            this.btnOpenDocument.TabIndex = 2;
            this.btnOpenDocument.Text = "Open Documents";
            this.btnOpenDocument.Click += new System.EventHandler(this.btnOpenDocument_Click);
            // 
            // btnMailMerge
            // 
            this.btnMailMerge.Location = new System.Drawing.Point(354, 3);
            this.btnMailMerge.Name = "btnMailMerge";
            this.btnMailMerge.Size = new System.Drawing.Size(99, 24);
            this.btnMailMerge.TabIndex = 3;
            this.btnMailMerge.Text = "Mail Merge";
            
            // 
            // btnOpenEmail
            // 
            this.btnOpenEmail.Location = new System.Drawing.Point(257, 3);
            this.btnOpenEmail.Name = "btnOpenEmail";
            this.btnOpenEmail.Size = new System.Drawing.Size(91, 24);
            this.btnOpenEmail.TabIndex = 6;
            this.btnOpenEmail.Text = "Emails";
            
            // 
            // pnlLedger
            // 
            this.pnlLedger.Controls.Add(this.btnLedger);
            this.pnlLedger.Controls.Add(this.btnAccount);
            this.pnlLedger.Location = new System.Drawing.Point(595, 3);
            this.pnlLedger.Name = "pnlLedger";
            this.pnlLedger.Size = new System.Drawing.Size(262, 32);
            this.pnlLedger.TabIndex = 8;
            // 
            // btnLedger
            // 
            this.btnLedger.Location = new System.Drawing.Point(78, 3);
            this.btnLedger.Name = "btnLedger";
            this.btnLedger.Size = new System.Drawing.Size(99, 24);
            this.btnLedger.TabIndex = 3;
            this.btnLedger.Text = "Open Ledger";
            this.btnLedger.Click += new System.EventHandler(this.btnLedger_Click);
            // 
            // btnAccount
            // 
            this.btnAccount.Location = new System.Drawing.Point(4, 3);
            this.btnAccount.Name = "btnAccount";
            this.btnAccount.Size = new System.Drawing.Size(68, 24);
            this.btnAccount.TabIndex = 0;
            this.btnAccount.Text = "Account";
            this.btnAccount.Click += new System.EventHandler(this.btnAccount_Click);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = global::DMS.Properties.Resources.resfresh;
            this.btnRefresh.Location = new System.Drawing.Point(3, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(22, 22);
            this.btnRefresh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.btnRefresh.TabIndex = 12;
            this.btnRefresh.TabStop = false;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // UcClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdClients);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcClientList";
            this.Size = new System.Drawing.Size(939, 342);
            this.Load += new System.EventHandler(this.UcClientList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.panel1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.pnlDMS.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDocMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenDocument)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnMailMerge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnOpenEmail)).EndInit();
            this.pnlLedger.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAccount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdClients;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Telerik.WinControls.UI.RadButton btnOpenDocument;
        private Telerik.WinControls.UI.RadCheckBox cbShowFilter;
        private Telerik.WinControls.UI.RadButton btnMailMerge;
        private Telerik.WinControls.UI.RadButton btnAdd;
        private Telerik.WinControls.UI.RadButton btnUpdate;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadDropDownList drpSelGrp;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnDocMerge;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private Telerik.WinControls.UI.RadButton btnOpenEmail;
        private System.Windows.Forms.Panel pnlDMS;
        private System.Windows.Forms.Panel pnlLedger;
        private Telerik.WinControls.UI.RadButton btnAccount;
        private Telerik.WinControls.UI.RadButton btnLedger;
        private System.Windows.Forms.PictureBox btnRefresh;
    }
}
