using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace DMS
{
    public class LedgerGridBase: UserControlBase
    {
        protected tblAnalysisCodeController analysisCnt = new tblAnalysisCodeController();
        protected List<tblVATRate> lstVat = new List<tblVATRate>();
        protected tblVATRate zeroVat;
        protected List<tblAnalysisCode> lstAna;
        private Telerik.WinControls.UI.RadGridView dataGrid;

        string colDateName = "colDate", colGrossTakingName = "colGrossTaking", colNotes = "colNotes", colDetails = "colDetails",
               colNetTakingName = "colNetTaking", colVatRateIDName = "colDrpVATRateID", colVatName = "colVAT",
               colID = "colID", colAnalysisCodeName = "colAnalysisCodeID", colBankReconcileName = "colReconciliation",
               colChequeNoName = "colChequeNo", colTipsName = "colTips";
        string colPayment = "colPayment", colReceipt = "colReceipt", colTick = "colTick";

        protected List<long> lstSplitID = new List<long>();
        List<string> lstSpltColNames = new List<string>();
        
        public LedgerGridBase()
        {
            lstSpltColNames.Add(colDateName);
            lstSpltColNames.Add(colGrossTakingName);
            lstSpltColNames.Add(colVatRateIDName);
            lstSpltColNames.Add(colVatName);
        }

        protected void BaseSettings(RadGridView grid)
        {
            dataGrid = grid;
        }
        protected void GridCommomSettings()
        {
            dataGrid.GridBehavior = new EnterKeyGridBehaviour();
            dataGrid.AllowAddNewRow = dataGrid.AllowDeleteRow = true;
            this.DisableCellHighlighting(dataGrid);
        }
        
        protected void LoadVATntblAnalysisCode()
        {
            lstVat = new tblVATRateController().FetchAll();
            lstVat = lstVat.OrderBy(x => x.Percentage).ToList<tblVATRate>();
            zeroVat = lstVat.FirstOrDefault(x => x.Percentage == 0);

            lstAna = analysisCnt.FetchAll();
            lstAna = lstAna.OrderBy(x => x.Code).ToList<tblAnalysisCode>();
        }

        protected void DefaultValuesNeeded(object sender, Telerik.WinControls.UI.GridViewRowEventArgs e)
        {
            SetColumnValue(e.Row, colDateName, DateTime.Now.ToString(AppConstants.DateFormatShort), true);

            if (e.Row.Index == -1 && lstSplitID.Count > 0)
                SetColumnValue(e.Row, colGrossTakingName, "0", true);
            else
                SetColumnValue(e.Row, colGrossTakingName, "0", false);

            SetColumnValue(e.Row, colVatRateIDName, lstVat[0].ID.ToString(), true);
            SetColumnValue(e.Row, colVatName, "0", true);
            SetColumnValue(e.Row, colNetTakingName, "0", false);
            SetColumnValue(e.Row, colNotes, string.Empty, false);
            SetColumnValue(e.Row, colDetails, "", true);
            SetColumnValue(e.Row, colAnalysisCodeName, lstAna[0].ID.ToString(), true);
            SetColumnValue(e.Row, colBankReconcileName, "false", false);
            SetColumnValue(e.Row, colChequeNoName, "", false);
            SetColumnValue(e.Row, colPayment, "0", false);
            SetColumnValue(e.Row, colReceipt, "", false);
            SetColumnValue(e.Row, colTick, "false", false);
            SetColumnValue(e.Row, colTipsName, "0", false);
        }

        void SetColumnValue(GridViewRowInfo row,string colName, string defaultValue, bool overidePre)
        {
            if (dataGrid.Columns.Contains(colName))
            {
                if (colName == colChequeNoName)
                {
                    string nextChequeNo = string.Empty;
                    if (dataGrid.Rows.Count > 0)
                    {
                        try
                        {
                            Int64 lastChequeNo = Convert.ToInt64(dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[colChequeNoName].Value);
                            nextChequeNo = (lastChequeNo + 1).ToString();
                        }
                        catch (Exception ecp)
                        {
                            nextChequeNo = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[colChequeNoName].Value.ToString();
                        }
                    }
                    row.Cells[colName].Value = nextChequeNo;
                }
                else
                {
                    row.Cells[colName].Value = defaultValue;
                    if (dataGrid.Rows.Count > 0 && overidePre)
                    {
                        row.Cells[colName].Value = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[colName].Value;
                    }
                }
            }
        }

        //protected string GetPreRowValue(string colName)
        //{
        //    string val = null;
        //    if (dataGrid.Rows.Count > 0)
        //    {
        //        val = dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[colName].Value.ToString();
        //    }
        //    return val;
        //}
        
        protected bool IsPreEqual(CellValidatingEventArgs e)
        {
            bool val = false;
            if (dataGrid.Rows.Count > 0)
            {
                val = e.Value.ToString() == dataGrid.Rows[dataGrid.Rows.Count - 1].Cells[e.Column.Name].Value.ToString();
            }
            return val;
        }

        protected void ValidateSplit(CellValidatingEventArgs e)
        {
            //only apply this to new row otherwise all old records would be effected
            if (e.RowIndex == -1 && lstSplitID.Count > 0 && (e.Column.Name == colDateName || e.Column.Name == colGrossTakingName || e.Column.Name == colVatRateIDName))
            {
                
                if (IsPreEqual(e))
                {
                    return;
                }
                else
                {
                    dataGrid.CurrentRow.ErrorText = string.Format("Split row must have same values in {0}.", e.Column.HeaderText);
                    e.Cancel = true;
                    return;
                }
            }
        }
    }
}
