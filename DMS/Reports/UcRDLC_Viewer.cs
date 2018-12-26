using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using DAL;
using DAL.Controllers;
using System.IO;

namespace DMS.Reports
{
    public partial class UcRDLC_Viewer : UserControlBase
    {
        public UcRDLC_Viewer()
        {
            InitializeComponent();
            SqlServerTypes.Utilities.LoadNativeAssemblies(AppDomain.CurrentDomain.BaseDirectory);
        }
        private void UcRDLC_Viewer_Load(object sender, EventArgs e)
        {

        }

        private void LoadReport(DataTable dt, string sourceName, string rptPath, string displayName, ReportParameter[] arrParam = null)
        {
            this.Caption = displayName;

            reportViewer1.Clear();
            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource reportDataSource = new ReportDataSource(sourceName, dt);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.ReportPath = rptPath;
            if (arrParam != null)
            {
                reportViewer1.LocalReport.SetParameters(arrParam);
            }
            
            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DisplayName = displayName;
        }

        public void JournalsReport(long docItemID, ReportType rptTyp)
        {
            LedgerReportController c = new LedgerReportController(docItemID);
            var dt = c.JournalReport(rptTyp);

            if (rptTyp == ReportType.Journal_Double_Accrual_Prepayment)
            {
                LoadReport(dt, "DataSet1", @".\Reports\RDLC\RptJournals.rdlc", "Journals Report");
            }
            if (rptTyp == ReportType.Journal_MultipleEnt)
            {
                LoadReport(dt, "DataSet1", @".\Reports\RDLC\RptMultipleEntJournal.rdlc", "Journals Report");
            }
        }

        public void ThisYrClossingTrialBal(long docItemID)
        {
            

            LedgerReportController c = new LedgerReportController(docItemID);
            c.LoadReportData(new DateTime(1990, 1, 1), new DateTime(2030, 1, 1));
            var ds = c.ThisYrClosingTrialBal();

            ReportParameter rp1 = new ReportParameter("CurrentYear", Convert.ToDateTime(c.FolderYearEnd).Year.ToString());
            ReportParameter rp2 = new ReportParameter("LastYear", (Convert.ToDateTime(c.FolderYearEnd).Year - 1).ToString());
            ReportParameter rp3 = new ReportParameter("NetThisYr", c.thisYrProfit.ToString());
            ReportParameter rp4 = new ReportParameter("NetLastYr", c.lastYrProfit.ToString());
            var arr = new ReportParameter[] { rp1, rp2, rp3, rp4 };
            
            LoadReport(ds.Tables[0], "DataSet1", @".\Reports\RDLC\RptThisYrClosingTrialBal.rdlc", "Closing Trial Balance", arr);
        }

        public void PaymentSummary(long docItemID)
        {
            string clientName = string.Empty;
            //add by @zgc 2018年11月20日05:23:02
            string txtPeriod = string.Empty;
            tblItemInfoController itemInfoCnt = new tblItemInfoController();
            var lstInfo = itemInfoCnt.FetchAllByDocumentItemID(docItemID);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                tblItemInfo itemInfo = lstInfo[0];
                if (itemInfo != null)
                {
                    txtPeriod = itemInfo.Period;
                }
            }
            //end @zgc
            var dt = ReportsHelper.PaymentSummary(docItemID);
            var dtSheet = ReportsHelper.PaymentSheet(docItemID, out clientName);
            this.Caption = "Payment Summary";

            reportViewer1.Clear();
            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource reportDataSource = new ReportDataSource("dtPaymentSummary", dt);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            ReportDataSource rd2 = new ReportDataSource("dtPaymentSheet", dtSheet);
            reportViewer1.LocalReport.DataSources.Add(rd2);

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("paramClientName", clientName));
            //add by @zgc 2018年11月20日05:25:34
            parameters.Add(new ReportParameter("paramPeriod", txtPeriod));
            //end by@zgc

            reportViewer1.LocalReport.ReportPath = @".\Reports\RDLC\RptPaymentSummary.rdlc";
            this.reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DisplayName = this.Caption;
        }

        public void NextYrOpnBal(long docItemID)
        {
            string clientName = string.Empty;

            var n = new NextYrOpeningBalRpt();
            n.GenReport(docItemID);
            DataTable dtNextYr = n.dtNextYr;

            this.Caption = "Next Year Opening Balance";

            reportViewer1.Clear();
            reportViewer1.LocalReport.DataSources.Clear();
            
            ReportDataSource rd2 = new ReportDataSource("dtNextYrOpnBal", dtNextYr);
            reportViewer1.LocalReport.DataSources.Add(rd2);

            reportViewer1.LocalReport.ReportPath = @".\Reports\RDLC\RptNextYrOpnBal.rdlc";

            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DisplayName = this.Caption;

            //string clientName = string.Empty;

            //var n = new NextYrOpeningBalRpt();
            //n.GenReport(docItemID);
            //DataTable dtPLReserve = n.dtPLReserve;
            //DataTable dtNextYr = n.dtNextYr;

            //var dtSheet = ReportsHelper.PaymentSheet(docItemID, out clientName);
            //this.Caption = "Next Year Opening Balance";

            //reportViewer1.Clear();
            //reportViewer1.LocalReport.DataSources.Clear();

            //ReportDataSource reportDataSource = new ReportDataSource("dtPLReserve", dtPLReserve);
            //reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            //ReportDataSource rd2 = new ReportDataSource("dtNextYrOpnBal", dtNextYr);
            //reportViewer1.LocalReport.DataSources.Add(rd2);

            //reportViewer1.LocalReport.ReportPath = @".\Reports\RDLC\RptNextYrOpnBal.rdlc";

            //reportViewer1.RefreshReport();
            //reportViewer1.LocalReport.DisplayName = this.Caption;
        }

        public void PLReserve(long docItemID)
        {
            string clientName = string.Empty;

            var n = new NextYrOpeningBalRpt();
            n.GenReport(docItemID);
            DataTable dtPLReserve = n.dtPLReserve;
            
            this.Caption = "Next Year Opening Balance";
            reportViewer1.Clear();
            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource reportDataSource = new ReportDataSource("dtPLReserve", dtPLReserve);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);
            reportViewer1.LocalReport.ReportPath = @".\Reports\RDLC\RptPLReserve.rdlc";

            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DisplayName = this.Caption;
        }


        public void ReconcileReport(long docItemID, long nominalCodeID, string nominalCodeDesc, int stPeriod, int endPeriod,
                                    string clientName, DateTime reconcileDate, decimal acctBal, decimal outPayments,
                                    decimal outRecp, decimal balPerStm)
        {
            tblExcelSheetController cnt = new tblExcelSheetController();
            var dt = cnt.GetReconcileData(docItemID, stPeriod, endPeriod, nominalCodeID, false);

            this.Caption = "Reconcile Report";
            reportViewer1.Clear();
            reportViewer1.LocalReport.DataSources.Clear();

            ReportDataSource reportDataSource = new ReportDataSource("DataSet1", dt);
            reportViewer1.LocalReport.DataSources.Add(reportDataSource);

            List<ReportParameter> parameters = new List<ReportParameter>();
            parameters.Add(new ReportParameter("clientName", clientName));
            parameters.Add(new ReportParameter("nominalCodeDesc", nominalCodeDesc));
            parameters.Add(new ReportParameter("stPeriod", stPeriod.ToString()));
            parameters.Add(new ReportParameter("endPeriod", endPeriod.ToString()));
            parameters.Add(new ReportParameter("reconcileDate", reconcileDate.ToString()));
            parameters.Add(new ReportParameter("acctBal", acctBal.ToString()));
            parameters.Add(new ReportParameter("outPayments", outPayments.ToString()));
            parameters.Add(new ReportParameter("outRecp", outRecp.ToString()));
            parameters.Add(new ReportParameter("balPerStm", balPerStm.ToString()));

            reportViewer1.LocalReport.ReportPath = @".\Reports\RDLC\RptReconcile.rdlc";
            this.reportViewer1.LocalReport.SetParameters(parameters);

            reportViewer1.RefreshReport();
            reportViewer1.LocalReport.DisplayName = this.Caption;
        }

        public bool ExportExcel(string fileName)
        {
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;

            byte[] bytes = reportViewer1.LocalReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);

            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(bytes, 0, bytes.Length);
            fs.Close();
            return true;
        }
    }
}
