using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Utilities;

namespace DAL
{
    public enum ReportType
    {
        FullReport = 0, Journal_Double_Accrual_Prepayment = 1, Journal_MultipleEnt = 2, LedgerSummaryOnly = 3
    }
    public class LedgerReportController
    {
        #region Column names

        public const string colID = "ID";
        public const string colDate = "Date";
        public const string colDescription = "Description";
        public const string colReference = "Reference";
        public const string colGross = "Gross";
        public const string colVATCode = "VATCode";
        public const string colVAT = "VAT";
        public const string colNet = "Net";

        public const string colNominalCodeID = "NominalCodeID";
        public const string colNominalCode = "NominalCode";
        public const string colComment = "Comment";
        public const string colPaymentNomCodeID = "PaymentNomCodeID";
        public const string colNomCodeDesc = "NomCodeDesc";
        public const string colPaymentNomCodeDesc = "PaymentNomCodeDesc";

        public const string colDocumentItemID = "DocumentItemID";

        public const string colSource = "Source";
        public const string colDebit = "Debit";
        public const string colCredit = "Credit";
        public const string colBalance = "Balance";
        public const string colNotes = "Notes";
        public const string colOrder = "Order";

        public const string colLastYrDebit = "LastYearDebit";
        public const string colLastYrCredit = "LastYearCredit";
        public const string colItemType = "ItemType";


        public const string colRecordType = "RecordType";

        public const string colDocName = "DocName";
        #endregion

        AppTimer timer = new AppTimer();
        public string timerMsg = "";

        long docItemID;
        public string ClientName;
        public string FolderYearEnd;
        public DataTable dtReport = new DataTable();
        public long yrEndFolID;

        public decimal thisYrProfit = 0, lastYrProfit = 0;


        tblChartAccountController cntChartAcc = new tblChartAccountController();
        List<tblChartAccount> lstChartAcc;

        tblVATRateController cntVAT = new tblVATRateController();
        List<tblVATRate> lstVatRate;

        public LedgerReportController(long _docItemID)
        {
            docItemID = _docItemID;

            dtReport.Columns.Add(colDate, typeof(DateTime));
            dtReport.Columns.Add(colDescription);
            dtReport.Columns.Add(colDocName);
            dtReport.Columns.Add(colSource);
            dtReport.Columns.Add(colDebit, typeof(Decimal));
            dtReport.Columns.Add(colCredit, typeof(Decimal));
            dtReport.Columns.Add(colBalance, typeof(Decimal));
            dtReport.Columns.Add(colOrder, typeof(int));
            dtReport.Columns.Add(colNominalCodeID);
            dtReport.Columns.Add(colNominalCode);
            dtReport.Columns.Add(colNotes);
            dtReport.Columns.Add(colRecordType);
            dtReport.Columns.Add(colDocumentItemID);
            dtReport.Columns.Add(colID);
        }

        public void LoadClientInfo()
        {
            VwDocumentList_Controller c = new VwDocumentList_Controller();
            var doc = c.Find(docItemID);
            if (doc != null)
            {
                ClientName = doc.Client_Name;
            }

            LedgerRepository repCntr = new LedgerRepository();
            var yrEndFol = repCntr.GetYearEndFolder(docItemID);
            yrEndFolID = yrEndFol.ID;

            FolderYearEnd = yrEndFol.YearEndDate.Value.ToString(AppConstants.DateFormatYearEnd);
        }

        public tblChartAccount GetAnalysisCode(string code)
        {
            //var code = desc.Split("-".ToCharArray())[0].Trim();
            code = code.Trim();
            var objCode = lstChartAcc.FirstOrDefault(x => x.Code.Trim() == code);
            return objCode;
        }

        void ClearData()
        {
            timer.Start();
            dtReport.Clear();
            StopTimer("ClearData()");
        }

        void StopTimer(string msg)
        {
            if (string.IsNullOrWhiteSpace(timerMsg))
            {
                timerMsg = DateTime.Now.ToLongTimeString() + "   " + ClientName ?? "" + "    ";
            }

            timerMsg += msg + " = " + timer.Stop() + ".   ";
        }

        DataSet GetSpData(DateTime stDate, DateTime endDate, int stPeriod, int endPeriod, ReportType rptType)
        {
            timer.Start();

            LedgerRepository repCntr = new LedgerRepository();
            var fol = repCntr.GetYearEndFolder(docItemID);

            lstChartAcc = cntChartAcc.FetchByYearEndID(fol.ID);

            lstVatRate = cntVAT.FetchByYearEndID(fol.ID);

            bool isLedgerSummary = rptType == ReportType.LedgerSummaryOnly;

            var ds = DBHelper.ExecuteSP(SPNames.SpLedgerReport
                //new SqlParameter("@ClientID", clientID), 
                , new SqlParameter("@stDate", stDate)
                , new SqlParameter("@endDate", endDate)
                , new SqlParameter("@yrEndFolderID", fol.ID)
                , new SqlParameter("@openingTrialTagValue", Tags.TagValue(Tags.TagType.OpeningTrialBal))
                , new SqlParameter("@stPeriod", stPeriod)
                , new SqlParameter("@endPeriod", endPeriod)
                , new SqlParameter("@IsSummary", isLedgerSummary)
                );


            StopTimer("GetSPData()");

            return ds;
        }
        public void LoadReportData(DateTime stDate, DateTime endDate, int stPeriod = 0, int endPeriod = 12, ReportType rptType = ReportType.FullReport)
        {
            timerMsg = "";

            ClearData();

            var ds = GetSpData(stDate, endDate, stPeriod, endPeriod, rptType);

            timer.Start();

            if (!ds.IsNullOrEmpty())
            {
                string VATCodeID = "";//set default
                string VATCodeDesc = "845 - VAT control account";//set default

                var dtExcel = ds.Tables[0];
                var dtTrialBal = ds.Tables[1];
                var dtVAtCodeInfo = ds.Tables[2];
                var dtSDB = ds.Tables[3];
                var dtJournal = ds.Tables[4];
                var dtMultipleEntJournal = ds.Tables[5];

                if (dtVAtCodeInfo.Rows.Count > 0)
                {
                    VATCodeDesc = dtVAtCodeInfo.Rows[0][colNomCodeDesc].ToString();
                    VATCodeID = dtVAtCodeInfo.Rows[0]["ID"].ToString();
                }

                if (rptType == ReportType.FullReport || rptType == ReportType.LedgerSummaryOnly)
                {
                    #region SDB

                    //Add SDB Sum

                    DataView view = new DataView(dtSDB);
                    DataTable distinctDocs = view.ToTable(true, "DocumentItemID", "Nominal1", "Nominal2", "Nominal3", "Nominal4", "Nominal5", "Nominal6", "TotalVAT1", "TotalVAT2", "TotalVAT3", "TotalVAT4", "TotalVAT5", "TotalVAT6", "VatDate", colDocName);
                    distinctDocs.Columns.Add(colID);
                    Single TotalVATSum = 0;

                    foreach (DataRow dr in distinctDocs.Rows)
                    {
                        for (int i = 1; i < 7; i++)
                        {
                            var colNomCodeID = "Nominal" + i.ToString();
                            var colTotalVat = "TotalVAT" + i.ToString();

                            var objNom = lstChartAcc.FirstOrDefault(x => x.ID == (int)dr[colNomCodeID]);

                            if (objNom != null)
                            {
                                AddNewRow_SDB_VAT(dr, i);

                                //增加每一项的VAT
                                var drSDB_VAT = dtReport.NewRow();

                                drSDB_VAT[colDate] = dr["VatDate"];

                                drSDB_VAT[colDebit] = 0;
                                drSDB_VAT[colCredit] = dr[colTotalVat];

                                drSDB_VAT[colNominalCodeID] = VATCodeID;
                                drSDB_VAT[colNominalCode] = VATCodeDesc;

                                drSDB_VAT[colDescription] = "Output VAT";
                                drSDB_VAT[colDocName] = dr[colDocName];
                                drSDB_VAT[colDocumentItemID] = dr[colDocumentItemID];
                                drSDB_VAT[colID] = dr[colID];
                                drSDB_VAT[colSource] = string.Empty;
                                drSDB_VAT[colNotes] = string.Empty;
                                drSDB_VAT[colOrder] = 1;

                                dtReport.Rows.Add(drSDB_VAT);
                            }
                            //AddNewRow_SDB_VAT(dr, i);
                            if (dr["TotalVAT" + i] != DBNull.Value)
                            {
                                try
                                {
                                    TotalVATSum += Convert.ToSingle(dr["TotalVAT" + i]);
                                }
                                catch (Exception ecp)
                                { }
                            }
                        }
                    }

                   /* if (distinctDocs.Rows.Count > 0)
                    {
                        var drSrc = distinctDocs.Rows[0];
                        var drSDB_VAT = dtReport.NewRow();

                        drSDB_VAT[colDate] = drSrc["VatDate"];

                        drSDB_VAT[colDebit] = 0;
                        drSDB_VAT[colCredit] = TotalVATSum;

                        drSDB_VAT[colNominalCodeID] = VATCodeID;
                        drSDB_VAT[colNominalCode] = VATCodeDesc;

                        drSDB_VAT[colDescription] = "Output VAT";
                        drSDB_VAT[colDocName] = drSrc[colDocName];
                        drSDB_VAT[colDocumentItemID] = drSrc[colDocumentItemID];
                        drSDB_VAT[colID] = drSrc[colID];
                        drSDB_VAT[colSource] = string.Empty;
                        drSDB_VAT[colNotes] = string.Empty;
                        drSDB_VAT[colOrder] = 1;

                        dtReport.Rows.Add(drSDB_VAT);
                    }*/
                    foreach (DataRow dr in dtSDB.Rows)
                    {
                        AddNewRow_SDB(dr);
                    }

                    #endregion
                }

                if (rptType == ReportType.FullReport || rptType == ReportType.LedgerSummaryOnly)
                {
                    #region TrialBalance

                    foreach (DataRow dr in dtTrialBal.Rows)
                    {
                        AddNewRow_TrialBal(dr);
                    }

                    #endregion
                }

                if (rptType == ReportType.FullReport || rptType == ReportType.LedgerSummaryOnly || rptType == ReportType.Journal_Double_Accrual_Prepayment)
                {
                    #region Journal

                    foreach (DataRow dr in dtJournal.Rows)
                    {
                        ProcessRow_Journals(dr);
                    }

                    #endregion
                }

                if (rptType == ReportType.FullReport || rptType == ReportType.LedgerSummaryOnly || rptType == ReportType.Journal_MultipleEnt)
                {
                    #region Multiple Ent Journal

                    foreach (DataRow dr in dtMultipleEntJournal.Rows)
                    {
                        AddRowInLedger_MultipleEntJournals(dr);
                    }

                    #endregion
                }

                if (rptType == ReportType.FullReport || rptType == ReportType.LedgerSummaryOnly)
                {
                    #region tblExcelSheet Records

                    foreach (DataRow dr in dtExcel.Rows)
                    {
                        string fileName = dr["Name"].ToString();
                        Tags.TagType fileType = AppConstants.LedgerFolder.GetTagFromFileName(fileName);

                        #region Process VAT: Add a seperate row for VAT

                        if (
                            !AppConstants.sIsSplitText(dr["Description"].ToString())
                            //!string.Equals(dr["Description"].ToString(), AppConstants.splitValue)
                            )//dont process split rows
                        {
                            if (dr[colVAT] != DBNull.Value)//dont process records whos VAT code is not defined.
                            {
                                decimal val = 0;
                                if (decimal.TryParse(dr[colVAT].ToString(), out val))
                                {
                                    if (val != 0)//dont add 0. As the are there for enteries where VAT columns are hidden.
                                    {
                                        var drVAT = AddNewRow_Payment(dr);
                                        drVAT[colNominalCode] = VATCodeDesc;

                                        if (fileType == Tags.TagType.Payment_FileType)
                                        {
                                            drVAT[colDebit] = dr[colVAT];
                                        }
                                        else if (fileType == Tags.TagType.Receipts_FileType)
                                        {
                                            drVAT[colCredit] = dr[colVAT];
                                        }
                                    }
                                }
                            }
                        }

                        #endregion

                        #region Process Net: Add a seperate row for Net

                        var drNominal = AddNewRow_Payment(dr);
                        drNominal[colNominalCode] = dr[colNomCodeDesc];
                        if (fileType == Tags.TagType.Payment_FileType)
                        {
                            drNominal[colDebit] = dr[colNet];
                        }
                        else if (fileType == Tags.TagType.Receipts_FileType)
                        {
                            drNominal[colCredit] = dr[colNet];
                        }
                        #endregion

                        #region Process Gross: Add a seperate row for gross

                        if (
                            !AppConstants.sIsSplitText(dr["Description"].ToString())
                            //!string.Equals(dr["Description"].ToString(), AppConstants.splitValue)
                            )//dont process split rows
                        {
                            if (dr[colPaymentNomCodeDesc] != DBNull.Value)//dont process records whos payment nominal code is not defined.
                            {
                                var drPaymentNominal = AddNewRow_Payment(dr);
                                drPaymentNominal[colNominalCode] = dr[colPaymentNomCodeDesc];
                                if (fileType == Tags.TagType.Payment_FileType)
                                {
                                    drPaymentNominal[colCredit] = dr[colGross];
                                }
                                else if (fileType == Tags.TagType.Receipts_FileType)
                                {
                                    drPaymentNominal[colDebit] = dr[colGross];
                                }
                            }
                        }
                        #endregion
                    }

                    #endregion
                }

                #region Running Total

                DataView dv = dtReport.DefaultView;
                dv.Sort = string.Format("{0},{1},{2} ASC", colNominalCode, colOrder, colDate);
                DataTable sortedDT = dv.ToTable();

                decimal runningTotal = 0;
                string strPreNom = null;

                if (sortedDT.Rows.Count > 0)
                    strPreNom = sortedDT.Rows[0][colNominalCode].ToString();

                foreach (DataRow dr in sortedDT.Rows)
                {
                    var strCurrNom = dr[colNominalCode].ToString();
                    if (!string.Equals(strPreNom, strCurrNom))
                    {
                        runningTotal = 0;
                    }

                    decimal debt = 0, cred = 0;
                    if (dr[colDebit] != DBNull.Value)
                    {
                        debt = Convert.ToDecimal(dr[colDebit]);
                    }
                    if (dr[colCredit] != DBNull.Value)
                    {
                        cred = Convert.ToDecimal(dr[colCredit]);
                    }

                    runningTotal = runningTotal + debt - cred;
                    dr[colBalance] = runningTotal;
                    strPreNom = strCurrNom;

                }

                dtReport = sortedDT;

                #endregion

                StopTimer("LoadReportData");
            }
        }

        #region Payment and Receipts
        DataRow AddNewRow_Payment(DataRow drSrc)
        {
            var drDestination = dtReport.NewRow();
            drDestination[colDate] = drSrc[colDate];
            drDestination[colDescription] = drSrc[colDescription];
            drDestination[colSource] = drSrc[colReference];
            drDestination[colDocName] = drSrc[colDocName];
            drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
            drDestination[colID] = drSrc[colID];

            drDestination[colNotes] = drSrc[colComment];
            drDestination[colNominalCodeID] = drSrc[colNominalCode];

            drDestination[colOrder] = 1;

            dtReport.Rows.Add(drDestination);
            return drDestination;
        }

        #endregion

        #region Trial Balance

        DataRow AddNewRow_TrialBal(DataRow drSrc)
        {
            var drDestination = dtReport.NewRow();

            drDestination[colDate] = drSrc[colDate];

            drDestination[colDebit] = drSrc[colDebit];
            drDestination[colCredit] = drSrc[colCredit];

            drDestination[colNominalCodeID] = drSrc[colNominalCodeID];
            drDestination[colNominalCode] = drSrc[colNomCodeDesc];

            drDestination[colDescription] = "Opening Trial Balance";
            drDestination[colSource] = string.Empty;
            drDestination[colDocName] = drSrc[colDocName];
            drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
            drDestination[colID] = drSrc[colID];

            drDestination[colNotes] = string.Empty;
            drDestination[colOrder] = 0;

            dtReport.Rows.Add(drDestination);
            return drDestination;
        }

        #endregion

        #region Journals

        void ProcessRow_Journals(DataRow drSrc)
        {
            AddRowInLedger_Journals(drSrc, true);
            AddRowInLedger_Journals(drSrc, false);
        }

        DataRow AddRowInLedger_Journals(DataRow drSrc, bool isCredit)
        {
            var drDestination = dtReport.NewRow();

            drDestination[colDate] = drSrc[colDate];

            if (drSrc["DebitNominalCodeID"] != DBNull.Value && isCredit == false)
            {
                drDestination[colDebit] = drSrc["Amount"];
                drDestination[colNominalCodeID] = drSrc["DebitNominalCodeID"];
                drDestination[colNominalCode] = drSrc["DebitNominalCode"];
            }

            if (drSrc["CreditNominalCodeID"] != DBNull.Value && isCredit == true)
            {
                drDestination[colCredit] = drSrc["Amount"];
                drDestination[colNominalCodeID] = drSrc["CreditNominalCodeID"];
                drDestination[colNominalCode] = drSrc["CreditNominalCode"];
            }

            drDestination[colDescription] = drSrc[colDescription];
            drDestination[colSource] = drSrc[colReference];
            drDestination[colDocName] = drSrc[colDocName];
            drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
            drDestination[colID] = drSrc[colID];
            drDestination[colNotes] = drSrc[colNotes];
            drDestination[colOrder] = 2;//Add these in last of every thing elese

            drDestination[colRecordType] = (int)ReportType.Journal_Double_Accrual_Prepayment;
            drDestination[colDocumentItemID] = drSrc[colDocumentItemID];

            dtReport.Rows.Add(drDestination);
            return drDestination;
        }

        DataRow AddRowInLedger_MultipleEntJournals(DataRow drSrc)
        {
            var drDestination = dtReport.NewRow();

            drDestination[colDate] = drSrc[colDate];
            drDestination[colDescription] = drSrc[colDescription];
            drDestination[colSource] = drSrc[colReference];
            drDestination[colDocName] = drSrc[colDocName];
            drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
            drDestination[colID] = drSrc[colID];

            drDestination[colNominalCodeID] = drSrc[colNominalCodeID];
            drDestination[colDebit] = drSrc[colDebit];
            drDestination[colCredit] = drSrc[colCredit];

            drDestination[colNominalCode] = drSrc[colNominalCode];

            drDestination[colNotes] = drSrc[colNotes];
            drDestination[colOrder] = 2;//Add these in last of every thing elese

            drDestination[colRecordType] = (int)ReportType.Journal_MultipleEnt;
            drDestination[colDocumentItemID] = drSrc[colDocumentItemID];

            dtReport.Rows.Add(drDestination);
            return drDestination;
        }


        #endregion

        #region SDB
        void AddNewRow_SDB_VAT(DataRow drSrc, int VatNo)
        {
            var colNomCodeID = "Nominal" + VatNo;
            var colTotalVat = "TotalVAT" + VatNo;

            var drDestination = dtReport.NewRow();

            var objNom = lstChartAcc.FirstOrDefault(x => x.ID == (int)drSrc[colNomCodeID]);

            if (objNom != null)
            {
                //-- To supress entries that dont have any VAT amount
                //if (drSrc[colTotalVat] != DBNull.Value && AppConverter.ToFloat(drSrc[colTotalVat], 0) > 0)
                {
                    drDestination[colDate] = drSrc["VatDate"];

                    drDestination[colDebit] = drSrc[colTotalVat];
                    drDestination[colCredit] = 0;

                    drDestination[colNominalCodeID] = drSrc[colNomCodeID];
                    drDestination[colNominalCode] = objNom.Code + " - " + objNom.Description;

                    drDestination[colDescription] = "Output VAT";
                    drDestination[colSource] = string.Empty;
                    drDestination[colDocName] = drSrc[colDocName];
                    drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
                    drDestination[colID] = drSrc[colID];
                    drDestination[colNotes] = string.Empty;
                    drDestination[colOrder] = 2;

                    dtReport.Rows.Add(drDestination);
                }
            }

        }

        void AddNewRow_SDB(DataRow drSrc)
        {
            var colfileNomCodeID = "FileNomCode";
            for (int i = 1; i <= 6; i++)
            {
                AddSDB(i, drSrc);
            }

            var drDestination = dtReport.NewRow();

            var objNom = lstChartAcc.FirstOrDefault(x => x.ID == (int)drSrc[colfileNomCodeID]);

            if (objNom != null)
            {
                drDestination[colDate] = drSrc[colDate];

                drDestination[colDebit] = drSrc["Total"];
                drDestination[colCredit] = 0;

                drDestination[colNominalCodeID] = drSrc[colfileNomCodeID];
                drDestination[colNominalCode] = objNom.Code + " - " + objNom.Description;

                drDestination[colDescription] = "Sales Day Book";
                drDestination[colSource] = string.Empty;
                drDestination[colDocName] = drSrc[colDocName];
                drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
                drDestination[colID] = drSrc[colID];
                drDestination[colNotes] = string.Empty;
                drDestination[colOrder] = 1;

                dtReport.Rows.Add(drDestination);
            }
        }

        void AddSDB(int index, DataRow drSrc)
        {
            string colGross = "Gross" + index;
            string colNominalID = "Nominal" + index;
            string colVATID = "VAT" + index;

            var objNom = lstChartAcc.FirstOrDefault(x => x.ID == (int)drSrc[colNominalID]);

            if (objNom != null)
            {
                var drDestination = dtReport.NewRow();

                drDestination[colDate] = drSrc[colDate];

                drDestination[colDebit] = 0;
                drDestination[colCredit] = drSrc[colGross];

                drDestination[colNominalCodeID] = drSrc[colNominalID];
                drDestination[colNominalCode] = objNom.Code + " - " + objNom.Description;

                drDestination[colDescription] = "SDB - " + objNom.Description;
                drDestination[colSource] = string.Empty;
                drDestination[colDocName] = drSrc[colDocName];
                drDestination[colDocumentItemID] = drSrc[colDocumentItemID];
                drDestination[colID] = drSrc[colID];
                drDestination[colNotes] = string.Empty;
                drDestination[colOrder] = 1;

                dtReport.Rows.Add(drDestination);
            }
        }
        #endregion

        public DataSet ThisYrClosingTrialBal()
        {
            var dt = new DataTable(); // empty table with same schema
            dt.Columns.Add(colNominalCode);
            dt.Columns.Add(colDescription);
            dt.Columns.Add(colItemType);
            dt.Columns.Add(colDebit, typeof(decimal));
            dt.Columns.Add(colCredit, typeof(decimal));
            dt.Columns.Add(colBalance, typeof(decimal));
            dt.Columns.Add(colNominalCodeID, typeof(long));

            dt.Columns.Add(colLastYrDebit, typeof(decimal));
            dt.Columns.Add(colLastYrCredit, typeof(decimal));

            var query = dtReport.AsEnumerable().GroupBy(row => new { NomCodeDescription = row.Field<string>(colNominalCode) });
            foreach (var x in query)
            {
                string name = x.Key.NomCodeDescription;

                string code = name.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                string des = name.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];
                decimal? deb = x.Sum(r => r.Field<decimal?>(colDebit));
                decimal? crd = x.Sum(r => r.Field<decimal?>(colCredit));
                long? currNomCodeID = null;

                var dr = x.FirstOrDefault();
                if (dr != null)
                {
                    currNomCodeID = Convert.ToInt64(dr[colNominalCodeID]);
                }

                decimal bal = (crd.HasValue ? crd.Value : 0) - (deb.HasValue ? deb.Value : 0);

                if (bal == 0)
                {
                    deb = crd = null;
                }
                else if (bal > 0)
                {
                    deb = null;
                    crd = bal;
                }
                else if (bal < 0)
                {
                    deb = Math.Abs(bal);
                    crd = null;
                }

                //dt.Rows.Add(code.Trim(), des, deb, crd, bal, currNomCodeID);

                var newRow = dt.NewRow();
                newRow[colNominalCode] = code.Trim();
                newRow[colDescription] = des;

                if (deb != null)
                    newRow[colDebit] = deb;

                if (crd != null)
                    newRow[colCredit] = crd;

                newRow[colBalance] = bal;

                newRow[colNominalCodeID] = currNomCodeID;
                dt.Rows.Add(newRow);

            }

            CorrectNominalCodeID(dt);

            #region Fill P/B
            LedgerRepository rep = new LedgerRepository();

            var yrEndFol = rep.GetYearEndFolder(docItemID);

            FolderYearEnd = yrEndFol.YearEndDate.Value.ToString(AppConstants.DateFormatYearEnd);

            var chartAcclst = cntChartAcc.FetchByYearEndID(yrEndFol.ID);

            foreach (DataRow drThisYr in dt.Rows)
            {
                var nomCodeVal = drThisYr[colNominalCode].ToString();
                var obj = chartAcclst.FirstOrDefault(x => x.Code == nomCodeVal);
                if (obj != null)
                    drThisYr[colItemType] = obj.Type;
            }
            #endregion

            #region Merge Last Year Values
            //Add Last Years data

            List<tblDocumentItem> lstResult = new List<tblDocumentItem>();
            rep.FindRecursive(yrEndFol.ID, Tags.TagType.LastYearClosingTrialBal, lstResult);
            if (lstResult.Count > 0)
            {
                var lastYrID = lstResult[0].ID;

                string qry = string.Format(@"select tblChartAccount.Code,tblClosingTrialBalance.* from tblClosingTrialBalance 
                                        join tblChartAccount on tblClosingTrialBalance.NominalCodeID = tblChartAccount.ID 
                                        where DocumentItemID = {0};", lastYrID);

                var lastYrDt = DBHelper.ExecuteSQL(qry).Tables[0];

                foreach (DataRow drThisYr in dt.Rows)
                {
                    var nomCodeVal = drThisYr[colNominalCode].ToString();
                    var arrLastYr = AppDataTable.FilterRows("Code", nomCodeVal, lastYrDt);
                    if (arrLastYr.Length > 0)
                    {
                        drThisYr[colLastYrDebit] = arrLastYr[0]["Debit"];
                        drThisYr[colLastYrCredit] = arrLastYr[0]["Credit"];
                    }
                }
            }
            #endregion

            decimal thisYrDeb = 0, thisYrCrd = 0, lastYrDeb = 0,  lastYrCrd = 0;

            foreach (DataRow dr in dt.Rows)
            {
                if(dr[colItemType].ToString().Trim() == "P")
                {
                    thisYrDeb += AppConverter.ToDecimal(dr[colDebit], 0);
                    thisYrCrd += AppConverter.ToDecimal(dr[colCredit], 0);

                    lastYrDeb += AppConverter.ToDecimal(dr[colLastYrDebit], 0);
                    lastYrCrd += AppConverter.ToDecimal(dr[colLastYrCredit], 0);
                }
            }

            thisYrProfit = thisYrCrd - thisYrDeb;
            lastYrProfit = lastYrCrd - lastYrDeb;

            var ds = new DataSet();
            ds.Tables.Add(dt);

            return ds;

        }

        void CorrectNominalCodeID(DataTable dt)//this method corrects NominalCodeID in datatable
        {
            List<tblChartAccount> lstCodes = new List<tblChartAccount>();

            foreach (DataRow dr in dt.Rows)
            {
                var objCode = GetAnalysisCode(dr[colNominalCode].ToString());
                dr[colNominalCodeID] = objCode.ID;
            }
        }

        public decimal NomCodeClosingTrialBal(long docItemID, int nomCodeID)//Get Closing bal of specific Nom Code
        {
            var objCode = cntChartAcc.Find(nomCodeID);

            LedgerReportController c = new LedgerReportController(docItemID);
            c.LoadReportData(new DateTime(1990, 1, 1), new DateTime(2030, 1, 1));
            var ds = c.ThisYrClosingTrialBal();
            decimal bal = 0;
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                if (dr[colNominalCode].ToString().Trim() == objCode.Code.Trim())
                {
                    try
                    {
                        bal = Convert.ToDecimal(dr[colBalance]);
                        //Trial Balance is calculated as crd - deb. 
                        //But in Ledger report balance is deb - crd
                        //To sync this data with ledger report make bal opposite sign.
                        bal = bal * -1;
                    }
                    catch { }

                    break;
                }
            }

            return bal;
        }

        public DataTable JournalReport(ReportType rptType)
        {
            LoadReportData(new DateTime(1999, 1, 1), new DateTime(2030, 1, 1));

            DataView dv = new DataView(dtReport);
            dv.RowFilter = string.Format("{0} = {1} and {2} = {3}", colRecordType, (int)rptType, colDocumentItemID, docItemID);  //colRecordType + " = 'j' and ";
            DataTable newDT = dv.ToTable();

            return newDT;
        }

        public void FilterReport(string codeToFilter)
        {
            codeToFilter = codeToFilter.Trim();
            var lstRowsToRemove = new List<DataRow>();
            foreach (DataRow dr in dtReport.Rows)
            {
                var nomCodeDesc = dr[colNominalCode].ToString();
                var code = nomCodeDesc.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                if (string.Compare(code.Trim(), codeToFilter, true) != 0)
                {
                    lstRowsToRemove.Add(dr);
                }
            }

            lstRowsToRemove.ForEach(x => dtReport.Rows.Remove(x));
        }

        public void FilterDayBooks()//include only payments, receipts, SDB
        {
            var lstRowsToRemove = new List<DataRow>();
            foreach (DataRow dr in dtReport.Rows)
            {
                var fileName = dr[colDocName].ToString();
                var tag = AppConstants.LedgerFolder.GetTagFromFileName(fileName);
                if (tag != Tags.TagType.Payment_FileType && tag != Tags.TagType.Receipts_FileType && tag != Tags.TagType.SDB_FileType)
                {
                    lstRowsToRemove.Add(dr);
                }
            }

            lstRowsToRemove.ForEach(x => dtReport.Rows.Remove(x));
        }
        
    }

}
