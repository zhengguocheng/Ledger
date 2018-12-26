namespace DMS
{
    partial class UcRecycleBin
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
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn1 = new Telerik.WinControls.UI.GridViewImageColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.grdUsers = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cbShowFilter = new Telerik.WinControls.UI.RadCheckBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRestore = new Telerik.WinControls.UI.RadButton();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // grdUsers
            // 
            this.grdUsers.AutoScroll = true;
            this.grdUsers.BackColor = System.Drawing.SystemColors.Control;
            this.grdUsers.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdUsers.ForeColor = System.Drawing.Color.Black;
            this.grdUsers.Location = new System.Drawing.Point(0, 56);
            this.grdUsers.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdUsers
            // 
            this.grdUsers.MasterTemplate.AllowAddNewRow = false;
            this.grdUsers.MasterTemplate.AllowCellContextMenu = false;
            this.grdUsers.MasterTemplate.AllowDeleteRow = false;
            this.grdUsers.MasterTemplate.AllowDragToGroup = false;
            this.grdUsers.MasterTemplate.AllowEditRow = false;
            this.grdUsers.MasterTemplate.AutoGenerateColumns = false;
            this.grdUsers.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewImageColumn1.AllowFiltering = false;
            gridViewImageColumn1.AllowHide = false;
            gridViewImageColumn1.AllowResize = false;
            gridViewImageColumn1.AllowSort = false;
            gridViewImageColumn1.FieldName = "DocImage";
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.Name = "colImg";
            gridViewImageColumn1.Width = 31;
            gridViewTextBoxColumn1.FieldName = "Name";
            gridViewTextBoxColumn1.HeaderText = "Name";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "colName";
            gridViewTextBoxColumn1.Width = 262;
            gridViewTextBoxColumn2.FieldName = "Path";
            gridViewTextBoxColumn2.HeaderText = "Path";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "colPath";
            gridViewTextBoxColumn2.Width = 330;
            gridViewTextBoxColumn3.FieldName = "UpdatedAt";
            gridViewTextBoxColumn3.HeaderText = "Delete Date";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.Name = "colUpdatedAt";
            gridViewTextBoxColumn3.Width = 236;
            this.grdUsers.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewImageColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3});
            this.grdUsers.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdUsers.MasterTemplate.EnableFiltering = true;
            this.grdUsers.MasterTemplate.EnableGrouping = false;
            this.grdUsers.MasterTemplate.ShowFilteringRow = false;
            this.grdUsers.Name = "grdUsers";
            this.grdUsers.ReadOnly = true;
            this.grdUsers.ShowGroupPanel = false;
            this.grdUsers.Size = new System.Drawing.Size(877, 310);
            this.grdUsers.TabIndex = 4;
            this.grdUsers.Text = "radGridView1";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.cbShowFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(877, 24);
            this.panel1.TabIndex = 6;
            // 
            // cbShowFilter
            // 
            this.cbShowFilter.Location = new System.Drawing.Point(3, 2);
            this.cbShowFilter.Margin = new System.Windows.Forms.Padding(2);
            this.cbShowFilter.Name = "cbShowFilter";
            this.cbShowFilter.Size = new System.Drawing.Size(75, 18);
            this.cbShowFilter.TabIndex = 0;
            this.cbShowFilter.Text = "Show Filter";
            this.cbShowFilter.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.cbShowFilter_ToggleStateChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.btnRestore);
            this.flowLayoutPanel1.Controls.Add(this.btnDelete);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(877, 32);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // btnRestore
            // 
            this.btnRestore.Location = new System.Drawing.Point(3, 3);
            this.btnRestore.Name = "btnRestore";
            this.btnRestore.Size = new System.Drawing.Size(68, 24);
            this.btnRestore.TabIndex = 0;
            this.btnRestore.Text = "Restore";
            this.btnRestore.Click += new System.EventHandler(this.btnRestore_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(77, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(68, 24);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UcRecycleBin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdUsers);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcRecycleBin";
            this.Size = new System.Drawing.Size(877, 366);
            this.Load += new System.EventHandler(this.UcUserList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdUsers)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbShowFilter)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnRestore)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdUsers;
        private Telerik.WinControls.UI.RadCheckBox cbShowFilter;
        private Telerik.WinControls.UI.RadButton btnRestore;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private System.Windows.Forms.Panel panel1;
    }
}
