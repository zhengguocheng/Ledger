using System;
using System.Windows.Forms;
using DMS.CustomClasses;
using DAL;
using System.Data;
using System.Data.SqlClient;
using DMS.Reports;
using FrontEnd;
using System.Collections.Generic;
using DMS.UserControls;
using System.Net;
using System.Net.Mail;


namespace DMS
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            try
            {
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UserControlBase.UnhandledThreadException);
                LoadAppConstants();

                //GetSchema();

                TempraryDocuments.CleanTempFolder();
                //DisplayManager.LoadControl(new UcLedgerReport(19023, 564));
                //DisplayManager.LoadControl(new UcRptThisYrClosingTrialBal(19021));
                //DisplayManager.LoadControl(new UcExcelSheet(40166, "Payment"));
                
                DisplayManager.LoadControl(new UcLogin());
                Application.Run(DisplayManager.MainForm);
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
        }

        static void TestR()
        {
            var n = new NextYrOpeningBalRpt();
            n.GenReport(19020);

            DataTable dtPLReserve = n.dtPLReserve;
            DataSet ds1 = new DataSet("PLReserveReportDs");
            ds1.Tables.Add(dtPLReserve.Copy());
            DataSetSchema.WriteSchema(ds1, ds1.DataSetName, "PLReserveReportSchema");

            DataTable dtNextYr = n.dtNextYr;
            DataSet ds = new DataSet("NextYrOpnBalReportDs");
            ds.Tables.Add(dtNextYr.Copy());
            DataSetSchema.WriteSchema(ds, ds.DataSetName, "NextYrOpnBalReportSchema");
        }

        static void TestRep()
        {
            //byte[] arr = FileHelper.GetByteArray(@"D:\test.txt");
            //using (FileStream fs = new FileStream(@"D:\SkyDrive\Source Code backup\Repository\test.txt", FileMode.Create, FileAccess.Write, FileShare.None))
            //{
            //    fs.Write(arr, 0, arr.Length);
            //}

            //DAL.RepositoryExport e = new RepositoryExport();
            //e.Export(@"D:\SkyDrive\Source Code backup\Repository\");
        }

        static void LoadAppConstants()
        {
            AppConstants.IsLedger = true;

            if (AppConstants.IsLedger)
                DisplayManager.MainForm = new LedgerMainForm();


            string macname = System.Environment.MachineName;

            AppConstants.SetTempDirectory(Application.StartupPath);

            if (string.Compare(macname, "shahbazkhurram", true) == 0 || string.Compare(macname, "VC1", true) == 0)
            {
                AppConstants.ConnString = "name=dbDMSEntitieslocal";
            }
            else
            {
                AppConstants.IsDeployed = true;
                AppConstants.SendEmails = true;
                AppConstants.RpstFolderPath = @"\\192.168.1.11\repository$";
                AppConstants.ConnString = "name=dbDMSEntities";
            }

            SettingsController cnt = new SettingsController();
            AppConstants.RpstFolderPath = cnt.GetSettings("RpstFolderPath") ?? DMSSettings.Default.RpstFolderPath;
            AppConstants.EmailKey = cnt.GetSettings("EmailKey") ?? DMSSettings.Default.EmailKey;
            AppConstants.DMSEmail = cnt.GetSettings("DMSEmail") ?? DMSSettings.Default.DMSEmail;
            AppConstants.LogDirPath = cnt.GetSettings("LogDirPath") ?? DMSSettings.Default.LogDirPath;
            AppConstants.IsDeployed = cnt.GetSettings("IsDeployed") != null ? bool.Parse(cnt.GetSettings("IsDeployed")) : DMSSettings.Default.IsDeployed;
            AppConstants.SendEmails = cnt.GetSettings("SendEmails") != null ? bool.Parse(cnt.GetSettings("SendEmails")) : DMSSettings.Default.SendEmails;
            AppConstants.LatestVersion = cnt.GetSettings("LatestVersion");

            //string s = AppConstants.GetPassword();
            #region this line is commented temporarly. Modify MatchedFolderPath value in db to '.\Matched PDF' and uncomment this line
            //AppConstants.MatchedFolderPath = cnt.GetSettings("MatchedFolderPath") ?? DMSSettings.Default.MatchedFolderPath;
            AppConstants.MatchedFolderPath = DMSSettings.Default.MatchedFolderPath;
            #endregion

            AppConstants.PDFAnalyzerExePath = cnt.GetSettings("PDFAnalyzerExePath") ?? DMSSettings.Default.PDFAnalyzerExePath;
        }

        static DateTime lastExp = DateTime.Now.AddDays(-5);

        static private void RepositoryExporting()
        {
            try
            {
                TimeSpan diff = DateTime.Now.Subtract(lastExp);
                if (diff.TotalHours > 5)
                {
                    DAL.RepositoryExport e = new RepositoryExport();
                    e.Export(@"d:\exp");
                    lastExp = DateTime.Now;
                }
            }
            catch (Exception ecp)
            {
                // Logger.LogException(ecp);
            }
        }

        static void ImportNominalCode()
        {
            tblAnalysisCodeController cnt = new tblAnalysisCodeController();
            string[] lines = System.IO.File.ReadAllLines(@"E:\Source Code\Projects\oDesk\Ivan\Ledger\Documents\Nominal Code.txt");
            foreach (string line in lines)
            {
                var arr = line.Split('\t');
                tblAnalysisCode c = new tblAnalysisCode();
                c.Code = arr[0];
                c.Notes = arr[1];
                cnt.Save(c);
            }
        }

        static void GetSchema()
        {
            //var d = DBHelper.ExecuteSP(SPNames.SpExcelSheetSummary, new SqlParameter("@docItemID", 18679));
            //DataSet ds = new DataSet("PaymentSheetSchema");
            //ds.Tables.Add(d.Tables[3].Copy());
            //DataSetSchema.WriteSchema(ds, ds.DataSetName, "PaymentSheetSchema");


            LedgerReportController c = new LedgerReportController(19021);
            c.LoadReportData(new DateTime(1990, 1, 1), new DateTime(2030, 1, 1));
            var d = c.ThisYrClosingTrialBal();

            DataSet ds = new DataSet("NewThisYrClosingTBSchema");
            ds.Tables.Add(d.Tables[0].Copy());
            DataSetSchema.WriteSchema(ds, ds.DataSetName, "NewThisYrClosingTBSchema");

        }

        static void TestBal()
        {
            long docID = 18934;
            LedgerRepository r = new LedgerRepository();
            tblDocumentItem preYE = r.PreviousYearEndFolder(docID);

            List<tblDocumentItem> lst = new List<tblDocumentItem>();
            r.FindRecursive(preYE.ID, Tags.TagType.ThisYearClosingTrialBal, lst);

            tblDocumentItem preTB = lst[0];


            //LedgerReportController c = new LedgerReportController(preTB.ID);
            //c.LoadReportData(new DateTime(1990, 1, 1), new DateTime(2030, 1, 1));
            //var ds = c.ThisYrClosingTrialBal();

            LedgerReportController c = new LedgerReportController(preTB.ID);
            c.LoadReportData(new DateTime(2010, 1, 1), new DateTime(2016, 1, 1));
            var ds = c.ThisYrClosingTrialBal();


            tblTrialBalanceController cnt = new tblTrialBalanceController();
            cnt.CopyData(ds.Tables[0], docID);

        }



    }
}
