using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;

namespace DMS
{
    public partial class UcUser : UserControlBase
    {
        UserController cntrl = new UserController();
        public User SelectedItem { get; set; }

        public UcUser()
        {
            InitializeComponent();
            SelectedItem = new User();
            this.Caption = "User";
        }

        private void UcUser_Load(object sender, EventArgs e)
        {
            cntrl = new UserController();
            drpRole.DataSource = AppConstants.AllRoles;
            LoadUser();
        }

        void LoadUser()
        {
            if (SelectedItem != null && SelectedItem.ID > 0)
            {
                txtFirstName.Text = SelectedItem.FirstName;
                txtLastName.Text = SelectedItem.LastName;
                txtDesignation.Text = SelectedItem.Designation;
                txtSalary.Text = SelectedItem.Salary.ToString();
                txtUserName.Text = SelectedItem.UserName;
                txtPassword.Text = SelectedItem.Password;
                txtEmail.Text = SelectedItem.Email;
                txtSheetPassword.Text = SelectedItem.SheetPassword;
                drpRole.SelectedValue = SelectedItem.Role;
            }
        }

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtFirstName.Text.Trim()))
            {
                ShowValidationError(txtFirstName, CustomMessages.GetValidationMessage("First Name"));
                return false;
            }

            if (string.IsNullOrEmpty(txtLastName.Text.Trim()))
            {
                ShowValidationError(txtLastName, CustomMessages.GetValidationMessage("Last Name"));
                return false;
            }

            if (string.IsNullOrEmpty(txtDesignation.Text.Trim()))
            {
                ShowValidationError(txtDesignation, CustomMessages.GetValidationMessage("Designation"));
                return false;
            }

            float f;
            if (!float.TryParse(txtSalary.Text, out f))
            {
                ShowValidationError(txtSalary, CustomMessages.GetValidationMessage("Charge out rate"));
                return false;
            }

            if (string.IsNullOrEmpty(txtSheetPassword.Text.Trim()))
            {
                ShowValidationError(txtSheetPassword, CustomMessages.GetValidationMessage("Sheet Password"));
                return false;
            }
            
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            
            SelectedItem.FirstName = txtFirstName.Text;
            SelectedItem.LastName = txtLastName.Text;
            SelectedItem.Designation = txtDesignation.Text; ;
            SelectedItem.Salary = float.Parse(txtSalary.Text);
            SelectedItem.UserName = txtUserName.Text;
            SelectedItem.Password = txtPassword.Text;
            SelectedItem.Role = drpRole.SelectedItem.ToString();
            SelectedItem.Email = txtEmail.Text.Trim();
            SelectedItem.SheetPassword = txtSheetPassword.Text.Trim();
            
            try
            {
                if (cntrl.Save(SelectedItem))
                {
                    ShowSaveConfirmation();
                    AuthenticationService.ReloadLoginedUser();//if logined user has changed its info then reload it.
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
            DisplayManager.LoadControl(new UcUserList());
        }


    }

}
