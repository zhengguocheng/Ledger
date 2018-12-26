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
using DMS.UserControls.Popups;
using System.IO;
using System.Diagnostics;

namespace DMS.Reports
{
    public partial class UcRptReconcile : UserControlBase
    {
        public long docItemID, NominalCodeID;
        public int stPeriod, endPeriod;
        public string clientName, NomCodeDesc;
        public DateTime reconcileDate;
        public decimal acctBal, outPayments, outRecp, balPerStm;
        UcRDLC_Viewer ucRptViewer;
        
        public UcRptReconcile(long _docItemID)
        {
            InitializeComponent();
            Caption = "Journals Report";
            docItemID = _docItemID;
        }

        private void UcRptJournals_Load(object sender, EventArgs e)
        {
            ucRptViewer = new UcRDLC_Viewer();
            ucRptViewer.ReconcileReport(docItemID,NominalCodeID,NomCodeDesc,stPeriod,endPeriod,clientName,reconcileDate,acctBal,outPayments,outRecp,balPerStm);
            ucRptViewer.Dock = DockStyle.Fill;
            pnlRpt.Controls.Add(ucRptViewer);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            //UserControlBase uc = null;
            //uc = new UcBankReconcile(docItemID);
            //DisplayManager.LoadControl(uc);

            DisplayManager.LoadControl(new UcNavigation(false), true);

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            UcInputText cnt = new UcInputText();

            if (DisplayManager.ShowDialouge(cnt) == DialogResult.OK)
            {
                var filePath = cnt.InputPath+".xls";
                bool saved = ucRptViewer.ExportExcel(filePath);
                if(saved)
                {
                    if(DialogResult.Yes == DisplayManager.DisplayMessage("File has been saved. Do you want to open it?",MessageType.Confirmation))
                    {
                        Process.Start(filePath);
                    }
                }
            }
        }


    }
}
