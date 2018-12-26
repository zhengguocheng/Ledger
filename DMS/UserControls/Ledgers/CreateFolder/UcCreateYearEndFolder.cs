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
    //This control is not used anymore
    public partial class UcCreateYearEndFolder : UserControlBase
    {
        int clientID = 0;

        public UcCreateYearEndFolder(int pClientID)
        {
            InitializeComponent();
            this.Caption = "Create YearEnd Folders";
            clientID = pClientID;
        }

        private void UcAccountChart_Load(object sender, EventArgs e)
        {
            dpStart.ApplyDMSSettings();
            dpEnd.ApplyDMSSettings();
            
            LoadItem();
            AddKeyDownControls(txtYearEnd, dpStart, dpEnd);
        }

        void LoadItem()
        {
            DeadlineTracking itm = DMSQueries.Find_DeadlineTracking(clientID);
            if (itm == null || !itm.AccountingYearEndDate.HasValue)
            {
                txtYearEnd.Text = string.Empty;
                dpStart.SetDate(DateTime.Now);
                dpEnd.SetDate(DateTime.Now);
            }
            else if (itm.AccountingYearEndDate.HasValue)
            {
                txtYearEnd.Text = itm.AccountingYearEndDate.Value.ToString(AppConstants.DateFormatShort);
                dpStart.SetDate(itm.AccountingYearEndDate.Value);
                DateTime endDate;
                try
                {
                    endDate = new DateTime(DateTime.Now.Year, itm.AccountingYearEndDate.Value.Month, itm.AccountingYearEndDate.Value.Day);
                }
                catch (ArgumentOutOfRangeException ecp)//if yearend = 29/02/2012 (leapyear) then endDate might throw exception in this case decrease the day 
                {
                    endDate = new DateTime(DateTime.Now.Year, itm.AccountingYearEndDate.Value.Month, (itm.AccountingYearEndDate.Value.Day-1));
                }
                endDate = DateHelper.LastDayOfMonthFromDateTime(endDate);
                dpEnd.SetDate(endDate);
            }
        }

        bool InputValidate()
        {
            if (dpStart.IsNull())
            {
                ShowValidationError(dpStart, CustomMessages.GetValidationMessage("From"));
                return false;
            }
        
            if (dpEnd.IsNull())
            {
                ShowValidationError(dpStart, CustomMessages.GetValidationMessage("End"));
                return false;
            }
           
            return true;
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            //This control is not used anymore
            //if (!InputValidate())
            //    return;

            //Repository rep = new Repository(AppConstants.RecordType.Ledger);

            //List<tblDocumentItem> lst = rep.FetchRootNode(clientID);

            //if (lst != null && lst.Count > 0)
            //{
            //    DefaultFolders f = new DefaultFolders();
            //    f.CreateEndYearFolders(clientID,dpStart.Value,dpEnd.Value);
            //}
            //this.ParentForm.DialogResult = DialogResult.OK;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            GoBack();
        }

        void GoBack()
        {
            //DisplayManager.LoadControl(new UcAnalysisCodeList());
            this.ParentForm.DialogResult = DialogResult.Cancel;
        }
    }

}
