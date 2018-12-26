using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMS
{
    public class DropDownItem
    {
        public string DisplayText { get; set; }
        public string Value { get; set; }

        public DropDownItem()
        {
        }
        public DropDownItem(string displayText, string val)
        {
            DisplayText = displayText;
            Value = val;
        }
    }
}
