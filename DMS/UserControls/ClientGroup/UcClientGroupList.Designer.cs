namespace DMS.UserControls
{
    partial class UcClientGroupList
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
            Telerik.WinControls.UI.GridViewCheckBoxColumn gridViewCheckBoxColumn1 = new Telerik.WinControls.UI.GridViewCheckBoxColumn();
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
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnSave = new Telerik.WinControls.UI.RadButton();
            this.btnCancel = new Telerik.WinControls.UI.RadButton();
            this.cbShowFilter = new Telerik.WinControls.UI.RadCheckBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.drpSelGrp = new Telerik.WinControls.UI.RadDropDownList();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).BeginInit();
            this.SuspendLayout();
            // 
            // grdClients
            // 
            this.grdClients.AutoScroll = true;
            this.grdClients.BackColor = System.Drawing.SystemColors.Control;
            this.grdClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClients.ForeColor = System.Drawing.Color.Black;
            this.grdClients.Location = new System.Drawing.Point(0, 36);
            this.grdClients.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // grdClients
            // 
            this.grdClients.MasterTemplate.AllowAddNewRow = false;
            this.grdClients.MasterTemplate.AllowCellContextMenu = false;
            this.grdClients.MasterTemplate.AllowDeleteRow = false;
            this.grdClients.MasterTemplate.AllowDragToGroup = false;
            this.grdClients.MasterTemplate.AllowRowResize = false;
            this.grdClients.MasterTemplate.AutoGenerateColumns = false;
            this.grdClients.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewCheckBoxColumn1.HeaderText = "Select";
            gridViewCheckBoxColumn1.Name = "colChkBox";
            gridViewCheckBoxColumn1.Width = 47;
            gridViewTextBoxColumn1.FieldName = "ID";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "ID";
            gridViewTextBoxColumn1.ReadOnly = true;
            gridViewTextBoxColumn1.Width = 39;
            gridViewTextBoxColumn2.FieldName = "Reference";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Reference";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "Reference";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 77;
            gridViewTextBoxColumn3.FieldName = "Client_Name";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Client Name";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.Name = "Client_Name";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 131;
            gridViewTextBoxColumn4.FieldName = "Trading_Name";
            gridViewTextBoxColumn4.HeaderText = "Trading Name";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.Name = "colTradingName";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 87;
            gridViewTextBoxColumn5.FieldName = "Post_Code";
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "Post Code";
            gridViewTextBoxColumn5.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn5.Name = "Post_Code";
            gridViewTextBoxColumn5.ReadOnly = true;
            gridViewTextBoxColumn5.Width = 70;
            gridViewTextBoxColumn6.FieldName = "Company_Number";
            gridViewTextBoxColumn6.HeaderText = "Company No.";
            gridViewTextBoxColumn6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn6.Name = "colCompanyNo";
            gridViewTextBoxColumn6.ReadOnly = true;
            gridViewTextBoxColumn6.Width = 64;
            gridViewTextBoxColumn7.FieldName = "EmailAddress1";
            gridViewTextBoxColumn7.HeaderText = "Email 1";
            gridViewTextBoxColumn7.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn7.Name = "colEmail1";
            gridViewTextBoxColumn7.ReadOnly = true;
            gridViewTextBoxColumn7.Width = 74;
            gridViewTextBoxColumn8.FieldName = "EmailAddress2";
            gridViewTextBoxColumn8.HeaderText = "Email 2";
            gridViewTextBoxColumn8.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn8.Name = "colEmail2";
            gridViewTextBoxColumn8.ReadOnly = true;
            gridViewTextBoxColumn8.Width = 74;
            gridViewTextBoxColumn9.FieldName = "FaxNo";
            gridViewTextBoxColumn9.HeaderText = "Fax No.";
            gridViewTextBoxColumn9.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn9.Name = "colFaxNo";
            gridViewTextBoxColumn9.ReadOnly = true;
            gridViewTextBoxColumn9.Width = 56;
            gridViewTextBoxColumn10.FieldName = "UTR";
            gridViewTextBoxColumn10.FormatString = "";
            gridViewTextBoxColumn10.HeaderText = "UTR";
            gridViewTextBoxColumn10.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn10.Name = "UTR";
            gridViewTextBoxColumn10.ReadOnly = true;
            gridViewTextBoxColumn10.Width = 52;
            gridViewTextBoxColumn11.FieldName = "National_Insurance";
            gridViewTextBoxColumn11.FormatString = "";
            gridViewTextBoxColumn11.HeaderText = "National Insurance";
            gridViewTextBoxColumn11.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn11.Name = "National_Insurance";
            gridViewTextBoxColumn11.ReadOnly = true;
            gridViewTextBoxColumn11.Width = 64;
            this.grdClients.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewCheckBoxColumn1,
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
            // 
            // 
            // 
            this.grdClients.RootElement.ForeColor = System.Drawing.Color.Black;
            this.grdClients.ShowGroupPanel = false;
            this.grdClients.Size = new System.Drawing.Size(845, 346);
            this.grdClients.TabIndex = 0;
            this.grdClients.Text = "radGridView1";
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnSave);
            this.flowLayoutPanel1.Controls.Add(this.btnCancel);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 382);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(845, 39);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(4, 4);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(86, 30);
            this.btnSave.TabIndex = 4;
            this.btnSave.Text = "Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(98, 4);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(86, 30);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cbShowFilter
            // 
            this.cbShowFilter.Location = new System.Drawing.Point(4, 5);
            this.cbShowFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.cbShowFilter.Name = "cbShowFilter";
            this.cbShowFilter.Size = new System.Drawing.Size(110, 22);
            this.cbShowFilter.TabIndex = 0;
            this.cbShowFilter.Text = "Show All Filter";
            this.cbShowFilter.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.cbShowFilter_ToggleStateChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.drpSelGrp);
            this.panel1.Controls.Add(this.cbShowFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(845, 36);
            this.panel1.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(464, 7);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(106, 17);
            this.label1.TabIndex = 11;
            this.label1.Text = "Filter by Group:";
            this.label1.Visible = false;
            // 
            // drpSelGrp
            // 
            this.drpSelGrp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDownList;
            this.drpSelGrp.Location = new System.Drawing.Point(581, 2);
            this.drpSelGrp.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.drpSelGrp.MaxDropDownItems = 15;
            this.drpSelGrp.Name = "drpSelGrp";
            this.drpSelGrp.ShowImageInEditorArea = true;
            this.drpSelGrp.Size = new System.Drawing.Size(212, 25);
            this.drpSelGrp.TabIndex = 10;
            this.drpSelGrp.Visible = false;
            this.drpSelGrp.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.drpSelGrp_SelectedIndexChanged);
            // 
            // UcClientGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdClients);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "UcClientGroupList";
            this.Size = new System.Drawing.Size(845, 421);
            this.Load += new System.EventHandler(this.UcClientList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.drpSelGrp)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdClients;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Telerik.WinControls.UI.RadCheckBox cbShowFilter;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadButton btnSave;
        private Telerik.WinControls.UI.RadDropDownList drpSelGrp;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnCancel;
    }
}
