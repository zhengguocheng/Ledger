using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class Tags
    {
        public enum TagType
        {
            None = 0, Star = 1, PDFMatched = 2, Email = 3, EmailAttachment = 4, Document = 5,
            YearEndFolder = 6, PettyCash_FileType = 8, Banking_FileType = 9,
            Ledger_FileType = 10, BankPayment_FileType = 11, PCB_FileType = 12,
            BankReconcile_FileType = 13, Payment_FileType = 14,
            LedgerReport = 15, Receipts_FileType = 16, TrialBal_FileType = 17,

            OpeningTrialBal = 18, LastYearClosingTrialBal = 19, ThisYearClosingTrialBal = 20, NextYearOpeningTrialBal = 21,

            SDB_FileType =22,

            Journal_FileType = 23,
            DoubleEntriesJournal = 24, MultipleEntriesJournal = 25, AccrualJournal = 26, PrepaymentJournal = 27,

            VatReport = 28, AnalysisReport = 29,
            NotesFolder = 30
        }

        public static string TagValue(TagType tag)
        {
            string val = null;
            int n = (int)tag;
            val = string.Format("({0})", n);
            return val;
        }

        public static List<TagType> GetTagList(string tagString)
        {
            List<TagType> lst = new List<TagType>();
            foreach (TagType tag in Enum.GetValues(typeof(TagType)))
            {
                if(IsTagged(tagString,tag))
                {
                    lst.Add(tag);
                }
            }
            return lst;
        }

        public static bool IsTagged(string tagString, TagType type)
        {
            return (!string.IsNullOrEmpty(tagString)) && tagString.Contains(TagValue(type));
        }

        public static string AddTag(string tagString, TagType type)
        {
            //if there is no tag added then there is possibility that tag value will be null
            if (tagString == null)
            {
                tagString = string.Empty;
            }

            if (tagString != null && !IsTagged(tagString, type))//if specified tag is not already there
            {
                tagString = tagString + TagValue(type);
            }
            return tagString;
        }

        public static string RemoveTag(string tagString, TagType type)
        {
            if (tagString != null && IsTagged(tagString, type))//if specified tag is there
            {
                tagString = tagString.Replace(TagValue(type), string.Empty);
            }
            return tagString;
        }

        public static string ToggleTag(string tagString, TagType type)
        {
            tagString = IsTagged(tagString, type) ? RemoveTag(tagString, type) : AddTag(tagString, type);
            return tagString;
        }
    }
}
