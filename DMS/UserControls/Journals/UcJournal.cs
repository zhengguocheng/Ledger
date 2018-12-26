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

namespace DMS.UserControls
{
    public partial class UcJournal : LedgerSheetBaseJournal
    {
        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        tblItemInfo itemInfo;
        public const string SeperatorChar = "-";
        Tags.TagType tag;
        public UcJournal(long _docItemID, Tags.TagType _tag)
        {
            InitializeComponent();

            #region Load Caption
            VwDocumentList_Controller dcnt = new VwDocumentList_Controller();
            var d = dcnt.Find(_docItemID);
            this.Caption = d.Client_Name + " - " + Path.GetFileNameWithoutExtension(d.Name);
            groupBox1.Text = tag.ToString() + " " + "Details";
            txtClientName.Text = d.Client_Name;

            #endregion

            tag = _tag;
            TableName = "tblJournals";
            DocumentItemID = _docItemID;

            ledgerGrid1.sInitiatializeGrid(this.Caption);
            ledgerGrid1.DocumentItemID = _docItemID;

            LedgerRepository repCntr = new LedgerRepository();
            var doc = repCntr.GetYearEndFolder(DocumentItemID);
            ledgerGrid1.YrEndFolID = doc.ID;

            if (doc.YearEndDate.HasValue)
                txtYearEnd.Text = doc.YearEndDate.Value.ToString(AppConstants.DateFormatYearEnd);

            ledgerGrid1.grdDataHelper = new LedgerGridData(ledgerGrid1.YrEndFolID);

            CommonSettings(ledgerGrid1, btnSearch, btnClear, btnBack);
        }
        private void UcExcelSheet_Load(object sender, EventArgs e)
        {
            DisplayManager.OnControlLoading = onNewControlLoading;
            this.ParentForm.FormClosing += ParentForm_FormClosing;

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

                }
            }

            ledgerGrid1.sBindData(GetData());
        }


        void SetColumns()
        {
            //ledgerGrid1.CurrentWorksheet.Reset();
            //ledgerGrid1.VatHidden = chkHideVat.Checked;

            var colList = new List<LedgerColumn>();

            colList.Add(new LedgerColumn("Date", EnumColumnFormat.Date, EnumLedgetType.Date, true));
            colList.Add(new LedgerColumn("Reference", EnumColumnFormat.Text, EnumLedgetType.Reference, true));
            colList.Add(new LedgerColumn("Details", EnumColumnFormat.Text, EnumLedgetType.Description));
           
            colList.Add(new LedgerColumn("Code Dr", EnumColumnFormat.Text, EnumLedgetType.DebitNominalCodeID, true));
            colList.Add(new LedgerColumn("Code Cr", EnumColumnFormat.Text, EnumLedgetType.CreditNominalCodeID, true));
            //modify By@zgc 
            //modify by @zgc 2018.11.10 类型字段后续要转为字符串和数据库中的字段对应，擅自修改后导致字段对应不上出现Double M表中提示错误信息
            //故要重新修改回来
            //colList.Add(new LedgerColumn("Details", EnumColumnFormat.Text, EnumLedgetType.Details));
            //colList.Add(new LedgerColumn("Code Dr", EnumColumnFormat.Text, EnumLedgetType.CodeDr));
            //colList.Add(new LedgerColumn("Code Cr", EnumColumnFormat.Text, EnumLedgetType.CodeCr));
            //end modify by @zgc

            colList.Add(new LedgerColumn("Amount", EnumColumnFormat.Amount, EnumLedgetType.Amount, true));
            colList.Add(new LedgerColumn("Notes", EnumColumnFormat.Text, EnumLedgetType.Notes));

            ledgerGrid1.sAddColumns(colList);

            ledgerGrid1.sGetColumn(EnumLedgetType.Date).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Reference).Width = 80;
            //modify by @zgc
            ledgerGrid1.sGetColumn(EnumLedgetType.Description).Width = 150;
            ledgerGrid1.sGetColumn(EnumLedgetType.DebitNominalCodeID).Width = 120;
            ledgerGrid1.sGetColumn(EnumLedgetType.CreditNominalCodeID).Width = 120;
            //ledgerGrid1.sGetColumn(EnumLedgetType.Details).Width = 150;
            //ledgerGrid1.sGetColumn(EnumLedgetType.CodeDr).Width = 120;
           // ledgerGrid1.sGetColumn(EnumLedgetType.CodeCr).Width = 120;
            //end modify by@zgc
            ledgerGrid1.sGetColumn(EnumLedgetType.Amount).Width = 80;
            ledgerGrid1.sGetColumn(EnumLedgetType.Notes).Width = 190;



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
                    DisplayManager.DisplayMessage("File has been saved.", MessageType.Success);
                    MessageBox.Show("File has been saved.", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private bool SaveItemInfo()
        {

            if (itemInfo == null)
            {
                itemInfo = new tblItemInfo();
            }

            itemInfo.DocumentItemID = ledgerGrid1.DocumentItemID;

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
            var uc = new UcRptJournals(ledgerGrid1.DocumentItemID, tag);
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
                    tblJournalsController cnt = new tblJournalsController();
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
            double gross = 0, vat = 0, net = 0;
            gross = ledgerGrid1.sGetColumnSum(EnumLedgetType.Gross);
            net = ledgerGrid1.sGetColumnSum(EnumLedgetType.Net);

            lblTotals.Text = string.Format("Gross = {0},  Net = {1}", gross, net);

        }





        private void btnSave_Click(object sender, EventArgs e)
        {

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
    }
}
