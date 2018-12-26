using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using DMS.CustomClasses;
using DAL;
using DAL.Controllers;
using Telerik.WinControls.UI;
using DMS.Reports;
using unvell.ReoGrid;
using unvell.ReoScript;
using System.IO;
using System.Reflection;
using System.ComponentModel;
using Utilities.Excel;

namespace DMS.UserControls
{
    public partial class UcExcelSheet : LedgerSheetBase
    {
        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        tblItemInfo itemInfo;
        bool allVATNull = true;
        public const string SeperatorChar = "-";
        string sheetType;

        public UcExcelSheet(long _docItemID, string _sheetType)
        {
            InitializeComponent();

            #region Load Caption
            VwDocumentList_Controller dcnt = new VwDocumentList_Controller();
            var d = dcnt.Find(_docItemID);
            this.Caption = d.Client_Name + " - " + Path.GetFileNameWithoutExtension(d.Name);
            groupBox1.Text = _sheetType + " " + "Details";
            #endregion

            sheetType = _sheetType;
            TableName = "tblExcelSheet";
            DocumentItemID = _docItemID;

            ledgerGrid1.sInitiatializeGrid(this.Caption);
            ledgerGrid1.DocumentItemID = _docItemID;

            LedgerRepository repCntr = new LedgerRepository();
            var doc = repCntr.GetYearEndFolder(DocumentItemID);
            ledgerGrid1.YrEndFolID = doc.ID;

            ledgerGrid1.grdDataHelper = new LedgerGridData(ledgerGrid1.YrEndFolID);

            CommonSettings(ledgerGrid1, btnSearch, btnClear, btnBack);
            btnUnlock.Visible = false;
        }
        private void UcExcelSheet_Load(object sender, EventArgs e)
        {
            DisplayManager.OnControlLoading = onNewControlLoading;
            this.ParentForm.FormClosing += ParentForm_FormClosing;

            lblTotals.Text = null;
            DropDownHelper.BindNominalCode(drpNominalCode, ledgerGrid1.YrEndFolID);

            var dtSrc = GetData();

            allVATNull = true;
            if (dtSrc.Rows.Count > 0)
            {
                foreach (DataRow dr in dtSrc.Rows)
                {
                    if (dr[EnumLedgetType.VATCode.ToString()] != DBNull.Value)
                    {
                        allVATNull = false;
                        break;
                    }
                }
            }

            chkHideVat.Checked = allVATNull;

            SetColumns();
            ledgerGrid1.sOnParentLoad();
            LoadData();

            this.chkHideVat.CheckedChanged += new System.EventHandler(this.chkHideVat_CheckedChanged);
            this.chkLockSheet.CheckedChanged += new System.EventHandler(this.chkLockSheet_CheckedChanged);
            timer1.Enabled = true;
        }

        void LoadData()
        {
            var lstInfo = itemInfoCnt.FetchAllByDocumentItemID(DocumentItemID);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                itemInfo = lstInfo[0];
                if (itemInfo != null)
                {
                    txtPeriod.Text = itemInfo.Period;
                    if (itemInfo.AnalysisCodeID.HasValue)
                        DropDownHelper.SelectByValue(drpNominalCode, itemInfo.AnalysisCodeID.Value.ToString());
                    if (itemInfo.LockedByUserID.HasValue)
                        chkLockSheet.Checked = true;
                }
            }

            ledgerGrid1.sBindData(GetData());

            UnlockSheet();
        }

        void UnlockSheet()
        {
            if (itemInfo != null && itemInfo.LockedByUserID.HasValue
                //&& itemInfo.LockedByUserID.Value != AuthenticationService.LoginUser.ID
                )
            {
                ApplyLock(true);
                UserController userCnt = new UserController();
                var usr = userCnt.Find(itemInfo.LockedByUserID.Value);

                string msg = string.Format("This sheet is locked by {0}. Do you want to unlock?", usr.FirstName + " " + usr.LastName);

                if (DisplayManager.DisplayMessage(msg, MessageType.Confirmation) == DialogResult.Yes)
                {
                    UcUnLockSheet uc = new UcUnLockSheet();
                    if (DisplayManager.ShowDialouge(uc, "Unlock Sheet") == DialogResult.OK)
                    {
                        if (uc.InputString == usr.SheetPassword)
                        {
                            ApplyLock(false);
                            SaveItemInfo();
                        }
                        else
                        {
                            DisplayManager.DisplayMessage("Password is incorrect. Please go to your profile in 'User Management' and recheck your 'Sheet Password'.", MessageType.Error);
                        }
                    }
                }
            }
        }
        void ApplyLock(bool applylock)
        {
            btnSave.Enabled = btnDeleteRow.Enabled = btnInsertRow.Enabled = chkHideVat.Enabled = chkLockSheet.Enabled = !applylock;
            btnUnlock.Visible = applylock;
            ledgerGrid1.sSetReadOnly(applylock);

            if (!applylock)
            {
                //Uncheck chkLockSheet
                enableCheckEvent = false;
                chkLockSheet.Checked = false;
                enableCheckEvent = true;

            }
        }
        void SetColumns()
        {
            //ledgerGrid1.CurrentWorksheet.Reset();
            //ledgerGrid1.VatHidden = chkHideVat.Checked;

            var colList = new List<LedgerColumn>();

            colList.Add(new LedgerColumn("Date", EnumColumnFormat.Date, EnumLedgetType.Date, true));
            colList.Add(new LedgerColumn("Description", EnumColumnFormat.Text, EnumLedgetType.Description));
            colList.Add(new LedgerColumn("Reference", EnumColumnFormat.Text, EnumLedgetType.Reference, false));
            colList.Add(new LedgerColumn("Gross", EnumColumnFormat.Amount, EnumLedgetType.Gross, true));

            if (!chkHideVat.Checked)
            {
                colList.Add(new LedgerColumn(VATCodeColumnName, EnumColumnFormat.Text, EnumLedgetType.VATCode, false));
                colList.Add(new LedgerColumn("VAT", EnumColumnFormat.Amount, EnumLedgetType.VAT, false));
            }

            colList.Add(new LedgerColumn("Net", EnumColumnFormat.Amount, EnumLedgetType.Net, true));
            colList.Add(new LedgerColumn("Nominal Code", EnumColumnFormat.Text, EnumLedgetType.NominalCode, true));
            colList.Add(new LedgerColumn("Comment", EnumColumnFormat.Text, EnumLedgetType.Comment));

            ledgerGrid1.sAddColumns(colList);

            ledgerGrid1.sGetColumn(EnumLedgetType.Date).Width = 80;

            if (!chkHideVat.Checked)
            {
                ledgerGrid1.sGetColumn(EnumLedgetType.VATCode).Width = 80;
                ledgerGrid1.sGetColumn(EnumLedgetType.VAT).Width = 80;
            }
            ledgerGrid1.sGetColumn(EnumLedgetType.Gross).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Net).Width = 80;

            ledgerGrid1.sGetColumn(EnumLedgetType.Description).Width = 210;
            ledgerGrid1.sGetColumn(EnumLedgetType.Comment).Width = 190;



            ledgerGrid1.sApplyColumnWidth();
        }
        private void chkHideVat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHideVat.Checked)
            {
                var vat = ledgerGrid1.sGetColumnSum(EnumLedgetType.VAT);
                if (vat != 0)
                {
                    DisplayManager.DisplayMessage("Please remove all VAT entries before hiding VAT column.", MessageType.Error);
                    this.chkHideVat.CheckedChanged -= new System.EventHandler(this.chkHideVat_CheckedChanged);
                    chkHideVat.Checked = false;
                    this.chkHideVat.CheckedChanged += new System.EventHandler(this.chkHideVat_CheckedChanged);
                    return;
                }

                if (DisplayManager.DisplayMessage("You would lose all VAT Code and VAT data. Do you still want to proceed?", MessageType.Confirmation) == DialogResult.Yes)
                {
                    ledgerGrid1.sRemoveCol(EnumLedgetType.VATCode);
                    ledgerGrid1.sRemoveCol(EnumLedgetType.VAT);
                }
                else
                {
                    this.chkHideVat.CheckedChanged -= new System.EventHandler(this.chkHideVat_CheckedChanged);
                    chkHideVat.Checked = false;
                    this.chkHideVat.CheckedChanged += new System.EventHandler(this.chkHideVat_CheckedChanged);
                }
            }
            else
            {
                ledgerGrid1.sInsertColumn(new LedgerColumn(VATCodeColumnName, EnumColumnFormat.Dropdown, EnumLedgetType.VATCode, false), 4);
                ledgerGrid1.sInsertColumn(new LedgerColumn("VAT", EnumColumnFormat.Amount, EnumLedgetType.VAT, false), 5);
            }

            ledgerGrid1.sSetNominal();
           
        }
        private void drpNominalCode_TextChanged(object sender, EventArgs e)
        {
            string code = drpNominalCode.Text;
            if (!string.IsNullOrWhiteSpace(code) && code.Contains(SeperatorChar))
            {
                code = code.Split(SeperatorChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].TrimEnd();
            }
            ledgerGrid1.NominalCode = code;
        }
        private void btnSave_MouseClick(object sender, MouseEventArgs e)
        {
            if (SaveItemInfo())
            {
                var lst = ledgerGrid1.sValidateFile();
                if (lst.Count > 0)
                {
                    DisplayManager.DisplayMessage(lst.Count + " rows are invalid. Please remove errors before saving file", MessageType.Error);
                }
                else
                {
                    ledgerGrid1.sSaveData();
                    LoadData();
                    DisplayManager.DisplayMessage("File has been saved.", MessageType.Success);
                    MessageBox.Show("File has been saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool SaveItemInfo()
        {
            ClearErrorProvider();
            if (!CustomValidator.IsInt(txtPeriod.Text.Trim()))
            {
                ShowValidationError(txtPeriod, CustomMessages.GetValidationMessage("Period"));
                return false;
            }

            if (itemInfo == null)
            {
                itemInfo = new tblItemInfo();

            }

            itemInfo.DocumentItemID = ledgerGrid1.DocumentItemID;
            itemInfo.AnalysisCodeID = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));
            itemInfo.Period = txtPeriod.Text;

            if (chkLockSheet.Checked)
            {
                itemInfo.LockedByUserID = AuthenticationService.LoginUser.ID;
            }
            else
            {
                itemInfo.LockedByUserID = null;
            }

            try
            {
                if (!itemInfo.YearEndFolderID.HasValue)
                {
                    LedgerRepository repCntr = new LedgerRepository();
                    var doc = repCntr.GetYearEndFolder(ledgerGrid1.DocumentItemID);
                    if (doc != null)
                    {
                        itemInfo.YearEndFolderID = doc.ID;
                    }
                }
            }
            catch { }

            return itemInfoCnt.Save(itemInfo);
        }

        private void btnLedgerRpt_Click(object sender, EventArgs e)
        {
            var uc = new UcRptPaymentSummary(ledgerGrid1.DocumentItemID, sheetType);
            DisplayManager.LoadControl(uc, false, this);
        }

        private void btnInsertRow_Click(object sender, EventArgs e)
        {
            //ledgerGrid1.sSetAllColumnFormat();
            //this.ledgerGrid1.CurrentWorksheet.InsertRows(ledgerGrid1.CurrentWorksheet.FocusPos.Row + 1, 1);
            //ledgerGrid1.sSetAllColumnFormat();
            //ledgerGrid1.sSetNominal();

            ledgerGrid1.InsertRows(1);
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
            {

                var id = ledgerGrid1.sGetID(ledgerGrid1.CurrentWorksheet.FocusPos.Row);
                if (id.HasValue)
                {
                    tblExcelSheetController cnt = new tblExcelSheetController();
                    if (cnt.Delete(id.Value))
                    {
                        //Delete all split rows whose Split parent rows are deleted
                        DBHelper.ExecuteSP_NoRead(SPNames.DeleteOrphanSplitRows);

                        DisplayManager.DisplayMessage(CustomMessages.GenericRecordDeleted, MessageType.Success);
                        LoadData();
                    }
                }
                else
                    ledgerGrid1.CurrentWorksheet.DeleteRows(ledgerGrid1.CurrentWorksheet.FocusPos.Row, 1);
            }
        }

        private void RefreshTotals()
        {
            float gross = 0, vat = 0, net = 0;
            gross = ledgerGrid1.sGetColumnSum(EnumLedgetType.Gross);
            net = ledgerGrid1.sGetColumnSum(EnumLedgetType.Net);

            if (chkHideVat.Checked)
                lblTotals.Text = string.Format("Gross = {0},  Net = {1}", gross, net);
            else
            {
                vat = ledgerGrid1.sGetColumnSum(EnumLedgetType.VAT);
                lblTotals.Text = string.Format("Gross = {0},  VAT = {1},   Net = {2}", gross, vat, net);
            }
        }

        bool enableCheckEvent = true;
        private void chkLockSheet_CheckedChanged(object sender, EventArgs e)
        {
            if (!enableCheckEvent)
                return;

            if (chkLockSheet.Checked)
            {
                bool isValidOperation = true;
                if (ledgerGrid1.IsEdited)
                {
                    DisplayManager.DisplayMessage("Please save this sheet before locking.", MessageType.Error);
                    isValidOperation = false;
                }
                if (!AuthenticationService.IsAdminLogin())
                {
                    DisplayManager.DisplayMessage("Only administrator can lock this sheet.", MessageType.Error);
                    isValidOperation = false;
                }
                if (string.IsNullOrWhiteSpace(AuthenticationService.LoginUser.SheetPassword))
                {
                    DisplayManager.DisplayMessage("Please add your 'Sheet Password' at your 'User profile' page.", MessageType.Error);
                    isValidOperation = false;
                }

                if (isValidOperation == false)
                {
                    //Uncheck chkLockSheet
                    enableCheckEvent = false;
                    chkLockSheet.Checked = false;
                    enableCheckEvent = true;
                }

                if (isValidOperation)
                {
                    if (SaveItemInfo())
                    {
                        DisplayManager.DisplayDeskTopAlert("You have successfully locked this sheet.");
                        ApplyLock(true);
                    }
                    else
                    {
                        //Uncheck chkLockSheet
                        enableCheckEvent = false;
                        chkLockSheet.Checked = false;
                        enableCheckEvent = true;

                    }
                }
            }
            else
            {
                SaveItemInfo();
                DisplayManager.DisplayDeskTopAlert("You have un-locked this sheet.");
            }
        }

        public bool onNewControlLoading()
        {
            if (ledgerGrid1.IsEdited)
            {
                DisplayManager.DisplayMessage("Please save this sheet before leaving.", MessageType.Error);
                return false;
            }

            return true;
        }

        void ParentForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!onNewControlLoading())
                e.Cancel = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                timer1.Enabled = false;
                RefreshTotals();
            }
            catch { }
            finally
            {
                timer1.Enabled = true;
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            UnlockSheet();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            
            //ledgerGrid1.CurrentWorksheet.ExportAsCSV("exp.csv");
            var dt = ledgerGrid1.sGetExportDataTable();
            var ds = new DataSet();
            ds.Tables.Add(dt);

            saveFileDialog1.FileName = "Export " + this.Caption + ".xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string fileName = saveFileDialog1.FileName;
                if (ExcelExport.Export(ds, fileName))
                {
                    DisplayManager.DisplayMessage("Data exported successfuly.", MessageType.Success);
                    if (DisplayManager.DisplayMessage("Data is exported to specified location. Do you want to open exported file?", MessageType.Confirmation) == DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(fileName);
                    }
                }
            }
        }

    }
}
