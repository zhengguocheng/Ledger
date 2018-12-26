namespace DMS.Reports
{
    partial class UcRptThisYrClosingTrialBal
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
            this.pnlRpt = new System.Windows.Forms.Panel();
            this.grdItems = new Telerik.WinControls.UI.RadGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnRefresh = new Telerik.WinControls.UI.RadButton();
            this.txtLastYr = new Telerik.WinControls.UI.RadTextBox();
            this.lblLastYr = new Telerik.WinControls.UI.RadLabel();
            this.txtThisYr = new Telerik.WinControls.UI.RadTextBox();
            this.lblThisYr = new Telerik.WinControls.UI.RadLabel();
            this.rdGrid = new Telerik.WinControls.UI.RadRadioButton();
            this.rdReport = new Telerik.WinControls.UI.RadRadioButton();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.pnlRpt.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastYr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastYr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThisYr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblThisYr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlRpt
            // 
            this.pnlRpt.Controls.Add(this.grdItems);
            this.pnlRpt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRpt.Location = new System.Drawing.Point(0, 62);
            this.pnlRpt.Name = "pnlRpt";
            this.pnlRpt.Size = new System.Drawing.Size(901, 398);
            this.pnlRpt.TabIndex = 5;
            // 
            // grdItems
            // 
            this.grdItems.AutoScroll = true;
            this.grdItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdItems.EnableHotTracking = false;
            this.grdItems.HideSelection = true;
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
            this.grdItems.MasterTemplate.EnableGrouping = false;
            this.grdItems.MasterTemplate.ShowFilteringRow = false;
            this.grdItems.Name = "grdItems";
            this.grdItems.ShowGroupPanel = false;
            this.grdItems.Size = new System.Drawing.Size(901, 398);
            this.grdItems.TabIndex = 2;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnRefresh);
            this.panel1.Controls.Add(this.txtLastYr);
            this.panel1.Controls.Add(this.lblLastYr);
            this.panel1.Controls.Add(this.txtThisYr);
            this.panel1.Controls.Add(this.lblThisYr);
            this.panel1.Controls.Add(this.rdGrid);
            this.panel1.Controls.Add(this.rdReport);
            this.panel1.Controls.Add(this.btnBack);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(901, 62);
            this.panel1.TabIndex = 4;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(72, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(63, 22);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // txtLastYr
            // 
            this.txtLastYr.Location = new System.Drawing.Point(476, 28);
            this.txtLastYr.Name = "txtLastYr";
            this.txtLastYr.ReadOnly = true;
            this.txtLastYr.Size = new System.Drawing.Size(181, 20);
            this.txtLastYr.TabIndex = 10;
            // 
            // lblLastYr
            // 
            this.lblLastYr.Location = new System.Drawing.Point(353, 29);
            this.lblLastYr.Name = "lblLastYr";
            this.lblLastYr.Size = new System.Drawing.Size(117, 18);
            this.lblLastYr.TabIndex = 9;
            this.lblLastYr.Text = "2015 Net Profit / Loss:";
            // 
            // txtThisYr
            // 
            this.txtThisYr.Location = new System.Drawing.Point(123, 28);
            this.txtThisYr.Name = "txtThisYr";
            this.txtThisYr.ReadOnly = true;
            this.txtThisYr.Size = new System.Drawing.Size(181, 20);
            this.txtThisYr.TabIndex = 8;
            // 
            // lblThisYr
            // 
            this.lblThisYr.Location = new System.Drawing.Point(3, 29);
            this.lblThisYr.Name = "lblThisYr";
            this.lblThisYr.Size = new System.Drawing.Size(117, 18);
            this.lblThisYr.TabIndex = 7;
            this.lblThisYr.Text = "2015 Net Profit / Loss:";
            // 
            // rdGrid
            // 
            this.rdGrid.Location = new System.Drawing.Point(212, 7);
            this.rdGrid.Name = "rdGrid";
            this.rdGrid.Size = new System.Drawing.Size(41, 18);
            this.rdGrid.TabIndex = 5;
            this.rdGrid.Text = "Grid";
            this.rdGrid.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdReport_ToggleStateChanged);
            // 
            // rdReport
            // 
            this.rdReport.Location = new System.Drawing.Point(141, 7);
            this.rdReport.Name = "rdReport";
            this.rdReport.Size = new System.Drawing.Size(54, 18);
            this.rdReport.TabIndex = 4;
            this.rdReport.Text = "Report";
            this.rdReport.ToggleStateChanged += new Telerik.WinControls.UI.StateChangedEventHandler(this.rdReport_ToggleStateChanged);
            // 
            // btnBack
            // 
            this.btnBack.Location = new System.Drawing.Point(3, 3);
            this.btnBack.Name = "btnBack";
            this.btnBack.Size = new System.Drawing.Size(63, 22);
            this.btnBack.TabIndex = 3;
            this.btnBack.Text = "Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // UcRptThisYrClosingTrialBal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pnlRpt);
            this.Controls.Add(this.panel1);
            this.Name = "UcRptThisYrClosingTrialBal";
            this.Size = new System.Drawing.Size(901, 460);
            this.Load += new System.EventHandler(this.UcRptPaymentSummary_Load);
            this.pnlRpt.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdItems.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdItems)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnRefresh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLastYr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLastYr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtThisYr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblThisYr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadButton btnBack;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel pnlRpt;
        private Telerik.WinControls.UI.RadGridView grdItems;
        private Telerik.WinControls.UI.RadRadioButton rdGrid;
        private Telerik.WinControls.UI.RadRadioButton rdReport;
        private Telerik.WinControls.UI.RadTextBox txtLastYr;
        private Telerik.WinControls.UI.RadLabel lblLastYr;
        private Telerik.WinControls.UI.RadTextBox txtThisYr;
        private Telerik.WinControls.UI.RadLabel lblThisYr;
        private Telerik.WinControls.UI.RadButton btnRefresh;

    }
}
