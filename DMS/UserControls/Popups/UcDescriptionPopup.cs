using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DMS.UserControls.Popups
{
    public partial class UcDescriptionPopup : UserControlBase
    {
        public string SelectedText;

        public UcDescriptionPopup()
        {
            InitializeComponent();
        }

        private void UcNominalCodePopup_Load(object sender, EventArgs e)
        {
            DropDownHelper.BindDescription(drpDescription);
            drpDescription.DropDownListElement.AutoCompleteAppend.LimitToList = false;

        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            SelectedText = drpDescription.Text;
            if (!string.IsNullOrWhiteSpace(SelectedText) && SelectedText.Contains(UcExcelSheet.SeperatorChar))
            {
                SelectedText = SelectedText.Split(UcExcelSheet.SeperatorChar.ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0].TrimEnd();
            }
            DisplayManager.CloseDialouge(DialogResult.OK);
        }
        
        private void drpVATCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                btnOk_Click(null, null);
        }
    }
}
