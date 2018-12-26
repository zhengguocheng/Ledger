﻿namespace DMS
{
    partial class UcPettyCash
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
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn1 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn3 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn4 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewComboBoxColumn gridViewComboBoxColumn2 = new Telerik.WinControls.UI.GridViewComboBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn5 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            Telerik.WinControls.UI.GridViewTextBoxColumn gridViewTextBoxColumn6 = new Telerik.WinControls.UI.GridViewTextBoxColumn();
            this.panel1 = new System.Windows.Forms.Panel();
            this.grdItems = new Telerik.WinControls.UI.RadGridView();
            this.panel3 = new System.Windows.Forms.Panel();
            this.btnGoBack = new Telerik.WinControls.UI.RadButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtNet = new Telerik.WinControls.UI.RadTextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtVAT = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtTotalGross = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.chkVAT = new Telerik.WinControls.UI.RadCheckBox();
            this.btnDelete = new Telerik.WinControls.UI.RadButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).BeginInit();
            this.panel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnGoBack)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalGross)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVAT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.grdItems);
            this.panel1.Controls.Add(this.panel3);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 32);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1127, 377);
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
            gridViewDateTimeColumn1.Width = 67;
            gridViewTextBoxColumn1.FieldName = "Details";
            gridViewTextBoxColumn1.FormatString = "";
            gridViewTextBoxColumn1.HeaderText = "Description";
            gridViewTextBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn1.Name = "colDetails";
            gridViewTextBoxColumn1.Width = 189;
            gridViewTextBoxColumn2.FieldName = "GrossTaking";
            gridViewTextBoxColumn2.FormatString = "";
            gridViewTextBoxColumn2.HeaderText = "Gross";
            gridViewTextBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn2.Name = "colGrossTaking";
            gridViewTextBoxColumn2.Width = 108;
            gridViewComboBoxColumn1.DisplayMember = null;
            gridViewComboBoxColumn1.FieldName = "VATRateID";
            gridViewComboBoxColumn1.FormatString = "";
            gridViewComboBoxColumn1.HeaderText = "VAT (%)";
            gridViewComboBoxColumn1.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewComboBoxColumn1.MinWidth = 100;
            gridViewComboBoxColumn1.Name = "colDrpVATRateID";
            gridViewComboBoxColumn1.ValueMember = null;
            gridViewComboBoxColumn1.Width = 153;
            gridViewTextBoxColumn3.FieldName = "VAT";
            gridViewTextBoxColumn3.FormatString = "";
            gridViewTextBoxColumn3.HeaderText = "VAT (Calculated)";
            gridViewTextBoxColumn3.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn3.MinWidth = 100;
            gridViewTextBoxColumn3.Name = "colVAT";
            gridViewTextBoxColumn3.Width = 166;
            gridViewTextBoxColumn4.FieldName = "NetTaking";
            gridViewTextBoxColumn4.FormatString = "";
            gridViewTextBoxColumn4.HeaderText = "Net";
            gridViewTextBoxColumn4.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn4.MinWidth = 100;
            gridViewTextBoxColumn4.Name = "colNetTaking";
            gridViewTextBoxColumn4.Width = 118;
            gridViewComboBoxColumn2.DisplayMember = null;
            gridViewComboBoxColumn2.FieldName = "AnalysisCodeID";
            gridViewComboBoxColumn2.HeaderText = "Analysis";
            gridViewComboBoxColumn2.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewComboBoxColumn2.MinWidth = 100;
            gridViewComboBoxColumn2.Name = "colAnalysisCodeID";
            gridViewComboBoxColumn2.ValueMember = null;
            gridViewComboBoxColumn2.Width = 100;
            gridViewTextBoxColumn5.FieldName = "Notes";
            gridViewTextBoxColumn5.FormatString = "";
            gridViewTextBoxColumn5.HeaderText = "Notes";
            gridViewTextBoxColumn5.HeaderTextAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            gridViewTextBoxColumn5.Name = "colNotes";
            gridViewTextBoxColumn5.Width = 212;
            gridViewTextBoxColumn6.FieldName = "ID";
            gridViewTextBoxColumn6.FormatString = "";
            gridViewTextBoxColumn6.HeaderText = "ID";
            gridViewTextBoxColumn6.IsVisible = false;
            gridViewTextBoxColumn6.Name = "colID";
            gridViewTextBoxColumn6.Width = 32;
            this.grdItems.MasterTemplate.Columns.AddRange(new Telerik.WinControls.UI.GridViewDataColumn[] {
            gridViewDateTimeColumn1,
            gridViewTextBoxColumn1,
            gridViewTextBoxColumn2,
            gridViewComboBoxColumn1,
            gridViewTextBoxColumn3,
            gridViewTextBoxColumn4,
            gridViewComboBoxColumn2,
            gridViewTextBoxColumn5,
            gridViewTextBoxColumn6});
            this.grdItems.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdItems.MasterTemplate.EnableGrouping = false;
            this.grdItems.Name = "grdItems";
            this.grdItems.ShowGroupPanel = false;
            this.grdItems.Size = new System.Drawing.Size(1127, 290);
            this.grdItems.TabIndex = 1;
            this.grdItems.CellEndEdit += new Telerik.WinControls.UI.GridViewCellEventHandler(this.grdItems_CellEndEdit);
            this.grdItems.CellValidating += new Telerik.WinControls.UI.CellValidatingEventHandler(this.grdItems_CellValidating);
            this.grdItems.CurrentRowChanged += new Telerik.WinControls.UI.CurrentRowChangedEventHandler(this.grdItems_CurrentRowChanged);
            this.grdItems.UserDeletingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.grdItems_UserDeletingRow);
            this.grdItems.DefaultValuesNeeded += new Telerik.WinControls.UI.GridViewRowEventHandler(this.grdItems_DefaultValuesNeeded);
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.btnGoBack);
            this.panel3.Controls.Add(this.groupBox1);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel3.Location = new System.Drawing.Point(0, 290);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(1127, 87);
            this.panel3.TabIndex = 0;
            // 
            // btnGoBack
            // 
            this.btnGoBack.Location = new System.Drawing.Point(2, 55);
            this.btnGoBack.Margin = new System.Windows.Forms.Padding(2);
            this.btnGoBack.Name = "btnGoBack";
            this.btnGoBack.Size = new System.Drawing.Size(74, 22);
            this.btnGoBack.TabIndex = 4;
            this.btnGoBack.Text = "Go Back";
            this.btnGoBack.Click += new System.EventHandler(this.btnGoBack_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtNet);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.txtVAT);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtTotalGross);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(1127, 50);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Total";
            // 
            // txtNet
            // 
            this.txtNet.Location = new System.Drawing.Point(440, 19);
            this.txtNet.Name = "txtNet";
            this.txtNet.ReadOnly = true;
            this.txtNet.Size = new System.Drawing.Size(134, 20);
            this.txtNet.TabIndex = 5;
            this.txtNet.TabStop = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(410, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(27, 13);
            this.label3.TabIndex = 4;
            this.label3.Text = "Net:";
            // 
            // txtVAT
            // 
            this.txtVAT.Location = new System.Drawing.Point(249, 19);
            this.txtVAT.Name = "txtVAT";
            this.txtVAT.ReadOnly = true;
            this.txtVAT.Size = new System.Drawing.Size(134, 20);
            this.txtVAT.TabIndex = 3;
            this.txtVAT.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(215, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "VAT:";
            // 
            // txtTotalGross
            // 
            this.txtTotalGross.Location = new System.Drawing.Point(56, 19);
            this.txtTotalGross.Name = "txtTotalGross";
            this.txtTotalGross.ReadOnly = true;
            this.txtTotalGross.Size = new System.Drawing.Size(134, 20);
            this.txtTotalGross.TabIndex = 1;
            this.txtTotalGross.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Gross:";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.chkVAT);
            this.panel2.Controls.Add(this.btnDelete);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1127, 32);
            this.panel2.TabIndex = 3;
            // 
            // chkVAT
            // 
            this.chkVAT.Location = new System.Drawing.Point(3, 7);
            this.chkVAT.Name = "chkVAT";
            this.chkVAT.Size = new System.Drawing.Size(40, 18);
            this.chkVAT.TabIndex = 3;
            this.chkVAT.Text = "VAT";
            this.chkVAT.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.chkVAT_ToggleStateChanged);
            // 
            // btnDelete
            // 
            this.btnDelete.Location = new System.Drawing.Point(75, 6);
            this.btnDelete.Margin = new System.Windows.Forms.Padding(2);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.Size = new System.Drawing.Size(92, 22);
            this.btnDelete.TabIndex = 2;
            this.btnDelete.Text = "Delete Row";
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // UcPettyCash
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcPettyCash";
            this.Size = new System.Drawing.Size(1127, 409);
            this.Load += new System.EventHandler(this.UcTaskList_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.panel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.btnGoBack)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTotalGross)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkVAT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnDelete)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdItems;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private Telerik.WinControls.UI.RadButton btnDelete;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadTextBox txtNet;
        private System.Windows.Forms.Label label3;
        private Telerik.WinControls.UI.RadTextBox txtVAT;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtTotalGross;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel3;
        private Telerik.WinControls.UI.RadButton btnGoBack;
        private Telerik.WinControls.UI.RadCheckBox chkVAT;

    }
}
