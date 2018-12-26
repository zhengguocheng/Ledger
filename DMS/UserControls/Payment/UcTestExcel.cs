using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;
using unvell.ReoGrid.CellTypes;

namespace DMS.UserControls
{
    public partial class UcTestExcel : UserControlBase
    {
        DropdownListCell dropdown;
        ReoGridPos lastPos;

        public UcTestExcel()
        {
            InitializeComponent();
            this.Caption = "Payment";
            PerformGridSetting();
        }

        void PerformGridSetting()
        {
            excelGrid.CurrentWorksheet.RowCount = 1000;

            excelGrid.CurrentWorksheet.ColumnCount = 10;
            excelGrid.CurrentWorksheet.Name = "Payment";
            excelGrid.CurrentWorksheet.ColumnHeaders[0].Text = "Date";
            excelGrid.CurrentWorksheet.ColumnHeaders[1].Text = "Description";
            excelGrid.CurrentWorksheet.ColumnHeaders[2].Text = "Ref";
            excelGrid.CurrentWorksheet.ColumnHeaders[3].Text = "Gross";
            excelGrid.CurrentWorksheet.ColumnHeaders[4].Text = "VAT Code";
            excelGrid.CurrentWorksheet.ColumnHeaders[5].Text = "VAT";
            excelGrid.CurrentWorksheet.ColumnHeaders[6].Text = "Net";
            excelGrid.CurrentWorksheet.ColumnHeaders[7].Text = "Nominal Code";
            excelGrid.CurrentWorksheet.ColumnHeaders[8].Text = "Comment";
            excelGrid.CurrentWorksheet.ColumnHeaders[9].Text = "Tick";

            excelGrid.CurrentWorksheet.ColumnHeaders[9].DefaultCellBody = typeof(unvell.ReoGrid.CellTypes.CheckBoxCell);

            // set horizontal alignment for all cells on this column to center
            excelGrid.CurrentWorksheet.ColumnHeaders[9].Style.HorizontalAlign = ReoGridHorAlign.Center;

            // give check box a small padding (2 pixels)
            //excelGrid.CurrentWorksheet.ColumnHeaders[9].Style.Padding = new System.Windows.Forms.Padding(2);
            
            excelGrid.CurrentWorksheet["J1:J5"] = new object[] { false, true, false, false, true };

            excelGrid.CurrentWorksheet.SetRangeDataFormat(new ReoGridRange(0, 0, 200, 0), CellDataFormatFlag.DateTime,
                                                          new DateTimeDataFormatter.DateTimeFormatArgs
                                                          {
                                                              // culture
                                                              CultureName = "en-US",
                                                              // pattern
                                                              Format = "dd/MM/yyyy",
                                                          });

            excelGrid.CurrentWorksheet.SetRangeDataFormat(new ReoGridRange(0, 1, 200, 1), CellDataFormatFlag.Number,
                                                          new NumberDataFormatter.NumberFormatArgs()
                                                          {
                                                              // decimal digit places 0.1234
                                                              DecimalPlaces = 2,
                                                              // use separator: 123,456
                                                              UseSeparator = true,
                                                          });
            excelGrid.CurrentWorksheet[0, 3] = "Apple";

            excelGrid.CurrentWorksheet.FocusPosChanged += CurrentWorksheet_FocusPosChanged;
            excelGrid.CurrentWorksheet.BeforeCellEdit += CurrentWorksheet_BeforeCellEdit;
            excelGrid.CurrentWorksheet.AfterCellEdit += CurrentWorksheet_AfterCellEdit;
            excelGrid.CurrentWorksheet.CellDataChanged += CurrentWorksheet_CellDataChanged;

            

        }

        void CurrentWorksheet_BeforeCellEdit(object sender, unvell.ReoGrid.Events.CellBeforeEditEventArgs e)
        {
            //ValidateInput(e);
            if (e.Cell.Column == 3)
            {
                var sheet = excelGrid.CurrentWorksheet;
                dropdown = new DropdownListCell(
                     "Apple", "Orange", "Banana", "Pear",
                     "Pumpkin", "Cherry", "Coconut"
                   );
                dropdown.SelectedItemChanged += dropdown_SelectedItemChanged;
                sheet[e.Cell.Row, e.Cell.Column] = dropdown;
            }
        }

        void dropdown_SelectedItemChanged(object sender, EventArgs e)
        {
            int n = dropdown.SelectedIndex;
        }

        void CurrentWorksheet_CellDataChanged(object sender, unvell.ReoGrid.Events.CellEventArgs e)
        {

            var d = e.Cell.Data;
            //if (e.Cell.Column == 3 && e.Cell.Data != null)
            //{
            //    excelGrid.CurrentWorksheet.CellDataChanged -= CurrentWorksheet_CellDataChanged;
            //    e.Cell.DataFormat = CellDataFormatFlag.Text;

            //    excelGrid.CurrentWorksheet.SetCellData(e.Cell.Row, e.Cell.Column,"a");
            //    e.Cell.DataFormat = CellDataFormatFlag.Text;


            //    excelGrid.CurrentWorksheet.CellDataChanged += CurrentWorksheet_CellDataChanged;
            //}
        }

        void CurrentWorksheet_AfterCellEdit(object sender, unvell.ReoGrid.Events.CellAfterEditEventArgs e)
        {
            //this method not called after dropdown selection changed
            
            bool valid = true;
            lastPos = e.Cell.Position;
            if (e.Cell.Column == 0)
            {

                if (e.NewData == null)
                {
                    valid = false;
                }

                try
                {
                    Convert.ToDateTime(e.NewData);
                }
                catch
                {
                    valid = false;
                }
                
                if (!valid)
                {
                    e.NewData = "***";
                    excelGrid.CurrentWorksheet.FocusPos = new ReoGridPos(e.Cell.Row, e.Cell.Column);
                    excelGrid.CurrentWorksheet.StartEdit(e.Cell.Row, e.Cell.Column);
                    DisplayManager.DisplayMessage("Please enter valid date", MessageType.Error);
                }
            }
        }

        bool ValidateInput(unvell.ReoGrid.Events.CellBeforeEditEventArgs e)
        {
            bool valid = true;
            
            if(e.Cell.Column == 0)
            {
                try
                {
                    Convert.ToDateTime(e.EditText);
                }
                catch
                {
                    valid = false;
                }
                if(string.IsNullOrEmpty(e.EditText))
                {
                    valid = false;

                }

                if(!valid)
                {
                    DisplayManager.DisplayMessage("Please enter valid date", MessageType.Error);
                    e.IsCancelled = true;
                }
            }

            return valid;
        }

        void CurrentWorksheet_FocusPosChanged(object sender, unvell.ReoGrid.Events.CellPosEventArgs e)
        {
         
            if (lastPos != null)
            {
                if (excelGrid.CurrentWorksheet[lastPos] == "***")
                {
                    excelGrid.CurrentWorksheet.FocusPosChanged -= CurrentWorksheet_FocusPosChanged;

                    excelGrid.CurrentWorksheet.FocusPos = lastPos;
                    excelGrid.CurrentWorksheet.StartEdit(lastPos);
                    excelGrid.CurrentWorksheet.FocusPosChanged += CurrentWorksheet_FocusPosChanged;
                }
            }
        }

        private void UcPayment_Load(object sender, EventArgs e)
        {
            excelGrid.CurrentWorksheet.SetRangeStyle(ReoGridRange.EntireRange, new WorksheetRangeStyle()
            {
                Flag = PlainStyleFlag.FontSize | PlainStyleFlag.FontName,
                FontName = "Arial",
                FontSize = 9,
            });
        }

        private void pnlHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        
    }
}
