using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using Utilities;

namespace DAL
{
    public class VATReportController
    {
        #region Columns
        public const string col_DocumentItemID = "DocumentItemID";
        public const string col_T1 = "T1";
        public const string col_T2 = "T2";
        public const string col_T3 = "T3";
        public const string col_T4 = "T4";
        public const string col_T5 = "T5";
        public const string col_T6 = "T6";
        public const string col_Name = "Name";
        public const string col_NomCode1 = "NomCode1";
        public const string col_NomCode2 = "NomCode2";
        public const string col_NomCode3 = "NomCode3";
        public const string col_NomCode4 = "NomCode4";
        public const string col_NomCode5 = "NomCode5";
        public const string col_NomCode6 = "NomCode6";
        public const string col_SumVat = "SumVat";
        public const string col_Gross = "Gross";
        public const string col_SheetName = "SheetName";
        public const string col_NomCode = "NomCode";
        public const string col_NomCodeDesc = "NomCodeDesc";
        public const string col_TotalNet = "TotalNet";
        public const string col_PaymentSum = "PaymentSum";
        public const string col_Vat = "Vat";
        public const string col_Net = "Net";
        #endregion

        const string variableCol = "varcol";
        public static string GetCols()
        {
            var output = "";
            var str = "DocumentItemID,T1,T2,T3,T4,T5,T6,Name,NomCode1,NomCode2,NomCode3,NomCode4,NomCode5,NomCode6,SumVat,Gross,DocumentItemID,SheetName,NomCode,NomCodeDesc,TotalNet";
            var arr = str.Split(",".ToCharArray());
            foreach (var item in arr)
            {
                output += string.Format(@"public const string col_{0} = ""{0}"";", item) + Environment.NewLine;
            }
            return output;
        }

        long docItemID;
        DateTime stDate, endDate;
        int stPeriod, endPeriod;

        public string ClientName, FolderYearEnd;

        DataTable dtSDB = new DataTable();
        DataTable dtPayment = new DataTable();
        DataTable dtRecp = new DataTable();
        public decimal PaymentVat, SDB_Vat, RecptVat;
        public decimal PaymentGross, SDB_Gross, RecptGross;

        public decimal PaymentReportGross, PaymentReportVat, PaymentReportNet;
        //public

        public VATReportController(long _docItemID)
        {
            docItemID = _docItemID;
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
            //yrEndFolID = yrEndFol.ID;
            FolderYearEnd = yrEndFol.YearEndDate.Value.ToString(AppConstants.DateFormatYearEnd);
        }

        public void LoadReportData(DateTime _stDate, DateTime _endDate, int _stPeriod, int _endPeriod)
        {
            stDate = _stDate;
            endDate = _endDate;
            stPeriod = _stPeriod;
            endPeriod = _endPeriod;

            PaymentVat = SDB_Vat = 0;
            
            LedgerRepository repCntr = new LedgerRepository();
            var fol = repCntr.GetYearEndFolder(docItemID);

            var ds = DBHelper.ExecuteSP(SPNames.VATReportSP
                , new SqlParameter("@yrEndFolderID", fol.ID)
                , new SqlParameter("@stDate", stDate)
                , new SqlParameter("@endDate", endDate)
                , new SqlParameter("@stPeriod", stPeriod)
                , new SqlParameter("@endPeriod", endPeriod)
                );

            if (ds.Tables.Count >= 1)
                dtSDB = ds.Tables[0];
            if (ds.Tables.Count >= 2)
                dtPayment = ds.Tables[1];
            if (ds.Tables.Count >= 3)
                dtRecp = ds.Tables[2];
        }

        public DataTable GetSDB()
        {
            var dtResult = new DataTable();

            #region Add Columns
            dtResult.Columns.Add(col_DocumentItemID);
            dtResult.Columns.Add(col_Name);
            dtResult.Columns.Add(col_Gross, typeof(decimal));
            dtResult.Columns.Add(col_SumVat, typeof(decimal));

            List<string> lst = new List<string>();
            foreach (DataRow dr in dtSDB.Rows)
            {
                AddDistinct(lst, "N01_" + dr[col_NomCode1].ToString());
                AddDistinct(lst, "N02_" + dr[col_NomCode2].ToString());
                AddDistinct(lst, "N03_" + dr[col_NomCode3].ToString());
                AddDistinct(lst, "N04_" + dr[col_NomCode4].ToString());
                AddDistinct(lst, "N05_" + dr[col_NomCode5].ToString());
                AddDistinct(lst, "N06_" + dr[col_NomCode6].ToString());
            }

            foreach (var item in lst)
            {
                string str = item.Substring(4);//子字符串包含当前索引的字符
                if (!string.IsNullOrWhiteSpace(str))
                {
                    var col = dtResult.Columns.Add(item, typeof(decimal));
                    col.Caption = variableCol;
                }
            }

            #endregion

            foreach (DataRow dr in dtSDB.Rows)
            {
                var drResult = dtResult.NewRow();
                drResult[col_DocumentItemID] = dr[col_DocumentItemID];
                drResult[col_Name] = dr[col_Name];
                drResult[col_Gross] = dr[col_Gross];
                drResult[col_SumVat] = dr[col_SumVat];

                var code = "N01_" + dr[col_NomCode1].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_T1];

                code = "N02_" + dr[col_NomCode2].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_T2];

                code = "N03_" + dr[col_NomCode3].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_T3];

                code = "N04_" + dr[col_NomCode4].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_T4];

                code = "N05_" + dr[col_NomCode5].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_T5];

                code = "N06_" + dr[col_NomCode6].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_T6];

                dtResult.Rows.Add(drResult);
            }

            SDB_Vat = AppDataTable.GetSum(col_SumVat, dtResult);
            SDB_Gross = AppDataTable.GetSum(col_Gross, dtResult);

            return dtResult;
        }

        public DataTable GetReceipts()
        {
            var dtResult = new DataTable();

            #region Add Columns
            dtResult.Columns.Add(col_DocumentItemID);
            dtResult.Columns.Add(col_SheetName);
            dtResult.Columns.Add(col_Gross, typeof(decimal));
            dtResult.Columns.Add(col_SumVat, typeof(decimal));

            foreach (DataRow dr in dtRecp.Rows)
            {
                if (!dtResult.Columns.Contains(dr[col_NomCode].ToString()))
                {
                    dtResult.Columns.Add(dr[col_NomCode].ToString(), typeof(decimal));
                }
            }
            
            #endregion

            foreach (DataRow dr in dtRecp.Rows)
            {
                DataRow drResult = null;
                string qry = string.Format("DocumentItemID = '{0}'", dr[col_DocumentItemID].ToString());
                var arr = dtResult.Select(qry);
                if (arr.Length > 0)
                {
                    drResult = arr[0];
                }
                else
                {
                    drResult = dtResult.NewRow();
                    drResult[col_DocumentItemID] = dr[col_DocumentItemID];
                    drResult[col_SheetName] = dr[col_SheetName];
                    dtResult.Rows.Add(drResult);
                }

                var code = dr[col_NomCode].ToString();
                if (dtResult.Columns.Contains(code))
                {
                    drResult[code] = dr[col_Net];
                    drResult[col_SumVat] = AppConverter.ToDecimal(drResult[col_SumVat], 0) + AppConverter.ToDecimal(dr[col_Vat], 0);
                    drResult[col_Gross] = AppConverter.ToDecimal(drResult[col_Gross], 0) + AppConverter.ToDecimal(dr[col_Net], 0) + +AppConverter.ToDecimal(dr[col_Vat], 0);
                }
            }

            RecptVat = AppDataTable.GetSum(col_SumVat, dtResult);
            RecptGross = AppDataTable.GetSum(col_Gross, dtResult);

            return dtResult;
        }

        void AddDistinct(List<string> lst, string val)
        {
            if (!lst.Exists(x => x == val))
                lst.Add(val);
        }

        public DataTable GetPayment()
        {
            var dtResult = new DataTable();

            #region Add Columns
            dtResult.Columns.Add(col_DocumentItemID);
            dtResult.Columns.Add(col_NomCode);
            dtResult.Columns.Add(col_NomCodeDesc);

            List<string> lst = new List<string>();
            foreach (DataRow dr in dtPayment.Rows)
            {
                AddDistinct(lst, dr[col_SheetName].ToString());
            }

            foreach (var item in lst)
            {
                var col = dtResult.Columns.Add(item, typeof(decimal));
                col.Caption = variableCol;
            }

            dtResult.Columns.Add(col_PaymentSum, typeof(decimal));

            #endregion

            foreach (DataRow dr in dtPayment.Rows)
            {
                DataRow drResult = null;
                string qry = string.Format("NomCode = '{0}'", dr[col_NomCode].ToString());
                var arr = dtResult.Select(qry);
                if (arr.Length > 0)
                {
                    drResult = arr[0];
                }
                else
                {
                    drResult = dtResult.NewRow();
                    drResult[col_DocumentItemID] = dr[col_DocumentItemID];
                    drResult[col_NomCode] = dr[col_NomCode];
                    drResult[col_NomCodeDesc] = dr[col_NomCodeDesc];
                    dtResult.Rows.Add(drResult);
                }

                var code = dr[col_SheetName].ToString();
                if (dtResult.Columns.Contains(code))
                    drResult[code] = dr[col_TotalNet];
            }

            foreach (DataRow dr in dtResult.Rows)
            {
                decimal total = 0;
                foreach (var col in lst)
                {
                    total += AppConverter.ToDecimal(dr[col], 0);
                }
                dr[col_PaymentSum] = total;

                if(dr[col_NomCode].ToString().Trim() == "845")
                {
                    PaymentVat = total;
                }
            }



            PaymentGross = AppDataTable.GetSum(col_PaymentSum, dtResult);


            return dtResult;
        }
        private DataTable GetDataTable(string tableName, long docItemID)
        {
            string query = string.Format("select * from {0} where DocumentItemID = {1};", tableName, docItemID);
            var ds = DBHelper.ExecuteSQL(query);
            return ds.Tables[0];
        }
        public DataTable GetPaymentReport()
        {
            string strDocumentID;
            string clientName;
            long docItemID;
            DataTable dtPaymentReport = new DataTable();
            //1. 先选择pmn文档名称和documentId
            List<long> documentIdlist = new List<long>();
            PaymentReportGross = PaymentReportNet = PaymentReportVat = 0;
            foreach (DataRow dr in dtPayment.Rows)
            {
                if (dr[col_SheetName].ToString().ToUpper().Contains("NET WAGES"))
                    continue;
                strDocumentID = dr[col_DocumentItemID].ToString();

                //尝试进行转换 ，返回一个bool类型。true：转换成功  false：转换失败
                if (!long.TryParse(strDocumentID, out docItemID))
                    continue;

                if (documentIdlist.Contains(docItemID))
                    continue;
                else
                    documentIdlist.Add(docItemID);

                DataTable tempdt = ReportsHelper.PaymentSheet(docItemID, out clientName);
                DataView dv = tempdt.DefaultView;
                //dv.RowFilter = "VATCode IS NOT NULL";

                DataTable dt = dv.ToTable(false);

                dt.Columns.Add("Source");

                if (dtPaymentReport.Rows.Count == 0)
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow tdr = dt.Rows[i];

                        if (string.IsNullOrWhiteSpace(tdr["VATCode"].ToString()))
                        {
                            string splitValue = ">>>>";
                            if (!tdr["Description"].ToString().Contains(splitValue))
                                dt.Rows.RemoveAt(i);
                            else
                            {
                                tdr["Source"] = Path.GetFileNameWithoutExtension(dr[col_SheetName].ToString());
                                PaymentReportGross += AppConverter.ToDecimal(tdr[col_Gross], 0);
                                PaymentReportVat += AppConverter.ToDecimal(tdr[col_Vat], 0);
                                PaymentReportNet += AppConverter.ToDecimal(tdr[col_Net], 0);
                            }
                        }
                        else
                        {
                            tdr["Source"] = Path.GetFileNameWithoutExtension(dr[col_SheetName].ToString());
                            PaymentReportGross += AppConverter.ToDecimal(tdr[col_Gross], 0);
                            PaymentReportVat += AppConverter.ToDecimal(tdr[col_Vat], 0);
                            PaymentReportNet += AppConverter.ToDecimal(tdr[col_Net], 0);
                        }
                    }
                    dtPaymentReport = dt.Copy();
                }
                else
                {
                    for (int i = dt.Rows.Count - 1; i >= 0; i--)
                    {
                        DataRow tdr = dt.Rows[i];
                        if (!string.IsNullOrWhiteSpace(tdr["VATCode"].ToString()))
                        {
                            tdr["Source"] = Path.GetFileNameWithoutExtension(dr[col_SheetName].ToString());
                            dtPaymentReport.ImportRow(tdr);
                            PaymentReportGross += AppConverter.ToDecimal(tdr[col_Gross], 0);
                            PaymentReportVat += AppConverter.ToDecimal(tdr[col_Vat], 0);
                            PaymentReportNet += AppConverter.ToDecimal(tdr[col_Net], 0);
                        }
                        else
                        {
                            string splitValue = ">>>>";
                            if (tdr["Description"].ToString().Contains(splitValue))
                            {
                                tdr["Source"] = Path.GetFileNameWithoutExtension(dr[col_SheetName].ToString());
                                dtPaymentReport.ImportRow(tdr);
                                PaymentReportGross += AppConverter.ToDecimal(tdr[col_Gross], 0);
                                PaymentReportVat += AppConverter.ToDecimal(tdr[col_Vat], 0);
                                PaymentReportNet += AppConverter.ToDecimal(tdr[col_Net], 0);
                            }
                        }
                      
                    }
                }

            }

            return dtPaymentReport;
        }
        //object GetVal(DataTable dtSDB, object docItemID,string code)
        //{
        //    var arr = dtSDB.Select(col_DocumentItemID + " = " + code);
        //    if(arr.Length > 0)
        //    {
        //        object val = DBNull.Value;
        //        var dr = arr[0];

        //        if(dr[col_NomCode1] == code)
        //        {
        //            val = dr[col_T1];
        //        }
        //        else if (dr[col_NomCode2] == code)
        //        {
        //            val = dr[col_T2];
        //        }
        //        else if (dr[col_NomCode3] == code)
        //        {
        //            val = dr[col_T3];
        //        }
        //        else if (dr[col_NomCode4] == code)
        //        {
        //            val = dr[col_T4];
        //        }
        //        else if (dr[col_NomCode5] == code)
        //        {
        //            val = dr[col_T5];
        //        }
        //        else if (dr[col_NomCode6] == code)
        //        {
        //            val = dr[col_T6];
        //        }
        //    }
        //}
    }


}
