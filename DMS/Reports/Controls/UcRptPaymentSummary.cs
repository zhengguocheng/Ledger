using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Reporting.WinForms;
using System.Data.SqlClient;
using DAL;
using DMS.UserControls;

namespace DMS.Reports
{
    public partial class UcRptPaymentSummary : UserControlBase
    {
        long docItemID;
        string sheetType;
        public UcRptPaymentSummary(long _docItemID, string _sheetType)
        {
            InitializeComponent();
            Caption = _sheetType + " Summary";
            docItemID = _docItemID;
            sheetType = _sheetType;
        }

        private void UcRptPaymentSummary_Load(object sender, EventArgs e)
        {
            UcRDLC_Viewer uc = new UcRDLC_Viewer();
            uc.PaymentSummary(docItemID);
            uc.Dock = DockStyle.Fill;
            pnlRpt.Controls.Add(uc);
        }

        public void PaymentSummary()
        {
            //var dt = ReportsHelper.PaymentSummary(docItemID);
            //LoadReport(dt, "DataSet1", @".\Reports\RDLC\RptPaymentSummary.rdlc", "Payment Summary");
        }
       
        private void btnBack_Click(object sender, EventArgs e)
        {
            var uc = new UcExcelSheet(docItemID,sheetType);
            DisplayManager.LoadControl(uc);
        }

       
    }
}
