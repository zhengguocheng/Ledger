using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using System.Collections;

namespace DMS.UserControls
{
    public partial class UcClient : UserControlBase
    {
        ClientController cntrl = new ClientController();
        public Client SelectedClient { get; set; }

        public UcClient()
        {
            SelectedClient = new Client();
            InitializeComponent();
            this.Caption = "Client Information";
        }

        void LoadClient()
        {
            if (SelectedClient != null)
            {
                txtClientName.Text = SelectedClient.Client_Name;
                txtTradingName.Text = SelectedClient.Trading_Name;
                txtRefrence.Text = SelectedClient.Reference;
                txtPostalCode.Text = SelectedClient.Post_Code;
                txtNIN.Text = SelectedClient.National_Insurance;
                txtUTR.Text = SelectedClient.UTR;
                txtVAT.Text = SelectedClient.VAT_Registration_Number;
                txtCompanyNo.Text = SelectedClient.Company_Number;
                txtPayee.Text = SelectedClient.PAYEE;
                txtPayeeAccRef.Text = SelectedClient.PayeeAccRef;
                txtBussinessAdd.Text = SelectedClient.BusinessAddress;
                txtHomeAdd.Text = SelectedClient.HomeAddress;
                txtBussinessPhone.Text = SelectedClient.BusinessPhoneNo;
                txtHomePhone.Text = SelectedClient.HomePhoneNo;
                txtFaxNo.Text = SelectedClient.FaxNo;
                txtMobNo1.Text = SelectedClient.MobileNo1;
                txtMob2.Text = SelectedClient.MobileNo2;
                txtEmail1.Text = SelectedClient.EmailAddress1;
                //txtEmail2.Text = SelectedClient.EmailAddress2;
            }
        }

        bool InputValidate()
        {
            ClearErrorProvider();
            if (string.IsNullOrEmpty(txtClientName.Text))
            {
                ShowValidationError(txtClientName, CustomMessages.GetValidationMessage("Client Name"));
                return false;
            }
            if (string.IsNullOrEmpty(txtRefrence.Text))
            {
                ShowValidationError(txtRefrence, CustomMessages.GetValidationMessage("Refrence"));
                return false;
            }

            return true;
        }

        #region Control Events

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (!InputValidate())
                    return;

                if (SelectedClient != null)
                {
                    if (SelectedClient.ID == 0)//if new client
                    {
                        SelectedClient.IsActive = true;//set is active to true
                    }
                    SelectedClient.Client_Name = txtClientName.Text;
                    SelectedClient.Trading_Name = txtTradingName.Text;
                    SelectedClient.Reference = txtRefrence.Text;
                    SelectedClient.Post_Code = txtPostalCode.Text;
                    SelectedClient.National_Insurance = txtNIN.Text;
                    SelectedClient.UTR = txtUTR.Text;
                    SelectedClient.VAT_Registration_Number = txtVAT.Text;
                    SelectedClient.Company_Number = txtCompanyNo.Text;
                    SelectedClient.PAYEE = txtPayee.Text;
                    SelectedClient.PayeeAccRef = txtPayeeAccRef.Text;
                    SelectedClient.BusinessAddress = txtBussinessAdd.Text;
                    SelectedClient.HomeAddress = txtHomeAdd.Text;
                    SelectedClient.BusinessPhoneNo = txtBussinessPhone.Text;
                    SelectedClient.HomePhoneNo = txtHomePhone.Text;
                    SelectedClient.FaxNo = txtFaxNo.Text;
                    SelectedClient.MobileNo1 = txtMobNo1.Text;
                    SelectedClient.MobileNo2 = txtMob2.Text;
                    SelectedClient.EmailAddress1 = txtEmail1.Text;
                    //SelectedClient.EmailAddress2 = txtEmail2.Text;

                    if (cntrl.Save(SelectedClient))
                    {
                        DefaultFolders d = new DefaultFolders();
                        d.CreateDefaultFolders(SelectedClient.ID, SelectedClient.Client_Name, AppConstants.RecordType.Client);
                        ShowSaveConfirmation();
                        GoBack();
                    }
                }
            }
            catch (Exception ecp)
            {
                HandleException(ecp);
            }
        }

        private void FrmClient_Load(object sender, EventArgs e)
        {
            LoadClient();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        void GoBack()
        {
            if(SelectedClient != null)
                UcClientList.PreSelectedClientID = SelectedClient.ID;
            
            DisplayManager.LoadControl(new UcClientList());
        }

        #endregion

        private void groupBox1_Enter(object sender, EventArgs e)
        {}

        private void txtBussinessAdd_TextChanged(object sender, EventArgs e)
        {}
        
    }

}
