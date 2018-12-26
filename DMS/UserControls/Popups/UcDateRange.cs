using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DAL;
using DAL.CustomClasses;

namespace DMS.UserControls
{
    public partial class UcDateRange : UserControlBase
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public UcDateRange()
        {
            InitializeComponent();
            this.Caption = "Date Range";
            
            dpStart.ApplyDMSSettings();
            dpStart.SetDate(new DateTime(DateTime.Now.Year,DateTime.Now.Month,1));

            dpEnd.ApplyDMSSettings();
            dpEnd.SetDate(new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month)));
        }

        public void SetEndDate(DateTime date)
        {
            dpStart.SetDate(new DateTime(date.Year, date.Month, 1));
            dpEnd.SetDate(date);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(dpStart.IsNull())
            {
                DisplayManager.DisplayMessage("Please select start date.", MessageType.Error);
                return;
            }
            if (dpEnd.IsNull())
            {
                DisplayManager.DisplayMessage("Please select end date.", MessageType.Error);
                return;
            }

            if(dpStart.Value > dpEnd.Value)
            {
                DisplayManager.DisplayMessage("Start date must be less then end date.", MessageType.Error);
                return;
            }

            StartDate = dpStart.Value;
            EndDate = dpEnd.Value;

            DisplayManager.CloseDialouge(DialogResult.OK);
            

        }

        private void UcDateRange_Load(object sender, EventArgs e)
        {

        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DisplayManager.CloseDialouge(DialogResult.Cancel);
        }
    }
}
