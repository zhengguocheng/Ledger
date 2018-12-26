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
    public partial class UcDescription : UserControlBase
    {
        tblDescriptionController cntrl = new tblDescriptionController();
        public tblDescription SelectedItem { get; set; }

        public UcDescription()
        {
            InitializeComponent();
            SelectedItem = new tblDescription();
            this.Caption = "Description";
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            LoadItem();
            AddKeyDownControls(txtDescription);

        }

        void LoadItem()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtDescription.Text = SelectedItem.Description;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtDescription.Text.Trim()))
            {
                ShowValidationError(txtDescription, CustomMessages.GetValidationMessage("Description"));
                return false;
            }
            
           
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;

            SelectedItem.Description = txtDescription.Text;
            
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
            DisplayManager.LoadControl(new UcDescriptionList());
        }
    }

}
