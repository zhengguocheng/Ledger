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
using Telerik.WinControls;
using System.Globalization;
using DMS.UserControls;
using Telerik.WinControls.UI.Export;
using System.IO;
using System.Diagnostics;
using DAL.CustomClasses;
using Utilities;
using DMS.Reports;

namespace DMS
{
    public enum LedgerColumnType { None, Currency, Date }
    public partial class UcLedgerReport : UserControlBase
    {
        Font grpTotalFont;
        long docItemID;
        int clientID;
        LedgerReportController legReportCnt;
        long? NomCodeIDToLoad;

        //AppTimer timer = new AppTimer();

        public UcLedgerReport(long _docItemID, int _clientID, long? _nomCodeIDToLoad = null)
        {
            InitializeComponent();
            this.Caption = "Nominal Ledger Report";
            docItemID = _docItemID;
            clientID = _clientID;
            dpStDate.ApplyDMSSettings();
            dpEndDate.ApplyDMSSettings();

            if (docItemID > 0)
                legReportCnt = new LedgerReportController(docItemID);

            NomCodeIDToLoad = _nomCodeIDToLoad;
        }

        private void UcLedgerReport_Load(object sender, EventArgs e)
        {
            grdItems.ViewCellFormatting += grdItems_CellFormatting;
            grdItems.GroupSummaryEvaluate += grdItems_GroupSummaryEvaluate;
            this.DisableCellHighlighting(grdItems);
            LoadGridColumns();
            grpTotalFont = new Font("Arial", 9f, FontStyle.Bold);

            DropDownHelper.BindNominalCode(drpNominalCode, 0, true);


            legReportCnt.LoadClientInfo();
            txtClientName.Text = legReportCnt.ClientName;
            txtYearEnd.Text = legReportCnt.FolderYearEnd;
            txtStPeriod.Text = "1";
            txtEndPeriod.Text = "12";
            try
            {
                var d1 = Convert.ToDateTime(txtYearEnd.Text.Trim());
                var d2 = d1.AddYears(-1).AddDays(1);

                dpStDate.Value = d2;
                dpEndDate.Value = d1;
            }
            catch { }

            if (NomCodeIDToLoad.HasValue)
            {
                chkAll.Checked = true;
                DropDownHelper.SelectByValue(drpNominalCode, NomCodeIDToLoad.Value.ToString());
                btnReport_Click(null, null);
            }
        }

        void LoadGridColumns()
        {
            AddColumn(LedgerReportController.colDate, "Date", LedgerColumnType.Date);
            AddColumn(LedgerReportController.colDescription, "Description");
            AddColumn(LedgerReportController.colSource, "Reference");
            AddColumn(LedgerReportController.colDocName, "Source File", LedgerColumnType.None, true);
            AddColumn(LedgerReportController.colDebit, "Debit", LedgerColumnType.Currency);
            AddColumn(LedgerReportController.colCredit, "Credit", LedgerColumnType.Currency);
            AddColumn(LedgerReportController.colBalance, "Balance", LedgerColumnType.Currency);
            AddColumn(LedgerReportController.colNominalCode, "Nominal Code");
            AddColumn(LedgerReportController.colNotes, "Notes");
        }

        string GetColumnName(string src)
        {
            var colName = "col" + src;
            return colName;
        }
        void AddColumn(string source, string header, LedgerColumnType type = LedgerColumnType.None, bool isHperLinkCol = false)
        {

            GridViewDataColumn col;
            //GridViewHyperlinkColumn col;

            if (type == LedgerColumnType.Date)
            {
                col = new GridViewDateTimeColumn();

                col.DataType = typeof(DateTime);
                col.FormatString = "{0:dd/MM/yyyy}";
                //col.FormatInfo = CultureInfo.CreateSpecificCulture("en-GB");
            }
            else if (type == LedgerColumnType.Currency)
            {
                col = new GridViewDecimalColumn(source);
                col.FormatString = "{0:N2}  ";
                col.DataType = typeof(Decimal);
                col.TextAlignment = ContentAlignment.MiddleRight;
                //col.FormatInfo = CultureInfo.CreateSpecificCulture("en-GB");
            }
            else
            {
                col = new GridViewTextBoxColumn();
            }

            if (isHperLinkCol)
            {
                var linkCol = new GridViewHyperlinkColumn();
                linkCol.HyperlinkOpenAction = HyperlinkOpenAction.SingleClick;
                grdItems.HyperlinkOpening += grdItems_HyperlinkOpening;
                col = linkCol;
            }

            col.HeaderText = header;
            col.Name = GetColumnName(source);
            col.FieldName = source;

            grdItems.Columns.Add(col);
        }

        void grdItems_HyperlinkOpening(object sender, HyperlinkOpeningEventArgs e)
        {
            e.Cancel = true;

            var dr = (DataRowView)e.Row.DataBoundItem;
            var docItemID = Convert.ToInt64(dr[LedgerReportController.colDocumentItemID]);
            var id = AppConverter.ToInt(dr[LedgerReportController.colID], 0);

            Repository c = new Repository(AppConstants.RecordType.Ledger);
            var doc = c.Find(docItemID);
            GlobalParams.IdToScroll = id;
            UcNavigation.OpenVirtualFile(doc, this);
        }

        void LoadData()
        {
            //timer.Start();

            legReportCnt = new LedgerReportController(docItemID);
            legReportCnt.LoadClientInfo();

            ReportType rptType = chkSummaryLedger.Checked ? ReportType.LedgerSummaryOnly : ReportType.FullReport;

            if (chkAll.Checked)
                legReportCnt.LoadReportData(new DateTime(1980, 1, 1), new DateTime(2020, 12, 31), 1, 12, rptType);
            else
                legReportCnt.LoadReportData(dpStDate.Value, dpEndDate.Value, AppConverter.ToInt(txtStPeriod.Text, 1), AppConverter.ToInt(txtEndPeriod.Text, 12), rptType);

            //DisplayManager.DisplayMessage(legReportCnt.timerMsg, MessageType.Success);

            //StopTimer("legReportCnt.LoadReportData");

            //timer.Start();
            var nomCodeDesc = DropDownHelper.GetSelectedText(drpNominalCode);
            if (!string.IsNullOrWhiteSpace(nomCodeDesc))
            {
                var code = nomCodeDesc.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
                legReportCnt.FilterReport(code);
            }

            if (chkDayBooks.Checked)
            {
                legReportCnt.FilterDayBooks();
            }

            StopTimer("legReportCnt.FilterDayBooks");

            try
            {
                //timer.Start();

                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = legReportCnt.dtReport;

                grdItems.MasterTemplate.GroupDescriptors.Clear();
                grdItems.SummaryRowsBottom.Clear();

                grdItems.MasterTemplate.EnableGrouping = true;
                grdItems.MasterTemplate.GroupDescriptors.Add(GetColumnName(LedgerReportController.colNominalCode), ListSortDirection.Ascending);

                GridViewSummaryItem summaryItem = new GridViewSummaryItem(GetColumnName(LedgerReportController.colBalance), "{0:N2}  ", GridAggregateFunction.Last);

                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem(new GridViewSummaryItem[] { summaryItem });
                grdItems.SummaryRowsBottom.Add(summaryRowItem);
                grdItems.MasterTemplate.AutoExpandGroups = true;

                StopTimer("Grid Load");
            }
            catch (Exception ec)
            {
                throw ec;
            }
        }

        #region Grid Style
        void grdItems_CellFormatting(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            if (e.CellElement.RowInfo is GridViewGroupRowInfo)
            {
                e.CellElement.DrawFill = true;
                e.CellElement.BackColor = Color.Silver;
                e.CellElement.TextAlignment = ContentAlignment.MiddleLeft;
                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
                //e.CellElement.Font = grpTotalFont; it hides the column
            }
            else if (e.CellElement.RowInfo is GridViewSummaryRowInfo)
            {
                e.CellElement.DrawFill = true;
                //e.CellElement.ForeColor = Color.Yellow;
                e.CellElement.Font = grpTotalFont;
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;

                e.CellElement.GradientStyle = Telerik.WinControls.GradientStyles.Solid;
                e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
            }
            else
            {
                e.CellElement.ResetValue(LightVisualElement.DrawFillProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(LightVisualElement.BackColorProperty, ValueResetFlags.Local);
                //e.CellElement.ResetValue(LightVisualElement.TextAlignmentProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(LightVisualElement.GradientStyleProperty, ValueResetFlags.Local);
                e.CellElement.ResetValue(LightVisualElement.FontProperty, ValueResetFlags.Local);
            }
        }

        void grdItems_GroupSummaryEvaluate(object sender, Telerik.WinControls.UI.GroupSummaryEvaluationEventArgs e)
        {

            if (e.SummaryItem.Name == GetColumnName(LedgerReportController.colNominalCode))
            {
                if (e.Value != null)
                {
                    e.FormatString = String.Format("{0}", e.Value.ToString().ToUpper());
                }
            }
        }

        #endregion

        private void btnBack_Click(object sender, EventArgs e)
        {
            if (NomCodeIDToLoad.HasValue)
            {
                //DisplayManager.LoadControl(new UcRptThisYrClosingTrialBal(docItemID), true);
                DisplayManager.LoadPrev(typeof(UcRptThisYrClosingTrialBal));
            }
            else
                DisplayManager.LoadControl(new UcNavigation(false), true);
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            this.ClearErrorProvider();

            if (chkAll.Checked == false)
            {
                DateTime min = new DateTime(1980, 1, 1);
                DateTime max = new DateTime(2020, 12, 31);


                if (dpStDate.IsNull() || dpStDate.Value < min || dpStDate.Value > max)
                {
                    ShowValidationError(dpStDate, CustomMessages.GetValidationMessage("Start Date"));
                    return;
                }
                if (dpEndDate.IsNull() || dpEndDate.Value < min || dpEndDate.Value > max)
                {
                    ShowValidationError(dpEndDate, CustomMessages.GetValidationMessage("End Date"));
                    return;
                }

                int stPrd = AppConverter.ToInt(txtStPeriod.Text, 0);
                int endPrd = AppConverter.ToInt(txtEndPeriod.Text, 0);

                if (stPrd == 0 || stPrd < 1 || stPrd > 12)
                {
                    ShowValidationError(txtStPeriod, CustomMessages.GetValidationMessage("Start Period"));
                    return;
                }
                if (endPrd == 0 || endPrd < 1 || endPrd > 12)
                {
                    ShowValidationError(txtEndPeriod, CustomMessages.GetValidationMessage("End Period"));
                    return;
                }
                if (stPrd > endPrd)
                {
                    ShowValidationError(txtEndPeriod, "Start period must be less then or equal to end period.");
                    return;
                }
            }

            LoadData();
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            dpEndDate.Enabled = dpStDate.Enabled = txtEndPeriod.Enabled = txtStPeriod.Enabled = !chkAll.Checked;
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                ExportToExcelML exporter = new ExportToExcelML(this.grdItems);
                exporter.HiddenColumnOption = Telerik.WinControls.UI.Export.HiddenOption.DoNotExport;
                exporter.ExportVisualSettings = true;
                exporter.SheetMaxRows = ExcelMaxRows._1048576;
                exporter.SheetName = "Ledger Report";
                exporter.SummariesExportOption = SummariesOption.ExportAll;

                //it takes absolute path. Dont include char like /,\,: in file name
                var path = Path.Combine(folderBrowserDialog1.SelectedPath, "Ledger Report-" + DateTime.Now.Ticks + ".xls");
                exporter.RunExport(path);

                Process.Start(path);
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            grdItems.PrintPreview();
        }

        protected override void Dispose(bool disposing)
        {
            //dont dispose controls as the are used when this control is shown again
        }

        void StopTimer(string msg)
        {
            //msg = DateTime.Now.ToLongTimeString() + " --> " + msg + " = " + timer.Stop();
            //DisplayManager.DisplayMessage(msg , MessageType.Success);
        }

        private void chkSummaryLedger_CheckedChanged(object sender, EventArgs e)
        {

        }

    }
}
