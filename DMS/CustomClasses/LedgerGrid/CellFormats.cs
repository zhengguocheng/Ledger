using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using unvell.ReoGrid;
using unvell.ReoGrid.DataFormat;

namespace DMS.CustomClasses
{
    
    class MyTextFormatter : IDataFormatter
    {
        public string FormatCell(ReoGridCell cell)
        {
            if (cell.Data != null)
            {
                var val = cell.Data.ToString();
                return val;
            }
            return null;
        }

        public bool PerformTestFormat()
        {
            return true;
        }
    }
}
