using DAL;
using DAL.Controllers;
using DMS.UserControls;
using DMS.UserControls.Popups;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.Actions;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.Data;
using unvell.ReoGrid.DataFormat;
using unvell.ReoGrid.Events;
using unvell.ReoScript;

namespace DMS.CustomClasses
{
    public class SDBGrid : ReoGridControl
    {
        public ReoGridPos LastEditPos;
        public bool LastEditValid;
        public List<LedgerColumn> colList = new List<LedgerColumn>();
        public DataTable SourceDataTable;
        public long DocumentItemID, YrEndFolID;
        public string NominalCode;
        tblSDBController cnt = new tblSDBController();
        //public const string splitValue = AppConstants.splitValue;
        public LedgerGridData grdDataHelper;
        public bool IsEdited = false;

        #region My Events

        public event sDelegateNeedSumUpdate sNeedSumUpdate;
        public delegate void sDelegateNeedSumUpdate();

        #endregion

        public bool VatHidden
        {
            get
            {
                for (int i = 0; i < this.CurrentWorksheet.Columns; i++)
                {
                    if (this.CurrentWorksheet.ColumnHeaders[i].Text == UcExcelSheet.VATCodeColumnName)
                        return false;
                }
                return true;
            }
        }



        #region Custom
        public void sInitiatializeGrid(string sheetName)
        {
            this.CurrentWorksheet.Name = sheetName;
            this.CurrentWorksheet.RowCount = 1000;
            sRegesterEvents();
        }

        public void sRegesterEvents()
        {
            this.CurrentWorksheet.BeforeCellEdit += CurrentWorksheet_BeforeCellEdit;
            this.CurrentWorksheet.AfterCellEdit += CurrentWorksheet_AfterCellEdit;
            this.CurrentWorksheet.FocusPosChanged += CurrentWorksheet_FocusPosChanged;
            this.CurrentWorksheet.CellDataChanged += CurrentWorksheet_CellDataChanged;
            this.CurrentWorksheet.BeforeCellKeyDown += sheet_BeforeCellKeyDown;
        }

        public void sRemoveEvents()
        {
            this.CurrentWorksheet.BeforeCellEdit -= CurrentWorksheet_BeforeCellEdit;
            this.CurrentWorksheet.AfterCellEdit -= CurrentWorksheet_AfterCellEdit;
            this.CurrentWorksheet.FocusPosChanged -= CurrentWorksheet_FocusPosChanged;
            this.CurrentWorksheet.CellDataChanged -= CurrentWorksheet_CellDataChanged;
            this.CurrentWorksheet.BeforeCellKeyDown -= sheet_BeforeCellKeyDown;
        }

        public void sOnParentLoad()
        {
            this.CurrentWorksheet.SetRangeStyle(ReoGridRange.EntireRange, new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap,
                TextWrapMode = TextWrapMode.WordBreak,
            });
        }

        public void sApplyFilter()
        {
            //filter = this.CurrentWorksheet.CreateColumnFilter(0, colList.Count - 1, 0, unvell.ReoGrid.Data.AutoColumnFilterUI.DropdownButtonAndPanel);
            //filter.Apply();
        }

        public void sClearFilter()
        {
            //filter.Detach();
            //ApplyFilter();
            //BindData(SourceDataTable);
        }

        public void sSetReadOnly(bool val)
        {
            this.CurrentWorksheet.IterateCells(ReoGridRange.EntireRange, (row, col, cell) =>
            {
                if (cell != null)
                {
                    cell.IsReadOnly = val;
                }
                return true;
            });
        }


        #region Columns

        public void sApplyColumnAlignment()//Call this after loading data into grid. Otherwise alignment dont take effect
        {
            foreach (var col in colList)
            {
                ReoGridHorAlign align = ReoGridHorAlign.Left;
                if (col.Format == EnumColumnFormat.Amount)
                    align = ReoGridHorAlign.Right;

                //ledgerGrid1.CurrentWorksheet.SetRangeStyle(new ReoGridRange(0, col.Index, this.CurrentWorksheet.RowCount, 1), new WorksheetRangeStyle
                //{
                //    Flag = PlainStyleFlag.HorizontalAlign,
                //    HAlign = align
                //});

                this.CurrentWorksheet.ColumnHeaders[col.Index].Style.HorizontalAlign = align;
            }
        }

        public void sApplyColumnWidth()
        {
            foreach (var col in colList)
            {
                this.CurrentWorksheet.ColumnHeaders[col.Index].Width = col.Width;
            }
        }
        public void sAddColumns(List<LedgerColumn> cols)
        {
            colList.Clear();
            int ind = 0;
            foreach (var item in cols)
            {
                this.CurrentWorksheet.ColumnHeaders[ind].Text = item.Name;
                item.Index = ind;
                colList.Add(item);
                ind++;
                sSetColumnFormat(item);
            }

            this.CurrentWorksheet.ColumnCount = cols.Count;
            sApplyFilter();
        }

        public void sSetHeader(EnumLedgetType type, string headerText)
        {
            this.CurrentWorksheet.ColumnHeaders[sGetColumn(type).Index].Text = headerText;
        }

        public string sGetHeader(EnumLedgetType type)
        {
            return this.CurrentWorksheet.ColumnHeaders[sGetColumn(type).Index].Text;
        }
        public string sGetHeader(int index)
        {
            return this.CurrentWorksheet.ColumnHeaders[index].Text;
        }

        public void sInsertColumn(LedgerColumn col, int index)
        {
            this.CurrentWorksheet.InsertColumns(index, 1);
            this.CurrentWorksheet.ColumnHeaders[index].Text = col.Name;

            col.Index = index;
            colList.Insert(index, col);
            sSetColumnIndex();

            sSetAllColumnFormat();
            sApplyColumnAlignment();
        }

        void sSetColumnIndex()
        {
            int ind = 0;
            foreach (var item in colList)
            {
                item.Index = ind;
                ind++;
            }
        }

        public void sRemoveCol(EnumLedgetType type)
        {
            try
            {
                var c = sGetColumn(type);
                colList.Remove(c);
                this.CurrentWorksheet.DeleteColumns(c.Index, 1);
                sSetColumnIndex();
                sSetAllColumnFormat();
            }
            catch { }
        }

        public LedgerColumn sGetColumn(int columnIndex)
        {
            var col = colList.FirstOrDefault(x => x.Index == columnIndex);
            if (col != null)
            {
                return col;
            }
            else
            {
                throw GridException.GetException(GridException.ErrorType.ColumnNotFound);
            }

        }

        public LedgerColumn sGetColumn(EnumLedgetType type)
        {
            var col = colList.FirstOrDefault(x => x.LedgerType == type);
            if (col != null)
            {
                return col;
            }
            else
            {
                throw GridException.GetException(GridException.ErrorType.ColumnNotFound);
            }

        }

        public ReoGridRange sGetColumnRange(EnumLedgetType type)
        {
            var col = sGetColumn(type);
            ReoGridRange r = new ReoGridRange(new ReoGridPos(0, col.Index), new ReoGridPos(this.CurrentWorksheet.RowCount - 1, col.Index));
            return r;
        }


        object sGetColumnValue(int row, EnumLedgetType type)
        {
            if (VatHidden && type == EnumLedgetType.VATCode)
                return null;
            if (VatHidden && type == EnumLedgetType.VAT)
                return 0;

            var colGross = sGetColumn(type);
            return sGetColumnValue(row, colGross.Index);
        }

        object sGetColumnValue(int row, int col)
        {
            var c = sGetColumn(col);
            return this.CurrentWorksheet[row, col];
        }

        public decimal sGetColumnSum(EnumLedgetType type)
        {
            decimal sum = 0, cellValue = 0;

            ReoGridRange r = sGetColumnRange(type);
            this.CurrentWorksheet.IterateCells(r, (row, col, cell) =>
            {
                if (cell.Data != null)
                {
                    if (decimal.TryParse(cell.Data.ToString(), out cellValue))
                        sum += cellValue;
                }
                return true;
            });

            return sum;
        }

        void sSetAllColumnFormat()
        {
            foreach (var item in colList)
            {
                sSetColumnFormat(item);
            }
        }
        void sSetColumnFormat(LedgerColumn col)
        {
            if (col.Format == EnumColumnFormat.Amount)
            {
                this.CurrentWorksheet.SetRangeDataFormat(new ReoGridRange(0, col.Index, this.CurrentWorksheet.RowCount, col.Index), CellDataFormatFlag.Number,
                                                              new NumberDataFormatter.NumberFormatArgs()
                                                              {
                                                                  DecimalPlaces = 2,
                                                                  UseSeparator = true,// use separator: 123,456
                                                              });
            }
            else if (col.Format == EnumColumnFormat.Date)
            {
                //When we set format as Date, Grid display time when user edit data
                //this.CurrentWorksheet.SetRangeDataFormat(new ReoGridRange(0, col.Index, this.CurrentWorksheet.RowCount, col.Index), CellDataFormatFlag.DateTime,
                //                                        new DateTimeDataFormatter.DateTimeFormatArgs
                //                                        {
                //                                            // culture
                //                                            CultureName = "en-US",
                //                                            // pattern
                //                                            Format = "dd/MM/yyyy",
                //                                        });
            }
            else
            {
                this.CurrentWorksheet.SetRangeDataFormat(new ReoGridRange(0, col.Index, this.CurrentWorksheet.RowCount, col.Index), CellDataFormatFlag.Text);
            }
        }

        public void sHideColumn(EnumLedgetType type)
        {
            //there is issue with hiding column. When hide some column events dont work properly
            //var c = GetColumn(type);
            //this.CurrentWorksheet.HideColumns(c.Index, 1);
        }

        #endregion

        #region Data

        void sSetDataItem(int row, int id)
        {
            DataItemInfo info = new DataItemInfo();
            info.ID = id;
            this.CurrentWorksheet.Cells[row, 0].Tag = info;

            if (GlobalParams.IdToScroll == Convert.ToInt64(id))
            {
                this.ScrollToRow(row);
            }
        }

        void sRemoveAllDataItem()
        {
            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                this.CurrentWorksheet.Cells[i, 0].Tag = null;
            }
        }

        public void sBindData(DataTable dt)
        {
            //dicRowID.Clear();
            SourceDataTable = dt;
            this.sRemoveEvents();
            sRemoveAllDataItem();

            //this.CurrentWorksheet.ClearRangeContent(new ReoGridRange(0, 0, this.CurrentWorksheet.RowCount, colList.Count), CellElementFlag.Data);

            this.CurrentWorksheet.ClearRangeContent(ReoGridRange.EntireRange, CellElementFlag.Data);

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    //dicRowID.Add(i, (int)dr[EnumLedgetType.ID.ToString()]);
                    sSetDataItem(i, (int)dr[EnumLedgetType.ID.ToString()]);


                    foreach (LedgerColumn legCol in colList)
                    {

                        var val = dr[legCol.LedgerType.ToString()];

                        if (val == DBNull.Value)
                            val = null;

                        if (legCol.Format == EnumColumnFormat.Date)
                            val = LedgerGridData.ToDateString(val.ToString());

                        if (legCol.Format == EnumColumnFormat.Amount)
                        {
                            val = Convert.ToDouble(val);//hack. We need to convert to display number formating. if we convert to decimal then formatting dont work
                        }

                        this.CurrentWorksheet.SetCellData(i, legCol.Index, val);
                    }
                }
            }
            finally
            {
                this.sRegesterEvents();

                sApplyColumnAlignment();
            }
        }

        public DataTable sGetExportDataTable()
        {
            DataTable dt = new DataTable();

            for (int i = 0; i < this.CurrentWorksheet.ColumnCount; i++)
            {
                var colName = sGetHeader(i);
                if (!dt.Columns.Contains(colName) && !string.IsNullOrWhiteSpace(colName))
                {
                    dt.Columns.Add(colName);
                }
            }

            for (int row = 0; row < this.CurrentWorksheet.RowCount; row++)
            {
                if (!sIsRowEmpty(row))
                {
                    var dr = dt.NewRow();

                    for (int col = 0; col < this.CurrentWorksheet.ColumnCount; col++)
                    {
                        var colName = sGetHeader(col);
                        if (!string.IsNullOrWhiteSpace(colName))
                        {
                            var val = sGetColumnValue(row, col);
                            if (colName != EnumColumnFormat.Date.ToString())
                            {
                                val = AppConverter.ToFloat(val, 0).ToString("N2");
                            }
                            dr[colName] = val;
                        }
                    }

                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        public int? sGetID(int row)
        {
            //int id;
            //if (dicRowID.TryGetValue(row, out id))
            //{
            //    return id;
            //}
            //else
            //    return null;

            if (this.CurrentWorksheet.Cells[row, 0].Tag != null)
            {
                var info = (DataItemInfo)this.CurrentWorksheet.Cells[row, 0].Tag;
                return info.ID;
            }

            return null;
        }
        public void sSaveData()
        {
            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                if (sIsRowEmpty(i) == false && sIsRowValid(i))
                {
                    sCalulateAutoValues(i);
                    var ent = sGetDataItem(i);
                    cnt.Save(ent);
                }
            }
            IsEdited = false;
        }

        DataRow sGetDataRow(int row)
        {
            DataRow dr = SourceDataTable.NewRow();
            int? id = sGetID(row);
            if (id.HasValue)
            {
                dr[EnumLedgetType.ID.ToString()] = id;
            }
            else
                dr[EnumLedgetType.ID.ToString()] = DBNull.Value;

            dr[EnumLedgetType.DocumentItemID.ToString()] = DocumentItemID;

            foreach (var legCol in colList)
            {
                object val = sGetColumnValue(row, legCol.Index);


                if (VatHidden)
                {

                }

                if (dr.Table.Columns.Contains(legCol.LedgerType.ToString()))
                {
                    if (val == null)
                        val = DBNull.Value;

                    dr[legCol.LedgerType.ToString()] = val;
                }
            }

            return dr;
        }

        public tblSDB sGetDataItem(int row)
        {
            DataRow dr = sGetDataRow(row);
            int? id = sGetID(row);
            tblSDB ent = (id.HasValue) ? cnt.Find(id.Value) : new tblSDB();
            if (ent == null)
                ent = new tblSDB();
            DataTableMapper.SetItemFromRow<tblSDB>(ent, dr);
            return ent;
        }

        #endregion

        #region Validation
        public List<int> sValidateFile()
        {
            sRemoveBgColor();
            List<int> invalidRows = new List<int>();
            for (int row = 0; row < this.CurrentWorksheet.RowCount; row++)
            {
                //try
                {
                    if (!sIsRowValid(row))
                    {
                        invalidRows.Add(row);
                    }
                    else
                    {
                        if (!sIsCalculationValid(row))
                        {
                            sSetRowError(row);

                            throw GridException.GetException(GridException.ErrorType.InvalidCalculation, row + 1);
                        }
                    }
                }
                //catch (Exception ecp)
                //{
                //    continue;
                //}
            }
            return invalidRows;
        }

        public bool sIsCalculationValid(int row)
        {
            return true;
        }

        bool sIsSplitDescription(int row)
        {
            return false;
        }

        bool sIsSplitParentNonCal(int row)
        {
            return false;
        }

        public bool sIsRowValid(int row)
        {
            bool isValid = true;
            bool isEmpty = sIsRowEmpty(row);

            if (!isEmpty)
            {
                for (int col = 0; col < this.CurrentWorksheet.ColumnCount; col++)
                {
                    ReoGridPos pos = new ReoGridPos(row, col);
                    sValidateCell(pos);
                    if (!LastEditValid)
                    {
                        sSetInvalidCell(pos);
                        isValid = false;
                    }
                    else
                        sSetValidCell(pos);
                }
            }

            return isValid;
        }

        public bool sIsRowEmpty(int row)
        {
            bool empty = true;
            for (int col = 0; col < this.CurrentWorksheet.ColumnCount; col++)
            {
                ReoGridPos pos = new ReoGridPos(row, col);
                if (sGetColumnValue(pos.Row, pos.Col) != null)
                {
                    empty = false;
                    break;
                }
            }
            return empty;
        }

        public int sFirstEmptyRow()
        {
            int emptyRowIndex = 0;
            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                if (this.sIsRowEmpty(i))
                {
                    emptyRowIndex = i;
                    break;
                }
            }
            return emptyRowIndex;
        }

        void sSetValidCell(ReoGridPos pos)
        {
            this.CurrentWorksheet.RemoveRangeStyle(new ReoGridRange(pos), PlainStyleFlag.BackColor);
        }
        void sSetInvalidCell(ReoGridPos pos)
        {
            sSetBackground(pos, Color.Pink);
        }

        string cellError = null;
        void sValidateCell(ReoGridPos pos)
        {
            LastEditPos = pos;
            cellError = null;
            LedgerColumn col = sGetColumn(pos.Col);
            var newValue = sGetColumnValue(pos.Row, pos.Col);

            bool valueNull = newValue == null || string.IsNullOrWhiteSpace(newValue.ToString());


            if (valueNull && col.IsRequired == false)//Allow empty values
            {
                LastEditValid = true;
                return;
            }
            else if (valueNull && col.IsRequired)
            {
                LastEditValid = false;
                return;
            }

            if (col.Format == EnumColumnFormat.Dropdown || col.Format == EnumColumnFormat.Text)
            {
                if (valueNull)
                    LastEditValid = false;
                else
                    LastEditValid = true;
            }
            else if (col.Format == EnumColumnFormat.Date)
            {
                LastEditValid = CustomValidator.IsDate(newValue);
            }
            else if (col.Format == EnumColumnFormat.Amount)
            {
                LastEditValid = CustomValidator.IsDecimal(newValue);
            }

            //if user enter a value in column which dont have Nominal code 
            if (newValue != null && !string.IsNullOrWhiteSpace(newValue.ToString()))
            {
                float n;
                float.TryParse(newValue.ToString(), out n);

                //only 0 is allowed
                if (n != 0 && string.IsNullOrWhiteSpace(sGetHeader(col.Index)))
                {
                    cellError = string.Format("You cannot enter value in column {0}. Because there is no Nominal code attached with this column.", col.Index + 1);
                    LastEditValid = false;
                }
            }

            if (!LastEditValid)
                sSetInvalidCell(LastEditPos);
            else
                sSetValidCell(LastEditPos);
        }

        #endregion


        #region Style
        public void sRemoveBgColor()
        {
            this.CurrentWorksheet.RemoveRangeStyle(ReoGridRange.EntireRange, PlainStyleFlag.BackColor);
        }

        public void sSetBackground(ReoGridPos pos, Color c)
        {
            ReoGridRange r = new ReoGridRange(pos);
            this.CurrentWorksheet.SetRangeStyle(r, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.BackColor,
                BackColor = c,
            });
        }

        public void sSetRowError(int row)
        {
            var colGross = sGetColumn(EnumLedgetType.Gross);
            var colNet = sGetColumn(EnumLedgetType.Net);
            ReoGridRange r = new ReoGridRange(new ReoGridPos(row, colGross.Index), new ReoGridPos(row, colNet.Index));
            //ReoGridRange r = new ReoGridRange(new ReoGridPos(row, 0));
            this.CurrentWorksheet.SetRangeStyle(r, new WorksheetRangeStyle
            {
                Flag = PlainStyleFlag.BackColor,
                BackColor = Color.LightPink,
            });
        }

        #endregion

        #region Search Content

        public void sSearchContent()
        {
            UcGridSearch cnt = new UcGridSearch();

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            {
                this.CurrentWorksheet.RemoveRangeStyle(ReoGridRange.EntireRange, PlainStyleFlag.BackColor);

                if (!string.IsNullOrWhiteSpace(cnt.SearchTerm))
                {
                    var term = cnt.SearchTerm.ToLower();

                    this.CurrentWorksheet.IterateCells(ReoGridRange.EntireRange, (row, col, cell) =>
                    {
                        if (cell.Data != null && cell.Data.ToString().ToLower().Contains(term))
                        {
                            this.sSetBackground(new ReoGridPos(row, col), Color.LightGray);
                        }
                        return true;
                    });
                }
            }
        }

        #endregion

        void sCalulateAutoValues(int row)
        {
            sCalulateAutoValues(new ReoGridPos(row, 0));
        }
        void sCalulateAutoValues(ReoGridPos pos)
        {
            if (sIsRowEmpty(pos.Row))
                return;

            //LedgerColumn col = sGetColumn(pos.Col);
            try
            {
                var g1 = AppConverter.ToFloat(sGetColumnValue(pos.Row, EnumLedgetType.Gross1), 0);
                var g2 = AppConverter.ToFloat(sGetColumnValue(pos.Row, EnumLedgetType.Gross2), 0);
                var g3 = AppConverter.ToFloat(sGetColumnValue(pos.Row, EnumLedgetType.Gross3), 0);
                var g4 = AppConverter.ToFloat(sGetColumnValue(pos.Row, EnumLedgetType.Gross4), 0);
                var g5 = AppConverter.ToFloat(sGetColumnValue(pos.Row, EnumLedgetType.Gross5), 0);
                var g6 = AppConverter.ToFloat(sGetColumnValue(pos.Row, EnumLedgetType.Gross6), 0);

                this.CurrentWorksheet[pos.Row, sGetColumn(EnumLedgetType.Total).Index] = g1 + g2 + g3 + g4 + g5 + g6;
            }
            catch { }
        }
        void sPopulateAutoValues(ReoGridPos pos)
        {
            var data = sGetColumnValue(pos.Row, pos.Col);

            if (data == null || string.IsNullOrWhiteSpace(data.ToString()))
            {
                if (pos.Row > 0)
                {
                    object upperCellVal = sGetColumnValue(pos.Row - 1, pos.Col);
                    LedgerColumn col = sGetColumn(pos.Col);

                    if (col.LedgerType == EnumLedgetType.Date)
                    {
                        if (CustomValidator.IsDate(upperCellVal))
                        {
                            upperCellVal = Convert.ToDateTime(upperCellVal).AddDays(1).ToString("dd/MM/yyyy");
                        }
                        this.CurrentWorksheet[pos] = upperCellVal;
                    }
                }
            }
        }

        #endregion

        #region Events

        bool cellEdited = false;
        void sheet_BeforeCellKeyDown(object sender, BeforeCellKeyDownEventArgs e)
        {
            if (e.KeyCode == unvell.ReoGrid.Interaction.KeyCode.Enter)
            {
                sPopulateAutoValues(e.CellPosition);
            }

            sCalulateAutoValues(e.CellPosition);
        }

        //Before cell value is changed. It fires for every key press. If cell values is not changed it is not fired. Doesnot work for dropdown.
        void CurrentWorksheet_BeforeCellEdit(object sender, unvell.ReoGrid.Events.CellBeforeEditEventArgs e)
        {
            //ChangePos(e.Cell);

            #region Commented
            //var col = sGetColumn(e.Cell.Column);

            ////make split column readonly
            //if (col.LedgerType == EnumLedgetType.Description)
            //{
            //    if (sIsSplitRow(e.Cell.Row))
            //    {
            //        e.IsCancelled = true;
            //    }
            //}

            //if (col != null && (col.LedgerType == EnumLedgetType.Description))
            //{
            //    e.IsCancelled = true;
            //    var cnt = new UcDescriptionPopup();
            //    if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            //    {
            //        if (!string.IsNullOrWhiteSpace(cnt.SelectedText))
            //            this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.SelectedText;
            //    }
            //}
            //if (col != null && (col.LedgerType == EnumLedgetType.NominalCode))
            //{
            //    e.IsCancelled = true;
            //    var cnt = new UcNominalCodePopup(YrEndFolID);
            //    if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            //    {
            //        if (!string.IsNullOrWhiteSpace(cnt.NominalCode))
            //            this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.NominalCode;
            //    }
            //}
            //if (col != null && (col.LedgerType == EnumLedgetType.VATCode))
            //{
            //    e.IsCancelled = true;
            //    var cnt = new UcVATCodePopup(YrEndFolID);
            //    if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            //    {
            //        if (!string.IsNullOrWhiteSpace(cnt.SelectedText))
            //            this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.SelectedText;
            //    }
            //}

            //if (col.LedgerType == EnumLedgetType.VATCode)
            //{
            //    var dropdown = new DropdownListCell(LedgerGridData.GetVatDisplayTextList());
            //    this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = dropdown;
            //}
            //else
            //{
            //    //var col = GetColumn(e.Cell.Column);

            //}

            //else if (col.LedgerType == EnumLedgetType.NominalCode)
            //{
            //    var dropdown = new DropdownListCell(LedgerGridData.GetNominalDisplayTextList());
            //    this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = dropdown;
            //}
            #endregion
        }

        //After cell value is changed and foucus changed from that cell. Doesnot work for dropdown.
        void CurrentWorksheet_AfterCellEdit(object sender, unvell.ReoGrid.Events.CellAfterEditEventArgs e)
        {
            //ChangePos(e.Cell);

            cellEdited = true;

        }

        void ChangePos(ReoGridPos nextColPos)
        {
            if (cellEdited)
            {
                try
                {
                    sRemoveEvents();

                    var nextColHeader = sGetHeader(nextColPos.Col);
                    if (string.IsNullOrWhiteSpace(nextColHeader))
                    {
                        var pos = new ReoGridPos(nextColPos.Row + 1, 1);
                        //this.CurrentWorksheet.StartEdit(pos);
                        this.CurrentWorksheet.FocusPos = pos;//new ReoGridPos(pos.Row, pos.Col - 1);//this is hack.CurrentWorksheet.FocusPos focus right column of the desired column under BeforeCellKeyDown event.
                    }
                }
                finally
                {
                    cellEdited = false;
                    sRegesterEvents();
                }
            }
        }

        //When data of cell is changed. Work also for dropdown.
        void CurrentWorksheet_CellDataChanged(object sender, unvell.ReoGrid.Events.CellEventArgs e)
        {
            sRemoveEvents();
            try
            {
                sValidateCell(e.Cell.Position);
                //if (LastEditValid)
                sCalulateAutoValues(e.Cell.Position);
                if (!LastEditValid && cellError != null)
                    DisplayManager.DisplayMessage(cellError, MessageType.Error);


            }
            finally
            {
                sRegesterEvents();
                IsEdited = true;
            }
        }

        void CurrentWorksheet_FocusPosChanged(object sender, unvell.ReoGrid.Events.CellPosEventArgs e)
        {
            ChangePos(e.Position);


            //if (LastEditPos != null)
            //{
            //if (!LastEditValid)
            //{
            //    //this.CurrentWorksheet.FocusPosChanged -= CurrentWorksheet_FocusPosChanged;
            //    //this.CurrentWorksheet.FocusPos = LastEditPos;
            //    //this.CurrentWorksheet.StartEdit(LastEditPos);
            //    //DisplayManager.DisplayMessage(CustomMessages.GetValidationMessage(GetColumn(LastEditPos.Col).Name), MessageType.Error);
            //    //this.CurrentWorksheet.FocusPosChanged += CurrentWorksheet_FocusPosChanged;

            //    SetErrorCell(LastEditPos);
            //}
            //else
            //    SetCorrectCell(LastEditPos);

            //}

        }

        #endregion


    }
}
