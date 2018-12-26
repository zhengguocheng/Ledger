using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SpecialFiles
    {
        public static void AddReports(long parentFolID)
        {
            AddSpecialFile(parentFolID, "Ledger Report.rpt", Tags.TagType.LedgerReport);
            AddSpecialFile(parentFolID, "Vat Report.rpt", Tags.TagType.VatReport);
            AddSpecialFile(parentFolID, "Analysis Report.rpt", Tags.TagType.AnalysisReport);
        }

        public static void AddTrialBalanceFiles(long parentFolID)
        {
            AddSpecialFile(parentFolID, "Opening Trial Balance.spf", Tags.TagType.OpeningTrialBal,Tags.TagType.TrialBal_FileType);
            AddSpecialFile(parentFolID, "Last Year Closing Trial Balance.spf", Tags.TagType.LastYearClosingTrialBal, Tags.TagType.TrialBal_FileType);
            AddSpecialFile(parentFolID, "Closing Trial Balance.spf", Tags.TagType.ThisYearClosingTrialBal, Tags.TagType.TrialBal_FileType);
            AddSpecialFile(parentFolID, "Next Year Opening Trial Balance.spf", Tags.TagType.NextYearOpeningTrialBal, Tags.TagType.TrialBal_FileType);
        }

        public static void AddJournalsFiles(long parentFolID)
        {
            AddSpecialFile(parentFolID, "Double Entries Journals.jrl", Tags.TagType.DoubleEntriesJournal, Tags.TagType.Journal_FileType);
            AddSpecialFile(parentFolID, "Multiple Entries Journal.jrl", Tags.TagType.MultipleEntriesJournal, Tags.TagType.Journal_FileType);
            AddSpecialFile(parentFolID, "Accrual Journal.jrl", Tags.TagType.AccrualJournal, Tags.TagType.Journal_FileType);
            AddSpecialFile(parentFolID, "Pre Payment Journal.jrl", Tags.TagType.PrepaymentJournal, Tags.TagType.Journal_FileType);
        }

        public static void AddReconcileFiles(long parentFolID)
        {
            AddSpecialFile(parentFolID, "Bank Reconciliation.brc", Tags.TagType.BankReconcile_FileType);
        }

        public static void AddSpecialFile(long CurrentFolderID, string name, Tags.TagType tagType, Tags.TagType extraTag = Tags.TagType.None)
        {
            tblDocumentItem doc = new tblDocumentItem();
            var repCntr = new Repository(AppConstants.RecordType.Ledger);
            doc.RecordID = repCntr.Find(CurrentFolderID).RecordID;
            doc.ParentID = CurrentFolderID;

            if (!repCntr.DocumentExist(name, doc.ParentID, doc.RecordID))
            {
                doc.Name = name;
                doc.IsFolder = false;
                doc.TempByteData = new byte[0];
                doc.AddTag(tagType);
                if(extraTag != Tags.TagType.None)
                {
                    doc.AddTag(extraTag);
                }
                repCntr.AddVirtualFile(doc);
            }
        }

    }
}
