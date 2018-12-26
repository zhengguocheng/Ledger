using DAL;
using DAL.Controllers;
using DMS.UserControls;
using DMS.UserControls.Popups;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;
using unvell.ReoGrid.Events;
using Utilities;

namespace DMS.CustomClasses
{
    public struct RowInfo
    {
        public int index { get; set; }
        public long RecordID { get; set; }
    }
    public class LedgerGrid : ReoGridControl
    {
        public ReoGridPos LastEditPos;
        public bool LastEditValid;
        public List<LedgerColumn> colList = new List<LedgerColumn>();
        public DataTable SourceDataTable;
        public long DocumentItemID, YrEndFolID;
        public string NominalCode;
        tblExcelSheetController cnt = new tblExcelSheetController();
        public LedgerGridData grdDataHelper;
        public bool IsEdited = false;
        tblSupplierController supCnt = new tblSupplierController();
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

        void sRegesterEvents()
        {
            this.CurrentWorksheet.BeforeCellEdit += CurrentWorksheet_BeforeCellEdit;
            this.CurrentWorksheet.AfterCellEdit += CurrentWorksheet_AfterCellEdit;
            this.CurrentWorksheet.FocusPosChanged += CurrentWorksheet_FocusPosChanged;
            this.CurrentWorksheet.CellDataChanged += CurrentWorksheet_CellDataChanged;
            this.CurrentWorksheet.BeforeCellKeyDown += sheet_BeforeCellKeyDown;
        }

        void sRemoveEvents()
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

        public void InsertRows(int count)
        {
            sSetAllColumnFormat();
            CurrentWorksheet.InsertRows(this.CurrentWorksheet.FocusPos.Row + 1, count);
            sSetAllColumnFormat();
            sSetNominal();
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
            int? splitParent = null;
            var c = sGetColumn(col);

            if (sIsSplitRepeatColumn(c) && sIsSplitRow(row))
            {
                if (c.LedgerType == EnumLedgetType.Gross || c.LedgerType == EnumLedgetType.VAT)
                {
                    return 0;
                }
                else
                {
                    splitParent = sGetSplitParentRow(row);
                    if (splitParent != null)
                        return this.CurrentWorksheet[splitParent.Value, col];
                    else
                        throw GridException.GetException(GridException.ErrorType.SplitParentInvalid, row + 1);
                }
            }

            return this.CurrentWorksheet[row, col];
        }

        public float sGetColumnSum(EnumLedgetType type)
        {
            float sum = 0, cellValue = 0;

            ReoGridRange r = sGetColumnRange(type);
            this.CurrentWorksheet.IterateCells(r, (row, col, cell) =>
            {
                if (cell.Data != null)
                {
                    if (float.TryParse(cell.Data.ToString(), out cellValue))
                        sum += cellValue;
                }
                return true;
            });

            return sum;
        }

        public void sSetAllColumnFormat()
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

        DataTable SortBySplitRows(DataTable dt)
        {
            var dtNew = dt.Clone();

            foreach (DataRow dr in dt.Rows)
            {
                var description = dr[EnumLedgetType.Description.ToString()].ToString();
                var isSplit = AppConstants.sIsSplitText(description); //string.Equals(description, AppConstants.splitValue);
                if (!isSplit)
                {
                    dtNew.ImportRow(dr);

                    //Now add all split rows 
                    var recordID = dr[EnumLedgetType.ID.ToString()].ToString();
                    var arr = dt.Select("SplitParentID = " + recordID, EnumLedgetType.ID.ToString());
                    foreach (DataRow splitRow in arr)
                    {
                        dtNew.ImportRow(splitRow);
                    }
                }
            }
            return dtNew;
        }

        public void sBindData(DataTable dt)
        {
            //dicRowID.Clear();
            dt = SortBySplitRows(dt);
            SourceDataTable = dt;

            //this.CurrentWorksheet.ClearRangeContent(new ReoGridRange(0, 0, this.CurrentWorksheet.RowCount, colList.Count), CellElementFlag.Data);

            this.sRemoveEvents();
            sRemoveAllDataItem();

            this.CurrentWorksheet.ClearRangeContent(ReoGridRange.EntireRange, CellElementFlag.Data);

            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    //dicRowID.Add(i, (int)dr[EnumLedgetType.ID.ToString()]);
                    sSetDataItem(i, (int)dr[EnumLedgetType.ID.ToString()]);

                    bool isSplit = false;
                    var description = dr[EnumLedgetType.Description.ToString()].ToString();
                    isSplit = AppConstants.sIsSplitText(description);//string.Equals(description, AppConstants.splitValue);

                    foreach (LedgerColumn legCol in colList)
                    {
                        var val = dr[legCol.LedgerType.ToString()];

                        if (val == DBNull.Value)
                            val = null;

                        if (legCol.Format == EnumColumnFormat.Date)
                            val = LedgerGridData.ToDateString(val.ToString());

                        if (legCol.LedgerType == EnumLedgetType.NominalCode)
                        {
                            val = grdDataHelper.FetchNominalDisplayText(val);
                        }
                        else if (VatHidden == false && legCol.LedgerType == EnumLedgetType.VATCode)
                        {
                            val = grdDataHelper.FetchVatDisplayText(val);
                        }
                        else if (legCol.Format == EnumColumnFormat.Amount)
                        {
                            val = Convert.ToDouble(val);//hack. We need to convert to display number formating. if we convert to decimal then formatting dont work
                        }

                        if (isSplit && legCol.LedgerType != EnumLedgetType.Description && legCol.LedgerType != EnumLedgetType.Net && legCol.LedgerType != EnumLedgetType.NominalCode && legCol.LedgerType != EnumLedgetType.Comment)
                        {
                            continue;
                        }
                        //this.CurrentWorksheet[i, legCol.Index] = val;
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
                var colName = sGetColumn(i).Name;
                if (!dt.Columns.Contains(colName))
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
                        var legCol = sGetColumn(col);
                        var colName = legCol.Name;
                        var val = sGetColumnValue(row, col);

                        if (legCol.Format == EnumColumnFormat.Amount)
                        {
                            val = AppConverter.ToFloat(val, 0).ToString("N2");
                        }

                        dr[colName] = val;
                    }
                    dt.Rows.Add(dr);
                }
            }

            return dt;
        }

        public int? sGetID(int row)
        {
            if (this.CurrentWorksheet.Cells[row, 0].Tag != null)
            {
                var info = (DataItemInfo)this.CurrentWorksheet.Cells[row, 0].Tag;
                return info.ID;
            }

            return null;
        }

        int? prevParentRowID = null;
        public void sSaveData()
        {
            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                if (sIsRowEmpty(i) == false && sIsRowValid(i))
                {
                    var ent = sGetDataItem(i);
                    if (ent.NominalCode == null)
                    {
                        throw new Exception("Nominal code is null.");
                    }

                    if (sIsSplitRow(i))
                    {
                        ent.SplitParentID = prevParentRowID;
                    }

                    cnt.Save(ent);

                    if (sIsSplitRow(i) == false)
                    {
                        prevParentRowID = ent.ID;
                    }

                    try
                    {
                        if (!sIsSplitDescription(i))
                            supCnt.Add_If_NotExist(ent);//when data is copied then we need to add supplier
                    }
                    catch { }
                }


                var id = sGetID(i);

                if (sIsRowEmpty(i) && id.HasValue)//it means user has deleted the data of this row
                {
                    try
                    {
                        cnt.Delete(id.Value);
                    }
                    catch (Exception ecp)
                    {
                        throw ecp;
                    }
                }
            }


            //Delete all split rows whose Split parent rows are deleted
            DBHelper.ExecuteSP_NoRead(SPNames.DeleteOrphanSplitRows);

            IsEdited = false;
        }

        public void sSetNominal()
        {
            var ind = sGetColumn(EnumLedgetType.NominalCode).Index;

            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                if (sIsRowEmpty(i) == false)
                {
                    var val = this.sGetColumnValue(i, ind);
                    if (val != null && val.ToString().Length == 1)
                    {
                        val = "00" + val.ToString();
                    }
                    if (val != null && val.ToString().Length == 2)
                    {
                        val = "0" + val.ToString();
                    }
                    this.CurrentWorksheet[i, ind] = val;

                }
            }
        }

        DataRow sGetDataRow(int row)
        {
            DataRow dr = SourceDataTable.NewRow();
            int? id = sGetID(row);

            if (id.HasValue)
            {
                dr[EnumLedgetType.ID.ToString()] = id;
                //Copy existing row values in new row
                var arr = AppDataTable.FilterRows(EnumLedgetType.ID.ToString(), id.Value, SourceDataTable);
                if (arr.Length > 0)
                    dr.ItemArray = arr[0].ItemArray.Clone() as object[];
            }
            else
                dr[EnumLedgetType.ID.ToString()] = DBNull.Value;

            dr[EnumLedgetType.DocumentItemID.ToString()] = DocumentItemID;


            foreach (var legCol in colList)
            {
                object val = sGetColumnValue(row, legCol.Index);

                if (legCol.LedgerType == EnumLedgetType.NominalCode)
                {
                    val = grdDataHelper.FetchNominalValue(val);
                }
                else if (VatHidden == false && legCol.LedgerType == EnumLedgetType.VATCode)
                {
                    val = grdDataHelper.FetchVatValue(val);
                }

                if (VatHidden)
                {
                    dr[EnumLedgetType.VATCode.ToString()] = DBNull.Value;
                    dr[EnumLedgetType.VAT.ToString()] = DBNull.Value;
                }

                if (val == null || val.ToString() == "")
                {
                    val = DBNull.Value;
                }

                dr[legCol.LedgerType.ToString()] = val;
            }



            return dr;
        }

        public tblExcelSheet sGetDataItem(int row)
        {
            DataRow dr = sGetDataRow(row);
            int? id = sGetID(row);

            tblExcelSheet ent = (id.HasValue) ? cnt.Find(id.Value) : new tblExcelSheet();
            if (ent == null)
                ent = new tblExcelSheet();

            DataTableMapper.SetItemFromRow<tblExcelSheet>(ent, dr);
            return ent;
        }

        #endregion

        #region Validation
        public List<int> sValidateFile()
        {
            sCalculateFile();
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
            if (sIsSplitParentNonCal(row))
            {
                decimal totalNet = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.Net));
                decimal gross = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.Gross));
                decimal vat = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.VAT));

                int splitRow = row + 1;

                while (sIsSplitDescription(splitRow))
                {
                    totalNet += Convert.ToDecimal(sGetColumnValue(splitRow, EnumLedgetType.Net));
                    splitRow++;
                }

                if (gross != totalNet + vat)
                {
                    return false;
                }
            }
            else if (sIsSplitDescription(row))//if row is split then dont do anything as the calculation is already done under split parent
            {
            }
            else
            {
                var net = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.Net));
                var gross = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.Gross));
                var vat = AppConverter.ToDecimal(sGetColumnValue(row, EnumLedgetType.VAT), 0);
                if (gross != net + vat)
                {
                    return false;
                }
            }
            return true;
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

        void sSetValidCell(ReoGridPos pos)
        {
            this.CurrentWorksheet.RemoveRangeStyle(new ReoGridRange(pos), PlainStyleFlag.BackColor);
        }
        void sSetInvalidCell(ReoGridPos pos)
        {
            sSetBackground(pos, Color.Pink);
        }

        void sValidateCell(ReoGridPos pos)
        {
            LastEditPos = pos;

            LedgerColumn col = sGetColumn(pos.Col);
            var newValue = sGetColumnValue(pos.Row, pos.Col);

            if (sIsSplitRow(pos.Row))
            {
                if (sIsSplitRepeatColumn(col))
                {
                    LastEditValid = true;
                    return;
                }
            }


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

            if (!LastEditValid)
                sSetInvalidCell(LastEditPos);
            else
                sSetValidCell(LastEditPos);
        }

        #endregion

        #region Split

        void sAddSplitRow(int row)
        {
            decimal total = 0;
            int i = row - 1;
            decimal gross = 0;

            for (; i >= 0; i--)
            {
                if (sIsSplitRow(i) || sIsSplitParent(i))
                {
                    try
                    {
                        total += Convert.ToDecimal(sGetColumnValue(i, EnumLedgetType.Net));
                    }
                    catch { }

                    if (sIsSplitParent(i))
                        try
                        {
                            total += Convert.ToDecimal(sGetColumnValue(i, EnumLedgetType.VAT));
                            gross = Convert.ToDecimal(sGetColumnValue(i, EnumLedgetType.Gross));
                            break;
                        }
                        catch { }
                }
                else
                    break;
            }

            if (total < gross)
            {
                //modify by @zgc 
                //date
                var datepos = new ReoGridPos(row, sGetColumn(EnumLedgetType.Date).Index);

                sPopulateAutoValues(datepos);

                //reference
                var referencepos = new ReoGridPos(row, sGetColumn(EnumLedgetType.Reference).Index);
                sPopulateAutoValues(referencepos);

                if (!VatHidden)
                {
                    //vat
                    var vatpos = new ReoGridPos(row, sGetColumn(EnumLedgetType.VAT).Index);
                    sPopulateAutoValues(vatpos);
                    //vatcode
                    var vatcodepos = new ReoGridPos(row, sGetColumn(EnumLedgetType.VATCode).Index);
                    sPopulateAutoValues(vatcodepos);
                }
                //end modify by @zgc

                var desc = sGetColumnValue(row - 1, sGetColumn(EnumLedgetType.Description).Index);
                

                this.CurrentWorksheet[row, sGetColumn(EnumLedgetType.Description).Index] = AppConstants.BuildSplitText(desc.ToString());
                var pos = new ReoGridPos(row, sGetColumn(EnumLedgetType.Net).Index);

                this.CurrentWorksheet[pos] = gross - total;
                this.CurrentWorksheet.StartEdit(pos);
                this.CurrentWorksheet.FocusPos = new ReoGridPos(pos.Row, pos.Col - 1);//this is hack.CurrentWorksheet.FocusPos focus right column of the desired column under BeforeCellKeyDown event.

            }
        }

        public bool sIsSplitRow(int row)
        {
            object val = sGetColumnValue(row, EnumLedgetType.Description);
            if (val != null)
            {
                if (AppConstants.sIsSplitText(val.ToString()))//val.ToString() == AppConstants.splitValue)
                    return true;
            }
            return false;
        }

        public bool sIsSplitParent(int row)
        {
            if (!sIsSplitRow(row) && !sIsRowEmpty(row) && sIsRowValid(row))
            {
                try
                {
                    var net = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.Net));
                    var gross = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.Gross));
                    var vat = Convert.ToDecimal(sGetColumnValue(row, EnumLedgetType.VAT));
                    return net + vat < gross;
                }
                catch { }
            }
            return false;
        }

        public bool sIsSplitRepeatColumn(LedgerColumn col)
        {
            if (col.LedgerType == EnumLedgetType.Date || col.LedgerType == EnumLedgetType.Reference
                    || col.LedgerType == EnumLedgetType.Gross || col.LedgerType == EnumLedgetType.VATCode ||
                    col.LedgerType == EnumLedgetType.VAT)
            {
                return true;
            }
            else
                return false;
        }

        public int? sGetSplitParentRow(int row)
        {
            int splitParent = row - 1;

            for (; splitParent >= 0; splitParent--)
            {
                if (sIsSplitParent(splitParent))
                {
                    return splitParent;
                }
            }

            return null;
        }

        bool sIsSplitDescription(int row)
        {
            var desc = (sGetColumnValue(row, EnumLedgetType.Description));
            if (desc != null)
                return AppConstants.sIsSplitText(desc.ToString());
            return false;
        }

        //bool sIsSplitText(string txt)
        //{
        //    return string.Equals(txt, AppConstants.splitValue);
        //}

        bool sIsSplitParentNonCal(int row)
        {
            if (!sIsSplitDescription(row) && sIsSplitDescription(row + 1))
                return true;
            else
                return false;
        }


        #endregion

        #region UI

        //public void ScrollToRow(int rowNo)
        //{
        //    CurrentWorksheet.ScrollToCell(rowNo, 0);
        //    CurrentWorksheet.SelectRows(rowNo, 1);
        //}

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

        void sCalculateFile()//All file calculation
        {
            return;//now i am calculating vat as its values is changed.

            var colNet = sGetColumn(EnumLedgetType.Net).Index;

            for (int row = 0; row < this.CurrentWorksheet.RowCount; row++)
            {
                if (!sIsRowEmpty(row) && !sIsSplitParent(row) && !sIsSplitRow(row))
                {

                    var objGross = sGetColumnValue(row, EnumLedgetType.Gross);
                    var objCode = sGetColumnValue(row, EnumLedgetType.VATCode);
                    var objVat = grdDataHelper.CalculateVat(objCode, objGross);
                    this.CurrentWorksheet[row, colNet] = LedgerGridData.CalculateNet(objVat, objGross);

                    if (!VatHidden)
                    {
                        var colVAT = sGetColumn(EnumLedgetType.VAT).Index;

                        if (objCode == null)
                        {
                            this.CurrentWorksheet[row, colVAT] = 0;
                        }
                        if (objVat != null)
                        {
                            this.CurrentWorksheet[row, colVAT] = AppConverter.ToDecimal(objVat, 0).ToString("N2");
                        }
                    }
                }
            }
        }

        void sCalulateAutoValues(ReoGridPos pos)
        {
            #region commented
            //LedgerColumn col = sGetColumn(pos.Col);

            //if (col.LedgerType == EnumLedgetType.VATCode || col.LedgerType == EnumLedgetType.Gross)
            //{
            //    var objGross = sGetColumnValue(pos.Row, EnumLedgetType.Gross);

            //    object objVat = null;
            //    if (!VatHidden)
            //    {
            //        var objCode = sGetColumnValue(pos.Row, EnumLedgetType.VATCode);
            //        var colVatIndex = sGetColumn(EnumLedgetType.VAT).Index;
            //        objVat = grdDataHelper.CalculateVat(objCode, objGross);
            //        this.CurrentWorksheet[pos.Row, colVatIndex] = objVat;
            //    }

            //    var colNet = sGetColumn(EnumLedgetType.Net).Index;
            //    this.CurrentWorksheet[pos.Row, colNet] = LedgerGridData.CalculateNet(objVat, objGross);
            //}
            #endregion

            LedgerColumn colEdited = sGetColumn(pos.Col);

            if (colEdited.LedgerType == EnumLedgetType.VATCode || colEdited.LedgerType == EnumLedgetType.Gross || colEdited.LedgerType == EnumLedgetType.VAT)
            {
                var objGross = sGetColumnValue(pos.Row, EnumLedgetType.Gross);
                decimal vat = 0;
                if (!VatHidden)
                {
                    object objVat = null;

                    if (colEdited.LedgerType == EnumLedgetType.VATCode)//if user edited VATCode then calculate value according to new vatcode
                    {
                        var objCode = sGetColumnValue(pos.Row, EnumLedgetType.VATCode);
                        objVat = grdDataHelper.CalculateVat(objCode, objGross);
                    }
                    else//if user edited gross or VAT amount then user entered amount in VAT column
                    {
                        objVat = sGetColumnValue(pos.Row, EnumLedgetType.VAT);
                    }

                    vat = AppConverter.ToDecimal(objVat, 0);

                    var colVatIndex = sGetColumn(EnumLedgetType.VAT).Index;
                    this.CurrentWorksheet[pos.Row, colVatIndex] = vat.ToString("N2");
                }

                var colNet = sGetColumn(EnumLedgetType.Net).Index;
                this.CurrentWorksheet[pos.Row, colNet] = LedgerGridData.CalculateNet(vat, objGross);
            }

        }
        void sPopulateAutoValues(ReoGridPos pos)
        {
            var data = sGetColumnValue(pos.Row, pos.Col);

            if (data == null || string.IsNullOrWhiteSpace(data.ToString()))
            {
                object upperCellVal = data;
                LedgerColumn col = sGetColumn(pos.Col);

                for (int i = pos.Row - 1; i >= 0; i--)
                {
                    upperCellVal = sGetColumnValue(i, pos.Col);
                    if (upperCellVal != null && !string.IsNullOrWhiteSpace(upperCellVal.ToString())
                        //&& upperCellVal.ToString() != AppConstants.splitValue
                        && !AppConstants.sIsSplitText(upperCellVal.ToString())
                        )
                    {
                        break;
                    }
                }

                if (col.LedgerType == EnumLedgetType.NominalCode)
                {
                    //if (!string.IsNullOrWhiteSpace(this.NominalCode))
                    //    this.CurrentWorksheet[pos] = NominalCode;
                    //else
                    this.CurrentWorksheet[pos] = upperCellVal;//new requirement. Repeat upper cell code.
                }
                else if (col.RepeatType == EnumRepeatType.UpperCell)
                {
                    this.CurrentWorksheet[pos] = upperCellVal;
                }
                else if (col.RepeatType == EnumRepeatType.UpperCellIncrement)
                {
                    if (CustomValidator.IsInt(upperCellVal))
                    {
                        upperCellVal = Convert.ToInt32(upperCellVal) + 1;
                    }
                    this.CurrentWorksheet[pos] = upperCellVal;
                }
            }
        }

        #endregion

        #region Events
        void sheet_BeforeCellKeyDown(object sender, BeforeCellKeyDownEventArgs e)
        {
            if (e.KeyCode == unvell.ReoGrid.Interaction.KeyCode.Enter)
            {
                sPopulateAutoValues(e.CellPosition);

                var colNominal = sGetColumn(EnumLedgetType.NominalCode);
                if (e.Cell != null && colNominal.Index == e.Cell.Column)
                {
                    if (sIsSplitParent(e.Cell.Row) || sIsSplitRow(e.Cell.Row))
                    {
                        sAddSplitRow(e.Cell.Row + 1);

                    }
                }
            }

            if (e.KeyCode == unvell.ReoGrid.Interaction.KeyCode.Delete)
            {
                var colVatCode = sGetColumn(EnumLedgetType.VATCode);

                if (!VatHidden && e.Cell != null && colVatCode.Index == e.Cell.Column)
                {
                    this.CurrentWorksheet[e.Cell.Row, sGetColumn(EnumLedgetType.VAT).Index] = 0;
                    this.CurrentWorksheet[e.Cell.Row, sGetColumn(EnumLedgetType.Net).Index] = sGetColumnValue(e.Cell.Row, sGetColumn(EnumLedgetType.Gross).Index);
                }
            }
        }

        //Before cell value is changed. It fires for every key press. If cell values is not changed it is not fired. Doesnot work for dropdown.
        void CurrentWorksheet_BeforeCellEdit(object sender, unvell.ReoGrid.Events.CellBeforeEditEventArgs e)
        {
            var col = sGetColumn(e.Cell.Column);

            //make split column readonly
            if (col.LedgerType == EnumLedgetType.Description)
            {
                if (sIsSplitRow(e.Cell.Row))
                {
                    e.IsCancelled = true;
                }
            }

            if (col != null && (col.LedgerType == EnumLedgetType.Description))
            {
                e.IsCancelled = true;
                var cnt = new UcSupplierPopup(YrEndFolID, e.EditText);
                if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(cnt.SelectedText))
                    {
                        this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.SelectedText;

                        if (!string.IsNullOrWhiteSpace(cnt.SelectedNominalCode))
                        {
                            var currentNomCode = sGetColumnValue(e.Cell.Row, EnumLedgetType.NominalCode);
                            if (currentNomCode == null || string.IsNullOrWhiteSpace(currentNomCode.ToString()))
                            {
                                this.CurrentWorksheet[e.Cell.Row, sGetColumn(EnumLedgetType.NominalCode).Index] = cnt.SelectedNominalCode;
                            }
                            else
                            {
                                if (DisplayManager.DisplayMessage("Do you want to overwrite Nominal Code for this row?", MessageType.Confirmation) == DialogResult.Yes)
                                {
                                    this.CurrentWorksheet[e.Cell.Row, sGetColumn(EnumLedgetType.NominalCode).Index] = cnt.SelectedNominalCode;
                                }
                            }
                        }
                    }
                }
            }
            if (col != null && (col.LedgerType == EnumLedgetType.NominalCode))
            {
                e.IsCancelled = true;
                var cnt = new UcNominalCodePopup(YrEndFolID, e.EditText);
                if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(cnt.NominalCode))
                        this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.NominalCode;
                }
            }
            if (col != null && (col.LedgerType == EnumLedgetType.VATCode))
            {
                e.IsCancelled = true;
                var cnt = new UcVATCodePopup(YrEndFolID);
                if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(cnt.SelectedText))
                        this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.SelectedText;
                }
            }

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
        }

        //After cell value is changed and foucus changed from that cell. Doesnot work for dropdown.
        void CurrentWorksheet_AfterCellEdit(object sender, unvell.ReoGrid.Events.CellAfterEditEventArgs e)
        {


        }

        //When data of cell is changed. Work also for dropdown.
        void CurrentWorksheet_CellDataChanged(object sender, unvell.ReoGrid.Events.CellEventArgs e)
        {
            sRemoveEvents();
            try
            {
                sValidateCell(e.Cell.Position);
                if (LastEditValid)
                    sCalulateAutoValues(e.Cell.Position);
            }
            finally
            {
                sRegesterEvents();
                IsEdited = true;
            }
        }

        void CurrentWorksheet_FocusPosChanged(object sender, unvell.ReoGrid.Events.CellPosEventArgs e)
        {
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
