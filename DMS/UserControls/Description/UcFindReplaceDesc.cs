using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL.Controllers;

namespace DMS.UserControls
{
    public partial class UcFindReplaceDesc : UserControlBase
    {
        public UcFindReplaceDesc()
        {
            InitializeComponent();
            this.Caption = "Description";

        }
        
        private void btnClear_Click(object sender, EventArgs e)
        {
            txtFind.Text = txtReplaceWith.Text = string.Empty;
        }


        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtFind.Text.Trim()))
            {
                ShowValidationError(txtFind, CustomMessages.GetValidationMessage("Find"));
                return false;
            }
            if (string.IsNullOrEmpty(txtReplaceWith.Text.Trim()))
            {
                ShowValidationError(txtReplaceWith, CustomMessages.GetValidationMessage("Replace With"));
                return false;
            }


            return true;
        }

        private void btnReplace_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;

            if (MessageBox.Show("This operation will replace all the descriptions all Payment & Receipt sheets in database, Do you want to proceed?", "Find & Replace", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                tblExcelSheetController.FindReplaceDescription(txtFind.Text.Trim(), txtReplaceWith.Text.Trim());
                DisplayManager.DisplayMessage("Description has been replaced successfully.", MessageType.Success);
            }
        }
    }
}
