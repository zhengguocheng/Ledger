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
    public partial class UcAccountChart : UserControlBase
    {
        tblChartAccountController cntrl = new tblChartAccountController();
        public tblChartAccount SelectedItem { get; set; }

        public UcAccountChart()
        {
            InitializeComponent();
            SelectedItem = new tblChartAccount();
            this.Caption = "Chart Account";
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            LoadItem();
        }

        void LoadItem()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtHeading.Text = SelectedItem.Heading.ToString();
                txtCode.Text = SelectedItem.Code;
            }
        }

        bool InputValidate()
        {

            if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            {
                ShowValidationError(txtCode, CustomMessages.GetValidationMessage("Code"));
                return false;
            }
            
            if (string.IsNullOrEmpty(txtHeading.Text.Trim()))
            {
                ShowValidationError(txtHeading, CustomMessages.GetValidationMessage("Heading"));
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.Heading = txtHeading.Text;
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
            DisplayManager.LoadControl(new UcAccountChartList());
        }
    }

}
