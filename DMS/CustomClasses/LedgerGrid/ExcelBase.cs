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
    public class ExcelBase : ReoGridControl
    {
        public ReoGridPos LastEditPos;
        public bool LastEditValid;
        public List<LedgerColumn> colList = new List<LedgerColumn>();
        public DataTable SourceDataTable;
        public long DocumentItemID, YrEndFolID;
        public LedgerGridData grdDataHelper;
        public bool IsEdited = false;
        public decimal totalPay, totalRec, outPay, outRec;
        protected bool IscellEdited = false;


        #region My Events

        public event sDelegateNeedSumUpdate sNeedSumUpdate;
        public delegate void sDelegateNeedSumUpdate();

        #endregion

        #region Custom

        public void sOnParentLoad()
        {
            this.CurrentWorksheet.SetRangeStyle(ReoGridRange.EntireRange, new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.TextWrap,
                TextWrapMode = TextWrapMode.WordBreak,
            });

            //DataFormatterManager.Instance.DataFormatters.Add(CellDataFormatFlag.Custom, new MyTextFormatter());
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
            //sApplyFilter();
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

        public void sSetColumnIndex()
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
               // throw GridException.GetException(GridException.ErrorType.ColumnNotFound);
                return null;
            }

        }

        public ReoGridRange sGetColumnRange(EnumLedgetType type)
        {
            var col = sGetColumn(type);
            ReoGridRange r = new ReoGridRange(new ReoGridPos(0, col.Index), new ReoGridPos(this.CurrentWorksheet.RowCount - 1, col.Index));
            return r;
        }


        protected object sGetColumnValue(int row, EnumLedgetType type)
        {
            var colGross = sGetColumn(type);
            if (colGross != null)
                return sGetColumnValue(row, colGross.Index);
            else
                return null;
        }

        protected virtual object sGetColumnValue(int row, int col)
        {
            var c = sGetColumn(col);
            return this.CurrentWorksheet[row, col];
        }

        public double sGetColumnSum(EnumLedgetType type)
        {
            double sum = 0, cellValue = 0;

            ReoGridRange r = sGetColumnRange(type);
            this.CurrentWorksheet.IterateCells(r, (row, col, cell) =>
            {
                if (cell.Data != null)
                {
                    if (double.TryParse(cell.Data.ToString(), out cellValue))
                        sum += cellValue;
                }
                return true;
            });

            return sum;
        }

        public void sCalSum()
        {
            totalPay = totalRec = outPay = outRec = 0;
            foreach (DataRow dr in SourceDataTable.Rows)
            {
                totalPay += AppConverter.ToDecimal(dr[EnumLedgetType.Payments.ToString()], 0);
                totalRec += AppConverter.ToDecimal(dr[EnumLedgetType.Receipts.ToString()], 0);

                if(AppConverter.ToBool(dr[EnumLedgetType.Tick.ToString()],false) == false)//if unticked
                {
                    outPay += AppConverter.ToDecimal(dr[EnumLedgetType.Payments.ToString()], 0);
                    outRec += AppConverter.ToDecimal(dr[EnumLedgetType.Receipts.ToString()], 0);
                }
            }
        }

        public void sSetAllColumnFormat()
        {
            foreach (var item in colList)
            {
                sSetColumnFormat(item);
            }
        }
        public void sSetColumnFormat(LedgerColumn col)
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
            else if (col.Format == EnumColumnFormat.CheckBox)
            {
                //this.CurrentWorksheet.ColumnHeaders[5].DefaultCellBody = typeof(unvell.ReoGrid.CellTypes.CheckBoxCell);
                //this.CurrentWorksheet.ColumnHeaders[5].Style.HorizontalAlign = ReoGridHorAlign.Center;
                // give check box a small padding (2 pixels)
                //this.CurrentWorksheet.ColumnHeaders[col.Index].Style.Padding = new unvell.ReoGrid.PaddingValue(2); //new System.Windows.Forms.Padding(2);
                //CurrentWorksheet["F1:F5"] = new object[] { false, true, false, false, true };
            
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

        public string sGetHeaderText(int index)
        {
            return this.CurrentWorksheet.ColumnHeaders[index].Text;
        }


        #endregion

        #region Data

        public void sSetDataItem(int row, int id)
        {
            DataItemInfo info = new DataItemInfo();
            info.ID = id;
            this.CurrentWorksheet.Cells[row, 0].Tag = info;

            if (GlobalParams.IdToScroll == Convert.ToInt64(id))
            {
                this.ScrollToRow(row);
            }
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
        

        #endregion

        #region Validation
        
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

        public void sSetValidCell(ReoGridPos pos)
        {
            this.CurrentWorksheet.RemoveRangeStyle(new ReoGridRange(pos), PlainStyleFlag.BackColor);
        }
        void sSetInvalidCell(ReoGridPos pos)
        {
            sSetBackground(pos, Color.Pink);
        }

        public void sValidateCell(ReoGridPos pos)
        {
            LastEditPos = pos;

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

        public void sSetRowError(int row, int stColInd, int endColInd)
        {

            ReoGridRange r = new ReoGridRange(new ReoGridPos(row, stColInd), new ReoGridPos(row, endColInd));
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
                
        #endregion

        protected void sRemoveAllDataItem()
        {
            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                this.CurrentWorksheet.Cells[i, 0].Tag = null;
            }
        }
    }
}
