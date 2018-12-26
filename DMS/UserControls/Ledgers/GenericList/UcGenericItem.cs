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
    public partial class UcGenericItem : UserControlBase
    {
        tblGenericListController cntrl = new tblGenericListController();
        public tblGenericList SelectedItem { get; set; }
        GenericListType.TypeEnum type;

        public UcGenericItem(GenericListType.TypeEnum ptype)
        {
            InitializeComponent();
            SelectedItem = new tblGenericList();
            type = ptype;
            this.Caption = GenericListType.GetName(type);
        }

        
        private void UcGenericList_Load(object sender, EventArgs e)
        {
            LoadItem();
            AddKeyDownControls(txtName, txtDescription);
        }

        void LoadItem()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtDescription.Text = SelectedItem.Description;
                txtName.Text = SelectedItem.Name;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                ShowValidationError(txtName, CustomMessages.GetValidationMessage("Name"));
                return false;
            }
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.Description = txtDescription.Text.Trim();
            SelectedItem.Name = txtName.Text.Trim();
            SelectedItem.TypeID = (int)type;
            
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
            DisplayManager.LoadControl(new UcGenericList(type));
        }
    }

}
