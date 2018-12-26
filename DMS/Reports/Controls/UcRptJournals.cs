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
    public partial class UcRptJournals : UserControlBase
    {
        long docItemID;
        Tags.TagType tag;
        ReportType RptTyp;
        public UcRptJournals(long _docItemID, Tags.TagType _tag)
        {
            InitializeComponent();
            Caption = "Journals Report";
            docItemID = _docItemID;
            tag = _tag;

            if (tag == Tags.TagType.DoubleEntriesJournal || tag == Tags.TagType.AccrualJournal || tag == Tags.TagType.PrepaymentJournal)
            {
                RptTyp = ReportType.Journal_Double_Accrual_Prepayment;
            }
            else if (tag == Tags.TagType.MultipleEntriesJournal)
            {
                RptTyp = ReportType.Journal_MultipleEnt;
            }
        }

        private void UcRptJournals_Load(object sender, EventArgs e)
        {
            UcRDLC_Viewer uc = new UcRDLC_Viewer();

            uc.JournalsReport(docItemID, RptTyp);
            uc.Dock = DockStyle.Fill;
            pnlRpt.Controls.Add(uc);
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            UserControlBase uc = null;

            if (RptTyp == ReportType.Journal_Double_Accrual_Prepayment)
                uc = new UcJournal(docItemID, tag);
            else if (RptTyp == ReportType.Journal_MultipleEnt)
                uc = new UcMultipleEntJournal(docItemID, tag);

            DisplayManager.LoadControl(uc);
        }


    }
}
