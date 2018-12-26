using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Utilities;

namespace DAL.CustomClasses
{
    public class AnalysisInput
    {
        public int InputNo { get; set; }
        public int StartPeriod { get; set; }
        public int EndPeriod { get; set; }
        public string NomCode { get; set; }
        public decimal? PercentTo { get; set; }

        public string GetInfo()
        {
            return string.Format("Period {0} to {1}", StartPeriod, EndPeriod);
        }

        public string PercentToInfo()
        {
            if (!string.IsNullOrWhiteSpace(NomCode))
                return "% to " + NomCode;
            else if (PercentTo > 0)
                return "% to " + PercentTo.Value.ToString("N2");
            else
                return "% to ";
        }
    }
    public class AnalysisReportController
    {
        public LedgerReportController legReportCnt;
        public const string colPercentTo = "PercentTo";
        public const string colPercentGroup = "PercentGroup";
        long docItemID;
        public bool ShowSummary = false;

        public List<AnalysisInput> lstInput = new List<AnalysisInput>();

        public AnalysisReportController(long _docItemID)
        {
            docItemID = _docItemID;
            legReportCnt = new LedgerReportController(docItemID);
        }

        decimal GetNet(decimal? crd, decimal? deb)
        {
            //return Math.Abs((crd ?? 0) - (deb ?? 0));
            return ((crd ?? 0) - (deb ?? 0));
        }

        public DataTable GetReport()
        {
            for (int i = 0; i < lstInput.Count; i++)
            {
                lstInput[i].InputNo = i + 1;
            }

            var dtAna = new DataTable();
            dtAna.Columns.Add(LedgerReportController.colNominalCode);
            if (!ShowSummary)
                dtAna.Columns.Add(LedgerReportController.colDescription);

            foreach (var item in lstInput)
            {
                string colNet, colPerTo, colPerGrp;
                SetColNames(item.InputNo, out colNet, out colPerTo, out colPerGrp);

                dtAna.Columns.Add(colNet, typeof(decimal));
                dtAna.Columns.Add(colPerTo, typeof(decimal));
                dtAna.Columns.Add(colPerGrp, typeof(decimal));
            }

            foreach (var item in lstInput)
            {
                var dt = GetData(item);
                AddData(dtAna, dt, item.InputNo);
            }

            return dtAna;
        }

        void AddData(DataTable dtResult, DataTable dtSrc, int inpNo)
        {
            string colNom, colDes, colNet, colPerTo, colPerGrp;
            SetColNames(inpNo, out colNet, out colPerTo, out colPerGrp);
            foreach (DataRow drSrc in dtSrc.Rows)
            {
                string qry = null;
                if (ShowSummary)
                    qry = string.Format("[{0}] = '{1}'", LedgerReportController.colNominalCode, drSrc[LedgerReportController.colNominalCode].ToString());
                else
                {
                    var desc = drSrc[LedgerReportController.colDescription].ToString();
                    if (desc.Contains('\''))
                    {
                        desc = desc.Replace("'", "''");
                    }
                    qry = string.Format("[{0}] = '{1}' and [{2}] = '{3}'", LedgerReportController.colNominalCode, drSrc[LedgerReportController.colNominalCode].ToString(), LedgerReportController.colDescription, desc);
                }
                DataRow[] arr = null;

                arr = dtResult.Select(qry);

                DataRow drResult;

                if (arr.Length > 0)
                {
                    drResult = arr[0];
                }
                else
                {
                    drResult = dtResult.NewRow();
                    dtResult.Rows.Add(drResult);
                }

                drResult[LedgerReportController.colNominalCode] = drSrc[LedgerReportController.colNominalCode];
                if (!ShowSummary)
                    drResult[LedgerReportController.colDescription] = drSrc[LedgerReportController.colDescription];
                drResult[colNet] = drSrc[LedgerReportController.colNet];
                drResult[colPerTo] = drSrc[colPercentTo];
                drResult[colPerGrp] = drSrc[colPercentGroup];
            }
        }

        void SetColNames(int inpNo, out string colNet, out string colPerTo, out string colPerGrp)
        {
            colNet = LedgerReportController.colNet + inpNo;
            colPerTo = colPercentTo + inpNo;
            colPerGrp = colPercentGroup + inpNo;
        }

        public DataTable GetData(AnalysisInput inp)
        {
            string perCode = inp.NomCode;
            decimal? percentTo = inp.PercentTo;

            legReportCnt.LoadReportData(new DateTime(1980, 1, 1), new DateTime(2020, 12, 31), inp.StartPeriod, inp.EndPeriod);
            var dt = legReportCnt.dtReport;

            #region Totals
            var lstAnaTotals = from row in dt.AsEnumerable()
                               group row by new { nomCode = row.Field<string>(LedgerReportController.colNominalCode) } into grp
                               select new
                               {
                                   NomCode = grp.Key.nomCode,
                                   TotalCrd = grp.Sum(r => r.Field<Decimal?>(LedgerReportController.colCredit)),
                                   TotalDeb = grp.Sum(r => r.Field<Decimal?>(LedgerReportController.colDebit))
                               };

            var dtTotals = new DataTable();
            dtTotals.Columns.Add(LedgerReportController.colNominalCode);
            dtTotals.Columns.Add(LedgerReportController.colNet, typeof(decimal));
            dtTotals.Columns.Add(colPercentTo, typeof(decimal));
            dtTotals.Columns.Add(colPercentGroup, typeof(decimal));

            foreach (var item in lstAnaTotals)
            {
                dtTotals.Rows.Add(item.NomCode, GetNet(item.TotalCrd, item.TotalDeb), null, null);
                if (perCode != null)
                {
                    string nomCode = item.NomCode.Split("-".ToCharArray())[0].Trim();
                    if (nomCode == perCode)
                    {
                        //percentTo = Math.Abs((item.TotalCrd ?? 0) - (item.TotalDeb ?? 0));
                        percentTo = ((item.TotalCrd ?? 0) - (item.TotalDeb ?? 0));
                    }
                }
            }

            foreach (DataRow dr in dtTotals.Rows)
            {
                var total = dr.Field<Decimal?>(LedgerReportController.colNet);
                if (total != 0 && percentTo != 0)
                {
                    dr[colPercentTo] = (total / percentTo.Value) * 100;
                }
            }

            //dtTotals.TableName = "dtTotals";
            //dtTotals.WriteXml("dtTotals.xml");

            if (ShowSummary)
                return dtTotals;

            #endregion



            var lstAna = from row in dt.AsEnumerable()
                         group row by new { nomCode = row.Field<string>(LedgerReportController.colNominalCode), desc = row.Field<string>(LedgerReportController.colDescription).Trim().ToTitleCase() } into grp
                         select new
                         {
                             NomCode = grp.Key.nomCode,
                             Description = grp.Key.desc,
                             TotalCrd = grp.Sum(r => r.Field<Decimal?>(LedgerReportController.colCredit)),
                             TotalDeb = grp.Sum(r => r.Field<Decimal?>(LedgerReportController.colDebit))
                         };

            var dtAna = new DataTable();
            dtAna.Columns.Add(LedgerReportController.colNominalCode);
            dtAna.Columns.Add(LedgerReportController.colDescription);
            dtAna.Columns.Add(LedgerReportController.colCredit, typeof(decimal));
            dtAna.Columns.Add(LedgerReportController.colDebit, typeof(decimal));
            dtAna.Columns.Add(LedgerReportController.colNet, typeof(decimal));
            dtAna.Columns.Add(colPercentTo, typeof(decimal));
            dtAna.Columns.Add(colPercentGroup, typeof(decimal));

            foreach (var item in lstAna)
            {
                var net = GetNet(item.TotalCrd, item.TotalDeb);

                decimal? perTo = null;
                decimal? perGrp = null;
                if (percentTo.HasValue && percentTo != 0)
                {
                    perTo = (net / percentTo.Value) * 100;
                }

                var arr = AppDataTable.FilterRows(LedgerReportController.colNominalCode, item.NomCode, dtTotals);
                if (arr.Length > 0)
                {
                    var total = arr[0].Field<Decimal?>(LedgerReportController.colNet);
                    if (total != 0)
                    {
                        perGrp = net / total * 100;
                        //perGrp = Math.Abs(perGrp.Value);
                        
                    }

                    //if(item.NomCode.StartsWith("772"))
                    //{
                    //    GlobalLogger.LogMessage(string.Format("net = {0}, total = {1}, pergrp = {2}", net, total, perGrp));
                    //}
                }

                dtAna.Rows.Add(item.NomCode, item.Description, item.TotalCrd, item.TotalDeb, net, perTo, perGrp);


            }

            //dtAna.TableName = "dtAna";
            //dtAna.WriteXml("dtAna.xml");

            return dtAna;
        }

    }
}
