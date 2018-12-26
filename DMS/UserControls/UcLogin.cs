using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DMS;
using DAL;
using DMS.UserControls;

namespace FrontEnd
{
    public partial class UcLogin : UserControlBase
    {
        public UcLogin()
        {
            InitializeComponent();
            this.Caption = "Login";
        }

        private void UcLogin_Load(object sender, EventArgs e)
        {
            if (!AppConstants.IsDeployed)
            {
                txtUserName.Text = txtPassword.Text = "ivan";
            }
        }

        #region custom methods

        bool InputValidate()
        {
            if (string.IsNullOrEmpty(txtUserName.Text))
            {
                ShowValidationError(txtUserName, CustomMessages.GetValidationMessage("User Name"));
                return false;
            }
            if (string.IsNullOrEmpty(txtPassword.Text))
            {
                ShowValidationError(txtPassword, CustomMessages.GetValidationMessage("Password"));
                return false;
            }
            return true;
        }
        
        #endregion

        #region Control Events

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!InputValidate())
                return;
            ClearErrorProvider();
            AuthenticationService.Authenticate(txtUserName.Text, txtPassword.Text);
            if (AuthenticationService.IsAuthenticated)
            {
                UcClientList cnt = new UcClientList();
                DisplayManager.LoadControl(cnt);
                DisplayManager.CustomeAction(ActionType.LoginComplete);
            }
            else
            {
                DisplayManager.DisplayMessage(CustomMessages.AuthenticationFailed, MessageType.Error);
                btnClear_Click(null, null);
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtPassword.Text = txtUserName.Text = string.Empty;
            
            /*
            * manual import emails
            EmailImport imp = new EmailImport();
            imp.StartDate = new DateTime(2011, 1, 1);
            imp.EndDate = DateTime.Now.AddDays(1);
            DateTime s = DateTime.Now;
            imp.ImportInboxAsync();
            double ms = DateTime.Now.Subtract(s).TotalMilliseconds;
            */ 
            
            // * Moving outlook emails to seperate folders
            /* 
            OutEmailIntegration.MoveOutlookEmails m = new OutEmailIntegration.MoveOutlookEmails();
            m.MoveMails();
            * */
        }

        #endregion

        //this control is to be modified
    }
}
