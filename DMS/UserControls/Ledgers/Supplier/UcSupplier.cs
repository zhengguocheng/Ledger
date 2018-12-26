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

namespace DMS
{
    public partial class UcSupplier : UserControlBase
    {
        tblSupplierController cntrl = new tblSupplierController();
        public tblSupplier SelectedItem { get; set; }

        public long? yrEndFolID;

        public UcSupplier()
        {
            InitializeComponent();
            SelectedItem = new tblSupplier();
            this.Caption = "Supplier";
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            DropDownHelper.BindNominalCode(drpNominalCode, 0, true);
            LoadItem();
        }

        void LoadItem()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtName.Text = SelectedItem.Name.ToString();
                DropDownHelper.SelectByValue(drpNominalCode,SelectedItem.NominalCodeID.ToString());
                txtDescription.Text = SelectedItem.Description;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                ShowValidationError(txtName, CustomMessages.GetValidationMessage("Name"));
                return false;
            }
            //if (DropDownHelper.IsEmpty(drpNominalCode))
            //{
            //    ShowValidationError(drpNominalCode, CustomMessages.GetValidationMessage("Nominal Code"));
            //    return false;
            //}
            
           
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.Name = txtName.Text;
            SelectedItem.Description = txtDescription.Text;
            SelectedItem.NominalCodeID = Convert.ToInt32(DropDownHelper.GetSelectedValue(drpNominalCode));

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
            DisplayManager.LoadControl(new UcSupplierList());
        }
    }

}
