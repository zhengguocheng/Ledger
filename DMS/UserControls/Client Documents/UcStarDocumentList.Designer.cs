namespace DMS.UserControls
{
    partial class UcStarDocumentList
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
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn2 = new Telerik.WinControls.UI.GridViewImageColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdSplitter = new System.Windows.Forms.SplitContainer();
            this.grdDocuments = new Telerik.WinControls.UI.RadGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkPreview = new Telerik.WinControls.UI.RadCheckBox();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSplitter)).BeginInit();
            this.grdSplitter.Panel1.SuspendLayout();
            this.grdSplitter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPreview)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdSplitter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 31);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(846, 378);
            this.panel1.TabIndex = 4;
            // 
            // grdSplitter
            // 
            this.grdSplitter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSplitter.Location = new System.Drawing.Point(0, 0);
            this.grdSplitter.Name = "grdSplitter";
            this.grdSplitter.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // grdSplitter.Panel1
            // 
            this.grdSplitter.Panel1.Controls.Add(this.grdDocuments);
            this.grdSplitter.Size = new System.Drawing.Size(846, 378);
            this.grdSplitter.SplitterDistance = 191;
            this.grdSplitter.TabIndex = 3;
            // 
            // grdDocuments
            // 
            this.grdDocuments.AllowDrop = true;
            this.grdDocuments.AutoScroll = true;
            this.grdDocuments.BackColor = System.Drawing.SystemColors.Control;
            this.grdDocuments.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDocuments.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.grdDocuments.ForeColor = System.Drawing.Color.Black;
            this.grdDocuments.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.grdDocuments.Location = new System.Drawing.Point(0, 0);
            this.grdDocuments.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdDocuments
            // 
            this.grdDocuments.MasterTemplate.AllowAddNewRow = false;
            this.grdDocuments.MasterTemplate.AllowCellContextMenu = false;
            this.grdDocuments.MasterTemplate.AllowDeleteRow = false;
            this.grdDocuments.MasterTemplate.AllowDragToGroup = false;
            this.grdDocuments.MasterTemplate.AutoGenerateColumns = false;
            this.grdDocuments.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewImageColumn1.AllowFiltering = false;
            gridViewImageColumn1.AllowHide = false;
            gridViewImageColumn1.AllowResize = false;
            gridViewImageColumn1.FieldName = "StarImg";
            gridViewImageColumn1.FormatString = "";
            gridViewImageColumn1.HeaderImage = global::DMS.Properties.Resources.blackStar;
            gridViewImageColumn1.HeaderText = "";
            gridViewImageColumn1.MaxWidth = 30;
            gridViewImageColumn1.MinWidth = 30;
            gridViewImageColumn1.Name = "colStar";
            gridViewImageColumn1.StretchVertically = false;
            gridViewImageColumn1.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewImageColumn1.Width = 30;
            gridViewImageColumn2.AllowFiltering = false;
            gridViewImageColumn2.AllowHide = false;
            gridViewImageColumn2.AllowResize = false;
            gridViewImageColumn2.AllowSort = false;
            gridViewImageColumn2.AutoEllipsis = false;
            gridViewImageColumn2.FieldName = "DocImage";
            gridViewImageColumn2.FormatString = "";
            gridViewImageColumn2.MaxWidth = 30;
            gridViewImageColumn2.MinWidth = 30;
            gridViewImageColumn2.Name = "colImg";
            gridViewImageColumn2.StretchVertically = false;
            gridViewImageColumn2.TextAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            gridViewImageColumn2.Width = 30;
            gridViewTextBoxColumn1.FieldName = "Name";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Name";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.MaxWidth = 300;
            gridViewTextBoxColumn1.MinWidth = 50;
            gridViewTextBoxColumn1.Name = "colName";
            gridViewTextBoxColumn1.Width = 183;
            gridViewTextBoxColumn2.FieldName = "Client_Name";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Client";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.MaxWidth = 300;
            gridViewTextBoxColumn2.MinWidth = 50;
            gridViewTextBoxColumn2.Name = "colClientName";
            gridViewTextBoxColumn2.ReadOnly = true;
            gridViewTextBoxColumn2.Width = 187;
            gridViewTextBoxColumn3.FieldName = "Notes";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "Notes";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.ImageLayout = System.Windows.Forms.ImageLayout.None;
            gridViewTextBoxColumn3.MaxWidth = 200;
            gridViewTextBoxColumn3.MinWidth = 200;
            gridViewTextBoxColumn3.Name = "colNotes";
            gridViewTextBoxColumn3.ReadOnly = true;
            gridViewTextBoxColumn3.Width = 200;
            gridViewTextBoxColumn4.FieldName = "UpdatedAt";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "Updated";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.MaxWidth = 200;
            gridViewTextBoxColumn4.MinWidth = 200;
            gridViewTextBoxColumn4.Name = "colUpdatedAt";
            gridViewTextBoxColumn4.ReadOnly = true;
            gridViewTextBoxColumn4.Width = 200;
            this.grdDocuments.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewImageColumn1,
            gridViewImageColumn2,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4});
            this.grdDocuments.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdDocuments.MasterTemplate.EnableFiltering = true;
            this.grdDocuments.MasterTemplate.EnableGrouping = false;
            this.grdDocuments.MasterTemplate.HorizontalScrollState = Telerik.WinControls.UI.ScrollState.AlwaysShow;
            this.grdDocuments.Name = "grdDocuments";
            this.grdDocuments.ReadOnly = true;
            this.grdDocuments.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // 
            // 
            this.grdDocuments.RootElement.ForeColor = System.Drawing.Color.Black;
            this.grdDocuments.ShowGroupPanel = false;
            this.grdDocuments.Size = new System.Drawing.Size(846, 191);
            this.grdDocuments.TabIndex = 2;
            this.grdDocuments.Text = "radGridView1";
            this.grdDocuments.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.grdDocuments_CurrentRowChanged);
            this.grdDocuments.CellClick += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdDocuments_CellClick);
            this.grdDocuments.DoubleClick += new System.EventHandler(this.grdDocuments_DoubleClick);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkPreview);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(846, 31);
            this.panel2.TabIndex = 6;
            // 
            // chkPreview
            // 
            this.chkPreview.Location = new System.Drawing.Point(3, 3);
            this.chkPreview.Name = "chkPreview";
            this.chkPreview.Size = new System.Drawing.Size(59, 18);
            this.chkPreview.TabIndex = 5;
            this.chkPreview.Text = "Preview";
            this.chkPreview.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkPreview_ToggleStateChanged);
            // 
            // UcStarDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Name = "UcStarDocumentList";
            this.Size = new System.Drawing.Size(846, 409);
            this.Load += new System.EventHandler(this.UcStarDocumentList_Load);
            this.panel1.ResumeLayout(false);
            this.grdSplitter.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSplitter)).EndInit();
            this.grdSplitter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkPreview)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdDocuments;
        private System.Windows.Forms.SplitContainer grdSplitter;
        private System.Windows.Forms.Panel panel1;
        private Telerik.WinControls.UI.RadCheckBox chkPreview;
        private System.Windows.Forms.Panel panel2;
    }
}
