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
using Utilities.Excel;

namespace DMS.UserControls
{
    public partial class UcSDB : LedgerSheetBase3
    {
        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        tblItemInfo itemInfo;

        tblSDBSettingsController settingsCnt = new tblSDBSettingsController();
        tblSDBSetting settings;

        tblSDBController cnt = new tblSDBController();

        bool allVATNull = true;
        public const string SeperatorChar = "-";
        string sheetType;

        public UcSDB(long _docItemID, string _sheetType)
        {
            InitializeComponent();
            btnExport.Enabled = false;
            #region Load Caption
            VwDocumentList_Controller dcnt = new VwDocumentList_Controller();
            var d = dcnt.Find(_docItemID);
            this.Caption = d.Client_Name + " - " + Path.GetFileNameWithoutExtension(d.Name);
            groupBox1.Text = _sheetType + " " + "Details";
            #endregion

            sheetType = _sheetType;
            TableName = "tblSDB";
            DocumentItemID = _docItemID;

            dpMonth.ApplyDMSSettings();
            dpMonth.SetDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));


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

            lblGross1.Text = null;

            var lstNom = DropDownHelper.BindNominalCode(drpNominalCode, ledgerGrid1.YrEndFolID);

            #region Dropdown bindings

            string qry = string.Format("select ID,Code from tblChartAccount where YearEndFolderID = {0}", ledgerGrid1.YrEndFolID);
            var dtChart = DBHelper.ExecuteSQL(qry).Tables[0];
            DropDownHelper.Bind(drpNom1, dtChart, "ID", "Code");
            DropDownHelper.Bind(drpNom2, dtChart, "ID", "Code");
            DropDownHelper.Bind(drpNom3, dtChart, "ID", "Code");
            DropDownHelper.Bind(drpNom4, dtChart, "ID", "Code");
            DropDownHelper.Bind(drpNom5, dtChart, "ID", "Code");
            DropDownHelper.Bind(drpNom6, dtChart, "ID", "Code");

            qry = string.Format("select ID,Code from tblVATRate where YearEndFolderID = {0}", ledgerGrid1.YrEndFolID);
            var dtVat = DBHelper.ExecuteSQL(qry).Tables[0];
            DropDownHelper.Bind(drpVAT1, dtVat, "ID", "Code");
            DropDownHelper.Bind(drpVAT2, dtVat, "ID", "Code");
            DropDownHelper.Bind(drpVAT3, dtVat, "ID", "Code");
            DropDownHelper.Bind(drpVAT4, dtVat, "ID", "Code");
            DropDownHelper.Bind(drpVAT5, dtVat, "ID", "Code");
            DropDownHelper.Bind(drpVAT6, dtVat, "ID", "Code");

            #endregion

            var dtSrc = GetData();



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
                    dpMonth.SetDate(itemInfo.Date);
                    txtPeriod.Text = itemInfo.Period;
                    txtDescription.Text = itemInfo.BankName;

                    if (itemInfo.AnalysisCodeID.HasValue)
                        DropDownHelper.SelectByValue(drpNominalCode, itemInfo.AnalysisCodeID.Value.ToString());
                    if (itemInfo.LockedByUserID.HasValue)
                        chkLockSheet.Checked = true;
                }
            }

            var lst = settingsCnt.FetchAllByDocumentItemID(DocumentItemID);
            if (lst != null && lst.Count > 0)
            {
                settings = lst[0];
                if (settings != null)
                {
                    LoadNom(drpNom1, settings.Nominal1);
                    LoadNom(drpNom2, settings.Nominal2);
                    LoadNom(drpNom3, settings.Nominal3);
                    LoadNom(drpNom4, settings.Nominal4);
                    LoadNom(drpNom5, settings.Nominal5);
                    LoadNom(drpNom6, settings.Nominal6);

                    LoadNom(drpVAT1, settings.VAT1);
                    LoadNom(drpVAT2, settings.VAT2);
                    LoadNom(drpVAT3, settings.VAT3);
                    LoadNom(drpVAT4, settings.VAT4);
                    LoadNom(drpVAT5, settings.VAT5);
                    LoadNom(drpVAT6, settings.VAT6);

                    chkHideVat.Checked = settingsCnt.IsVATNull(settings);

                    chkOverrideVAT.Checked = settings.OverrideVAT.HasValue ? settings.OverrideVAT.Value : false;

                    txtVAT1.Text = settings.TotalVAT1.HasValue ? settings.TotalVAT1.ToString() : "0";
                    txtVAT2.Text = settings.TotalVAT2.HasValue ? settings.TotalVAT2.ToString() : "0";
                    txtVAT3.Text = settings.TotalVAT3.HasValue ? settings.TotalVAT3.ToString() : "0";
                    txtVAT4.Text = settings.TotalVAT4.HasValue ? settings.TotalVAT4.ToString() : "0";
                    txtVAT5.Text = settings.TotalVAT5.HasValue ? settings.TotalVAT5.ToString() : "0";
                    txtVAT6.Text = settings.TotalVAT6.HasValue ? settings.TotalVAT6.ToString() : "0";
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

        void LoadNom(RadDropDownList drp, decimal? val)
        {
            if (val.HasValue)
                DropDownHelper.SelectByValue(drp, val.ToString());
        }


        void ApplyLock(bool applylock)
        {
            btnSave.Enabled = btnDeleteRow.Enabled = btnInsertRow.Enabled = chkHideVat.Enabled = chkLockSheet.Enabled = !applylock;
            btnUnlock.Visible = applylock;
            if (applylock)
            {
                ledgerGrid1.sSetReadOnly(applylock);
            }
            else
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
            colList.Add(new LedgerColumn("", EnumColumnFormat.Amount, EnumLedgetType.Gross1));
            colList.Add(new LedgerColumn("", EnumColumnFormat.Amount, EnumLedgetType.Gross2));
            colList.Add(new LedgerColumn("", EnumColumnFormat.Amount, EnumLedgetType.Gross3));
            colList.Add(new LedgerColumn("", EnumColumnFormat.Amount, EnumLedgetType.Gross4));
            colList.Add(new LedgerColumn("", EnumColumnFormat.Amount, EnumLedgetType.Gross5));
            colList.Add(new LedgerColumn("", EnumColumnFormat.Amount, EnumLedgetType.Gross6));
            colList.Add(new LedgerColumn("Total", EnumColumnFormat.Amount, EnumLedgetType.Total));

            ledgerGrid1.sAddColumns(colList);

            ledgerGrid1.sGetColumn(EnumLedgetType.Date).Width = 80;

            ledgerGrid1.sGetColumn(EnumLedgetType.Gross1).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Gross2).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Gross3).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Gross4).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Gross5).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Gross6).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Total).Width = 80;

            ledgerGrid1.sApplyColumnWidth();
        }
        private void chkHideVat_CheckedChanged(object sender, EventArgs e)
        {
            if (chkHideVat.Checked)
            {
                if (DisplayManager.DisplayMessage("You would lose all VAT Code and VAT data. Do you still want to proceed?", MessageType.Confirmation) == DialogResult.Yes)
                {
                    DropDownHelper.SelectNull(drpVAT1);
                    DropDownHelper.SelectNull(drpVAT2);
                    DropDownHelper.SelectNull(drpVAT3);
                    DropDownHelper.SelectNull(drpVAT4);
                    DropDownHelper.SelectNull(drpVAT5);
                    DropDownHelper.SelectNull(drpVAT6);

                    drpVAT1.Visible = drpVAT2.Visible = drpVAT3.Visible = drpVAT4.Visible = drpVAT5.Visible = drpVAT6.Visible = false;
                    lblVAT.Visible = lblVATTotal.Visible = txtVAT1.Visible = txtVAT2.Visible = txtVAT3.Visible = txtVAT4.Visible = txtVAT5.Visible = txtVAT6.Visible = false;
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
                drpVAT1.Visible = drpVAT2.Visible = drpVAT3.Visible = drpVAT4.Visible = drpVAT5.Visible = drpVAT6.Visible = true;
                lblVAT.Visible = lblVATTotal.Visible = txtVAT1.Visible = txtVAT2.Visible = txtVAT3.Visible = txtVAT4.Visible = txtVAT5.Visible = txtVAT6.Visible = true;
            }

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
            if (SaveItemInfo() && SaveSettings())
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
                DisplayManager.DisplayMessage(CustomMessages.GetValidationMessage("Period"), MessageType.Error);
                return false;
            }

            if (itemInfo == null)
            {
                itemInfo = new tblItemInfo();

            }

            itemInfo.DocumentItemID = ledgerGrid1.DocumentItemID;
            itemInfo.AnalysisCodeID = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));
            itemInfo.Date = dpMonth.Value;
            itemInfo.Period = txtPeriod.Text;
            itemInfo.BankName = txtDescription.Text;

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

        private bool SaveSettings()
        {

            if (settings == null)
            {
                settings = new tblSDBSetting();
            }

            settings.DocumentItemID = ledgerGrid1.DocumentItemID;

            settings.Nominal1 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNom1));
            settings.Nominal2 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNom2));
            settings.Nominal3 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNom3));
            settings.Nominal4 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNom4));
            settings.Nominal5 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNom5));
            settings.Nominal6 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNom6));

            if (!chkHideVat.Checked)
            {
                settings.VAT1 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVAT1));
                settings.VAT2 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVAT2));
                settings.VAT3 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVAT3));
                settings.VAT4 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVAT4));
                settings.VAT5 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVAT5));
                settings.VAT6 = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVAT6));

                settings.TotalVAT1 = GetVat(txtVAT1, "VAT Total 1");
                settings.TotalVAT2 = GetVat(txtVAT2, "VAT Total 2");
                settings.TotalVAT3 = GetVat(txtVAT3, "VAT Total 3");
                settings.TotalVAT4 = GetVat(txtVAT4, "VAT Total 4");
                settings.TotalVAT5 = GetVat(txtVAT5, "VAT Total 5");
                settings.TotalVAT6 = GetVat(txtVAT6, "VAT Total 6");

                settings.OverrideVAT = chkOverrideVAT.Checked;

            }

            return settingsCnt.Save(settings);
        }

        decimal? GetVat(RadTextBox txtBox, string errorLabel)
        {
            if (string.IsNullOrWhiteSpace(txtBox.Text))
                return null;
            if (CustomValidator.IsDecimal(txtBox.Text))
                return Convert.ToDecimal(txtBox.Text);
            else
                throw new Exception("Please enter valid value in " + errorLabel);
        }

        private void btnLedgerRpt_Click(object sender, EventArgs e)
        {
            var uc = new UcRptPaymentSummary(ledgerGrid1.DocumentItemID, sheetType);
            DisplayManager.LoadControl(uc, false, this);
        }

        private void btnInsertRow_Click(object sender, EventArgs e)
        {
            this.ledgerGrid1.CurrentWorksheet.InsertRows(ledgerGrid1.CurrentWorksheet.FocusPos.Row + 1, 1);
        }

        private void btnDeleteRow_Click(object sender, EventArgs e)
        {
            if (DisplayManager.DisplayMessage(CustomMessages.DeleteConfirmation, MessageType.Confirmation) == DialogResult.Yes)
            {
                var id = ledgerGrid1.sGetID(ledgerGrid1.CurrentWorksheet.FocusPos.Row);
                if (id.HasValue)
                {

                    if (cnt.Delete(id.Value))
                    {
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
            btnExport.Enabled = true;
            foreach (var col in ledgerGrid1.colList)
            {

                decimal gross = 0, vat = 0, net = 0;

                if (col.LedgerType == EnumLedgetType.Date)
                    continue;

                var lblHeader = "lblGross" + col.Index;
                var lblGross = "txtGross" + col.Index;
                var lblVat = "txtVAT" + col.Index;
                var lblNet = "txtNet" + col.Index;

                SetLabelText(lblHeader, ledgerGrid1.sGetHeader(col.LedgerType), true);

                gross = ledgerGrid1.sGetColumnSum(col.LedgerType);
                SetLabelText(lblGross, gross);

                if (chkOverrideVAT.Checked)
                {
                    var txtVATCnt = (RadTextBox)GetControl(lblVat);
                    try
                    {
                        vat = Convert.ToDecimal(txtVATCnt.Text.Trim());
                    }
                    catch
                    {
                        vat = 0;
                    }
                }
                else
                {
                    RadDropDownList drpVatAll = (RadDropDownList)GetControl("drpVAT" + col.Index);
                    if (Convert.ToInt32(DropDownHelper.GetSelectedValue(drpVatAll)) > 0)
                    {
                        var code = DropDownHelper.GetSelectedText(drpVatAll);
                        var strVat = ledgerGrid1.grdDataHelper.CalculateVat(code, gross);
                        SetLabelText(lblVat, strVat);
                        vat = Convert.ToDecimal(strVat);
                    }
                }

                net = gross - vat;

                SetLabelText(lblNet, net);

            }
        }

        void SetLabelText(string lblName, object val, bool isLabel = false)
        {
            Control[] arr = this.Controls.Find(lblName, true);
            if (arr != null && arr.Length > 0)
            {
                if (isLabel)
                {
                    var lbl = (Label)arr[0];
                    lbl.Text = val.ToString();
                }
                else
                {
                    var lbl = (RadTextBox)arr[0];
                    var n = AppConverter.ToDecimal(val, 0);
                    lbl.Text = n.ToString("N2");
                }
            }
        }

        string GetLabelText(string lblName, bool isLabel = false)
        {
            Control[] arr = this.Controls.Find(lblName, true);
            
            if (arr != null && arr.Length > 0)
            {
                if (isLabel)
                {
                    var lbl = (Label)arr[0];
                    return lbl.Text;
                }
                else
                {
                    var lbl = (RadTextBox)arr[0];
                    return lbl.Text;
                }
            }

            return string.Empty;
        }

        Control GetControl(string name)
        {
            Control[] arr = this.Controls.Find(name, true);
            if (arr != null && arr.Length > 0)
            {
                return arr[0];
            }
            return null;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            SaveItemInfo();
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

        private void drpNom_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {
            try
            {
                var drp = (RadDropDownList)sender;
                var val = drp.Text;

                EnumLedgetType typ = EnumLedgetType.Gross1;

                if (drp.Name.EndsWith("2"))
                    typ = EnumLedgetType.Gross2;
                if (drp.Name.EndsWith("3"))
                    typ = EnumLedgetType.Gross3;
                if (drp.Name.EndsWith("4"))
                    typ = EnumLedgetType.Gross4;
                if (drp.Name.EndsWith("5"))
                    typ = EnumLedgetType.Gross5;
                if (drp.Name.EndsWith("6"))
                    typ = EnumLedgetType.Gross6;

                ledgerGrid1.sSetHeader(typ, val);
            }
            catch (Exception ecp) { }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnMonth_Click(object sender, EventArgs e)
        {
            var cnt = new UserControls.UcDateRange();
            cnt.SetEndDate(dpMonth.Value);

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            {
                try
                {
                    ledgerGrid1.sRemoveEvents();

                    int row = 0;//ledgerGrid1.sFirstEmptyRow();
                    int nonEmptyRow = ledgerGrid1.sFirstEmptyRow();

                    if (nonEmptyRow > 0)
                    {
                        if (DisplayManager.DisplayMessage("Do you want to overwrite existing data.", MessageType.Confirmation) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    int col = ledgerGrid1.sGetColumn(EnumLedgetType.Date).Index;

                    for (DateTime d = cnt.StartDate; d <= cnt.EndDate; d = d.AddDays(1))
                    {
                        ledgerGrid1.CurrentWorksheet.SetCellData(row, col, d.ToString(AppConstants.DateFormatShort));
                        row++;
                        if (row > 120)//restrict adding 120 enteries (4 months) at single time
                        {
                            break;
                        }
                    }
                    //set remaining Date cells to empty
                    for (int i = row; i < ledgerGrid1.CurrentWorksheet.RowCount; i++)
                    {
                        ledgerGrid1.CurrentWorksheet.SetCellData(row, col, null);
                        row++;
                    }
                }
                finally
                {
                    ledgerGrid1.sRegesterEvents();
                }
            }

        }

        private void chkOverrideVAT_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (SaveItemInfo())
                UnlockSheet();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            #region GetTotals

            var colName = "Name";
            var dtTotals = new DataTable();
            dtTotals.Columns.Add(colName);

            for (int i = 0; i < 6; i++)
            {
                var lblHeader = "lblGross" + i;
                var txt = GetLabelText(lblHeader, true);
                if (!string.IsNullOrWhiteSpace(txt) && txt.Trim() != "0")
                {
                    dtTotals.Columns.Add(txt);
                }
            }

            for (int r = 0; r < 3; r++)
            {
                var dr = dtTotals.NewRow();
                dtTotals.Rows.Add(dr);

                if (r == 0)
                {
                    dr[colName] = "Total";
                }
                if (r == 1)
                {
                    dr[colName] = "Vat";
                }
                if (r == 2)
                {
                    dr[colName] = "Net";
                }
            }

            //for (int r = 0; r < 3; r++)
            {
                for (int c = 1; c < 6; c++)
                {
                    if (c < dtTotals.Columns.Count)
                    {
                        var lblGross = "txtGross" + c;
                        var lblVat = "txtVAT" + c;
                        var lblNet = "txtNet" + c;
                        dtTotals.Rows[0][c] = GetLabelText(lblGross, false);
                        dtTotals.Rows[1][c] = GetLabelText(lblVat, false);
                        dtTotals.Rows[2][c] = GetLabelText(lblNet, false);
                    }
                }
            }


            #endregion


            var dt = ledgerGrid1.sGetExportDataTable();
            var ds = new DataSet();
            ds.Tables.Add(dt);
            ds.Tables.Add(dtTotals);

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
