namespace DMS
{
    partial class UcAccountGroupList
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdItems = new Telerik.WinControls.UI.RadGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.btnFilter = new Telerik.WinControls.UI.RadButton();
            this.drpYearEnd = new Telerik.WinControls.UI.RadDropDownList();
            this.label1 = new System.Windows.Forms.Label();
            this.drpClient = new Telerik.WinControls.UI.RadDropDownList();
            this.label11 = new System.Windows.Forms.Label();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            this.btnUpdate = new Telerik.WinControls.UI.RadButton();
            this.btnAdd = new Telerik.WinControls.UI.RadButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpYearEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpClient)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdItems);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 65);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(840, 309);
            this.panel1.TabIndex = 2;
            // 
            // grdItems
            // 
            this.grdItems.AutoScroll = true;
            this.grdItems.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
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
            gridViewTextBoxColumn1.FieldName = "Name";
            gridViewTextBoxColumn1.HeaderText = "Name";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "colName";
            gridViewTextBoxColumn1.Width = 170;
            gridViewTextBoxColumn2.FieldName = "Description";
            gridViewTextBoxColumn2.HeaderText = "Description";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "colDescription";
            gridViewTextBoxColumn2.Width = 650;
            this.grdItems.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2});
            this.grdItems.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdItems.MasterTemplate.EnableGrouping = false;
            this.grdItems.Name = "grdItems";
            // 
            // 
            // 
            this.grdItems.RootElement.AccessibleDescription = null;
            this.grdItems.RootElement.AccessibleName = null;
            this.grdItems.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 240, 150);
            this.grdItems.ShowGroupPanel = false;
            this.grdItems.Size = new System.Drawing.Size(840, 309);
            this.grdItems.TabIndex = 1;
            this.grdItems.DoubleClick += new System.EventHandler(this.grdItems_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnFilter);
            this.panel2.Controls.Add(this.drpYearEnd);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Controls.Add(this.drpClient);
            this.panel2.Controls.Add(this.label11);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Controls.Add(this.btnUpdate);
            this.panel2.Controls.Add(this.btnAdd);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(840, 65);
            this.panel2.TabIndex = 3;
            // 
            // btnFilter
            // 
            this.btnFilter.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnFilter.Location = new System.Drawing.Point(584, 6);
            this.btnFilter.Margin = new System.Windows.Forms.Padding(2);
            this.btnFilter.Name = "btnFilter";
            // 
            // 
            // 
            this.btnFilter.RootElement.AccessibleDescription = null;
            this.btnFilter.RootElement.AccessibleName = null;
            this.btnFilter.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnFilter.Size = new System.Drawing.Size(70, 20);
            this.btnFilter.TabIndex = 79;
            this.btnFilter.Text = "Filter";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // drpYearEnd
            // 
            this.drpYearEnd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.drpYearEnd.Location = new System.Drawing.Point(342, 6);
            this.drpYearEnd.Name = "drpYearEnd";
            // 
            // 
            // 
            this.drpYearEnd.RootElement.AccessibleDescription = null;
            this.drpYearEnd.RootElement.AccessibleName = null;
            this.drpYearEnd.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
            this.drpYearEnd.RootElement.StretchVertically = true;
            this.drpYearEnd.Size = new System.Drawing.Size(221, 20);
            this.drpYearEnd.TabIndex = 77;
            this.drpYearEnd.Text = "radDropDownList1";
            this.drpYearEnd.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.drpYearEnd_SelectedIndexChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(287, 8);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(54, 13);
            this.label1.TabIndex = 78;
            this.label1.Text = "Year End:";
            // 
            // drpClient
            // 
            this.drpClient.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.drpClient.Location = new System.Drawing.Point(44, 6);
            this.drpClient.Name = "drpClient";
            // 
            // 
            // 
            this.drpClient.RootElement.AccessibleDescription = null;
            this.drpClient.RootElement.AccessibleName = null;
            this.drpClient.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 125, 20);
            this.drpClient.RootElement.StretchVertically = true;
            this.drpClient.Size = new System.Drawing.Size(221, 20);
            this.drpClient.TabIndex = 75;
            this.drpClient.Text = "radDropDownList1";
            this.drpClient.SelectedIndexChanged += new Telerik.WinControls.UI.Data.PositionChangedEventHandler(this.drpClient_SelectedIndexChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(3, 8);
            this.label11.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(36, 13);
            this.label11.TabIndex = 76;
            this.label11.Text = "Client:";
            // 
            // btnDelete
            // 
            this.btnDelete.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnDelete.Location = new System.Drawing.Point(192, 31);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            // 
            // 
            // 
            this.btnDelete.RootElement.AccessibleDescription = null;
            this.btnDelete.RootElement.AccessibleName = null;
            this.btnDelete.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnDelete.Size = new System.Drawing.Size(70, 22);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnUpdate
            // 
            this.btnUpdate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnUpdate.Location = new System.Drawing.Point(118, 31);
            this.btnUpdate.Margin = new System.Windows.Forms.Padding(2);
            this.btnUpdate.Name = "btnUpdate";
            // 
            // 
            // 
            this.btnUpdate.RootElement.AccessibleDescription = null;
            this.btnUpdate.RootElement.AccessibleName = null;
            this.btnUpdate.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnUpdate.Size = new System.Drawing.Size(70, 22);
            this.btnUpdate.TabIndex = 1;
            this.btnUpdate.Text = "Update";
            this.btnUpdate.Click += new System.EventHandler(this.btnUpdate_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnAdd.Location = new System.Drawing.Point(44, 31);
            this.btnAdd.Margin = new System.Windows.Forms.Padding(2);
            this.btnAdd.Name = "btnAdd";
            // 
            // 
            // 
            this.btnAdd.RootElement.AccessibleDescription = null;
            this.btnAdd.RootElement.AccessibleName = null;
            this.btnAdd.RootElement.ControlBounds = new System.Drawing.Rectangle(0, 0, 110, 24);
            this.btnAdd.Size = new System.Drawing.Size(70, 22);
            this.btnAdd.TabIndex = 0;
            this.btnAdd.Text = "Add";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // UcAccountGroupList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcAccountGroupList";
            this.Size = new System.Drawing.Size(840, 374);
            this.Load += new System.EventHandler(this.UcTaskList_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpYearEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.drpClient)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnUpdate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAdd)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private Telerik.WinControls.UI.RadButton btnUpdate;
        private Telerik.WinControls.UI.RadButton btnAdd;
        private Telerik.WinControls.UI.RadButton btnFilter;
        private Telerik.WinControls.UI.RadDropDownList drpYearEnd;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadDropDownList drpClient;
        private System.Windows.Forms.Label label11;

    }
}
