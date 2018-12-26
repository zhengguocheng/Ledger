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

namespace DMS
{
    public partial class UcAnalysisReport : UserControlBase
    {
        Font grpTotalFont;
        long docItemID;
        int clientID;
        AnalysisReportController anCnt;

        public UcAnalysisReport(long _docItemID, int _clientID)
        {
            InitializeComponent();
            this.Caption = "Analysis Report";
            docItemID = _docItemID;
            clientID = _clientID;

            if (docItemID > 0)
                anCnt = new AnalysisReportController(docItemID);
        }

        private void Control_Load(object sender, EventArgs e)
        {
            grdItems.ViewCellFormatting += grdItems_CellFormatting;
            grdItems.GroupSummaryEvaluate += grdItems_GroupSummaryEvaluate;
            this.DisableCellHighlighting(grdItems);
            grpTotalFont = new Font("Arial", 9f, FontStyle.Bold);

            DropDownHelper.BindNominalCode(drpNominalCode1, 0, true);
            DropDownHelper.BindNominalCode(drpNominalCode2, 0, true);
            DropDownHelper.BindNominalCode(drpNominalCode3, 0, true);
            DropDownHelper.BindNominalCode(drpNominalCode4, 0, true);
            DropDownHelper.BindNominalCode(drpNominalCode5, 0, true);

            drpNominalCode1.SelectedIndex = 1;
            drpNominalCode2.SelectedIndex = 1;
            drpNominalCode3.SelectedIndex = 1;
            drpNominalCode4.SelectedIndex = 1;

            anCnt.legReportCnt.LoadClientInfo();
            txtClientName.Text = anCnt.legReportCnt.ClientName;
            txtYearEnd.Text = anCnt.legReportCnt.FolderYearEnd;

        }

        AnalysisInput GetInput(int n)
        {
            RadTextBox txtSt; RadTextBox txtEnd; RadTextBox txtPerTo; RadDropDownList drp;

            GetInputControls(n, out txtSt, out txtEnd, out txtPerTo, out drp);

            AnalysisInput inp = new AnalysisInput();

            inp.StartPeriod = AppConverter.ToInt(txtSt.Text, 0);
            inp.EndPeriod = AppConverter.ToInt(txtEnd.Text, 0);
            inp.PercentTo = AppConverter.ToDecimal(txtPerTo.Text, 0);
            if (drp.Text.Contains("-"))
                inp.NomCode = drp.Text.Split("-".ToCharArray())[0].Trim();

            if (inp.StartPeriod > 0 && inp.EndPeriod > 0)
                return inp;
            else
                return null;
        }

        void GetInputControls(int n, out RadTextBox txtSt, out RadTextBox txtEnd, out RadTextBox txtPerTo, out RadDropDownList drp)
        {
            txtSt = null;
            txtEnd = null;
            txtPerTo = null;
            drp = null;

            if (n == 1)
            {
                txtSt = txtStPeriod1;
                txtEnd = txtEndPeriod1;
                txtPerTo = txtPerTo1;
                drp = drpNominalCode1;
            }
            else if (n == 2)
            {
                txtSt = txtStPeriod2;
                txtEnd = txtEndPeriod2;
                txtPerTo = txtPerTo2;
                drp = drpNominalCode2;
            }
            else if (n == 3)
            {
                txtSt = txtStPeriod3;
                txtEnd = txtEndPeriod3;
                txtPerTo = txtPerTo3;
                drp = drpNominalCode3;
            }
            else if (n == 4)
            {
                txtSt = txtStPeriod4;
                txtEnd = txtEndPeriod4;
                txtPerTo = txtPerTo4;
                drp = drpNominalCode4;
            }
            else if (n == 5)
            {
                txtSt = txtStPeriod5;
                txtEnd = txtEndPeriod5;
                txtPerTo = txtPerTo5;
                drp = drpNominalCode5;
            }
        }

        void LoadGridColumns()
        {
            grdItems.Columns.Clear();
            ColumnGroupsViewDefinition view = new ColumnGroupsViewDefinition();

            AddColumn(LedgerReportController.colNominalCode, "Nominal Code");

            if (chkShowSummary.Checked)
            {
                var colGrp2 = new GridViewColumnGroup("");
                view.ColumnGroups.Add(colGrp2);

                colGrp2.Rows.Add(new GridViewColumnGroupRow());
                colGrp2.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnName(LedgerReportController.colNominalCode)]);
            }
            else
            {
                AddColumn(LedgerReportController.colDescription, "Description");

                var colGrp2 = new GridViewColumnGroup("");
                view.ColumnGroups.Add(colGrp2);

                colGrp2.Rows.Add(new GridViewColumnGroupRow());
                colGrp2.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnName(LedgerReportController.colDescription)]);
            }

            foreach (var item in anCnt.lstInput)
            {
                AddColumn(LedgerReportController.colNet + item.InputNo, "Amount", LedgerColumnType.Currency);
                if (!chkShowSummary.Checked)
                    AddColumn(AnalysisReportController.colPercentGroup + item.InputNo, "% in Group", LedgerColumnType.Currency);
                AddColumn(AnalysisReportController.colPercentTo + item.InputNo, item.PercentToInfo(), LedgerColumnType.Currency);

                var colGrp = new GridViewColumnGroup(item.GetInfo());
                view.ColumnGroups.Add(colGrp);

                colGrp.Rows.Add(new GridViewColumnGroupRow());
                colGrp.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnName(LedgerReportController.colNet + item.InputNo)]);
                if (!chkShowSummary.Checked)
                    colGrp.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnName(AnalysisReportController.colPercentGroup + item.InputNo)]);
                colGrp.Rows[0].Columns.Add(this.grdItems.Columns[GetColumnName(AnalysisReportController.colPercentTo + item.InputNo)]);
            }

            grdItems.ViewDefinition = view;
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


        void GetRpt()
        {
            anCnt.GetReport();
        }

        void LoadData()
        {
            anCnt.lstInput.Clear();
            for (int i = 1; i <= 5; i++)
            {
                var inp = GetInput(i);
                if (inp != null)
                    anCnt.lstInput.Add(inp);
            }

            anCnt.ShowSummary = chkShowSummary.Checked;
            var dt = anCnt.GetReport(); //anCnt.GetData(inp);
            
            //var dt = anCnt.GetData(inp);
            try
            {

                LoadGridColumns();

                grdItems.AutoGenerateColumns = false;
                grdItems.DataSource = dt;

                grdItems.MasterTemplate.GroupDescriptors.Clear();
                if (!chkShowSummary.Checked)
                {
                    grdItems.MasterTemplate.EnableGrouping = true;
                    grdItems.MasterTemplate.GroupDescriptors.Add(GetColumnName(LedgerReportController.colNominalCode), ListSortDirection.Ascending);
                }

                List<GridViewSummaryItem> lstSummryItms = new List<GridViewSummaryItem>();

                foreach (var item in anCnt.lstInput)
                {
                    GridViewSummaryItem summaryItem = new GridViewSummaryItem(GetColumnName(LedgerReportController.colNet + item.InputNo), "{0:N2}  ", GridAggregateFunction.Sum);
                    GridViewSummaryItem summaryItem2 = new GridViewSummaryItem(GetColumnName(AnalysisReportController.colPercentGroup + item.InputNo), "{0:N2}  ", GridAggregateFunction.Sum);
                    GridViewSummaryItem summaryItem3 = new GridViewSummaryItem(GetColumnName(AnalysisReportController.colPercentTo + item.InputNo), "{0:N2}  ", GridAggregateFunction.Sum);

                    lstSummryItms.Add(summaryItem);
                    lstSummryItms.Add(summaryItem2);
                    lstSummryItms.Add(summaryItem3);

                    //if (!chkShowSummary.Checked)
                    //{
                    //}
                }


                GridViewSummaryRowItem summaryRow = new GridViewSummaryRowItem(lstSummryItms.ToArray());

                grdItems.SummaryRowsBottom.Clear();
                grdItems.SummaryRowsBottom.Add(summaryRow);

                grdItems.MasterTemplate.AutoExpandGroups = true;
            }
            catch (Exception ec)
            {
                DisplayManager.DisplayMessage(ec.Message,MessageType.Error);
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
            DisplayManager.LoadControl(new UcNavigation(false), true);
        }
        private void btnReport_Click(object sender, EventArgs e)
        {
            this.ClearErrorProvider();


            LoadData();
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

        private void txtStPeriod3_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
