using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using DAL;
using DMS.UserControls;
using Telerik.WinControls.UI;
using Telerik.WinControls;

namespace DMS.Reports
{
    public partial class UcRptThisYrClosingTrialBal : UserControlBase
    {
        long docItemID;
        int clientID;
        UcRDLC_Viewer ucRptViewer;
        Font grpTotalFont;
        int yearEnd;
        public UcRptThisYrClosingTrialBal(long _docItemID)
        {
            InitializeComponent();
            grpTotalFont = new Font("Arial", 9f, FontStyle.Bold);

            Caption = "Closing Trial Balance";
            docItemID = _docItemID;

            grdItems.CellFormatting += radGridView1_CellFormatting1;
            grdItems.ViewCellFormatting += AllGrids_ViewCellFormatting;
            grdItems.RowFormatting += radGridView1_RowFormatting;

            var repCntr = new LedgerRepository();
            clientID = repCntr.Find(docItemID).RecordID;
        }

        private void UcRptPaymentSummary_Load(object sender, EventArgs e)
        {
            rdGrid.IsChecked = true;
        }

        private void LoadRDLC(bool refreshData = false)
        {
            if (ucRptViewer == null)
            {
                ucRptViewer = new UcRDLC_Viewer();
                ucRptViewer.ThisYrClossingTrialBal(docItemID);
                ucRptViewer.Dock = DockStyle.Fill;
                pnlRpt.Controls.Add(ucRptViewer);
            }
            else if (refreshData)
            {
                ucRptViewer.ThisYrClossingTrialBal(docItemID);
            }

            if (refreshData == false)//dont change visibility if user press refresh button
            {
                ucRptViewer.Visible = true;
                grdItems.Visible = false;
            }
        }

        void LoadGrid(bool refreshData = false)
        {
            if (grdItems.DataSource == null || refreshData)
            {
                LedgerReportController c = new LedgerReportController(docItemID);
                c.LoadReportData(new DateTime(1990, 1, 1), new DateTime(2030, 1, 1));
                var ds = c.ThisYrClosingTrialBal();
                yearEnd = Convert.ToDateTime(c.FolderYearEnd).Year;

                LoadGridColumns();
                grdItems.DataSource = ds.Tables[0];


                //lblNetProfitLoss.Text = string.Format("{0} Net Profit/Loss :    {1} \r\n{2} Net Profit/Loss :    {3}", yearEnd, c.thisYrProfit.ToString("N2"), yearEnd - 1, c.lastYrProfit.ToString("N2"));

                lblThisYr.Text = string.Format("{0} Net Profit / Loss :", yearEnd);
                lblLastYr.Text = string.Format("{0} Net Profit / Loss :", yearEnd - 1);

                txtThisYr.Text = c.thisYrProfit.ToString("N2");
                txtLastYr.Text = c.lastYrProfit.ToString("N2");
            }

            if (refreshData == false)//dont change visibility if user press refresh button
            {
                if (ucRptViewer != null)
                    ucRptViewer.Visible = false;
                grdItems.Visible = true;
            }
        }

        #region Grid Style

        void radGridView1_CellFormatting1(object sender, Telerik.WinControls.UI.CellFormattingEventArgs e)
        {
            //if (e.CellElement.ColumnInfo.Name == "Credit")
            //{
            //    e.CellElement.ForeColor = Color.Red;
            //}
            //else
            //{
            //    e.CellElement.ResetValue(LightVisualElement.ForeColorProperty, ValueResetFlags.Local);
            //}
        }
        void AllGrids_ViewCellFormatting(object sender, CellFormattingEventArgs e)
        {
            if (e.CellElement is GridSummaryCellElement)
            {
                e.CellElement.Font = grpTotalFont;
                //e.CellElement.Alignment = ContentAlignment.MiddleRight;
                e.CellElement.TextAlignment = ContentAlignment.MiddleRight;
            }
        }

        private void radGridView1_RowFormatting(object sender, RowFormattingEventArgs e)
        {
            ////if (e.RowElement.RowInfo.Cells[VATReportController.col_NomCodeDesc] != null && e.RowElement.RowInfo.Cells[VATReportController.col_NomCodeDesc].Value.ToString() == "Total-Gross")
            //if (e.RowElement.RowInfo.Index > 10)
            //{
            //    e.RowElement.Font = grpTotalFont;
            //}
            //else
            //{
            //    e.RowElement.ResetValue(LightVisualElement.FontProperty, ValueResetFlags.Local);
            //}
        }

        #endregion

        void LoadGridColumns()
        {
            if (grdItems.ColumnCount > 1)
                return;

            SetWidth(AddColumn(LedgerReportController.colNominalCode, "Nominal Code"), 100);
            SetWidth(AddColumn(LedgerReportController.colDescription, "Description", LedgerColumnType.None, true), 200);
            SetWidth(AddColumn(LedgerReportController.colItemType, "P/B"), 40);
            SetWidth(AddColumn(LedgerReportController.colDebit, "Debit", LedgerColumnType.Currency), 80);
            SetWidth(AddColumn(LedgerReportController.colCredit, "Credit", LedgerColumnType.Currency), 80);
            SetWidth(AddColumn(LedgerReportController.colLastYrDebit, "Debit", LedgerColumnType.Currency), 80);
            SetWidth(AddColumn(LedgerReportController.colLastYrCredit, "Credit", LedgerColumnType.Currency), 80);
            AddColumn("empty", "");//add this column to expand on space

            ColumnGroupsViewDefinition view = new ColumnGroupsViewDefinition();
            
            var colGrp2 = new GridViewColumnGroup("");
            view.ColumnGroups.Add(colGrp2);

            colGrp2.Rows.Add(new GridViewColumnGroupRow());
            colGrp2.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colNominalCode)]);
            colGrp2.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colDescription)]);
            colGrp2.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colItemType)]);
            
            var colGrp3 = new GridViewColumnGroup(yearEnd.ToString());
            view.ColumnGroups.Add(colGrp3);
            colGrp3.Rows.Add(new GridViewColumnGroupRow());
            colGrp3.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colDebit)]);
            colGrp3.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colCredit)]);


            var colGrp4 = new GridViewColumnGroup((yearEnd - 1).ToString());
            view.ColumnGroups.Add(colGrp4);
            colGrp4.Rows.Add(new GridViewColumnGroupRow());
            colGrp4.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colLastYrDebit)]);
            colGrp4.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic(LedgerReportController.colLastYrCredit)]);

            var colGrp5 = new GridViewColumnGroup("");
            view.ColumnGroups.Add(colGrp5);
            colGrp5.Rows.Add(new GridViewColumnGroupRow());
            colGrp5.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnNameStatic("empty")]);
            

            grdItems.ViewDefinition = view;


        }

        void SetWidth(GridViewDataColumn col, int width)
        {
            col.MinWidth = width;
            col.MaxWidth = width;
        }

        GridViewDataColumn AddColumn(string source, string header, LedgerColumnType type = LedgerColumnType.None, bool isHperLinkCol = false)
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
                AddSummary(grdItems, UserControlBase.GetColumnNameStatic(source));

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
            col.Name = UserControlBase.GetColumnNameStatic(source);
            col.FieldName = source;


            grdItems.Columns.Add(col);
            return col;
        }

        void AddSummary(RadGridView grd, string col)
        {
            GridViewSummaryItem summaryItem = new GridViewSummaryItem();
            summaryItem.Name = col;
            summaryItem.Aggregate = GridAggregateFunction.Sum;
            summaryItem.FormatString = "{0:N2}";

            if (grd.SummaryRowsBottom.Count > 0)
                grd.SummaryRowsBottom[0].Add(summaryItem);

            else
            {
                GridViewSummaryRowItem summaryRowItem = new GridViewSummaryRowItem();
                summaryRowItem.Add(summaryItem);
                grd.SummaryRowsBottom.Add(summaryRowItem);
            }

        }

        void grdItems_HyperlinkOpening(object sender, HyperlinkOpeningEventArgs e)
        {
            e.Cancel = true;

            var dr = (DataRowView)e.Row.DataBoundItem;

            //var nomCode = dr[LedgerReportController.colNominalCode].ToString();
            var nomCodeID = AppConverter.ToInt(dr[LedgerReportController.colNominalCodeID], 0);
            tblChartAccountController cnt = new tblChartAccountController();
            var itm = cnt.FetchSimilarRecord(nomCodeID, 0);

            var rpt = new UcLedgerReport(docItemID, clientID, itm.ID);
            DisplayManager.LoadControl(rpt, false, this);
        }
        private void btnBack_Click(object sender, EventArgs e)
        {
            //var uc = new UcExcelSheet(docItemID,sheetType);
            LoadPreviousPage();
        }

        private void rdReport_ToggleStateChanged(object sender, Telerik.WinControls.UI.StateChangedEventArgs args)
        {
            if (rdReport.IsChecked)
                LoadRDLC();
            else
            {
                LoadGrid();
            }

        }

        protected override void Dispose(bool disposing)
        {
            //if (disposing && (components != null))
            //{
            //    components.Dispose();
            //}
            //base.Dispose(disposing);
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            LoadGrid(true);
            LoadRDLC(true);
        }

        public override void ControlDisplayed()
        {
            btnRefresh_Click(null, null);
        }

    }
}
