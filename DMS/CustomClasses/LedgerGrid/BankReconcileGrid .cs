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
    public class BankReconcileGrid : ExcelBase
    {
        tblExcelSheetController cnt = new tblExcelSheetController();

        #region Custom
        public void sInitiatializeGrid(string sheetName)
        {
            this.CurrentWorksheet.Name = sheetName;
            this.CurrentWorksheet.RowCount = 5000;
            sRegesterEvents();
        }

        void sRegesterEvents()
        {
            this.CurrentWorksheet.BeforeCellEdit += CurrentWorksheet_BeforeCellEdit;
            this.CurrentWorksheet.AfterCellEdit += CurrentWorksheet_AfterCellEdit;
            this.CurrentWorksheet.FocusPosChanged += CurrentWorksheet_FocusPosChanged;
            this.CurrentWorksheet.CellDataChanged += CurrentWorksheet_CellDataChanged;
            this.CurrentWorksheet.BeforeCellKeyDown += sheet_BeforeCellKeyDown;

            //this.CurrentWorksheet.CellMouseUp +=    ;

        }

        void sRemoveEvents()
        {
            this.CurrentWorksheet.BeforeCellEdit -= CurrentWorksheet_BeforeCellEdit;
            this.CurrentWorksheet.AfterCellEdit -= CurrentWorksheet_AfterCellEdit;
            this.CurrentWorksheet.FocusPosChanged -= CurrentWorksheet_FocusPosChanged;
            this.CurrentWorksheet.CellDataChanged -= CurrentWorksheet_CellDataChanged;
            this.CurrentWorksheet.BeforeCellKeyDown -= sheet_BeforeCellKeyDown;
        }

        #region Data
        public void sBindData(DataTable dt)
        {
            var padding = new PaddingValue();
            padding.Left = 10;

            SourceDataTable = dt;

            this.sRemoveEvents();
            sRemoveAllDataItem();

            this.CurrentWorksheet.ClearRangeContent(ReoGridRange.EntireRange, CellElementFlag.Data);

            //code to remove previous chekboxes
            var chkboxCol = this.sGetColumn(EnumLedgetType.Tick);
            this.CurrentWorksheet.ClearRangeContent(ReoGridRange.FromCellPosition(0, chkboxCol.Index, this.CurrentWorksheet.RowCount - 1, chkboxCol.Index), CellElementFlag.All);


            try
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    DataRow dr = dt.Rows[i];
                    //dicRowID.Add(i, (int)dr[EnumLedgetType.ID.ToString()]);
                    sSetDataItem(i, (int)dr[EnumLedgetType.ID.ToString()]);

                    //var description = dr[EnumLedgetType.Description.ToString()].ToString();

                    foreach (LedgerColumn legCol in colList)
                    {
                        if (legCol.Format == EnumColumnFormat.Button)
                        {
                            //var cell = ledgerGrid1.CurrentWorksheet.Cells["C3"];
                            //cell.Style.TextColor = Color.Blue;
                            //var button = new ButtonCell("View");
                            //cell.Body = button;

                            //ButtonCell button = new ButtonCell("View");
                            //CurrentWorksheet[i, legCol.Index] = button;
                            //button.Cell.Style.TextColor = Color.Blue;
                            //button.Click += button_Click;

                            var button = new HyperlinkCell("View", false);
                            CurrentWorksheet[i, legCol.Index] = button;
                            button.Cell.Style.TextColor = Color.Blue;
                            button.Click += button_Click;
                            
                            continue;
                        }

                        var val = dr[legCol.LedgerType.ToString()];

                        if (legCol.Format == EnumColumnFormat.CheckBox)
                        {
                            var cell = new CheckBoxCell();
                            CurrentWorksheet[i, legCol.Index] = cell;

                            this.CurrentWorksheet.SetCellData(i, legCol.Index, val);
                            cell.Cell.Style.Padding = padding;
                        }
                        
                        else if (val != DBNull.Value)
                        {

                            if (legCol.Format == EnumColumnFormat.Date)
                                val = LedgerGridData.ToDateString(val.ToString());

                            if (legCol.Format == EnumColumnFormat.Amount)
                            {
                                val = Convert.ToDouble(val);//hack. We need to convert to display number formating. if we convert to decimal then formatting dont work
                            }

                            //this.CurrentWorksheet[i, legCol.Index] = val;
                            this.CurrentWorksheet.SetCellData(i, legCol.Index, val);
                        }
                    }
                }
            }
            finally
            {
                this.sRegesterEvents();
                sApplyColumnAlignment();
            }
        }

        void button_Click(object sender, EventArgs e) 
        {
            if(IsEdited)
            {
                sSaveData();
            }

            var lnk = (HyperlinkCell)sender;
            var itm = sGetDataItem(lnk.Cell.Row);

            GlobalParams.IdToScroll = itm.ID;
            GlobalParams.DocumentItemID = itm.DocumentItemID.Value;

            Repository c = new Repository(AppConstants.RecordType.Ledger);
            var doc = c.Find(itm.DocumentItemID.Value);


            var cnt = this.Parent;
            while(!(cnt is UserControlBase))
            {
                cnt = cnt.Parent;
            }

            UcNavigation.OpenVirtualFile(doc, (UserControlBase)cnt);

        }

        public void sSaveData()
        {
            for (int i = 0; i < this.CurrentWorksheet.RowCount; i++)
            {
                if (sIsRowEmpty(i) == false && sIsRowValid(i))
                {
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

            var legCol = sGetColumn(EnumLedgetType.Tick);
            object val = sGetColumnValue(row, legCol.Index);

            if (val == null)
                val = DBNull.Value;

            dr[legCol.LedgerType.ToString()] = val;

            return dr;
        }

        public tblExcelSheet sGetDataItem(int row)
        {
            DataRow dr = sGetDataRow(row);
            int? id = sGetID(row);
            tblExcelSheet ent = (id.HasValue) ? cnt.Find(id.Value) : new tblExcelSheet();
            if (ent == null)
                ent = new tblExcelSheet();

            //DataTableMapper.SetItemFromRow<tblExcelSheet>(ent, dr);
            ent.Tick = (bool)dr[EnumLedgetType.Tick.ToString()];
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
                            sSetRowError(row, sGetColumn(EnumLedgetType.Debit).Index, sGetColumn(EnumLedgetType.Credit).Index);

                            throw GridException.GetException(GridException.ErrorType.InvalidRows, row + 1);
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
            //if (!sIsRowEmpty(row))
            //{
            //    float crd, deb;
            //    crd = Convert.ToSingle(sGetColumnValue(row, EnumLedgetType.Credit));
            //    deb = Convert.ToSingle(sGetColumnValue(row, EnumLedgetType.Debit));

            //    if (crd < 0 || deb < 0)//credit or debit cant be negative
            //    {
            //        return false;
            //    }
            //    if (crd > 0 && deb > 0)//both cant have values
            //        return false;

            //    if (crd == 0 && deb == 0)//both cant be 0
            //        return false;
            //}
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




        void sSetInvalidCell(ReoGridPos pos)
        {
            sSetBackground(pos, Color.Pink);
        }


        #endregion


        void sShowCalculatedValues(ReoGridPos pos)
        {
            //LedgerColumn col = sGetColumn(pos.Col);

            //if (col.LedgerType == EnumLedgetType.CreditNominalCodeID || col.LedgerType == EnumLedgetType.DebitNominalCodeID)
            //{
            //    var colNet = sGetColumn(EnumLedgetType.Description).Index;
            //    this.CurrentWorksheet[pos.Row, colNet] = grdDataHelper.FetchNominalDescription(sGetColumnValue(pos.Row, EnumLedgetType.NominalCodeID));
            //}
        }

        void sPopulateAutoValues(ReoGridPos pos)//On new line populate auto values
        {
            //No Auto Complete values required
        }

        #endregion

        #region Events

        

        void sheet_BeforeCellKeyDown(object sender, BeforeCellKeyDownEventArgs e)
        {
            if (e.KeyCode == unvell.ReoGrid.Interaction.KeyCode.Space)
            {
                var c = sGetColumn(e.CellPosition.Col);
                if (c.LedgerType != EnumLedgetType.Tick && !sIsRowEmpty(e.CellPosition.Row))//dont insert value Tick is already selected in this case its value is automtically changed
                {
                    IsEdited = true;
                    var colNet = sGetColumn(EnumLedgetType.Tick).Index;
                    bool isChecked = (CurrentWorksheet[e.Cell.Row, colNet] as bool?) ?? false;
                    CurrentWorksheet[e.CellPosition.Row, colNet] = !isChecked;
                }
                sChangePos(new ReoGridPos(e.Cell.Row + 1, e.Cell.Column));
            }
        }

        //Before cell value is changed. It fires for every key press. If cell values is not changed it is not fired. Doesnot work for dropdown.
        void CurrentWorksheet_BeforeCellEdit(object sender, unvell.ReoGrid.Events.CellBeforeEditEventArgs e)
        {
            var col = sGetColumn(e.Cell.Column);

            if (col.LedgerType != EnumLedgetType.Tick)
            {
                e.IsCancelled = true;
            }
            else
            {
                IsEdited = true;
            }
        }

        //After cell value is changed and foucus changed from that cell. Doesnot work for dropdown.
        void CurrentWorksheet_AfterCellEdit(object sender, unvell.ReoGrid.Events.CellAfterEditEventArgs e)
        {
            IsEdited = true;
        }

        //When data of cell is changed. Work also for dropdown.
        void CurrentWorksheet_CellDataChanged(object sender, unvell.ReoGrid.Events.CellEventArgs e)
        {
            IsEdited = true;
            //sRemoveEvents();
            //try
            //{
            //    sValidateCell(e.Cell.Position);
            //    if (LastEditValid)
            //        sShowCalculatedValues(e.Cell.Position);
            //}
            //finally
            //{
            //    sRegesterEvents();
            //    IsEdited = true;
            //}
        }

        void CurrentWorksheet_FocusPosChanged(object sender, unvell.ReoGrid.Events.CellPosEventArgs e)
        {

        }

        #endregion

        void sChangePos(ReoGridPos pos)
        {
            try
            {
                sRemoveEvents();
                this.CurrentWorksheet.FocusPos = pos;//new ReoGridPos(pos.Row, pos.Col - 1);//this is hack.CurrentWorksheet.FocusPos focus right column of the desired column under BeforeCellKeyDown event.
            }
            finally
            {
                sRegesterEvents();
            }

        }
    }
}
