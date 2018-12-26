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
using System.Drawing;

namespace DMS.UserControls 
{
    public partial class UcClosingTrialBalance : LedgerSheetBaseClosingTrialBal
    {
        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        tblItemInfo itemInfo;
        public const string SeperatorChar = "-";
        //string sheetType;
        double difference = 0;

        public UcClosingTrialBalance(long _docItemID, Tags.TagType type)
        {
            InitializeComponent();

            #region Load Caption
            VwDocumentList_Controller dcnt = new VwDocumentList_Controller();
            var d = dcnt.Find(_docItemID);
            this.Caption = d.Client_Name + " - " + Path.GetFileNameWithoutExtension(d.Name);

            btnLoadPreYEData.Visible = false;

            if (type == Tags.TagType.LastYearClosingTrialBal)
            {
                groupBox1.Text = "Last Year Closing Trial Balance Details";
                btnLoadPreYEData.Visible = true;
                
                btnLoadPreYEData.ButtonElement.ToolTipText = "Copy data from 'Last Year Closing Trial Balance'.";
            }
            #endregion

            //sheetType = _sheetType;
            TableName = "tblClosingTrialBalance";
            DocumentItemID = _docItemID;

            ledgerGrid1.sInitiatializeGrid(this.Caption);
            ledgerGrid1.DocumentItemID = _docItemID;

            LedgerRepository repCntr = new LedgerRepository();
            var doc = repCntr.GetYearEndFolder(DocumentItemID);
            ledgerGrid1.YrEndFolID = doc.ID;

            ledgerGrid1.grdDataHelper = new LedgerGridData(ledgerGrid1.YrEndFolID);

            CommonSettings(ledgerGrid1, btnSearch, btnClear, btnBack);
        }
        private void UcTrialBalance_Load(object sender, EventArgs e)
        {
            DisplayManager.OnControlLoading = onNewControlLoading;
            this.ParentForm.FormClosing += ParentForm_FormClosing;

            dpDate.ApplyDMSSettings();
            lblTotals.Text = null;
            var dtSrc = GetData();

            SetColumns();
            ledgerGrid1.sOnParentLoad();
            LoadData();

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
                    if (itemInfo.Date.HasValue)
                        dpDate.Value = itemInfo.Date.Value;
                }
            }

            ledgerGrid1.sBindData(GetData());

            for (int i = 0; i < ledgerGrid1.CurrentWorksheet.RowCount; i++)
            {
                var dvalue = ledgerGrid1.CurrentWorksheet[i, 2];
                var cvalue = ledgerGrid1.CurrentWorksheet[i, 3];

                if (dvalue == null)
                    dvalue = 0;
                else
                    if (dvalue.ToString() == "")
                        dvalue = 0;


                if (cvalue == null)
                    cvalue = 0;
                else
                    if (cvalue.ToString() == "")
                        cvalue = 0;
                
               
                SetReadOnlyCell(Convert.ToDecimal(dvalue.ToString()), Convert.ToDecimal(cvalue.ToString()), i, 2, 3);
            }

        }

        void SetReadOnlyCell(decimal dvaule, decimal cvaule, int row, int debit, int credit)
        {
            ReoGridPos pos = new ReoGridPos(row, debit);

            if ((dvaule == 0 && cvaule == 0) || (dvaule != 0 && cvaule != 0))
            {
                ledgerGrid1.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = false;
                pos.Col = credit;
                ledgerGrid1.sSetValidCell(pos);
                ledgerGrid1.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = false;
                pos.Col = debit;
                ledgerGrid1.sSetValidCell(pos);
                if ((dvaule != 0 && cvaule != 0) && (dvaule != cvaule))
                {
                    if (Convert.ToDecimal(dvaule.ToString()) == 0)
                    {
                        ledgerGrid1.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = true;
                        pos.Col = debit;
                        ledgerGrid1.sSetBackground(pos, Color.LightGray);
                        ledgerGrid1.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = false;
                        pos.Col = credit;
                        ledgerGrid1.sSetValidCell(pos);
                    }
                    else
                    {
                        if (Convert.ToDecimal(cvaule.ToString()) == 0)
                        {
                            ledgerGrid1.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = true;
                            pos.Col = credit;
                            ledgerGrid1.sSetBackground(pos, Color.LightGray);
                            ledgerGrid1.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = false;
                            pos.Col = debit;
                            ledgerGrid1.sSetValidCell(pos);
                        }
                    }
                }
            }
            else
            {
                if (dvaule != 0)
                {
                    ledgerGrid1.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = true;
                    pos.Col = credit;
                    ledgerGrid1.sSetBackground(pos, Color.LightGray);
                    ledgerGrid1.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = false;
                    pos.Col = debit;
                    ledgerGrid1.sSetValidCell(pos);
                }
                else if (cvaule != 0)
                {
                    ledgerGrid1.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = true;
                    pos.Col = debit;
                    ledgerGrid1.sSetBackground(pos, Color.LightGray);
                    ledgerGrid1.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = false;
                    pos.Col = credit;
                    ledgerGrid1.sSetValidCell(pos);

                }
            }
        }

        void SetColumns()
        {
            //ledgerGrid1.CurrentWorksheet.Reset();

            var colList = new List<LedgerColumn>();

            colList.Add(new LedgerColumn("Nominal Code", EnumColumnFormat.Text, EnumLedgetType.NominalCodeID, true));
            colList.Add(new LedgerColumn("Description", EnumColumnFormat.Text, EnumLedgetType.Description));
            //colList.Add(new LedgerColumn("ItemType", EnumColumnFormat.Text, EnumLedgetType.ItemType,true));
            colList.Add(new LedgerColumn("Debit", EnumColumnFormat.Amount, EnumLedgetType.Debit, false));
            colList.Add(new LedgerColumn("Credit", EnumColumnFormat.Amount, EnumLedgetType.Credit, false));
            //colList.Add(new LedgerColumn("Year End", EnumColumnFormat.Text, EnumLedgetType.TransferNominalCodeID, false));

            ledgerGrid1.sAddColumns(colList);

            ledgerGrid1.sGetColumn(EnumLedgetType.NominalCodeID).Width = 120;
            ledgerGrid1.sGetColumn(EnumLedgetType.Description).Width = 210;
            //ledgerGrid1.sGetColumn(EnumLedgetType.ItemType).Width = 120;

            ledgerGrid1.sGetColumn(EnumLedgetType.Debit).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Credit).Width = 80;
            //ledgerGrid1.sGetColumn(EnumLedgetType.TransferNominalCodeID).Width = 120;

            ledgerGrid1.sApplyColumnWidth();
        }

        private void btnSave_MouseClick(object sender, MouseEventArgs e)
        {
            //if (difference != 0)
            //{
            //    DisplayManager.DisplayMessage("Total Credits must be aqual to total debits.", MessageType.Error);
            //    return;
            //}

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

            if (dpDate.GetNullValue() == null)
            {
                ShowValidationError(dpDate, CustomMessages.GetValidationMessage("Date"));
                return false;
            }

            if (itemInfo == null)
            {
                itemInfo = new tblItemInfo();
            }

            itemInfo.DocumentItemID = ledgerGrid1.DocumentItemID;

            try
            {
                itemInfo.Date = dpDate.Value;
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
                    tblClosingTrialBalanceController cnt = new tblClosingTrialBalanceController();
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
            double crd = 0, deb = 0;
            crd = ledgerGrid1.sGetColumnSum(EnumLedgetType.Credit);
            deb = ledgerGrid1.sGetColumnSum(EnumLedgetType.Debit);
            difference = Math.Abs(crd - deb);
            if (difference < .01)
                difference = 0;

            lblTotals.Text = string.Format("Debit = {0},  Credit = {1}, Difference = {2}", deb, crd,difference.ToString("N2"));
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
        }

        private void btnLock_Click(object sender, EventArgs e)
        {
            SaveItemInfo();
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

        void CopyData()
        {
            long docID = ledgerGrid1.DocumentItemID;
            LedgerRepository rep = new LedgerRepository();
            tblDocumentItem preYE = rep.PreviousYearEndFolder(docID);
            string err = null;
            if (preYE != null)
            {
                List<tblDocumentItem> lst = new List<tblDocumentItem>();
                rep.FindRecursive(preYE.ID, Tags.TagType.ThisYearClosingTrialBal, lst);
                if (lst.Count > 0)
                {
                    tblDocumentItem preTB = lst[0];

                    LedgerReportController c = new LedgerReportController(preTB.ID);
                    c.LoadReportData(new DateTime(2010, 1, 1), new DateTime(2016, 1, 1));
                    var ds = c.ThisYrClosingTrialBal();


                    tblClosingTrialBalanceController cnt = new tblClosingTrialBalanceController();
                    cnt.CopyData(ds.Tables[0], docID);

                    LoadData();
                }
                else
                    err = "Can't find last year Closing Trial Balance.";
            }
            else
            {
                err = "Can't find previous year-end.";
            }

            if(err != null)
                DisplayManager.DisplayMessage(err, MessageType.Error);
        }

        private void btnLoadPreYEData_Click(object sender, EventArgs e)
        {
            CopyData();
        }

    }
}
