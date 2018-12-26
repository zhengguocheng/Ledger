using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace DMS
{
    class EnterKeyGridBehaviour : BaseGridBehavior
    {
        public override bool ProcessKeyDown(KeyEventArgs keys)
        {
            if (keys.KeyData == Keys.Enter && this.GridControl.IsInEditMode)
            {
                this.GridControl.GridNavigator.SelectNextColumn();
            }
            return true;
        }
    }

   
}
