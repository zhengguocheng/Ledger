namespace DMS.UserControls
{
    partial class UcDocumentList
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
            Telerik.WinControls.UI.GridViewImageColumn gridViewImageColumn3 = new Telerik.WinControls.UI.GridViewImageColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn15 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn16 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn17 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn18 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn19 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn20 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn21 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.grdDocuments = new Telerik.WinControls.UI.RadGridView();
            this.radPanel1 = new Telerik.WinControls.UI.RadPanel();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.radButton1 = new Telerik.WinControls.UI.RadButton();
            this.btnAddFile = new Telerik.WinControls.UI.RadButton();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            this.btnDownload = new Telerik.WinControls.UI.RadButton();
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).BeginInit();
            this.radPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddFile)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownload)).BeginInit();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Title = "Open File";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.Title = "Save File";
            // 
            // grdDocuments
            // 
            this.grdDocuments.AutoScroll = true;
            this.grdDocuments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdDocuments.Location = new System.Drawing.Point(0, 32);
            // 
            // grdDocuments
            // 
            this.grdDocuments.MasterTemplate.AllowAddNewRow = false;
            this.grdDocuments.MasterTemplate.AllowCellContextMenu = false;
            this.grdDocuments.MasterTemplate.AllowDeleteRow = false;
            this.grdDocuments.MasterTemplate.AllowDragToGroup = false;
            this.grdDocuments.MasterTemplate.AllowEditRow = false;
            this.grdDocuments.MasterTemplate.AutoGenerateColumns = false;
            this.grdDocuments.MasterTemplate.AutoSizeColumnsMode = Telerik.WinControls.UI.GridViewAutoSizeColumnsMode.Fill;
            gridViewImageColumn3.FieldName = "DocImg";
            gridViewImageColumn3.HeaderText = "";
            gridViewImageColumn3.Name = "DocImg";
            gridViewImageColumn3.Width = 31;
            gridViewTextBoxColumn15.FieldName = "Name";
            gridViewTextBoxColumn15.HeaderText = "Name";
            gridViewTextBoxColumn15.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn15.Name = "Name";
            gridViewTextBoxColumn15.Width = 156;
            gridViewTextBoxColumn16.FieldName = "Notes";
            gridViewTextBoxColumn16.HeaderText = "Notes";
            gridViewTextBoxColumn16.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn16.Name = "Notes";
            gridViewTextBoxColumn16.Width = 106;
            gridViewTextBoxColumn17.FieldName = "Client_Name";
            gridViewTextBoxColumn17.HeaderText = "Client Name";
            gridViewTextBoxColumn17.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn17.Name = "Client_Name";
            gridViewTextBoxColumn17.Width = 106;
            gridViewTextBoxColumn18.FieldName = "CreatedByName";
            gridViewTextBoxColumn18.HeaderText = "Created By";
            gridViewTextBoxColumn18.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn18.Name = "CreatedByName";
            gridViewTextBoxColumn18.Width = 106;
            gridViewTextBoxColumn19.FieldName = "CreatedAT";
            gridViewTextBoxColumn19.HeaderText = "Created AT";
            gridViewTextBoxColumn19.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn19.Name = "CreatedAT";
            gridViewTextBoxColumn19.Width = 106;
            gridViewTextBoxColumn20.FieldName = "UpdatedByName";
            gridViewTextBoxColumn20.HeaderText = "Updated By";
            gridViewTextBoxColumn20.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn20.Name = "UpdatedByName";
            gridViewTextBoxColumn20.Width = 106;
            gridViewTextBoxColumn21.FieldName = "UpdatedAt";
            gridViewTextBoxColumn21.HeaderText = "Updated At";
            gridViewTextBoxColumn21.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn21.Name = "UpdatedAt";
            gridViewTextBoxColumn21.Width = 72;
            this.grdDocuments.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewImageColumn3,
            gridViewTextBoxColumn15,
            gridViewTextBoxColumn16,
            gridViewTextBoxColumn17,
            gridViewTextBoxColumn18,
            gridViewTextBoxColumn19,
            gridViewTextBoxColumn20,
            gridViewTextBoxColumn21});
            this.grdDocuments.MasterTemplate.EnableGrouping = false;
            this.grdDocuments.Name = "grdDocuments";
            this.grdDocuments.ReadOnly = true;
            this.grdDocuments.ShowGroupPanel = false;
            this.grdDocuments.Size = new System.Drawing.Size(803, 406);
            this.grdDocuments.TabIndex = 0;
            this.grdDocuments.Text = "radGridView1";
            this.grdDocuments.DoubleClick += new System.EventHandler(this.grdDocuments_DoubleClick);
            // 
            // radPanel1
            // 
            this.radPanel1.BackColor = System.Drawing.Color.Transparent;
            this.radPanel1.Controls.Add(this.btnBack);
            this.radPanel1.Controls.Add(this.radButton1);
            this.radPanel1.Controls.Add(this.btnAddFile);
            this.radPanel1.Controls.Add(this.btnDelete);
            this.radPanel1.Controls.Add(this.btnDownload);
            this.radPanel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.radPanel1.Location = new System.Drawing.Point(0, 0);
            this.radPanel1.Name = "radPanel1";
            this.radPanel1.Size = new System.Drawing.Size(803, 32);
            this.radPanel1.TabIndex = 4;
            ((Telerik.WinControls.Primitives.BorderPrimitive)(this.radPanel1.GetChildAt(0).GetChildAt(1))).Visibility = Telerik.WinControls.ElementVisibility.Hidden;
            // 
            // btnBack
            // 
            this.btnBack.ImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
            this.btnBack.Location = new System.Drawing.Point(344, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(80, 25);
            this.btnBack.TabIndex = 4;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // radButton1
            // 
            this.radButton1.Location = new System.Drawing.Point(86, 3);
            this.radButton1.Name = "radButton1";
            this.radButton1.Size = new System.Drawing.Size(80, 25);
            this.radButton1.TabIndex = 4;
            this.radButton1.Text = "Add Folder";
            this.radButton1.Click += new System.EventHandler(this.btnAddFolder_Click);
            // 
            // btnAddFile
            // 
            this.btnAddFile.Location = new System.Drawing.Point(2, 3);
            this.btnAddFile.Name = "btnAddFile";
            this.btnAddFile.Size = new System.Drawing.Size(80, 25);
            this.btnAddFile.TabIndex = 1;
            this.btnAddFile.Text = "Add File";
            this.btnAddFile.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(258, 3);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(80, 25);
            this.btnDelete.TabIndex = 3;
            this.btnDelete.Text = "Delete";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // btnDownload
            // 
            this.btnDownload.Location = new System.Drawing.Point(172, 3);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(80, 25);
            this.btnDownload.TabIndex = 2;
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // UcDocumentList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdDocuments);
            this.Controls.Add(this.radPanel1);
            this.Name = "UcDocumentList";
            this.Size = new System.Drawing.Size(803, 438);
            this.Load += new System.EventHandler(this.UcClientDocuments_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdDocuments)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radPanel1)).EndInit();
            this.radPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.radButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnAddFile)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDownload)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdDocuments;
        private Telerik.WinControls.UI.RadButton btnAddFile;
        private Telerik.WinControls.UI.RadButton btnDownload;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private Telerik.WinControls.UI.RadPanel radPanel1;
        private Telerik.WinControls.UI.RadButton radButton1;
        private Telerik.WinControls.UI.RadButton btnBack;
    }
}
