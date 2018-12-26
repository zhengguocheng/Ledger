using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DMS;
using Telerik.WinControls.UI;

namespace DMS
{
    public partial class UcLedger : UserControlBase
    {
        long documentItemID = 0;
        tblLedgerController entCntrl = new tblLedgerController();
        tblAnalysisCodeController analysisCnt = new tblAnalysisCodeController();
        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        tblItemInfo itemInfo;

        List<tblAnalysisCode> lstAna;

        string  colID = "colID", colDateName = "colDate", colReference = "colReference", colDetails = "colDetails",
                colDebit = "colDebit", colCredit = "colCredit", colBalance = "colBalance",
                colAnalysisCodeName = "colAnalysisCodeID", colNotes = "colNotes";

        public UcLedger(long _docItemID, string clientName)
        {
            InitializeComponent();
            this.Caption = "Ledger";
            this.crudMessage = new CustomMessages("Ledger Item");
            documentItemID = _docItemID;
            txtClient.Text = clientName;
        }
        private void UcTaskList_Load(object sender, EventArgs e)
        {
            grdItems.GridBehavior = new EnterKeyGridBehaviour();
            var lstInfo = itemInfoCnt.FetchAllByDocumentItemID(documentItemID);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                itemInfo = lstInfo[0];
            }
            //this.DisableCellHighlighting(grdItems); to hoghlight checkbox in editmode
            grdItems.AllowAddNewRow = grdItems.AllowDeleteRow = true;
            
            lstAna = analysisCnt.FetchAll();
            lstAna = lstAna.OrderBy(x => x.Code).ToList<tblAnalysisCode>();

            RefreshGrid();
        }
        void RefreshGrid()
        {
            Telerik.WinControls.UI.GridViewComboBoxColumn drpAna = (Telerik.WinControls.UI.GridViewComboBoxColumn)grdItems.Columns[colAnalysisCodeName];
            drpAna.DataSource = lstAna;
            drpAna.DisplayMember = "Code";
            drpAna.ValueMember = "ID";

            List<tblLedger> lst = entCntrl.FetchAllByDocumentItemID(documentItemID);
            lst = lst.OrderBy(x => x.Date).ToList<tblLedger>();

            grdItems.DataSource = lst;

            CalculateTotals();
            CalculateGridBalance();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
                {
                    tblLedger itm = (tblLedger)grdItems.SelectedRows[0].DataBoundItem;
                    if (entCntrl.Delete(itm.ID))
                    {
                        DisplayManager.DisplayCrudMessage(CrudMessageType.EntityDeleted, crudMessage);
                        RefreshGrid();
                    }
                }
            }
        }
        tblLedger GetItemFromDB(GridViewRowInfo row)
        {
            if (row.Cells[colID].Value == null)
            {
                return new tblLedger();
            }

            long id = Convert.ToInt64(row.Cells[colID].Value);
            tblLedger itm = entCntrl.Find(id);
            return itm;
        }

        void CalculateGridBalance()
        {
            for (int i = 0; i < grdItems.ChildRows.Count; i++)
            {
                GridViewRowInfo row = grdItems.ChildRows[i];
                FindRowBalance(row);                
            }
        }

        void FindRowBalance(GridViewRowInfo row)
        {
            decimal runningBal = 0;
            decimal c, d;
            GridViewRowInfo preRow = null;

            if(itemInfo != null)
            {
                runningBal = itemInfo.OpeningBal.Value;
            }

            if (row.Index == -1 && grdItems.Rows.Count > 0)//New row
            {
                preRow = grdItems.Rows[grdItems.Rows.Count -1];
            }
            else if (row.Index > 0)
            {
                preRow = grdItems.Rows[row.Index - 1];
            }

            if (preRow != null && preRow.Cells[colBalance].Value != null)
                decimal.TryParse(preRow.Cells[colBalance].Value.ToString(), out runningBal);

            if (decimal.TryParse(row.Cells[colCredit].Value.ToString(), out c))
            {
                runningBal = runningBal - c;
            }

            if (decimal.TryParse(row.Cells[colDebit].Value.ToString(), out d))
            {
                runningBal = runningBal + d;
            }

            row.Cells[colBalance].Value = runningBal.ToString();

        }

        private void grdItems_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            tblLedger itm = GetItemFromDB(e.Row);

            itm.Date = Convert.ToDateTime(e.Row.Cells[colDateName].Value);
            itm.Reference = e.Row.Cells[colReference].Value.ToString();
            itm.Details = e.Row.Cells[colDetails].Value.ToString();
            itm.Depit = Convert.ToDecimal(e.Row.Cells[colDebit].Value);
            itm.Credit = Convert.ToDecimal(e.Row.Cells[colCredit].Value);
            //itm.Balance = Convert.ToDecimal(e.Row.Cells[colBalance].Value);
            itm.DocumentItemID = documentItemID;
            itm.Notes = e.Row.Cells[colNotes].Value.ToString();
            itm.AnalysisCodeID = Convert.ToInt32(e.Row.Cells[colAnalysisCodeName].Value);
         
            if (entCntrl.Save(itm))
            {
                //if (e.RowIndex == -1)
                //{
                //    CalculateTotals(itm.GrossTaking.Value, itm.NetTaking.Value, itm.VAT.Value);
                //}
                //else
                //    CalculateTotals();
            }

            e.Row.Cells[colID].Value = itm.ID;

            if (e.Column.Name == colCredit || e.Column.Name == colDebit)
            {
                FindRowBalance(e.Row);
                CalculateTotals();
            }

            grdItems.CurrentRow.ErrorText = String.Empty;
        }

        void ApplyCalculation(tblLedger itm)
        {
            //tblVATRate vrate = lstVat.FirstOrDefault(x => x.ID == itm.VATRateID);
            //    itm.VAT = ((vrate.Percentage / 100) * itm.GrossTaking);
            //    itm.VAT = Math.Round(itm.VAT.Value, 2);

            //    itm.NetTaking = itm.GrossTaking - itm.VAT;
            //    itm.NetTaking = Math.Round(itm.NetTaking.Value, 2);
            //}
        }

        void CalculateTotals(decimal newCredit = 0, decimal newDebit = 0)
        {
            //new row is not added in grid at this time. Add new row values explicitly
            decimal debit = newCredit, credit = newDebit;
            decimal d, c;

            DateTime fromDate = DateTime.MinValue, toDate = DateTime.MinValue, currDate;

            foreach (var row in grdItems.Rows)
            {
                if (decimal.TryParse(row.Cells[colDebit].Value.ToString(),out d))
                    debit += d;

                if (decimal.TryParse(row.Cells[colCredit].Value.ToString(), out c))
                    credit += c;

                if (DateTime.TryParse(row.Cells[colDateName].Value.ToString(), out currDate))
                {
                    if (fromDate == DateTime.MinValue)
                        fromDate = currDate;
                    if (toDate == DateTime.MinValue)
                        toDate = currDate;
                    
                    if (currDate < fromDate)
                        fromDate = currDate;

                    if (currDate > toDate)
                        toDate = currDate;
                }

            }

            txtCredit.Text = credit.ToString("F");
            txtDebit.Text = debit.ToString("F");
            
            if(fromDate > DateTime.MinValue)
                txtFrom.Text = fromDate.ToString(AppConstants.DateFormatShort);
            
            if (toDate > DateTime.MinValue)
                txtTo.Text = toDate.ToString(AppConstants.DateFormatShort);

            if (itemInfo != null && itemInfo.OpeningBal.HasValue)
                txtOpeningBal.Text = itemInfo.OpeningBal.Value.ToString("F");
        }

        private void grdItems_CellValidating(object sender, Telerik.WinControls.UI.CellValidatingEventArgs e)
        {
            if (e.Row == null)//in case of row deleting
                return;

            if (e.RowIndex == -1)//new row
            {
                if (e.Row.Cells[colDateName].Value == null && e.Row.Cells[colReference].Value == null
                    && e.Row.Cells[colDebit].Value == null && e.Row.Cells[colCredit].Value == null
                    && e.Row.Cells[colDetails].Value == null && e.Row.Cells[colNotes].Value == null
                    && e.Row.Cells[colAnalysisCodeName].Value == null)
                    return;
            }

            string error = string.Empty;
            if (!ValidateCell(e.Column.Name, e.Value, ref error))
            {
                grdItems.CurrentRow.ErrorText = error;
                e.Cancel = true;
            }
        }

        bool ValidateCell(string cellName, object val, ref string errorMessage)
        {
            bool isValid = true;

            if (cellName == colDateName && val == null)
            {
                errorMessage = "Please enter valid Date.";
                isValid = false;
            }

            if (cellName == colDebit && !CustomValidator.IsDecimal(val, 2))
            {
                errorMessage = "Please enter valid debit. Max two digits after '.' are allowed.";
                isValid = false;
            }

            if (cellName == colCredit && !CustomValidator.IsDecimal(val, 2))
            {
                errorMessage = "Please enter valid credit. Max two digits after '.' are allowed.";
                isValid = false;
            }

            
            if (cellName == colAnalysisCodeName && !CustomValidator.IsInt(val))
            {
                errorMessage = "Please enter valid Analysis Code.";
                isValid = false;
            }
            return isValid;
        }

        private void grdItems_DefaultValuesNeeded(object sender, Telerik.WinControls.UI.GridViewRowEventArgs e)
        {
            DateTime lastDate = DateTime.Now;
            if (grdItems.Rows.Count > 0)
            {
                lastDate = Convert.ToDateTime(grdItems.Rows[grdItems.Rows.Count - 1].Cells[colDateName].Value);
            }
        
            e.Row.Cells[colDateName].Value = lastDate.ToString(AppConstants.DateFormatShort);
            
            e.Row.Cells[colReference].Value = string.Empty;
            e.Row.Cells[colDebit].Value = "0";
            e.Row.Cells[colCredit].Value = "0";
            e.Row.Cells[colDetails].Value = string.Empty;
            e.Row.Cells[colNotes].Value = string.Empty;
            e.Row.Cells[colAnalysisCodeName].Value = lstAna[0].ID.ToString();
            e.Row.Cells[colBalance].Value = "0";
        }

        private void grdItems_UserDeletingRow(object sender, Telerik.WinControls.UI.GridViewRowCancelEventArgs e)
        {
            bool isdeleted = false;

            if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
            {
                tblLedger itm = GetItemFromDB(e.Rows[0]);
                if (entCntrl.Delete(itm.ID))
                {
                    DisplayManager.DisplayCrudMessage(CrudMessageType.EntityDeleted, crudMessage);
                    RefreshGrid();
                    isdeleted = true;
                }
            }
            if (!isdeleted)
                e.Cancel = true;
        }

        private void grdItems_CurrentRowChanged(object sender, CurrentRowChangedEventArgs e)
        {
            //CalculateTotals();
        }

        private void btnGoBack_Click(object sender, EventArgs e)
        {
            DisplayManager.LoadControl(new UcNavigation(false), true);
        }

        private void grdItems_SortChanged(object sender, GridViewCollectionChangedEventArgs e)
        {
            CalculateGridBalance();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (itemInfo == null)
                itemInfo = new tblItemInfo();
         
            if (ValidateInput())
            {
                itemInfo.OpeningBal = Convert.ToDecimal(txtOpeningBal.Text);
                itemInfo.DocumentItemID = documentItemID;
                itemInfoCnt.Save(itemInfo);

                CalculateGridBalance();

                btnGoBack_Click(null, null);
            }
        }

        private bool ValidateInput()
        {
            decimal d;
            if (!decimal.TryParse(txtOpeningBal.Text, out d))
            {
                DisplayManager.DisplayMessage(CustomMessages.GetValidationMessage("Opening Balance"), MessageType.Error);
                return false;
            }
            return true;

        }
    }
}
