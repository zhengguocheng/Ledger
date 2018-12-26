namespace DMS
{
    partial class UcVATReport
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing && (components != null))
        //    {
        //        components.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.sDBTableBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.vATReportSchema = new DMS.Reports.Schema.VATReportSchema();
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.grdSDB = new Telerik.WinControls.UI.RadGridView();
            this.tabPage4 = new System.Windows.Forms.TabPage();
            this.grdReceipts = new Telerik.WinControls.UI.RadGridView();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.grdPayments = new Telerik.WinControls.UI.RadGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtValInput = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.txtValOut = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.txtPayVat = new System.Windows.Forms.Label();
            this.txtInVat = new System.Windows.Forms.Label();
            this.txtOutVat = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.reportViewer2 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.tabPage10 = new System.Windows.Forms.TabPage();
            this.reportViewer3 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.panel2 = new System.Windows.Forms.Panel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnExportData = new Telerik.WinControls.UI.RadButton();
            this.chkAll = new System.Windows.Forms.CheckBox();
            this.txtEndPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.txtStPeriod = new Telerik.WinControls.UI.RadTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnReport = new Telerik.WinControls.UI.RadButton();
            this.dpEndDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.dpStDate = new Telerik.WinControls.UI.RadDateTimePicker();
            this.btnBack = new Telerik.WinControls.UI.RadButton();
            this.txtYearEnd = new Telerik.WinControls.UI.RadTextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClientName = new Telerik.WinControls.UI.RadTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.tabPage8 = new System.Windows.Forms.TabPage();
            this.tabPage9 = new System.Windows.Forms.TabPage();
            this.tabPage11 = new System.Windows.Forms.TabPage();
            this.reportViewer4 = new Microsoft.Reporting.WinForms.ReportViewer();
            ((System.ComponentModel.ISupportInitialize)(this.sDBTableBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.vATReportSchema)).BeginInit();
            this.panel1.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdSDB)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSDB.MasterTemplate)).BeginInit();
            this.tabPage4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceipts)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceipts.MasterTemplate)).BeginInit();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments.MasterTemplate)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.tabPage7.SuspendLayout();
            this.tabPage10.SuspendLayout();
            this.panel2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStPeriod)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpEndDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpStDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).BeginInit();
            this.tabPage11.SuspendLayout();
            this.SuspendLayout();
            // 
            // sDBTableBindingSource
            // 
            this.sDBTableBindingSource.DataMember = "SDBTable";
            this.sDBTableBindingSource.DataSource = this.vATReportSchema;
            // 
            // vATReportSchema
            // 
            this.vATReportSchema.DataSetName = "VATReportSchema";
            this.vATReportSchema.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabControl1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 102);
            this.panel1.Margin = new System.Windows.Forms.Padding(2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(783, 382);
            this.panel1.TabIndex = 2;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage4);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage10);
            this.tabControl1.Controls.Add(this.tabPage11);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(783, 382);
            this.tabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.grdSDB);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(775, 356);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "SDB";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // grdSDB
            // 
            this.grdSDB.AutoScroll = true;
            this.grdSDB.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grdSDB.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdSDB.EnableHotTracking = false;
            this.grdSDB.HideSelection = true;
            this.grdSDB.Location = new System.Drawing.Point(3, 3);
            this.grdSDB.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdSDB
            // 
            this.grdSDB.MasterTemplate.AllowAddNewRow = false;
            this.grdSDB.MasterTemplate.AllowCellContextMenu = false;
            this.grdSDB.MasterTemplate.AllowDragToGroup = false;
            this.grdSDB.MasterTemplate.AllowEditRow = false;
            this.grdSDB.MasterTemplate.AutoGenerateColumns = false;
            this.grdSDB.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdSDB.MasterTemplate.EnableGrouping = false;
            this.grdSDB.MasterTemplate.ShowFilteringRow = false;
            this.grdSDB.Name = "grdSDB";
            // 
            // 
            // 
            this.grdSDB.RootElement.AccessibleDescription = null;
            this.grdSDB.RootElement.AccessibleName = null;
            this.grdSDB.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 3, 240, 150);
            this.grdSDB.ShowGroupPanel = false;
            this.grdSDB.Size = new System.Drawing.Size(769, 350);
            this.grdSDB.TabIndex = 1;
            // 
            // tabPage4
            // 
            this.tabPage4.Controls.Add(this.grdReceipts);
            this.tabPage4.Location = new System.Drawing.Point(4, 22);
            this.tabPage4.Name = "tabPage4";
            this.tabPage4.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage4.Size = new System.Drawing.Size(775, 356);
            this.tabPage4.TabIndex = 3;
            this.tabPage4.Text = "Receipts";
            this.tabPage4.UseVisualStyleBackColor = true;
            // 
            // grdReceipts
            // 
            this.grdReceipts.AutoScroll = true;
            this.grdReceipts.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grdReceipts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdReceipts.EnableHotTracking = false;
            this.grdReceipts.HideSelection = true;
            this.grdReceipts.Location = new System.Drawing.Point(3, 3);
            this.grdReceipts.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdReceipts
            // 
            this.grdReceipts.MasterTemplate.AllowAddNewRow = false;
            this.grdReceipts.MasterTemplate.AllowCellContextMenu = false;
            this.grdReceipts.MasterTemplate.AllowDragToGroup = false;
            this.grdReceipts.MasterTemplate.AllowEditRow = false;
            this.grdReceipts.MasterTemplate.AutoGenerateColumns = false;
            this.grdReceipts.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdReceipts.MasterTemplate.EnableGrouping = false;
            this.grdReceipts.MasterTemplate.ShowFilteringRow = false;
            this.grdReceipts.Name = "grdReceipts";
            // 
            // 
            // 
            this.grdReceipts.RootElement.AccessibleDescription = null;
            this.grdReceipts.RootElement.AccessibleName = null;
            this.grdReceipts.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 3, 240, 150);
            this.grdReceipts.ShowGroupPanel = false;
            this.grdReceipts.Size = new System.Drawing.Size(769, 350);
            this.grdReceipts.TabIndex = 3;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.grdPayments);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(775, 356);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Payments";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // grdPayments
            // 
            this.grdPayments.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.grdPayments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdPayments.EnableHotTracking = false;
            this.grdPayments.HideSelection = true;
            this.grdPayments.Location = new System.Drawing.Point(3, 3);
            this.grdPayments.Margin = new System.Windows.Forms.Padding(2);
            // 
            // grdPayments
            // 
            this.grdPayments.MasterTemplate.AllowAddNewRow = false;
            this.grdPayments.MasterTemplate.AllowCellContextMenu = false;
            this.grdPayments.MasterTemplate.AllowDragToGroup = false;
            this.grdPayments.MasterTemplate.AllowEditRow = false;
            this.grdPayments.MasterTemplate.AutoGenerateColumns = false;
            this.grdPayments.MasterTemplate.EnableAlternatingRowColor = true;
            this.grdPayments.MasterTemplate.EnableGrouping = false;
            this.grdPayments.MasterTemplate.ShowFilteringRow = false;
            this.grdPayments.Name = "grdPayments";
            // 
            // 
            // 
            this.grdPayments.RootElement.AccessibleDescription = null;
            this.grdPayments.RootElement.AccessibleName = null;
            this.grdPayments.RootElement.ControlBounds = new System.Drawing.Rectangle(3, 3, 240, 150);
            this.grdPayments.ShowGroupPanel = false;
            this.grdPayments.Size = new System.Drawing.Size(769, 350);
            this.grdPayments.TabIndex = 2;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtValInput);
            this.tabPage3.Controls.Add(this.label11);
            this.tabPage3.Controls.Add(this.txtValOut);
            this.tabPage3.Controls.Add(this.label9);
            this.tabPage3.Controls.Add(this.label7);
            this.tabPage3.Controls.Add(this.txtPayVat);
            this.tabPage3.Controls.Add(this.txtInVat);
            this.tabPage3.Controls.Add(this.txtOutVat);
            this.tabPage3.Controls.Add(this.label6);
            this.tabPage3.Controls.Add(this.label5);
            this.tabPage3.Controls.Add(this.label3);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(775, 356);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Summary";
            this.tabPage3.UseVisualStyleBackColor = true;
            this.tabPage3.Click += new System.EventHandler(this.tabPage3_Click);
            // 
            // txtValInput
            // 
            this.txtValInput.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValInput.Location = new System.Drawing.Point(135, 190);
            this.txtValInput.Name = "txtValInput";
            this.txtValInput.Size = new System.Drawing.Size(148, 17);
            this.txtValInput.TabIndex = 16;
            this.txtValInput.Text = "___";
            this.txtValInput.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(12, 190);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(125, 17);
            this.label11.TabIndex = 15;
            this.label11.Text = "Value of Input = ";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtValOut
            // 
            this.txtValOut.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValOut.Location = new System.Drawing.Point(135, 158);
            this.txtValOut.Name = "txtValOut";
            this.txtValOut.Size = new System.Drawing.Size(148, 17);
            this.txtValOut.TabIndex = 14;
            this.txtValOut.Text = "___";
            this.txtValOut.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.Location = new System.Drawing.Point(12, 159);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(125, 17);
            this.label9.TabIndex = 13;
            this.label9.Text = "Value of Output = ";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(58, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(195, 26);
            this.label7.TabIndex = 12;
            this.label7.Text = "VAT Return Summary";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPayVat
            // 
            this.txtPayVat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPayVat.Location = new System.Drawing.Point(135, 126);
            this.txtPayVat.Name = "txtPayVat";
            this.txtPayVat.Size = new System.Drawing.Size(148, 17);
            this.txtPayVat.TabIndex = 11;
            this.txtPayVat.Text = "___";
            this.txtPayVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtInVat
            // 
            this.txtInVat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtInVat.Location = new System.Drawing.Point(135, 94);
            this.txtInVat.Name = "txtInVat";
            this.txtInVat.Size = new System.Drawing.Size(148, 17);
            this.txtInVat.TabIndex = 10;
            this.txtInVat.Text = "___";
            this.txtInVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtOutVat
            // 
            this.txtOutVat.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOutVat.Location = new System.Drawing.Point(135, 62);
            this.txtOutVat.Name = "txtOutVat";
            this.txtOutVat.Size = new System.Drawing.Size(148, 17);
            this.txtOutVat.TabIndex = 9;
            this.txtOutVat.Text = "___";
            this.txtOutVat.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(12, 94);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(125, 17);
            this.label6.TabIndex = 8;
            this.label6.Text = "Input VAT = ";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(12, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 17);
            this.label5.TabIndex = 6;
            this.label5.Text = "VAT Payable = ";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 62);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(125, 17);
            this.label3.TabIndex = 4;
            this.label3.Text = "Output VAT = ";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.reportViewer1);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage5.Size = new System.Drawing.Size(775, 356);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "SDB1";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.sDBTableBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "DMS.Reports.RDLC.SDBReport.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(3, 3);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(769, 350);
            this.reportViewer1.TabIndex = 0;
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.reportViewer2);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(775, 356);
            this.tabPage7.TabIndex = 5;
            this.tabPage7.Text = "Receipts1";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // reportViewer2
            // 
            this.reportViewer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer2.LocalReport.ReportEmbeddedResource = "DMS.Reports.RDLC.ReceiptsReport.rdlc";
            this.reportViewer2.Location = new System.Drawing.Point(0, 0);
            this.reportViewer2.Name = "reportViewer2";
            this.reportViewer2.Size = new System.Drawing.Size(775, 356);
            this.reportViewer2.TabIndex = 3;
            // 
            // tabPage10
            // 
            this.tabPage10.Controls.Add(this.reportViewer3);
            this.tabPage10.Location = new System.Drawing.Point(4, 22);
            this.tabPage10.Name = "tabPage10";
            this.tabPage10.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage10.Size = new System.Drawing.Size(775, 356);
            this.tabPage10.TabIndex = 6;
            this.tabPage10.Text = "Payment1";
            this.tabPage10.UseVisualStyleBackColor = true;
            // 
            // reportViewer3
            // 
            this.reportViewer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer3.LocalReport.ReportEmbeddedResource = "DMS.Reports.RDLC.PaymentReport.rdlc";
            this.reportViewer3.Location = new System.Drawing.Point(3, 3);
            this.reportViewer3.Name = "reportViewer3";
            this.reportViewer3.Size = new System.Drawing.Size(769, 350);
            this.reportViewer3.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.groupBox1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(2);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(783, 102);
            this.panel2.TabIndex = 3;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnExportData);
            this.groupBox1.Controls.Add(this.chkAll);
            this.groupBox1.Controls.Add(this.txtEndPeriod);
            this.groupBox1.Controls.Add(this.txtStPeriod);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.btnReport);
            this.groupBox1.Controls.Add(this.dpEndDate);
            this.groupBox1.Controls.Add(this.dpStDate);
            this.groupBox1.Controls.Add(this.btnBack);
            this.groupBox1.Controls.Add(this.txtYearEnd);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtClientName);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(783, 102);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Report Information";
            // 
            // btnExportData
            // 
            this.btnExportData.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnExportData.Location = new System.Drawing.Point(253, 73);
            this.btnExportData.Name = "btnExportData";
            // 
            // 
            // 
            this.btnExportData.RootElement.AccessibleDescription = null;
            this.btnExportData.RootElement.AccessibleName = null;
            this.btnExportData.RootElement.ControlBounds = new System.Drawing.Rectangle(253, 67, 110, 24);
            this.btnExportData.Size = new System.Drawing.Size(80, 24);
            this.btnExportData.TabIndex = 87;
            this.btnExportData.Text = "Export";
            this.btnExportData.Click += new System.EventHandler(this.btnExportData_Click);
            // 
            // chkAll
            // 
            this.chkAll.AutoSize = true;
            this.chkAll.Location = new System.Drawing.Point(425, 46);
            this.chkAll.Name = "chkAll";
            this.chkAll.Size = new System.Drawing.Size(75, 17);
            this.chkAll.TabIndex = 86;
            this.chkAll.Text = "All Periods";
            this.chkAll.UseVisualStyleBackColor = true;
            this.chkAll.CheckedChanged += new System.EventHandler(this.chkAll_CheckedChanged);
            // 
            // txtEndPeriod
            // 
            this.txtEndPeriod.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtEndPeriod.Location = new System.Drawing.Point(192, 41);
            this.txtEndPeriod.Name = "txtEndPeriod";
            this.txtEndPeriod.NullText = "End Period";
            // 
            // 
            // 
            this.txtEndPeriod.RootElement.AccessibleDescription = null;
            this.txtEndPeriod.RootElement.AccessibleName = null;
            this.txtEndPeriod.RootElement.ControlBounds = new System.Drawing.Rectangle(192, 38, 100, 20);
            this.txtEndPeriod.RootElement.StretchVertically = true;
            this.txtEndPeriod.Size = new System.Drawing.Size(114, 20);
            this.txtEndPeriod.TabIndex = 85;
            this.txtEndPeriod.TabStop = false;
            // 
            // txtStPeriod
            // 
            this.txtStPeriod.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtStPeriod.Location = new System.Drawing.Point(78, 41);
            this.txtStPeriod.Name = "txtStPeriod";
            this.txtStPeriod.NullText = "Start Period";
            // 
            // 
            // 
            this.txtStPeriod.RootElement.AccessibleDescription = null;
            this.txtStPeriod.RootElement.AccessibleName = null;
            this.txtStPeriod.RootElement.ControlBounds = new System.Drawing.Rectangle(78, 38, 100, 20);
            this.txtStPeriod.RootElement.StretchVertically = true;
            this.txtStPeriod.Size = new System.Drawing.Size(110, 20);
            this.txtStPeriod.TabIndex = 83;
            this.txtStPeriod.TabStop = false;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 46);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(73, 13);
            this.label4.TabIndex = 82;
            this.label4.Text = "Select Period:";
            // 
            // btnReport
            // 
            this.btnReport.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnReport.Location = new System.Drawing.Point(78, 72);
            this.btnReport.Name = "btnReport";
            // 
            // 
            // 
            this.btnReport.RootElement.AccessibleDescription = null;
            this.btnReport.RootElement.AccessibleName = null;
            this.btnReport.RootElement.ControlBounds = new System.Drawing.Rectangle(78, 66, 110, 24);
            this.btnReport.Size = new System.Drawing.Size(80, 24);
            this.btnReport.TabIndex = 81;
            this.btnReport.Text = "Show Report";
            this.btnReport.Click += new System.EventHandler(this.btnReport_Click);
            // 
            // dpEndDate
            // 
            this.dpEndDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dpEndDate.Location = new System.Drawing.Point(471, 72);
            this.dpEndDate.Name = "dpEndDate";
            // 
            // 
            // 
            this.dpEndDate.RootElement.AccessibleDescription = null;
            this.dpEndDate.RootElement.AccessibleName = null;
            this.dpEndDate.RootElement.ControlBounds = new System.Drawing.Rectangle(471, 66, 164, 20);
            this.dpEndDate.RootElement.StretchVertically = true;
            this.dpEndDate.Size = new System.Drawing.Size(119, 20);
            this.dpEndDate.TabIndex = 79;
            this.dpEndDate.TabStop = false;
            this.dpEndDate.Text = "03 September 2015";
            this.dpEndDate.Value = new System.DateTime(2015, 9, 3, 15, 21, 19, 443);
            this.dpEndDate.Visible = false;
            // 
            // dpStDate
            // 
            this.dpStDate.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.dpStDate.Location = new System.Drawing.Point(596, 72);
            this.dpStDate.Name = "dpStDate";
            // 
            // 
            // 
            this.dpStDate.RootElement.AccessibleDescription = null;
            this.dpStDate.RootElement.AccessibleName = null;
            this.dpStDate.RootElement.ControlBounds = new System.Drawing.Rectangle(596, 66, 164, 20);
            this.dpStDate.RootElement.StretchVertically = true;
            this.dpStDate.Size = new System.Drawing.Size(119, 20);
            this.dpStDate.TabIndex = 77;
            this.dpStDate.TabStop = false;
            this.dpStDate.Text = "03 September 2015";
            this.dpStDate.Value = new System.DateTime(2015, 9, 3, 15, 21, 19, 443);
            this.dpStDate.Visible = false;
            // 
            // btnBack
            // 
            this.btnBack.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.btnBack.Location = new System.Drawing.Point(167, 72);
            this.btnBack.Name = "btnBack";
            // 
            // 
            // 
            this.btnBack.RootElement.AccessibleDescription = null;
            this.btnBack.RootElement.AccessibleName = null;
            this.btnBack.RootElement.ControlBounds = new System.Drawing.Rectangle(167, 66, 110, 24);
            this.btnBack.Size = new System.Drawing.Size(80, 24);
            this.btnBack.TabIndex = 76;
            this.btnBack.Text = "Go Back";
            this.btnBack.Click += new System.EventHandler(this.btnBack_Click);
            // 
            // txtYearEnd
            // 
            this.txtYearEnd.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtYearEnd.Location = new System.Drawing.Point(425, 15);
            this.txtYearEnd.Name = "txtYearEnd";
            this.txtYearEnd.ReadOnly = true;
            // 
            // 
            // 
            this.txtYearEnd.RootElement.AccessibleDescription = null;
            this.txtYearEnd.RootElement.AccessibleName = null;
            this.txtYearEnd.RootElement.ControlBounds = new System.Drawing.Rectangle(425, 14, 100, 20);
            this.txtYearEnd.RootElement.StretchVertically = true;
            this.txtYearEnd.Size = new System.Drawing.Size(228, 20);
            this.txtYearEnd.TabIndex = 5;
            this.txtYearEnd.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(364, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "Year End:";
            // 
            // txtClientName
            // 
            this.txtClientName.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.txtClientName.Location = new System.Drawing.Point(78, 15);
            this.txtClientName.Name = "txtClientName";
            this.txtClientName.ReadOnly = true;
            // 
            // 
            // 
            this.txtClientName.RootElement.AccessibleDescription = null;
            this.txtClientName.RootElement.AccessibleName = null;
            this.txtClientName.RootElement.ControlBounds = new System.Drawing.Rectangle(78, 14, 100, 20);
            this.txtClientName.RootElement.StretchVertically = true;
            this.txtClientName.Size = new System.Drawing.Size(228, 20);
            this.txtClientName.TabIndex = 3;
            this.txtClientName.TabStop = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Client Name:";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Please select the folder for importing all files in it.";
            // 
            // tabPage6
            // 
            this.tabPage6.Location = new System.Drawing.Point(0, 0);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(200, 100);
            this.tabPage6.TabIndex = 0;
            // 
            // tabPage8
            // 
            this.tabPage8.Location = new System.Drawing.Point(0, 0);
            this.tabPage8.Name = "tabPage8";
            this.tabPage8.Size = new System.Drawing.Size(200, 100);
            this.tabPage8.TabIndex = 0;
            // 
            // tabPage9
            // 
            this.tabPage9.Location = new System.Drawing.Point(0, 0);
            this.tabPage9.Name = "tabPage9";
            this.tabPage9.Size = new System.Drawing.Size(200, 100);
            this.tabPage9.TabIndex = 0;
            // 
            // tabPage11
            // 
            this.tabPage11.Controls.Add(this.reportViewer4);
            this.tabPage11.Location = new System.Drawing.Point(4, 22);
            this.tabPage11.Name = "tabPage11";
            this.tabPage11.Size = new System.Drawing.Size(775, 356);
            this.tabPage11.TabIndex = 7;
            this.tabPage11.Text = "Summary1";
            this.tabPage11.UseVisualStyleBackColor = true;
            // 
            // reportViewer4
            // 
            this.reportViewer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewer4.LocalReport.ReportEmbeddedResource = "DMS.Reports.RDLC.SummaryReport.rdlc";
            this.reportViewer4.Location = new System.Drawing.Point(0, 0);
            this.reportViewer4.Name = "reportViewer4";
            this.reportViewer4.Size = new System.Drawing.Size(775, 356);
            this.reportViewer4.TabIndex = 0;
            // 
            // UcVATReport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panel2);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "UcVATReport";
            this.Size = new System.Drawing.Size(783, 484);
            this.Load += new System.EventHandler(this.UcLedgerReport_Load);
            ((System.ComponentModel.ISupportInitialize)(this.sDBTableBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.vATReportSchema)).EndInit();
            this.panel1.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdSDB.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdSDB)).EndInit();
            this.tabPage4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdReceipts.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdReceipts)).EndInit();
            this.tabPage2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments.MasterTemplate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdPayments)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage5.ResumeLayout(false);
            this.tabPage7.ResumeLayout(false);
            this.tabPage10.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.btnExportData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtEndPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtStPeriod)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnReport)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpEndDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dpStDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.btnBack)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtYearEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtClientName)).EndInit();
            this.tabPage11.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Telerik.WinControls.UI.RadGridView grdSDB;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.GroupBox groupBox1;
        private Telerik.WinControls.UI.RadTextBox txtYearEnd;
        private System.Windows.Forms.Label label2;
        private Telerik.WinControls.UI.RadTextBox txtClientName;
        private System.Windows.Forms.Label label1;
        private Telerik.WinControls.UI.RadButton btnBack;
        private Telerik.WinControls.UI.RadButton btnReport;
        private Telerik.WinControls.UI.RadDateTimePicker dpEndDate;
        private Telerik.WinControls.UI.RadDateTimePicker dpStDate;
        private System.Windows.Forms.CheckBox chkAll;
        private Telerik.WinControls.UI.RadTextBox txtEndPeriod;
        private Telerik.WinControls.UI.RadTextBox txtStPeriod;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private Telerik.WinControls.UI.RadGridView grdPayments;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label txtPayVat;
        private System.Windows.Forms.Label txtInVat;
        private System.Windows.Forms.Label txtOutVat;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TabPage tabPage4;
        private Telerik.WinControls.UI.RadGridView grdReceipts;
        private System.Windows.Forms.Label txtValInput;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label txtValOut;
        private System.Windows.Forms.Label label9;
        private Telerik.WinControls.UI.RadButton btnExportData;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.TabPage tabPage5;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource sDBTableBindingSource;
        private Reports.Schema.VATReportSchema vATReportSchema;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.TabPage tabPage7;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer2;
        private System.Windows.Forms.TabPage tabPage8;
        private System.Windows.Forms.TabPage tabPage10;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer3;
        private System.Windows.Forms.TabPage tabPage9;
        private System.Windows.Forms.TabPage tabPage11;
        private Microsoft.Reporting.WinForms.ReportViewer reportViewer4;

    }
}
