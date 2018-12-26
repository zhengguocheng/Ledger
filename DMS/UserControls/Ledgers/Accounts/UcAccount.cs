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
using DMS.UserControls;

namespace DMS
{
    public partial class UcAccount : UserControlBase
    {
        tblAccountController cntrl = new tblAccountController();
        public tblAccount SelectedItem { get; set; }
        int clientID;

        public UcAccount(int clntID)
        {
            InitializeComponent();
            SelectedItem = new tblAccount();
            this.Caption = "Account";
            clientID = clntID;
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            dpDate.NullDate = AppConstants.NullDate;
            dpDate.Value = DateTime.Now;

            SelectedItem = cntrl.FindByClientID(clientID) ?? new tblAccount();
            AddKeyDownControls(txtRefrence, dpDate, txtAmount, drpVAT, drpAnalysisCode, txtNet, txtNotes, txtDetail);

            LoadItem();
        }

        void LoadItem()
        {
            drpVAT.DataSource = new tblVATRateController().FetchAll();
            drpVAT.DisplayMember = "Code";
            drpVAT.ValueMember = "ID";

            drpAnalysisCode.DataSource = new tblAnalysisCodeController().FetchAll();
            drpAnalysisCode.DisplayMember = "Code";
            drpAnalysisCode.ValueMember = "ID";

            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtRefrence.Text = SelectedItem.Reference;
                dpDate.SetDate(SelectedItem.Date);
                txtAmount.Text = SelectedItem.Amount.ToString();
                drpVAT.SelectedValue = SelectedItem.VAT_ID.Value;
                drpAnalysisCode.SelectedValue = SelectedItem.AnalysisCodeID;
                txtNet.Text = SelectedItem.Net.ToString();
                txtNotes.Text = SelectedItem.Notes;
                txtDetail.Text = SelectedItem.Detail;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtRefrence.Text.Trim()))
            {
                ShowValidationError(txtRefrence, CustomMessages.GetValidationMessage("Refrence"));
                return false;
            }

            if (dpDate.IsNull())
            {
                ShowValidationError(dpDate, CustomMessages.GetValidationMessage("Date"));
                return false;
            }

            
            if (!CustomValidator.IsDecimal(txtAmount.Text.Trim()))
            {
                ShowValidationError(txtAmount, CustomMessages.GetValidationMessage("Amount"));
                return false;
            }

            if (drpVAT.SelectedIndex < 0)
            {
                ShowValidationError(drpVAT, CustomMessages.GetValidationMessage("VAT"));
                return false;
            }

            if (drpAnalysisCode.SelectedIndex < 0)
            {
                ShowValidationError(drpAnalysisCode, CustomMessages.GetValidationMessage("Analysis Code"));
                return false;
            }

            if (!CustomValidator.IsDecimal(txtNet.Text.Trim()))
            {
                ShowValidationError(txtNet, CustomMessages.GetValidationMessage("Net"));
                return false;
            }

            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            if (!InputValidate())
                return;

            SelectedItem.Reference = txtRefrence.Text;
            SelectedItem.Date = dpDate.GetNullValue();
            SelectedItem.Amount = Convert.ToDecimal(txtAmount.Text);
            SelectedItem.VAT_ID = (int)drpVAT.SelectedValue;
            SelectedItem.AnalysisCodeID = (int)drpAnalysisCode.SelectedValue;
            SelectedItem.Net = Convert.ToDecimal(txtNet.Text);
            SelectedItem.Notes = txtNotes.Text.Trim();
            SelectedItem.Detail = txtDetail.Text.Trim();
            SelectedItem.ClientID = clientID;

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
            DisplayManager.LoadControl(new UcClientList());
        }

    }

}
