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
using unvell.ReoGrid.CellTypes;
using System.Drawing;

namespace DMS.UserControls
{
    public partial class UcBankReconcile : LedgerSheetBaseBankReconcile
    {
        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        tblExcelSheetController cnt = new tblExcelSheetController();
        tblItemInfo itemInfo;
        public const string SeperatorChar = "-";
        string clntName;
        public UcBankReconcile(long _docItemID)
        {
            if (_docItemID <= 0)//if _docItemID = 0 it means we are going to open pre control so no need to load new control
                return;

            InitializeComponent();

            chkShowAll.Checked = true;
            this.chkShowAll.CheckedChanged += new System.EventHandler(this.chkShowAll_CheckedChanged);

            #region Load Caption
            VwDocumentList_Controller dcnt = new VwDocumentList_Controller();
            var d = dcnt.Find(_docItemID);
            clntName = d.Client_Name;
            this.Caption = d.Client_Name + " - " + Path.GetFileNameWithoutExtension(d.Name);
            groupBox1.Text = "Bank Reconcile";
            #endregion

            TableName = "tblExcelSheet";
            DocumentItemID = _docItemID;

            ledgerGrid1.sInitiatializeGrid(this.Caption);
            ledgerGrid1.DocumentItemID = _docItemID;

            LedgerRepository repCntr = new LedgerRepository();
            var doc = repCntr.GetYearEndFolder(DocumentItemID);
            ledgerGrid1.YrEndFolID = doc.ID;

            ledgerGrid1.grdDataHelper = new LedgerGridData(ledgerGrid1.YrEndFolID);

            CommonSettings(ledgerGrid1, btnSearch, btnClear, btnBack);
            DropDownHelper.CommonSetting(drpNominalCode);
            dpRecDate.SetDate(DateTime.Now);

            //ledgerGrid1.CurrentWorksheet.ColumnHeaders[5].DefaultCellBody = typeof(unvell.ReoGrid.CellTypes.CheckBoxCell);
            //ledgerGrid1.CurrentWorksheet.ColumnHeaders[5].Style.HorizontalAlign = ReoGridHorAlign.Center;
            //ledgerGrid1.CurrentWorksheet.ColumnHeaders[5].Style.Padding = new unvell.ReoGrid.PaddingValue(2); //new System.Windows.Forms.Padding(2);

        }
        private void UcExcelSheet_Load(object sender, EventArgs e)
        {
            DisplayManager.OnControlLoading = onNewControlLoading;
            this.ParentForm.FormClosing += ParentForm_FormClosing;

            lblTotals.Text = null;
            DropDownHelper.BindNominalCode(drpNominalCode, ledgerGrid1.YrEndFolID);

            drpNominalCode.SelectedText = "771";//set 771 as default
            DropDownHelper.CorrectInvalidSearchTerm(drpNominalCode);//select the value

            SetColumns();
            LoadSort();
            ledgerGrid1.sOnParentLoad();
            LoadData();

            timer1.Enabled = true;
        }

        void LoadSort()
        {
            foreach (LedgerColumn col in ledgerGrid1.colList)
            {
                drpSort.Items.Add(col.Name);
            }
            drpSort.SelectedIndex = 0;
        }

        void LoadData()
        {
            var lstInfo = itemInfoCnt.FetchAllByDocumentItemID(DocumentItemID);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                itemInfo = lstInfo[0];
                if (itemInfo != null)
                {
                    //txtStPeriod.Text = itemInfo.Period;
                    //if (itemInfo.AnalysisCodeID.HasValue)
                    //    DropDownHelper.SelectByValue(drpNominalCode, itemInfo.AnalysisCodeID.Value.ToString());
                }
            }
        }

        void SetColumns()
        {
            //ledgerGrid1.CurrentWorksheet.Reset();
            //ledgerGrid1.VatHidden = chkHideVat.Checked;

            var colList = new List<LedgerColumn>();

            colList.Add(new LedgerColumn("Date", EnumColumnFormat.Date, EnumLedgetType.Date, true));
            colList.Add(new LedgerColumn("Description", EnumColumnFormat.Text, EnumLedgetType.Description));
            colList.Add(new LedgerColumn("Reference", EnumColumnFormat.Text, EnumLedgetType.Reference));
            colList.Add(new LedgerColumn("Payments", EnumColumnFormat.Amount, EnumLedgetType.Payments));
            colList.Add(new LedgerColumn("Receipts", EnumColumnFormat.Amount, EnumLedgetType.Receipts));
            colList.Add(new LedgerColumn("Tick", EnumColumnFormat.CheckBox, EnumLedgetType.Tick));
            colList.Add(new LedgerColumn("View", EnumColumnFormat.Button, EnumLedgetType.ViewSource));

            ledgerGrid1.sAddColumns(colList);

            ledgerGrid1.sGetColumn(EnumLedgetType.Date).Width = 100;
            ledgerGrid1.sGetColumn(EnumLedgetType.Description).Width = 250;
            ledgerGrid1.sGetColumn(EnumLedgetType.Reference).Width = 150;

            ledgerGrid1.sGetColumn(EnumLedgetType.Payments).Width = 120;
            ledgerGrid1.sGetColumn(EnumLedgetType.Receipts).Width = 120;
            ledgerGrid1.sGetColumn(EnumLedgetType.Tick).Width = 40;
            ledgerGrid1.sGetColumn(EnumLedgetType.ViewSource).Width = 40;

            ledgerGrid1.sApplyColumnWidth();

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
                    btnShowData_Click(null, null);
                    DisplayManager.DisplayMessage("File has been saved.", MessageType.Success);
                    MessageBox.Show("File has been saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        bool ValidateInput()
        {
            ClearErrorProvider();

            if (DropDownHelper.IsEmpty(drpNominalCode))
            {
                ShowValidationError(drpNominalCode, CustomMessages.GetValidationMessage("Nominal Code"));
                return false;
            }
            if (!CustomValidator.IsInt(txtStPeriod.Text.Trim()))
            {
                ShowValidationError(txtStPeriod, CustomMessages.GetValidationMessage("Start Period"));
                return false;
            }
            if (!CustomValidator.IsInt(txtEndPeriod.Text.Trim()))
            {
                ShowValidationError(txtEndPeriod, CustomMessages.GetValidationMessage("End Period"));
                return false;
            }
            return true;
        }

        private bool SaveItemInfo()
        {
            ClearErrorProvider();
            if (!CustomValidator.IsInt(txtStPeriod.Text.Trim()))
            {
                ShowValidationError(txtStPeriod, CustomMessages.GetValidationMessage("Period"));
                return false;
            }

            if (itemInfo == null)
            {
                itemInfo = new tblItemInfo();

            }

            itemInfo.DocumentItemID = ledgerGrid1.DocumentItemID;
            itemInfo.AnalysisCodeID = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));
            itemInfo.Period = txtStPeriod.Text;

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
            if (ValidateInput())
            {
                btnShowData_Click(null, null);
                RefreshTotals();
                var uc = new UcRptReconcile(ledgerGrid1.DocumentItemID);
                uc.clientName = clntName;
                uc.reconcileDate = dpRecDate.Value;
                uc.NomCodeDesc = DropDownHelper.GetSelectedText(drpNominalCode);
                uc.NominalCodeID = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));
                uc.acctBal = Convert.ToDecimal(txtAcctBal.Text);
                uc.outPayments = Convert.ToDecimal(txtOutPayment.Text);
                uc.outRecp = Convert.ToDecimal(txtOutRec.Text);
                uc.balPerStm = Convert.ToDecimal(txtBalPerSt.Text);
                uc.stPeriod = Convert.ToInt32(txtStPeriod.Text);
                uc.endPeriod = Convert.ToInt32(txtEndPeriod.Text);
                DisplayManager.LoadControl(uc, false, this);
            }
        }

        private void RefreshTotals()
        {
            try
            {
                ledgerGrid1.sCalSum();
                txtOutPayment.Text = ledgerGrid1.outPay.ToString("n2");
                txtOutRec.Text = ledgerGrid1.outRec.ToString("n2");
                txtAcctBal.Text = acctBal.ToString("n2");
                txtBalPerSt.Text = (acctBal + ledgerGrid1.outPay - ledgerGrid1.outRec).ToString("n2");
                //modify by @zgc 2018年11月8日20:26:57
                if (Convert.ToDouble(txtBalPerSt.Text.ToSingle()) != Convert.ToDouble(txtStatementBal.Text.ToSingle()))
                {
                    txtBalPerSt.ForeColor = System.Drawing.Color.Red;
                    txtStatementBal.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    txtBalPerSt.ForeColor = System.Drawing.Color.Green;
                    txtStatementBal.ForeColor = System.Drawing.Color.Green;
                }
                //end modify by @zgc 2018年11月8日20:27:18
                lblTotals.Text = string.Format("Total Payments = {0},    Total Receipts = {1},    Outstanding Payments = {2},    Outstanding Receipts = {3}", ledgerGrid1.totalPay, ledgerGrid1.totalRec, ledgerGrid1.outPay, ledgerGrid1.outRec);
            }
            catch (Exception ecp)
            { }
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

        decimal acctBal = 0;
        public override DataTable GetData()
        {
            var nom = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));

            LedgerReportController c = new LedgerReportController(DocumentItemID);
            acctBal = c.NomCodeClosingTrialBal(DocumentItemID, nom);

            bool? ticked = null;
            if (chkShowAll.Checked)
                ticked = null;
            else if (chkShowTick.Checked)
                ticked = true;
            else if (chkShowUntick.Checked)
                ticked = false;

            var dt = cnt.GetReconcileData(DocumentItemID, Convert.ToInt32(txtStPeriod.Text), Convert.ToInt32(txtEndPeriod.Text), nom, ticked);

            var dv = dt.AsDataView();
            dv.Sort = drpSort.Text;
            dt = dv.ToTable();

            return dt;
        }
        private void btnShowData_Click(object sender, EventArgs e)
        {
            if (ValidateInput())
            {
                ledgerGrid1.sBindData(GetData());
            }

        }
        private void chkShowAll_CheckedChanged(object sender, EventArgs e)
        {
            btnShowData_Click(null, null);
        }

        protected override void Dispose(bool disposing)
        {
            
        }

        public override void ControlClosing()
        {
            if (ledgerGrid1 != null && ledgerGrid1.IsEdited)
                ledgerGrid1.sSaveData();
        }
    }
}
