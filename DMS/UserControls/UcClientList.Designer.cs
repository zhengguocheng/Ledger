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
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn1 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn2 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.grdClients = new Telerik.WinControls.UI.RadGridView();
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).BeginInit();
            this.SuspendLayout();
            // 
            // grdClients
            // 
            this.grdClients.AutoScroll = true;
            this.grdClients.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdClients.Location = new System.Drawing.Point(0, 0);
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
            gridViewTextBoxColumn1.HeaderText = "ID";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "ID";
            gridViewTextBoxColumn1.Width = 56;
            gridViewTextBoxColumn2.FieldName = "Client_Name";
            gridViewTextBoxColumn2.HeaderText = "Name";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "Client_Name";
            gridViewTextBoxColumn2.Width = 186;
            gridViewTextBoxColumn3.FieldName = "Reference";
            gridViewTextBoxColumn3.HeaderText = "Reference";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.Name = "Reference";
            gridViewTextBoxColumn3.Width = 139;
            gridViewTextBoxColumn4.FieldName = "UTR";
            gridViewTextBoxColumn4.HeaderText = "UTR";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.Name = "UTR";
            gridViewTextBoxColumn4.Width = 171;
            gridViewTextBoxColumn5.FieldName = "Post_Code";
            gridViewTextBoxColumn5.HeaderText = "Post Code";
            gridViewTextBoxColumn5.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn5.Name = "Post_Code";
            gridViewTextBoxColumn5.Width = 139;
            gridViewTextBoxColumn6.FieldName = "National_Insurance";
            gridViewTextBoxColumn6.HeaderText = "National Insurance";
            gridViewTextBoxColumn6.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn6.Name = "National_Insurance";
            gridViewTextBoxColumn6.Width = 139;
            this.grdClients.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.grdClients.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdClients.MasterTemplate.EnableGrouping = false;
            this.grdClients.Name = "grdClients";
            this.grdClients.ReadOnly = true;
            this.grdClients.ShowGroupPanel = false;
            this.grdClients.Size = new System.Drawing.Size(846, 286);
            this.grdClients.TabIndex = 1;
            this.grdClients.Text = "radGridView1";
            this.grdClients.SelectionChanged += new System.EventHandler(this.grdClients_SelectionChanged);
            // 
            // UcClientList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grdClients);
            this.Name = "UcClientList";
            this.Size = new System.Drawing.Size(846, 286);
            this.Load += new System.EventHandler(this.UcClientList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.grdClients)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdClients;
    }
}
