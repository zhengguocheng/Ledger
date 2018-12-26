using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DMS;

namespace Ledgers
{
    public partial class UcVATRate : UserControlBase
    {
        tblVAT_RateController cntrl = new tblVAT_RateController();
        public tblVAT_Rate SelectedItem { get; set; }

        public UcVATRate()
        {
            InitializeComponent();
            SelectedItem = new tblVAT_Rate();
            this.Caption = "VAT Rate";
        }

        private void UcUser_Load(object sender, EventArgs e)
        {
            LoadUser();
        }

        void LoadUser()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtType.Text = SelectedItem.Type;
                txtPercent.Text = SelectedItem.Percentage.ToString();
                txtCode.Text = SelectedItem.Code;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtType.Text.Trim()))
            {
                ShowValidationError(txtType, CustomMessages.GetValidationMessage("Type"));
                return false;
            }

            if (string.IsNullOrEmpty(txtPercent.Text.Trim()))
            {
                ShowValidationError(txtPercent, CustomMessages.GetValidationMessage("Percentage"));
                return false;
            }

            float f;
            if (!float.TryParse(txtPercent.Text, out f))
            {
                ShowValidationError(txtPercent, CustomMessages.GetValidationMessage("Percentage"));
                return false;
            }

            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                ShowValidationError(txtCode, CustomMessages.GetValidationMessage("Code"));
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.Type = txtType.Text;
            SelectedItem.Percentage = Convert.ToSingle(txtPercent.Text);
            SelectedItem.Code = txtCode.Text;
            
            try
            {
                if (cntrl.Save(SelectedItem))
                {
                    ShowSaveConfirmation();
                    GoBack();
                }
            }
            catch (Exception ecp)
            {
                HandleException(ecp);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        void GoBack()
        {
            DisplayManager.LoadControl(new UcVATRateList());
        }

        private void UcVATRate_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.GetNextControl(ActiveControl, true) != null)
                {
                    e.Handled = true;
                    this.GetNextControl(ActiveControl, true).Focus();
                }
            }
        }
    }

}
