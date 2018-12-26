using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using Utilities;

namespace DAL
{
    public class NextYrOpeningBalRpt
    {
        const string colCredit = "Credit";
        const string colDebit = "Debit";
        const string colTransferNominalCodeID = "TransferNominalCodeID";
        const string colItemType = "ItemType";
        const string colNotes = "Notes";
        const string colPB = "PB";
        const string colRef = "Ref";
        const string colNominalCode = "NominalCode";
        const string colDecsription = "Description";

        //New
        const string colNominalCodeID = "NominalCodeID";

        public DataTable dtNextYr;
        public DataTable dtPLReserve;

        tblChartAccountController nomCodeCnt = new tblChartAccountController();

        DataTable AddNomCodes(DataTable dt, LedgerReportController c)
        {
            List<tblChartAccount> lstCodes = new List<tblChartAccount>();

            foreach (DataRow dr in dt.Rows)
            {
                var objCode = c.GetAnalysisCode(dr[colNominalCode].ToString());
                if (objCode.YearEndCodeID.HasValue)
                {
                    var objCodeToSrch = nomCodeCnt.Find(objCode.YearEndCodeID.Value);
                    if (objCodeToSrch != null)
                    {
                        var arr = dt.Select(colNominalCode + " = '" + objCodeToSrch.Code + "'", colNominalCode);
                        if (arr.Length == 0)
                        {
                            lstCodes.Add(objCodeToSrch);
                        }
                    }
                }
            }

            foreach (var objCode in lstCodes)
            {
                var dr = dt.NewRow();
                dr[colNominalCode] = objCode.Code;
                dr[colDecsription] = objCode.Description;
                dr[colDebit] = 0;
                dr[colCredit] = 0;
                dr["Balance"] = 0;
                dr[colNominalCodeID] = objCode.ID;
                dt.Rows.Add(dr);
            }

            return dt;
        }
        public void GenReport(long docItemID)
        {
            LedgerReportController c = new LedgerReportController(docItemID);
            c.LoadReportData(new DateTime(1990, 1, 1), new DateTime(2030, 1, 1));
            var dtClosing = c.ThisYrClosingTrialBal().Tables[0];

            dtClosing = AddNomCodes(dtClosing, c);

            dtClosing.Columns.Add(colTransferNominalCodeID);

            if (!dtClosing.Columns.Contains(colItemType))
                dtClosing.Columns.Add(colItemType);
            
            dtClosing.Columns.Add(colNotes);
            dtClosing.Columns.Add(colPB);
            dtClosing.Columns.Add(colRef);

            foreach (DataRow dr in dtClosing.Rows)
            {
                //var objCode = nomCodeCnt.Find(Convert.ToInt64(dr[colNominalCodeID]));

                var objCode = c.GetAnalysisCode(dr[colNominalCode].ToString());
                dr[colItemType] = objCode.Type;
                dr[colTransferNominalCodeID] = objCode.YearEndCodeID;
            }

            if (!AppDataTable.ExistRow(colNominalCode, "991", dtClosing))
            {
                var drPLR = dtClosing.NewRow();

                drPLR[colNominalCode] = "991";
                drPLR[colItemType] = "B";
                drPLR[colDebit] = 0;
                drPLR[colCredit] = 0;
                drPLR[colDecsription] = "P+L reserve retained profit b/f";

                dtClosing.Rows.Add(drPLR);
            }

            if (!AppDataTable.ExistRow(colNominalCode, "995B", dtClosing))
            {
                var drEqtDiv = dtClosing.NewRow();
                drEqtDiv[colNominalCode] = "995B";
                drEqtDiv[colItemType] = "B";
                drEqtDiv[colDebit] = 0;
                drEqtDiv[colCredit] = 0;
                drEqtDiv[colDecsription] = "Equity dividends paid during the year";
                dtClosing.Rows.Add(drEqtDiv);
            }

            var ds = GetSpData(docItemID);
            //dtPLReserve = GetPLReserver(ds.Tables[0]);
            dtPLReserve = GetPLReserver(dtClosing);
            dtNextYr = CalBalance(dtClosing, ds.Tables[1]);

            DataRow newRow = dtNextYr.NewRow();

            var crdTotal = Sum(dtPLReserve, colCredit);
            var debTotal = Sum(dtPLReserve, colDebit);
            var net = crdTotal - debTotal;

            if (net >= 0)
                newRow[colCredit] = net;
            else
                newRow[colDebit] = Math.Abs(net);

            newRow["NominalCode"] = "991";
            newRow["Description"] = "P+L reserve retained profit b/f";
            newRow[colRef] = "b/f";
            dtNextYr.Rows.Add(newRow);
        }

        decimal Sum(DataTable dt, string colName)
        {
            try
            {
                var obj = dt.Compute(string.Format("SUM({0})", colName), string.Empty);
                var d = Convert.ToDecimal(obj);
                return d;
            }
            catch
            {
                return 0;
            }
        }

        private DataTable GetPLReserver(DataTable dtClosingTB)
        {
            var dtCloned = dtClosingTB.Clone();

            foreach (DataRow dr in dtClosingTB.Rows)
            {
                if (dr[colItemType] != DBNull.Value)
                {
                    if (isP(dr))
                    {
                        dr[colPB] = "P";
                        dtCloned.ImportRow(dr);
                    }
                    else if (string.Compare(dr["NominalCode"].ToString().Trim(), "991") == 0 ||
                        string.Compare(dr["NominalCode"].ToString().Trim(), "995B") == 0)
                    {
                        dtCloned.ImportRow(dr);
                    }
                }
            }

            return dtCloned;
        }

        bool isP(DataRow dr)
        {
            var typ = dr[colItemType].ToString().Trim();
            if (string.Compare(typ, "p", true) == 0)
            {
                return true;
            }
            return false;
        }

        bool isB(DataRow dr)
        {
            var typ = dr[colItemType].ToString().Trim();
            if (string.Compare(typ, "b", true) == 0)
            {
                return true;
            }
            return false;
        }
        DataTable CalBalance(DataTable dtClosing, DataTable dtJournals)
        {
            DataTable dtSrc = dtClosing;
            var dtResult = dtSrc.Clone();

            foreach (DataRow dr in dtSrc.Rows)
            {
                if (isB(dr))
                {
                    if (dr[colNominalCode].ToString().Trim() != "751"
                        && dr[colNominalCode].ToString().Trim() != "995B"
                        && dr[colNominalCode].ToString().Trim() != "991")
                    {
                        dr[colRef] = "b/f";
                        dtResult.ImportRow(dr);
                    }
                }
            }

            #region TransferNominalCode

            List<DataRow> rowsToDelete = new List<DataRow>();
            List<DataRow> rowsToAdd = new List<DataRow>();

            foreach (DataRow drProcessing in dtResult.Rows)
            {
                if (drProcessing[colTransferNominalCodeID] != DBNull.Value)
                {
                    if (drProcessing[colNominalCode].ToString() == "662")
                    {

                    }

                    var arr = dtSrc.Select("NominalCodeID = " + drProcessing[colTransferNominalCodeID]);
                    if (arr.Length > 0)
                    {
                        try
                        {
                            var drSrc = arr[0];

                            if (isB(drSrc))//if type B then it means row is already added in dtResult
                            {
                                var rows = dtResult.Select(colNominalCode + " = " + drSrc[colNominalCode]);//get corresponding row that is added into dtResult
                                var drResult = rows[0];

                                //calculate new debit and credit for drResult

                                float netProcessing = AppConverter.ToFloat(drProcessing[colCredit], 0) - AppConverter.ToFloat(drProcessing[colDebit], 0);
                                float netRes = AppConverter.ToFloat(drResult[colCredit], 0) - AppConverter.ToFloat(drResult[colDebit], 0);

                                float net = netRes + netProcessing;
                                if (net >= 0)//if net is positive then set credit
                                {
                                    drResult[colCredit] = Math.Abs(net);//get absolute value
                                }
                                else//if net is negative then set debit
                                {
                                    drResult[colDebit] = Math.Abs(net);//get absolute value
                                }
                                rowsToDelete.Add(drProcessing);//drProcessing should be deleted from dtResult because its amount is added to corresponding row

                            }

                            if (isP(drSrc))//if type P then it means we need to add this row to dtResult
                            {
                                var newRow = dtResult.NewRow();

                                newRow[colRef] = "b/f";
                                newRow[colNominalCode] = drSrc[colNominalCode];
                                newRow[colDecsription] = drSrc[colDecsription];

                                float srcCredit = AppConverter.ToFloat(drProcessing[colCredit], 0);
                                float srcDebit = AppConverter.ToFloat(drProcessing[colDebit], 0);

                                if (srcCredit > 0)
                                {
                                    newRow[colCredit] = drProcessing[colCredit];
                                    newRow[colDebit] = 0;
                                }
                                if (srcDebit > 0)
                                {
                                    newRow[colDebit] = drProcessing[colDebit];
                                    newRow[colCredit] = 0;
                                }

                                //dtResult.Rows.Add(newRow);
                                rowsToAdd.Add(newRow);
                                rowsToDelete.Add(drProcessing);//drProcessing should be deleted from dtResult because its amount is added to corresponding row

                            }
                        }
                        catch { }
                    }
                }
            }

            //Now add rows in dtResult   
            foreach (var dr in rowsToAdd)
            {
                dtResult.Rows.Add(dr);
            }

            //Now delete rows in dtResult   
            foreach (var id in rowsToDelete)
            {
                dtResult.Rows.Remove(id);
            }




            #endregion

            #region Commented TransferNominalCode
            //foreach (DataRow drSrc in dtSrc.Rows)
            //{
            //    if (drSrc[colTransferNominalCodeID] != DBNull.Value)
            //    {
            //        var arr = dtResult.Select("NominalCodeID = " + drSrc[colTransferNominalCodeID]);
            //        if (arr.Length > 0)
            //        {
            //            try
            //            {
            //                var drResult = arr[0];

            //                if (isB(drResult))//if type B then add
            //                {
            //                    float netSrc = AppConverter.ToFloat(drSrc[colCredit], 0) - AppConverter.ToFloat(drSrc[colDebit], 0);
            //                    float netRes = AppConverter.ToFloat(drResult[colCredit], 0) - AppConverter.ToFloat(drResult[colDebit], 0);

            //                    float net = netRes + netSrc;
            //                    if (net >= 0)//if net is positive then set credit
            //                    {
            //                        drResult[colCredit] = Math.Abs(net);//get absolute value
            //                    }
            //                    else//if net is negative then set debit
            //                    {
            //                        drResult[colDebit] = Math.Abs(net);//get absolute value
            //                    }
            //                }
            //                if (isP(drResult))//if type P then overwrite
            //                {
            //                    float srcCredit = AppConverter.ToFloat(drSrc[colCredit], 0);
            //                    float srcDebit = AppConverter.ToFloat(drSrc[colDebit], 0);

            //                    if(srcCredit > 0)
            //                    {
            //                        drResult[colCredit] = drSrc[colCredit];
            //                        drResult[colDebit] = 0;
            //                    }
            //                    if (srcDebit > 0)
            //                    {
            //                        drResult[colDebit] = drSrc[colDebit];
            //                        drResult[colCredit] = 0;
            //                    }
            //                }
            //            }
            //            catch { }
            //        }
            //    }
            //}
            #endregion

            #region Add Journals



            foreach (DataRow drSrc in dtSrc.Rows)
            {
                if (drSrc["NominalCodeID"] == DBNull.Value)
                    continue;

                var arr = dtJournals.Select("IsPrePayment = 1 and CreditNominalCodeID = " + drSrc["NominalCodeID"]);
                var nom = drSrc["NominalCode"];
                foreach (DataRow drJournal in arr)
                {
                    var newRow = dtResult.NewRow();
                    newRow["NominalCode"] = drSrc["NominalCode"];
                    newRow["Description"] = drJournal["Reference"];
                    newRow[colRef] = drJournal["Description"];
                    newRow[colDebit] = drJournal["Amount"];
                    newRow[colNotes] = "Prepayment jouranls.";//"Prepayment jouranls, Reversed back to its original source as debit.";
                    dtResult.Rows.Add(newRow);
                }


                arr = dtJournals.Select("IsAccural = 1 and DebitNominalCodeID = " + drSrc["NominalCodeID"]);

                foreach (DataRow drJournal in arr)
                {
                    var newRow = dtResult.NewRow();
                    newRow["NominalCode"] = drSrc["NominalCode"];
                    newRow["Description"] = drJournal["Reference"];
                    newRow[colRef] = drJournal["Description"];
                    newRow[colCredit] = drJournal["Amount"];
                    newRow[colNotes] = "Accrual jouranls.";
                    dtResult.Rows.Add(newRow);
                }
            }

            foreach (DataRow dr in dtJournals.Rows)
            {
                if (dr["IsPrePayment"].ToString() == "1")
                {
                    var arr = dtResult.Select("NominalCodeID = " + dr["DebitNominalCodeID"]);
                    foreach (var row in arr)
                    {
                        dtResult.Rows.Remove(row);
                    }
                }

                if (dr["IsAccural"].ToString() == "1")
                {
                    var arr = dtResult.Select("NominalCodeID = " + dr["CreditNominalCodeID"]);
                    foreach (var row in arr)
                    {
                        dtResult.Rows.Remove(row);
                    }
                }
            }

            #endregion

            return dtResult;
        }
        DataSet GetSpData(long docItemID)
        {
            LedgerRepository repCntr = new LedgerRepository();
            var fol = repCntr.GetYearEndFolder(docItemID);

            var ds = DBHelper.ExecuteSP(SPNames.SpNextYrOpnBal,
                new SqlParameter("@yrEndFolderID", fol.ID),
                new SqlParameter("@accTagVal", Tags.TagValue(Tags.TagType.AccrualJournal)),
                new SqlParameter("@prePayTagVal", Tags.TagValue(Tags.TagType.PrepaymentJournal))
                );

            return ds;
        }
    }
}
