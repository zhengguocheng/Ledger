using DAL;
using DAL.Controllers;
using DAL.CustomClasses;
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
    public static class ReoGridExtention
    {
        public static void ScrollToRow(this ReoGridControl reoGrid, int rowNo)
        {
            if (rowNo >= 0 && rowNo < reoGrid.CurrentWorksheet.Rows)
            {
                reoGrid.CurrentWorksheet.ScrollToCell(rowNo, 0);
                reoGrid.CurrentWorksheet.SelectRows(rowNo, 1);
            }
        }

    }
}
