using System;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DAL;
using Telerik.WinControls.UI;
using Telerik.WinControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Diagnostics;
using System.Globalization;
using Utilities;
using Utilities.Excel;
using System.Reflection;
using Microsoft.Reporting.WinForms;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Collections.Generic;



using System.Linq;
using System.Text;
using System.Threading.Tasks;



using System.Xml;

using System.Xml.Serialization;
using NPOI.HSSF.UserModel;

namespace DMS
{
    public partial class UcVATReport : UserControlBase
    {
        Font grpTotalFont;
        long docItemID;
        VATReportController vatReportCnt;
        bool loaded = false;

        string rdlcPath = @".\Reports\RDLC\";
        string xsdPath = @".\Reports\Schema\";

        string sdbRdlcName = "SDBReport.rdlc";
        string sdbXsdName = "VATReportSchema.xsd";
        string sdbSaveRdlcName = "SDBReport1.rdlc";
        string sdbSavexsdName = "VATReportSchema1.xsd";

        string receiptsRdlcName = "ReceiptsReport.rdlc";
        string receiptsXsdName = "ReceiptsReportSchema.xsd";
        string receiptsSaveRdlcName = "ReceiptsReport1.rdlc";
        string receiptsSavexsdName = "ReceiptsReportSchema1.xsd";
        string paymentRdlcName = "PaymentReport.rdlc";
        string summaryRdlcName = "SummaryReport.rdlc";

        public UcVATReport(long _docItemID)
        {
            InitializeComponent();

            this.Caption = "VAT Report";
            docItemID = _docItemID;
            dpStDate.ApplyDMSSettings();
            dpEndDate.ApplyDMSSettings();

            vatReportCnt = new VATReportController(docItemID);
            grdSDB.CellFormatting += grdItems_CellFormatting;
            grdSDB.ViewCellFormatting += AllGrids_ViewCellFormatting;

            grdPayments.CellFormatting += grdItems_CellFormatting;
            grdPayments.RowFormatting += radGridView1_RowFormatting;
            grdPayments.ViewCellFormatting += AllGrids_ViewCellFormatting;

            grdReceipts.CellFormatting += grdItems_CellFormatting;
            grdReceipts.ViewCellFormatting += AllGrids_ViewCellFormatting;
        }

        private void UcLedgerReport_Load(object sender, EventArgs e)
        {
            this.DisableCellHighlighting(grdSDB);
            this.DisableCellHighlighting(grdPayments);
            this.DisableCellHighlighting(grdReceipts);

            grpTotalFont = new Font("Arial", 9f, FontStyle.Bold);

            vatReportCnt.LoadClientInfo();
            txtClientName.Text = vatReportCnt.ClientName;
            txtYearEnd.Text = vatReportCnt.FolderYearEnd;
            txtStPeriod.Text = "1";
            txtEndPeriod.Text = "12";
            try
            {
                var d1 = Convert.ToDateTime(txtYearEnd.Text.Trim());
                var d2 = d1.AddYears(-1).AddDays(1);

                dpStDate.Value = d2;
                dpEndDate.Value = d1;
            }
            catch { }
        }

        void LoadGridColumnsSDB(DataTable dtSDB)
        {
            grdSDB.Columns.Clear();
            grdSDB.SummaryRowsBottom.Clear();
            if (grdSDB.ColumnCount == 0)
            {
                AddColumn(grdSDB, VATReportController.col_Name, "Sales Day Books");
                AddColumn(grdSDB, VATReportController.col_Gross, "Gross", LedgerColumnType.Currency);
                AddColumn(grdSDB, VATReportController.col_SumVat, "VAT", LedgerColumnType.Currency);

                foreach (DataColumn col in dtSDB.Columns)
                {
                    if (col.ColumnName != VATReportController.col_Name && col.ColumnName != VATReportController.col_Gross
                        && col.ColumnName != VATReportController.col_SumVat && col.ColumnName != VATReportController.col_DocumentItemID)
                    {
                        AddColumn(grdSDB, col.ColumnName, col.ColumnName, LedgerColumnType.Currency);
                    }
                }
            }
        }

        void LoadGridColumnsPayment(DataTable dtPay)
        {
            grdPayments.Columns.Clear();
            grdPayments.SummaryRowsBottom.Clear();

            if (grdPayments.ColumnCount == 0)
            {
                var column = AddColumn(grdPayments, VATReportController.col_NomCode, "Nominal Code");
                column.MaxWidth = column.MinWidth = 60;
                column.WrapText = true;

                column = AddColumn(grdPayments, VATReportController.col_NomCodeDesc, "Description");
                column.MaxWidth = column.MinWidth = 180;
                column.WrapText = true;


                foreach (DataColumn col in dtPay.Columns)
                {
                    if (col.ColumnName != VATReportController.col_NomCode && col.ColumnName != VATReportController.col_NomCodeDesc
                        && col.ColumnName != VATReportController.col_DocumentItemID && col.ColumnName != VATReportController.col_PaymentSum)
                    {
                        AddColumn(grdPayments, col.ColumnName, col.ColumnName, LedgerColumnType.Currency);
                    }
                }

                AddColumn(grdPayments, VATReportController.col_PaymentSum, "Total", LedgerColumnType.Currency);

            }

            grdPayments.SummaryRowsBottom.Clear();

        }

        void LoadGridColumnsRecpt(DataTable dtRecpt)
        {
            grdReceipts.Columns.Clear();
            grdReceipts.SummaryRowsBottom.Clear();
            if (grdReceipts.ColumnCount == 0)
            {
                AddColumn(grdReceipts, VATReportController.col_SheetName, "Receipt");
                AddColumn(grdReceipts, VATReportController.col_Gross, "Gross", LedgerColumnType.Currency);
                AddColumn(grdReceipts, VATReportController.col_SumVat, "VAT", LedgerColumnType.Currency);

                foreach (DataColumn col in dtRecpt.Columns)
                {
                    if (col.ColumnName != VATReportController.col_SheetName && col.ColumnName != VATReportController.col_Gross
                        && col.ColumnName != VATReportController.col_SumVat && col.ColumnName != VATReportController.col_DocumentItemID)
                    {
                        AddColumn(grdReceipts, col.ColumnName, col.ColumnName, LedgerColumnType.Currency);
                    }
                }
            }
        }

        string GetColumnName(string src)
        {
            var colName = "col" + src;
            return colName;
        }
        GridViewDataColumn AddColumn(RadGridView grd, string source, string header, LedgerColumnType type = LedgerColumnType.None, bool isHperLinkCol = false)
        {

            GridViewDataColumn col;
            //GridViewHyperlinkColumn col;

            if (type == LedgerColumnType.Date)
            {
                col = new GridViewDateTimeColumn();

                col.DataType = typeof(DateTime);
                col.FormatString = "{0:dd/MM/yyyy}";
                //col.FormatInfo = CultureInfo.CreateSpecificCulture("en-GB");
            }
            else if (type == LedgerColumnType.Currency)
            {
                col = new GridViewDecimalColumn(source);
                col.FormatString = "{0:N2}";
                col.DataType = typeof(Decimal);
                col.TextAlignment = ContentAlignment.MiddleRight;

                AddSummary(grd, GetColumnName(source));

            }
            else
            {
                col = new GridViewTextBoxColumn();
            }

            if (isHperLinkCol)
            {
                var linkCol = new GridViewHyperlinkColumn();
                linkCol.HyperlinkOpenAction = HyperlinkOpenAction.SingleClick;
                col = linkCol;
            }

            col.HeaderText = header;
            col.Name = GetColumnName(source);
            col.MinWidth = 120;
            col.FieldName = source;

            grd.Columns.Add(col);
            return col;
        }

        int RowsOfPay = 0;
        DataSet dsExport = new DataSet();
        void LoadData()
        {
            dsExport.Clear();

            if (chkAll.Checked) 
                vatReportCnt.LoadReportData(new DateTime(1980, 1, 1), new DateTime(2030, 12, 31), 1, 12);
            else
                vatReportCnt.LoadReportData(new DateTime(1980, 1, 1), new DateTime(2030, 12, 31), AppConverter.ToInt(txtStPeriod.Text, 1), AppConverter.ToInt(txtEndPeriod.Text, 12));

            grdReceipts.AutoGenerateColumns = grdPayments.AutoGenerateColumns = grdSDB.AutoGenerateColumns = false;

            var dtSDB = vatReportCnt.GetSDB();
            LoadGridColumnsSDB(dtSDB);
            grdSDB.DataSource = dtSDB;

            int rows = grdSDB.RowCount;

            DataTable dtSDBReport = new DataTable();
            dtSDBReport = dtSDB.Copy();
           /* for(int i = 4; i  < dtSDBReport.Columns.Count;i++)
            {
                string item = "N_" + dtSDBReport.Columns[i].ToString();
                dtSDBReport.Columns[i].ColumnName = item;
            }
            */
            //SDB1
           
            
            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("paramClientName", vatReportCnt.ClientName));
            //add by @zgc 2018年11月20日05:25:34
            parameters.Add(new ReportParameter("paramStartPeriod", txtStPeriod.Text));
            parameters.Add(new ReportParameter("paramEndPeriod", txtEndPeriod.Text));
            //end by@zgc

           //修改rpdl xsd
            ModifyRpdlAndXsd(rdlcPath+sdbRdlcName,rdlcPath+sdbSaveRdlcName,xsdPath+sdbXsdName,xsdPath+sdbSavexsdName,dtSDBReport);
            
            this.reportViewer1.LocalReport.DataSources.Clear();
            this.reportViewer1.LocalReport.ReportPath = rdlcPath+sdbSaveRdlcName;
            this.reportViewer1.LocalReport.SetParameters(parameters);
            ReportDataSource rdsItem = new ReportDataSource("DataSet1", dtSDBReport);
            this.reportViewer1.LocalReport.DataSources.Add(rdsItem);
            this.reportViewer1.RefreshReport();
            
            //Payment
            var dtPay = vatReportCnt.GetPayment();
            DataTable dtPaymentReport = vatReportCnt.GetPaymentReport();
            //payment report
            this.reportViewer3.LocalReport.DataSources.Clear();
            this.reportViewer3.LocalReport.ReportPath = rdlcPath + paymentRdlcName;
            this.reportViewer3.LocalReport.SetParameters(parameters);
            ReportDataSource paymentRdsItem = new ReportDataSource("DataSet1", dtPaymentReport);
            this.reportViewer3.LocalReport.DataSources.Add(paymentRdsItem);
            this.reportViewer3.RefreshReport();

            var arr = AppDataTable.FilterRows(VATReportController.col_NomCode, "845", dtPay);
            if (arr.Length > 0)
            {
                DataRow drVat = dtPay.NewRow();
                drVat.ItemArray = arr[0].ItemArray;
                dtPay.Rows.Remove(arr[0]);


                DataRow drTotal = dtPay.NewRow();
                DataRow drNet = dtPay.NewRow();

                drNet[VATReportController.col_NomCodeDesc] = "Total - Net";
                drTotal[VATReportController.col_NomCodeDesc] = "Total - Gross";

                foreach (DataColumn column in dtPay.Columns)
                {
                    if (column.ColumnName != VATReportController.col_NomCode
                        && column.ColumnName != VATReportController.col_NomCodeDesc
                        && column.ColumnName != VATReportController.col_TotalNet)
                    {
                        drNet[column] = AppDataTable.GetSum(column.ColumnName, dtPay);
                        drTotal[column] = AppConverter.ToFloat(drVat[column], 0) + AppConverter.ToFloat(drNet[column], 0);
                    }
                }

                dtPay.Rows.InsertAt(drNet, dtPay.Rows.Count);//first add Net
                dtPay.Rows.InsertAt(drVat, dtPay.Rows.Count);//Add Vat
                dtPay.Rows.InsertAt(drTotal, dtPay.Rows.Count);//Add Vat
                RowsOfPay = dtPay.Rows.Count;
            }



            LoadGridColumnsPayment(dtPay);
            grdPayments.DataSource = dtPay;


            //Receipts
            var dtRecp = vatReportCnt.GetReceipts();
            LoadGridColumnsRecpt(dtRecp);
            grdReceipts.DataSource = dtRecp;

            //Receipts1
            DataTable dtReceiptsReport = new DataTable();
            dtReceiptsReport = dtRecp.Copy();
            for (int i = 4; i < dtReceiptsReport.Columns.Count; i++)
            {
                string item = "N_" + dtReceiptsReport.Columns[i].ToString();
                dtReceiptsReport.Columns[i].ColumnName = item;
            }

           


            List<ReportParameter> receiptsParameters = new List<ReportParameter>();
            receiptsParameters.Add(new ReportParameter("paramClientName", vatReportCnt.ClientName));
            //add by @zgc 2018年11月20日05:25:34
            receiptsParameters.Add(new ReportParameter("paramStartPeriod", txtStPeriod.Text));
            receiptsParameters.Add(new ReportParameter("paramEndPeriod", txtEndPeriod.Text));
            //end by@zgc

            //修改rpdl xsd
            ModifyRpdlAndXsd(rdlcPath+receiptsRdlcName,rdlcPath+receiptsSaveRdlcName,xsdPath+receiptsXsdName,xsdPath+receiptsSavexsdName,dtReceiptsReport);

            this.reportViewer2.LocalReport.DataSources.Clear();
            this.reportViewer2.LocalReport.ReportPath = rdlcPath + receiptsSaveRdlcName;
            this.reportViewer2.LocalReport.SetParameters(parameters);
            ReportDataSource receiptsRdsItem = new ReportDataSource("DataSet1", dtReceiptsReport);
            this.reportViewer2.LocalReport.DataSources.Add(receiptsRdsItem);
            this.reportViewer2.RefreshReport();




            var outputVat = vatReportCnt.SDB_Vat + vatReportCnt.RecptVat;
            txtOutVat.Text = outputVat.ToString("N2") + " £";
            txtInVat.Text = vatReportCnt.PaymentVat.ToString("N2") + " £";
            if (outputVat - vatReportCnt.PaymentVat < 0)
            {
                txtPayVat.Text = "(" + (vatReportCnt.PaymentVat - outputVat).ToString("N2") + ") £";
            }
            else
                txtPayVat.Text = (outputVat - vatReportCnt.PaymentVat).ToString("N2") + " £";

            var SDB_Net = (vatReportCnt.SDB_Gross - vatReportCnt.SDB_Vat);
            var RecptNet = (vatReportCnt.RecptGross - vatReportCnt.RecptVat);
            var PayNet = (vatReportCnt.PaymentGross - vatReportCnt.PaymentVat);

            // txtValOut.Text = (SDB_Net + RecptNet).ToString("N2") + " £";
            txtValOut.Text = (SDB_Net).ToString("N2") + " £";
            txtValInput.Text = (PayNet).ToString("N2") + " £";


            //Summary
            DataTable dtSummary = new DataTable();
            dtSummary.Columns.Add("OutVAT", typeof(System.Decimal));
            dtSummary.Columns.Add("InputVAT", typeof(System.Decimal));
            dtSummary.Columns.Add("NetPayable", typeof(System.Decimal));
            dtSummary.Columns.Add("OutValue", typeof(System.Decimal));
            dtSummary.Columns.Add("InputValue", typeof(System.Decimal));

            DataRow drSummary = dtSummary.NewRow();

            drSummary["OutVAT"] = Convert.ToDecimal(outputVat.ToString("N2"));
            drSummary["InputVAT"] = Convert.ToDecimal(vatReportCnt.PaymentVat.ToString("N2"));
            drSummary["NetPayable"] = Convert.ToDecimal((outputVat - vatReportCnt.PaymentVat).ToString("N2"));
            drSummary["OutValue"] = Convert.ToDecimal((SDB_Net).ToString("N2"));
            drSummary["InputValue"] = Convert.ToDecimal((vatReportCnt.PaymentReportNet).ToString("N2"));

            dtSummary.Rows.Add(drSummary);

            this.reportViewer4.LocalReport.DataSources.Clear();
            this.reportViewer4.LocalReport.ReportPath = rdlcPath + summaryRdlcName;
            this.reportViewer4.LocalReport.SetParameters(parameters);
            ReportDataSource summaryRdsItem = new ReportDataSource("DataSet1", dtSummary);
            this.reportViewer4.LocalReport.DataSources.Add(summaryRdsItem);
            this.reportViewer4.RefreshReport();


            dsExport.Tables.Add(dtSDB);
            dsExport.Tables.Add(dtPay);
            dsExport.Tables.Add(dtRecp);

        }

        private void ModifyRpdlAndXsd(string originalRdlcFileName,string saveRdlcFileName,string originalXsdFileName,string saveXsdFileName,DataTable dt)
        {
            List<string> colList = new List<string>();

          
            for (int i = 4; i < dt.Columns.Count; i++)
            {
                string item = dt.Columns[i].ToString();
                colList.Add(item);
            }
           
            //修改Rpdl
            ModifyRdlc(originalRdlcFileName,saveRdlcFileName,saveXsdFileName, colList);
            //修改xsd
            ModifyXsd(originalXsdFileName,saveXsdFileName, colList);

        }
        /// <summary>
        /// 修改RDLC文件
        /// </summary>
        /// <returns></returns>
        private XmlDocument ModifyRdlc(string rpdlfilenamewithpath,string saveRdlcFileName,string saveXsdFileName,List<string> colList)
        {
            XmlDocument xmlDoc = new XmlDocument();

            xmlDoc.Load(rpdlfilenamewithpath);
            foreach(string colName in colList)
            {
                AddOneColumnToRdlc(xmlDoc, colName);
            }
            //修改xsd文件位置
            XmlNodeList rdDataSetInfo = xmlDoc.GetElementsByTagName("rd:DataSetInfo");
            
            foreach(XmlNode node in rdDataSetInfo.Item(0).ChildNodes)
            {
                if(node.Name =="rd:SchemaPath")
                    node.InnerText = saveXsdFileName;
            }
            

            xmlDoc.Save(saveRdlcFileName);
            return xmlDoc;
        
        }
        private bool AddOneColumnToRdlc(XmlDocument xmlDoc,string colName)
        {
            //添加Field节点
            XmlNodeList fileds = xmlDoc.GetElementsByTagName("Fields");

            XmlNode filedNode = fileds.Item(0).LastChild.CloneNode(true);
            filedNode.Attributes["Name"].Value = colName;
            filedNode.FirstChild.InnerText = colName;
            fileds.Item(0).AppendChild(filedNode);

            //添加TablixColumn

            XmlNodeList tablixColumns = xmlDoc.GetElementsByTagName("TablixColumns");
            XmlNode tablixColumn = tablixColumns.Item(0).LastChild;
            XmlNode newtablixColumn = tablixColumn.CloneNode(true);
            tablixColumns.Item(0).AppendChild(newtablixColumn);

            //TablixMember
            XmlNodeList tablixMembers = xmlDoc.GetElementsByTagName("TablixColumnHierarchy");

            XmlNode tablixMember = tablixMembers.Item(0).FirstChild.LastChild;
            XmlNode newTablixMember = tablixMember.CloneNode(true);
            tablixMembers.Item(0).FirstChild.AppendChild(newTablixMember);

            XmlNodeList tablixRows = xmlDoc.GetElementsByTagName("TablixRows");

            int index = 0;
            //添加0~3行的列
            for(index = 0 ; index < 4; index++)
            {
                var tablixRowsRowCells = tablixRows.Item(0).ChildNodes[index].ChildNodes[1];
                XmlNode tablixRowCell = tablixRowsRowCells.LastChild;
                XmlNode newtablixRowCell = tablixRowCell.CloneNode(true);
                var textBox = newtablixRowCell.FirstChild.ChildNodes[0];
                textBox.Attributes["Name"].Value = "Programe_textbox" + colName+ index.ToString();

                var defaultName = textBox.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = "Programe_textbox" + colName+ index.ToString();

                tablixRowsRowCells.AppendChild(newtablixRowCell);
            }

            //TablixRows4
            var tablixRowsRowCells1 = tablixRows.Item(0).ChildNodes[index++].ChildNodes[1];
            XmlNode tablixRowCell1 = tablixRowsRowCells1.LastChild;
            XmlNode newtablixRowCell1 = tablixRowCell1.CloneNode(true);
            var textBox1 = newtablixRowCell1.FirstChild.ChildNodes[0];
            textBox1.Attributes["Name"].Value = colName+index.ToString();

            var paragraphs = textBox1.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
            paragraphs.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = colName;
            var defaultName1 = textBox1.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = colName+index.ToString();

            tablixRowsRowCells1.AppendChild(newtablixRowCell1);

            //TablixRows5
            var tablixRowsRowCells2 = tablixRows.Item(0).ChildNodes[index++].ChildNodes[1];
            XmlNode tablixRowCell2 = tablixRowsRowCells2.LastChild;
            XmlNode newtablixRowCell2 = tablixRowCell2.CloneNode(true);
            var textBox2 = newtablixRowCell2.FirstChild.ChildNodes[0];
            textBox2.Attributes["Name"].Value = colName;

            var paragraphs2 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
            paragraphs2.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "=Fields!"+ colName+".Value";
            var defaultName2 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = colName;

            tablixRowsRowCells2.AppendChild(newtablixRowCell2);


            //row6 空行
            var tablixRowsRowCells6 = tablixRows.Item(0).ChildNodes[index++].ChildNodes[1];
            XmlNode tablixRowCell6 = tablixRowsRowCells6.LastChild;
            XmlNode newtablixRowCell6 = tablixRowCell6.CloneNode(true);
            var textBox6 = newtablixRowCell6.FirstChild.ChildNodes[0];
            textBox6.Attributes["Name"].Value = "Programe_textbox" + colName + index.ToString();

            var defaultName6 = textBox6.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = "Programe_textbox" + colName + index.ToString();

            tablixRowsRowCells6.AppendChild(newtablixRowCell6);
            //row7 total
            var tablixRowsRowCells7 = tablixRows.Item(0).ChildNodes[index++].ChildNodes[1];
            XmlNode tablixRowCell7 = tablixRowsRowCells7.LastChild;
            XmlNode newtablixRowCell7 = tablixRowCell7.CloneNode(true);
            var textBox7 = newtablixRowCell7.FirstChild.ChildNodes[0];
            textBox7.Attributes["Name"].Value = colName+"1";
           
            var paragraphs7 = textBox7.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
            paragraphs7.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "=Sum(Fields!" + colName + ".Value)";
            var defaultName7 = textBox7.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = colName + "1";

            tablixRowsRowCells7.AppendChild(newtablixRowCell7);
//             //TablixRows6
//             var tablixRowsRowCells6 = tablixRows.Item(0).ChildNodes[1].ChildNodes[1];
//             XmlNode tablixRowCell6 = tablixRowsRowCells6.FirstChild;
//             XmlNode newtablixRowCell6 = tablixRowCell6.CloneNode(true);
//             var textBox6 = newtablixRowCell6.FirstChild.ChildNodes[0];
//             textBox6.Attributes["Name"].Value = "";
// 
//             var paragraphs6 = textBox2.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "Paragraphs").FirstOrDefault();
//             paragraphs6.FirstChild.FirstChild.FirstChild.FirstChild.InnerText = "";
//             var defaultName6 = textBox6.ChildNodes.Cast<XmlNode>().Where(item => item.Name == "rd:DefaultName").FirstOrDefault().InnerText = "";
// 
//             tablixRowsRowCells6.AppendChild(newtablixRowCell6);
           

            return true;
        }
        /// <summary>
        /// 修改xsd文件
        /// </summary>
        private void ModifyXsd(string xsdlfilenamewithpath,string saveXsdFileName, List<string> colList)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(xsdlfilenamewithpath);

            foreach (string colName in colList)
            {
                AddOneColumnToXsd(xmlDoc, colName);
            }

            xmlDoc.Save(saveXsdFileName);
        }
        private bool AddOneColumnToXsd(XmlDocument xmlDoc,string colName)
        {
            XmlNodeList nodeList = xmlDoc.GetElementsByTagName("xs:sequence");
            XmlNode node = nodeList.Item(0).LastChild.CloneNode(true);
            node.Attributes["name"].Value = colName;
            node.Attributes["msprop:Generator_ColumnVarNameInTable"].Value = "column"+colName;
            node.Attributes["msprop:Generator_ColumnPropNameInRow"].Value = colName;
            node.Attributes["msprop:Generator_ColumnPropNameInTable"].Value = colName+"Column";
            node.Attributes["msprop:Generator_UserColumnName"].Value = colName;
            nodeList.Item(0).AppendChild(node);
            return true;
        }
        void AddSummary(RadGridView grd, string col)
        {

            GridViewSummaryItem summaryItem = new GridViewSummaryItem();
            summaryItem.Name = col;
            summaryItem.Aggregate = GridAggregateFunction.Sum;
            summaryItem.FormatString = "{0:N2}";


            if (grd.SummaryRowsBottom.Count > 0)
                grd.SummaryRowsBottom[0].Add(summaryItem);

            else
            {
                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                grd.SummaryRowsBottom.Add(summaryRowItem);
            }

        }

        #region Grid Style


        void AllGrids_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                e.CellElement.Font = grpTotalFont;
                //e.CellElement.Alignment = ContentAlignment.MiddleRight;
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
            }
        }
        void grdItems_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {

            //if (e.CellElement.Value.ToString().Contains("149"))
            //{

            //}

            //if (e.CellElement.RowInfo is GridViewSummaryRowItem)
            //{
            //    e.CellElement.DrawFill = true;
            //    //e.CellElement.ForeColor = Color.Yellow;
            //    e.CellElement.Font = grpTotalFont;
            //    e.CellElement.TextAlignment = ContentAlignment.MiddleRight;

            //    e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
            //    e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
            //}

            //if (e.RowIndex == RowsOfPay - 1)
            //{
            //    e.CellElement.Font = grpTotalFont;
            //}

        }


        #endregion

        private void btnBack_Click(object sender, EventArgs e)
        {
            DisplayManager.LoadControl(new UcNavigation(false), true);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.ClearErrorProvider();

            if (chkAll.Checked == false)
            {
                DateTime min = new DateTime(1980, 1, 1);
                DateTime max = new DateTime(2020, 12, 31);


                if (dpStDate.IsNull() || dpStDate.Value < min || dpStDate.Value > max)
                {
                    ShowValidationError(dpStDate, CustomMessages.GetValidationMessage("Start Date"));
                    return;
                }
                if (dpEndDate.IsNull() || dpEndDate.Value < min || dpEndDate.Value > max)
                {
                    ShowValidationError(dpEndDate, CustomMessages.GetValidationMessage("End Date"));
                    return;
                }

                int stPrd = AppConverter.ToInt(txtStPeriod.Text, 0);
                int endPrd = AppConverter.ToInt(txtEndPeriod.Text, 0);

                if (stPrd == 0 || stPrd < 1 || stPrd > 12)
                {
                    ShowValidationError(txtStPeriod, CustomMessages.GetValidationMessage("Start Period"));
                    return;
                }
                if (endPrd == 0 || endPrd < 1 || endPrd > 12)
                {
                    ShowValidationError(txtEndPeriod, CustomMessages.GetValidationMessage("End Period"));
                    return;
                }
                if (stPrd > endPrd)
                {
                    ShowValidationError(txtEndPeriod, "Start period must be less then or equal to end period.");
                    return;
                }
            }

            LoadData();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            dpEndDate.Enabled = dpStDate.Enabled = txtEndPeriod.Enabled = txtStPeriod.Enabled = !chkAll.Checked;
        }

        DataTable GetDataSet(RadGridView grid)
        {
            ExportToCSV exporter = new ExportToCSV(grid);
            //string csvFileName =  DateTime.Now.Ticks + ".csv";
            var csvFileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location),DateTime.Now.Ticks + ".csv");

            exporter.RunExport(csvFileName);

            ExcelReader r = new ExcelReader();
            var dt = r.GetDataTableCSV(csvFileName);

            File.Delete(csvFileName);

            return dt;

            #region commented
            //if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    ExportToExcelML exporter = new ExportToExcelML(grid);
            //    exporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
            //    exporter.ExportVisualSettings = true;
            //    exporter.SheetMaxRows = ExcelMaxRows._1048576;
            //    exporter.SheetName = "sheet1";
            //    exporter.SummariesExportOption = SummariesOption.ExportAll;

            //    //it takes absolute path. Dont include char like /,\,: in file name
            //    var fileName = Path.Combine(folderBrowserDialog1.SelectedPath, "Ledger Report-" + DateTime.Now.Ticks + ".xls");

            //    exporter.RunExport(fileName);

            //    ExcelReader r = new ExcelReader();
            //    var dt = r.GetDataTable(fileName, 1);
            //    return dt;
            //}
            #endregion
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            grdSDB.PrintPreview();
        }

        protected override void Dispose(bool disposing)
        {
            //dont dispose controls as the are used when this control is shown again
        }

        private void tabPage3_Click(object sender, EventArgs e)
        {

        }

        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            //if (e.RowElement.RowInfo.Cells[VATReportController.col_NomCodeDesc] != null && e.RowElement.RowInfo.Cells[VATReportController.col_NomCodeDesc].Value.ToString() == "Total-Gross")
            if (e.RowElement.RowInfo.Index == RowsOfPay - 1 || e.RowElement.RowInfo.Index == RowsOfPay - 2 || e.RowElement.RowInfo.Index == RowsOfPay - 3)
            {
                e.RowElement.Font = grpTotalFont;
            }
            else
            {
                e.RowElement.ResetValue(LightVisualElement.FontProperty, ValueResetFlags.Local);
            }
        }

        private void btnExportData_Click(object sender, EventArgs e)
        {
            var ds = new DataSet();
            ds.Tables.Add(GetDataSet(grdSDB));
            ds.Tables.Add(GetDataSet(grdReceipts));
            ds.Tables.Add(GetDataSet(grdPayments));

            var dt = new DataTable();
            dt.Columns.Add("Name");
            dt.Columns.Add("Value");

            var dr = dt.NewRow();
            dr[0] = "Output VAT";
            dr[1] = txtOutVat.Text;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "Input VAT";
            dr[1] = txtInVat.Text;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "VAT Payable";
            dr[1] = txtPayVat.Text;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "Value of Output ";
            dr[1] = txtValOut.Text;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr[0] = "Value of Input";
            dr[1] = txtValInput.Text;
            dt.Rows.Add(dr);

            ds.Tables.Add(dt);

            saveFileDialog1.FileName = "Export " + this.Caption + ".xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                if (ExcelExport.Export(ds, fileName))
                {

                    string path = fileName.Substring(0, fileName.LastIndexOf("\\"));
                    string strExportFileName = path + @"\VAT Report.xls";

                    string sdbfilename = strExportFileName;
                    string receiptsfilename = path + @"\Receipts Report.xls";
                    string paymentfilename = path + @"\Payment Report.xls";
                    string summaryfilename = path + @"\Summary Report.xls";
                    //导出Excel文件
                    ExportReportViewToExcel(reportViewer1, sdbfilename);
                    ExportReportViewToExcel(reportViewer2, receiptsfilename);
                    ExportReportViewToExcel(reportViewer3, paymentfilename);
                    ExportReportViewToExcel(reportViewer4, summaryfilename);

                    //CopyExcelSheet(strExportFileName, sdbfilename,"SDB Report1");
                    CopyExcelSheet(receiptsfilename, sdbfilename, "Receipts Report1");
                    CopyExcelSheet(paymentfilename, sdbfilename, "Payment Report1");
                    CopyExcelSheet(summaryfilename, sdbfilename, "Summary Report1");


                    //File.Delete(sdbfilename);
                    File.Delete(receiptsfilename);
                    File.Delete(paymentfilename);
                    File.Delete(summaryfilename);
                    DisplayManager.DisplayMessage("Data exported successfuly.", MessageType.Success);
                    if (DisplayManager.DisplayMessage("Data is exported to specified location. Do you want to open exported file?", MessageType.Confirmation) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(strExportFileName);
                    }
                }
               
            }

            return;
            #region commented
            //saveFileDialog1.FileName = "Export " + this.Caption + ".xlsx";
            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    string fileName = saveFileDialog1.FileName;
            //    if (ExcelExport.Export(dsExport, fileName))
            //    {
            //        DisplayManager.DisplayMessage("Data exported successfuly.", MessageType.Success);
            //        if (DisplayManager.DisplayMessage("Data is exported to specified location. Do you want to open exported file?", MessageType.Confirmation) == DialogResult.Yes)
            //        {
            //            System.Diagnostics.Process.Start(fileName);
            //        }
            //    }
            //}
            #endregion


        }
        protected void ExportReportViewToExcel(ReportViewer report, string filename)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = report.LocalReport.Render(
               "Excel", null, out mimeType, out encoding,
                out extension,
               out streamids, out warnings);

            FileStream fs = new FileStream(filename,
               FileMode.Create);
            //ISheet sheet;
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();

        }
        private void CopyExcelSheet(string srcfilename, string desfilename, string sheetname)
        {
            //SDB Report.xls
            //Receipts Report.xls

            //string templetfilepath = @"E:\000\template.xls";//模版Excel

            //             string tpath = @"c:\SDB Report.xls";//中介Excel，以它为中介来导出，避免直接使用模块Excel而改变模块的格式
            //             FileInfo ff = new FileInfo(tpath);
            //             if (ff.Exists)
            //             {
            //                 ff.Delete();
            //             }
            //             FileStream fs = File.Create(tpath);
            //             HSSFWorkbook x1 = new HSSFWorkbook();
            //             x1.Write(fs);
            //             fs.Close();


            FileStream desfile = new FileStream(desfilename, FileMode.OpenOrCreate, FileAccess.Read);

            HSSFWorkbook desworkbook = new HSSFWorkbook(desfile);

            FileStream srcfile = new FileStream(srcfilename, FileMode.Open, FileAccess.Read);
            HSSFWorkbook srcworkbook = new HSSFWorkbook(srcfile);

            HSSFSheet CPS = srcworkbook.GetSheetAt(0) as HSSFSheet;
            //             for (int i = 0; i <= dty1.Rows.Count - 1; i++)//dty1是我用SQL查询出来当天单子集合
            //             {
            CPS.CopyTo(desworkbook, sheetname, true, true);

            using (FileStream fileSave = new FileStream(desfilename, FileMode.Open, FileAccess.Write))
            {
                desworkbook.Write(fileSave);
            }
            // }

            //book2.Write(fileSave2);
            //                 fileSave2.Close();
            //                 fileRead.Close();
        }
        //private void btnExport_Click(object sender, EventArgs e)
        //{

        //    //ledgerGrid1.CurrentWorksheet.ExportAsCSV("exp.csv");
        //    var dt = ledgerGrid1.sGetExportDataTable();
        //    var ds = new DataSet();
        //    ds.Tables.Add(dt);

        //    saveFileDialog1.FileName = "Export " + this.Caption + ".xlsx";
        //    if (saveFileDialog1.ShowDialog() == DialogResult.OK)
        //    {
        //        string fileName = saveFileDialog1.FileName;
        //        if (ExcelExport.Export(ds, fileName))
        //        {
        //            DisplayManager.DisplayMessage("Data exported successfuly.", MessageType.Success);
        //            if (DisplayManager.DisplayMessage("Data is exported to specified location. Do you want to open exported file?", MessageType.Confirmation) == DialogResult.Yes)
        //            {
        //                System.Diagnostics.Process.Start(fileName);
        //            }
        //        }
        //    }
        //}

    }
}
