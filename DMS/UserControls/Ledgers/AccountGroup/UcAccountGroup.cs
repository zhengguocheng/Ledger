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
    public partial class UcAccountGroup : UserControlBase
    {
        tblAccountGroupController cntrl = new tblAccountGroupController();
        public tblAccountGroup   SelectedItem { get; set; }

        public long? yrEndFolID;

        public UcAccountGroup()
        {
            InitializeComponent();
            SelectedItem = new tblAccountGroup();
            this.Caption = "VAT Rate";
        }

        private void UcUser_Load(object sender, EventArgs e)
        {
            AddKeyDownControls(txtName, txtDesc);
            LoadUser();
        }

        void LoadUser()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtName.Text = SelectedItem.Name;
                txtDesc.Text = SelectedItem.Description;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                ShowValidationError(txtName, CustomMessages.GetValidationMessage("Name"));
                return false;
            }

            if (string.IsNullOrEmpty(txtDesc.Text.Trim()))
            {
                ShowValidationError(txtDesc, CustomMessages.GetValidationMessage("Description"));
                return false;
            }


            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.Name = txtName.Text;
            SelectedItem.Description = txtDesc.Text;
            
            if(yrEndFolID.HasValue)
                SelectedItem.YearEndFolderID = yrEndFolID;
            
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
            DisplayManager.LoadControl(new UcAccountGroupList(),true);
        }

       
    }

}
