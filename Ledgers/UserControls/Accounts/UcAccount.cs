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
    public partial class UcAccount : UserControlBase
    {
        tblAccountController cntrl = new tblAccountController();
        public tblAccount SelectedItem { get; set; }

        public UcAccount()
        {
            InitializeComponent();
            SelectedItem = new tblAccount();
            this.Caption = "Account";
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            dpDate.NullDate = AppConstants.NullDate;
            dpDate.Value = DateTime.Now;
            


            LoadItem();
        }

        void LoadItem()
        {
            drpVAT.DataSource = new tblVAT_RateController().FetchAll();
            drpVAT.DisplayMember = "Code";
            drpVAT.ValueMember = "ID";


            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtRefrence.Text = SelectedItem.Reference;
                dpDate.SetDate(SelectedItem.Date);
                txtAmount.Text = SelectedItem.Amount.ToString();
                drpVAT.SelectedValue = SelectedItem.VAT_ID.Value;
                txtAnalysisCode.SelectedValue = SelectedItem.AnalysisCodeID.Value;
                txtNet.Text = SelectedItem.Net;
                txtNotes.Text = SelectedItem.Notes;
                txtDetail.Text = SelectedItem.Detail;
            }
        }

        bool InputValidate()
        {
            //if (string.IsNullOrEmpty(txtRefrence.Text.Trim()))
            //{
            //    ShowValidationError(txtRefrence, CustomMessages.GetValidationMessage("Code"));
            //    return false;
            //}
            
            //if (string.IsNullOrEmpty(txtDate.Text.Trim()))
            //{
            //    ShowValidationError(txtDate, CustomMessages.GetValidationMessage("Heading"));
            //    return false;
            //}
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
            SelectedItem.AnalysisCodeID = (int)txtAnalysisCode.SelectedValue;
            SelectedItem.Net = txtNet.Text;
            SelectedItem.Notes = txtNotes.Text;
            SelectedItem.Detail = txtDetail.Text;

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
            DisplayManager.LoadControl(new UcAccountChartList());
        }
    }

}
