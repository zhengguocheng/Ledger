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

    public partial class UcPCB : LedgerGridBase
    {

        long documentItemID = 0;
        tblPCBController entCntrl = new tblPCBController();

        string colDateName = "colDate", colGrossTakingName = "colGrossTaking", colNotes = "colNotes",
               colNetTakingName = "colNetTaking", colVatRateIDName = "colDrpVATRateID", colVatName = "colVAT",
               colID = "colID", colAnalysisCodeName = "colAnalysisCodeID";

        public UcPCB(long _docItemID)
        {
            InitializeComponent();
            this.Caption = "PCB List";
            this.crudMessage = new CustomMessages("PCB");
            documentItemID = _docItemID;
            BaseSettings(grdItems);
        }
        private void UcTaskList_Load(object sender, EventArgs e)
        {
            this.grdItems.UserAddingRow += new Telerik.WinControls.UI.GridViewRowCancelEventHandler(this.grdItems_UserAddingRow);

            GridCommomSettings();

            LoadVATntblAnalysisCode();

            RefreshGrid();

        }
        void RefreshGrid()
        {
            List<tblPCB> lst = entCntrl.FetchAllByDocumentItemID(documentItemID);
            bool allZero = lst.TrueForAll(x => x.VATRateID == zeroVat.ID);

            if (allZero)
            {
                chkVAT.ToggleState = Telerik.WinControls.Enumerations.ToggleState.Off;
                ShowVATColumns(false);
            }
            else
            {
                chkVAT.ToggleState = Telerik.WinControls.Enumerations.ToggleState.On;
                ShowVATColumns(true);
            }

            Telerik.WinControls.UI.GridViewComboBoxColumn drp = (Telerik.WinControls.UI.GridViewComboBoxColumn)grdItems.Columns[colVatRateIDName];
            drp.DataSource = lstVat;
            drp.DisplayMember = "Percentage";
            drp.ValueMember = "ID";

            Telerik.WinControls.UI.GridViewComboBoxColumn drpAna = (Telerik.WinControls.UI.GridViewComboBoxColumn)grdItems.Columns[colAnalysisCodeName];
            drpAna.DataSource = lstAna;
            drpAna.DisplayMember = "Code";
            drpAna.ValueMember = "ID";

            grdItems.DataSource = lst;

            CalculateTotals();
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (grdItems.SelectedRows.Count > 0)
            {
                if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
                {
                    tblPCB itm = (tblPCB)grdItems.SelectedRows[0].DataBoundItem;
                    if (entCntrl.Delete(itm.ID))
                    {
                        DisplayManager.DisplayCrudMessage(CrudMessageType.EntityDeleted, crudMessage);
                        RefreshGrid();
                    }
                }
            }

            //this.grdItems.Rows[grdItems.RowCount - 1].IsCurrent = true;
            //this.grdItems.Columns[2].IsCurrent = true;
            //this.grdItems.BeginEdit();
        }
        tblPCB GetItemFromDB(GridViewRowInfo row)
        {
            if (row.Cells[colID].Value == null)
            {
                return new tblPCB();
            }

            long id = Convert.ToInt64(row.Cells[colID].Value);
            tblPCB itm = entCntrl.Find(id);
            return itm;
        }

        private void grdItems_CellEndEdit(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            try
            {
                tblPCB itm = GetItemFromDB(e.Row);

                itm.Date = Convert.ToDateTime(e.Row.Cells[colDateName].Value);
                itm.GrossTaking = Convert.ToDecimal(e.Row.Cells[colGrossTakingName].Value);
                itm.NetTaking = Convert.ToDecimal(e.Row.Cells[colNetTakingName].Value);
                itm.VATRateID = Convert.ToInt32(e.Row.Cells[colVatRateIDName].Value);
                itm.VAT = Convert.ToDecimal(e.Row.Cells[colVatName].Value);
                itm.DocumentItemID = documentItemID;
                itm.Notes = e.Row.Cells[colNotes].Value.ToString();
                itm.AnalysisCodeID = Convert.ToInt32(e.Row.Cells[colAnalysisCodeName].Value);



                if (e.Column.Name == colGrossTakingName || e.Column.Name == colVatRateIDName)
                {
                    tblVATRate vrate = lstVat.FirstOrDefault(x => x.ID == itm.VATRateID);
                    if (vrate != null && itm.GrossTaking != null)
                    {
                        ApplyCalculation(itm);
                        e.Row.Cells[colVatName].Value = itm.VAT.Value.ToString();
                        e.Row.Cells[colNetTakingName].Value = itm.NetTaking.ToString();

                        if (e.RowIndex == -1 && lstSplitID.Count > 0)
                        {
                            var v = ProcessSplitRow(e.Row);
                            if(v.HasValue)
                            {
                                e.Row.Cells[colNetTakingName].Value = v.Value.ToString();
                            }
                        }
                    }
                    else
                    {
                        e.Row.Cells[colVatName].Value = null;
                        e.Row.Cells[colNetTakingName].Value = null;
                    }
                }

                if (entCntrl.Save(itm))
                {
                    if (e.RowIndex == -1)
                    {
                        CalculateTotals(itm.GrossTaking.Value, itm.NetTaking.Value, itm.VAT.Value);
                    }
                    else
                        CalculateTotals();
                }
                e.Row.Cells[colID].Value = itm.ID;
                grdItems.CurrentRow.ErrorText = String.Empty;


            }
            catch (Exception ecp)
            {

            }
        }

        void ApplyCalculation(tblPCB itm)
        {
            tblVATRate vrate = lstVat.FirstOrDefault(x => x.ID == itm.VATRateID);
            if (vrate != null && itm.GrossTaking != null)
            {
                itm.VAT = ((vrate.Percentage / 100) * itm.GrossTaking);
                itm.VAT = Math.Round(itm.VAT.Value, 2);

                itm.NetTaking = itm.GrossTaking - itm.VAT;
                itm.NetTaking = Math.Round(itm.NetTaking.Value, 2);
            }
        }

        void CalculateTotals(decimal newGross = 0, decimal newVat = 0, decimal newNet = 0)
        {
            //new row is not added in grid at this time. Add new row values explicitly
            decimal gross = newGross, vat = newVat, net = newNet;

            foreach (var row in grdItems.Rows)
            {
                gross += Convert.ToDecimal(row.Cells[colGrossTakingName].Value);
                net += Convert.ToDecimal(row.Cells[colNetTakingName].Value);
                vat += Convert.ToDecimal(row.Cells[colVatName].Value);
            }

            txtTotalGross.Text = gross.ToString();
            txtNet.Text = net.ToString();
            txtVAT.Text = vat.ToString();
        }

        private void grdItems_CellValidating(object sender, Telerik.WinControls.UI.CellValidatingEventArgs e)
        {
            if (e.Row == null)//in case of row deleting
                return;

            if (e.RowIndex == -1)//new row
            {
                if (e.Row.Cells[colDateName].Value == null && e.Row.Cells[colGrossTakingName].Value == null
                    && e.Row.Cells[colVatRateIDName].Value == null && e.Row.Cells[colVatName].Value == null
                    && e.Row.Cells[colNetTakingName].Value == null && e.Row.Cells[colNotes].Value == null
                    && e.Row.Cells[colAnalysisCodeName].Value == null)
                    return;
            }

            string error = string.Empty;
            if (!ValidateCell(e.Column.Name, e.Value, ref error))
            {
                grdItems.CurrentRow.ErrorText = error;
                e.Cancel = true;
            }

            ValidateSplit(e);
        }

        bool ValidateCell(string cellName, object val, ref string errorMessage)
        {
            bool isValid = true;

            if (cellName == colDateName && val == null)
            {
                errorMessage = "Please enter valid Date.";
                isValid = false;
            }

            if (cellName == colGrossTakingName && !CustomValidator.IsDecimal(val, 2))
            {
                errorMessage = "Please enter valid Gross Taking. Max two digits after '.' are allowed.";
                isValid = false;
            }

            if (cellName == colNetTakingName && !CustomValidator.IsDecimal(val, 2))
            {
                errorMessage = "Please enter valid Net Taking. Max two digits after '.' are allowed.";
                isValid = false;
            }

            if (cellName == colVatRateIDName && !CustomValidator.IsInt(val))
            {
                errorMessage = "Please enter valid Vat Rate.";
                isValid = false;
            }

            if (cellName == colAnalysisCodeName && !CustomValidator.IsInt(val))
            {
                errorMessage = "Please enter valid Analysis Code.";
                isValid = false;
            }

            if (cellName == colVatName && !CustomValidator.IsDecimal(val, 2))
            {
                errorMessage = "Please enter valid Vat. Max two digits after '.' are allowed.";
                isValid = false;
            }

            return isValid;
        }

        private void grdItems_DefaultValuesNeeded(object sender, Telerik.WinControls.UI.GridViewRowEventArgs e)
        {
            this.DefaultValuesNeeded(sender, e);

            #region commented
            //DateTime nextDate = DateTime.Now;
            //string nextChequeNo = string.Empty;

            //if (grdItems.Rows.Count > 0)
            //{
            //    try
            //    {
            //        DateTime lastDate = Convert.ToDateTime(grdItems.Rows[grdItems.Rows.Count - 1].Cells[colDateName].Value);
            //        nextDate = lastDate.AddDays(1);


            //    }
            //    catch (Exception ecp)
            //    {
            //        GlobalLogger.logger.LogException(ecp);
            //    }
            //}

            //e.Row.Cells[colDateName].Value = nextDate.ToString(AppConstants.DateFormatShort);

            //e.Row.Cells[colGrossTakingName].Value = "0";
            //e.Row.Cells[colVatRateIDName].Value = lstVat[0].ID.ToString();
            //e.Row.Cells[colVatName].Value = "0";
            //e.Row.Cells[colNetTakingName].Value = "0";
            //e.Row.Cells[colNotes].Value = string.Empty;
            //e.Row.Cells[colAnalysisCodeName].Value = lstAna[0].ID.ToString();
            #endregion
        }

        private void grdItems_UserDeletingRow(object sender, Telerik.WinControls.UI.GridViewRowCancelEventArgs e)
        {
            bool isdeleted = false;

            if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
            {
                tblPCB itm = GetItemFromDB(e.Rows[0]);
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

        private void chkVAT_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            if (args.ToggleState == Telerik.WinControls.Enumerations.ToggleState.Off)
            {
                List<tblPCB> lst = entCntrl.FetchNonZeroVat(documentItemID, zeroVat.ID);

                if (lst.Count > 0)
                {
                    if (DisplayManager.DisplayMessage("All Vat data will be removed from this form. Do you want to proceed?", MessageType.Confirmation) == DialogResult.Yes)
                    {
                        foreach (var itm in lst)
                        {
                            itm.VAT = 0;
                            itm.VATRateID = zeroVat.ID;
                            ApplyCalculation(itm);
                            entCntrl.Save(itm);
                        }
                        RefreshGrid();
                    }
                    else
                        return;
                }
                //else
                {
                    ShowVATColumns(false);
                }
            }
            else
            {
                ShowVATColumns(true);
            }
        }

        void ShowVATColumns(bool val)
        {
            grdItems.Columns[colVatName].IsVisible = val;
            grdItems.Columns[colVatRateIDName].IsVisible = val;
        }

        private void grdItems_UserAddingRow(object sender, GridViewRowCancelEventArgs e)
        {
            ProcessSplitRow(e.Rows[0]);
            return;

            
        }

        decimal? ProcessSplitRow(GridViewRowInfo row)
        {
            if (row.Index != -1)//if old rows call this method
                return null;

            decimal? remaining = 0;
            tblPCB itm = GetItemFromDB(row);

            decimal netTakingSum = itm.NetTaking.HasValue ? itm.NetTaking.Value : 0;

            if (lstSplitID.Count > 0)
            {
                foreach (long id in lstSplitID)
                {
                    tblPCB preItem = entCntrl.Find(id);
                    if (preItem.NetTaking.HasValue && preItem.ID != itm.ID)
                        netTakingSum += preItem.NetTaking.Value;
                }
            }

            remaining = (itm.GrossTaking - itm.VAT) - netTakingSum;

            if (remaining > 0)
            {
                if (!lstSplitID.Contains(itm.ID))
                    lstSplitID.Add(itm.ID);
            }
            else
            {
                lstSplitID.Clear();
            }

            return remaining;
        }

       
    }
}
