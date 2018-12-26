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
    public class MultipleEntJournalsGrid : ExcelBase
    {
        tblMultipleEntJournalController cnt = new tblMultipleEntJournalController();
       
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
        
        #region Data
        public void sBindData(DataTable dt)
        {
            SourceDataTable = dt;
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

                    //var description = dr[EnumLedgetType.Description.ToString()].ToString();

                    foreach (LedgerColumn legCol in colList)
                    {
                        var val = dr[legCol.LedgerType.ToString()];
                        if (val != DBNull.Value)
                        {

                            if (legCol.Format == EnumColumnFormat.Date)
                                val = LedgerGridData.ToDateString(val.ToString());

                            if (legCol.LedgerType == EnumLedgetType.NominalCodeID)
                            {
                                val = grdDataHelper.FetchNominalDisplayText(val);
                                this.CurrentWorksheet.GetCell(i, legCol.Index).DataFormat = CellDataFormatFlag.Text;
                            }
                            else if (legCol.Format == EnumColumnFormat.Amount)
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

            dr[EnumLedgetType.DocumentItemID.ToString()] = DocumentItemID;

            foreach (var legCol in colList)
            {
                object val = sGetColumnValue(row, legCol.Index);

                if (legCol.LedgerType == EnumLedgetType.NominalCodeID)
                {
                    val = grdDataHelper.FetchNominalValue(val);
                }

                if (val == null || string.IsNullOrWhiteSpace(val.ToString()))
                    val = DBNull.Value;

                dr[legCol.LedgerType.ToString()] = val;
            }

            return dr;
        }

        public tblMultipleEntJournal sGetDataItem(int row)
        {
            DataRow dr = sGetDataRow(row);
            int? id = sGetID(row);
            tblMultipleEntJournal ent = (id.HasValue) ? cnt.Find(id.Value) : new tblMultipleEntJournal();
            if (ent == null)
                ent = new tblMultipleEntJournal();
            DataTableMapper.SetItemFromRow<tblMultipleEntJournal>(ent, dr);
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
                            sSetRowError(row,sGetColumn(EnumLedgetType.Debit).Index,sGetColumn(EnumLedgetType.Credit).Index);

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
            if (!sIsRowEmpty(row))
            {
                float crd, deb;
                crd = AppConverter.ToFloat(sGetColumnValue(row, EnumLedgetType.Credit), 0);
                deb = AppConverter.ToFloat(sGetColumnValue(row, EnumLedgetType.Debit), 0);

                //if (crd < 0 || deb < 0)//credit or debit cant be negative
                //{
                //    return false;
                //}
                if (crd > 0 && deb > 0)//both cant have values
                    return false;

                if (crd == 0 && deb == 0)//both cant be 0
                    return false;
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

            var data = sGetColumnValue(pos.Row, pos.Col);

            if (data == null || string.IsNullOrWhiteSpace(data.ToString()))
            {
                object upperCellVal = data;
                LedgerColumn col = sGetColumn(pos.Col);

                for (int i = pos.Row - 1; i >= 0; i--)
                {
                    upperCellVal = sGetColumnValue(i, pos.Col);
                    if (upperCellVal != null && !string.IsNullOrWhiteSpace(upperCellVal.ToString()))
                    {
                        break;
                    }
                }

               //modify By @zgc
                //if (col.LedgerType == EnumLedgetType.Date || col.LedgerType == EnumLedgetType.Reference)
                if (col.LedgerType == EnumLedgetType.Date || col.LedgerType == EnumLedgetType.Reference ||
                   col.LedgerType == EnumLedgetType.Description || col.LedgerType == EnumLedgetType.NominalCodeID)
                //end modify @zgc
                {
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
            }
        }

        //Before cell value is changed. It fires for every key press. If cell values is not changed it is not fired. Doesnot work for dropdown.
        void CurrentWorksheet_BeforeCellEdit(object sender, unvell.ReoGrid.Events.CellBeforeEditEventArgs e)
        {
            var col = sGetColumn(e.Cell.Column);

            if (col != null && (col.LedgerType == EnumLedgetType.NominalCodeID))
            {
                e.IsCancelled = true;

              
              
                var cnt = new UcNominalCodePopup(YrEndFolID);
                if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
                {
                    if (!string.IsNullOrWhiteSpace(cnt.NominalCode))
                    {
                        e.Cell.DataFormat = CellDataFormatFlag.Text;
                        this.CurrentWorksheet[e.Cell.Row, e.Cell.Column] = cnt.NominalCode;
                    }
                }
            }
        }

        //After cell value is changed and foucus changed from that cell. Doesnot work for dropdown.
        void CurrentWorksheet_AfterCellEdit(object sender, unvell.ReoGrid.Events.CellAfterEditEventArgs e) { }
        //add by @zgc 修改Credit和Debit同时只能有一个可以编辑
        void ChangeCreditAndDebitStatus(ReoGridPos pos)
        {
            //modify by @zgc

            var col = sGetColumn(pos.Col);


            if (col.LedgerType == EnumLedgetType.Credit || col.LedgerType == EnumLedgetType.Debit)
            {
                var debit = sGetColumn(EnumLedgetType.Debit).Index;
                var dvaule = sGetColumnValue(pos.Row, debit);
                var credit = sGetColumn(EnumLedgetType.Credit).Index;
                var cvaule = sGetColumnValue(pos.Row, credit);

                if (dvaule == null && cvaule == null)
                {
                    this.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = false;
                    pos.Col = credit;
                    sSetValidCell(pos);
                    this.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = false;
                    pos.Col = debit;
                    sSetValidCell(pos);

                }
                else
                {
                    if (dvaule != null)
                    {
                        this.CurrentWorksheet.Cells[pos.Row, credit].IsReadOnly = true;
                        pos.Col = credit;
                        sSetBackground(pos, Color.LightGray);
                    }
                    else if (cvaule != null)
                    {
                        this.CurrentWorksheet.Cells[pos.Row, debit].IsReadOnly = true;
                        pos.Col = debit;
                        sSetBackground(pos, Color.LightGray);
                    }
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
                if (LastEditValid)
                    sShowCalculatedValues(e.Cell.Position);
            }
            finally
            {
                sRegesterEvents();
                IsEdited = true;
            }
            //Add by @zgc
            ChangeCreditAndDebitStatus(e.Cell.Position);
        }

        void CurrentWorksheet_FocusPosChanged(object sender, unvell.ReoGrid.Events.CellPosEventArgs e)
        {
            
        }

        #endregion


    }
}
