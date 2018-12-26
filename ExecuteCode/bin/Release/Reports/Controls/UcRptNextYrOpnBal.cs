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
    public partial class UcRptNextYrOpnBal : UserControlBase
    {
        long docItemID;
        string sheetType;
        public UcRptNextYrOpnBal(long _docItemID, string _sheetType)
        {
            InitializeComponent();
            Caption = " Next Year Opening Balance";
            docItemID = _docItemID;
            sheetType = _sheetType;
        }

        private void UcRptNextYrOpnBal_Load(object sender, EventArgs e)
        {
            NextYrOpnBal();
        }

        void NextYrOpnBal()
        {
            UcRDLC_Viewer uc = new UcRDLC_Viewer();
            uc.NextYrOpnBal(docItemID);
            uc.Dock = DockStyle.Fill;
            pnlRpt.Controls.Clear();
            pnlRpt.Controls.Add(uc);
        }

        void PLReserve()
        {
            UcRDLC_Viewer uc = new UcRDLC_Viewer();
            uc.PLReserve(docItemID); 
            uc.Dock = DockStyle.Fill;
            pnlRpt.Controls.Clear();
            pnlRpt.Controls.Add(uc);
        }
     
        private void btnBack_Click(object sender, EventArgs e)
        {
            LoadPreviousPage();
        }

        private void chkNxtYrOpn_CheckedChanged(object sender, EventArgs e)
        {
            if(chkNxtYrOpn.Checked)
            {
                NextYrOpnBal();
            }
            else
            {
                PLReserve();
            }
        }

       
    }
}
