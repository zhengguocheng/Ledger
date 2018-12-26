using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DAL
{
    public class AppConstants
    {
        public static bool IsLedger = false;

        public static string ConnString = "name=dbDMSEntities";
        public static string ClientName = "ClientName".Trim().ToLower(), ClientNameVariations = "ClientNameVariations".Trim().ToLower(), TradingName = "TradingName".Trim().ToLower(), Refrence = "Refrence".Trim().ToLower(), NIN = "NIN".Trim().ToLower(), CompanyNumber = "CompanyNumber".Trim().ToLower(), UTR = "UTR".Trim().ToLower(), UTRVariation = "UTRVariation".Trim().ToLower(), VatRegNo = "VatRegNo".Trim().ToLower(), VatRegNoVariation = "VatRegNoVariation".Trim().ToLower();

        public static string DateFormatShort = "dd/MM/yyyy";
        public static string DateTimeFormat = "dd/MM/yyyy hh:mm tt";
        public static string DateFormatFull = "dd/MM/yyyy hh:mm:ss tt";
        public static DateTime NullDate = new DateTime(1960, 1, 1);
        public static DateTime MaxDate = new DateTime(2020, 1, 1);
        public static string DateFormatSqlServer = "yyyy-MM-dd"; //'2015-01-01'
        public static string DateFormatYearEnd = "dd MMM yyyy";


        public const string splitValue = ">>>>";
        public static bool sIsSplitText(string txt)
        {
            //return string.Equals(txt, AppConstants.splitValue);

            bool n = false;
            if (txt != null)
                n = txt.Contains(splitValue);
            return n;
        }
        public static string BuildSplitText(string desc)
        {
            if (desc == null)
                desc = string.Empty;

            if (sIsSplitText(desc))
                return desc;
            else
                return desc + " " + AppConstants.splitValue;
        }


        public const int MessageMaxLimit = 20;
        public const int RecordPerPage = 10;

        public const string WordDocExtention = ".docx";
        public const string ExtractedImageFormat = "tif";
        public static string MsAccessFilter = "MS Access 2003 (*.mdb)|*.mdb|MS Access 2007 (*.accdb)|*.accdb";

        public const string RpstOutlookFolder = "OutlookEmails";

        public static string folAccounts = "Accounts", folCompliant = "Compliant", folTax = "Tax", folVAT = "VAT", folPAYE = "PAYE", folCompanies_House = "Companies House", folSundries = "Sundries", folPending = "Pending", folNotes = "Notes";

        #region Role Management

        public static string Role_Admin = "Admin";
        public static string Role_Contributer = "Contributer";
        public static string Role_Non = string.Empty;

        public static string[] AllRoles = new string[] { Role_Non, Role_Admin, Role_Contributer };

        #endregion

        public static string RpstFolderPath = null;
        public static string RepositoryExportPath = null;
        public static bool ExportImmediatly = false;

        public static bool IsDeployed = false;
        public static string EmailKey = "";
        public static string DMSEmail = "";
        public static bool SendEmails = false;
        public static string LatestVersion = null;

        public static string MatchedFolderPath = null;
        public static string PDFAnalyzerExePath = null;

        public static string LogDirPath = "";

        public static string WordTemplateDir = "WordTeplates";



        public static class LedgerFolder
        {

            #region Folder Names
            public const string Payment = "Payment";
            public const string Receipts = "Receipts";
            public const string Reconcile = "Reconcile";
            public const string SDB = "Sales Day Book";
            public const string Nominal = "Nominal";
            public const string Groups = "Groups";
            public const string Journals = "Journals";
            public const string Trial_Balance = "Trial Balance";
            public const string Reports = "Reports";
            public const string Database = "Database";
            public const string Notes = "Notes";
            public const string LedgerReportFolder = "Ledger Report";

            #endregion


            #region File Extentions
            public const string PaymentExt = ".pmn";
            public const string ReceiptsExt = ".rcp";
            public const string TrialBalanceExt = ".trb";
            public const string SDBExt = ".sdb";
            public const string JournalsExt = ".jrl";
            public const string ReconcileExt = ".brc";

            #endregion

            public static List<string> FolderList = new List<string> { Payment, Receipts, Reconcile, SDB, Nominal, 
                                                                        Groups, Journals, Trial_Balance, Reports, Database, Notes };

            public static List<string> ReportFolderList = new List<string> { LedgerReportFolder };

            public static bool IsLedgerFolder(string str)
            {
                return FolderList.Contains(str);
            }

            public static string GetExtention(string folderName)
            {
                string ext = string.Empty;
                switch (folderName)
                {
                    case Payment:
                        ext = PaymentExt; break;
                    case Receipts:
                        ext = ReceiptsExt; break;
                    case Trial_Balance:
                        ext = TrialBalanceExt; break;
                    case SDB:
                        ext = SDBExt; break;
                    case Journals:
                        ext = JournalsExt; break;
                    case Reconcile:
                        ext = ReconcileExt; break;
                    default:
                        break;
                }
                return ext;
            }

            public static Tags.TagType GetTag(string folderName)
            {
                Tags.TagType tag = Tags.TagType.None;
                switch (folderName)
                {
                    case Payment:
                        tag = Tags.TagType.Payment_FileType; break;
                    case Receipts:
                        tag = Tags.TagType.Receipts_FileType; break;
                    case Trial_Balance:
                        tag = Tags.TagType.TrialBal_FileType; break;
                    case SDB:
                        tag = Tags.TagType.SDB_FileType; break;
                    case Journals:
                        tag = Tags.TagType.Journal_FileType; break;
                    case Reconcile:
                        tag = Tags.TagType.BankReconcile_FileType; break;
                    default:
                        break;
                }
                return tag;
            }

            public static Tags.TagType GetTagFromFileName(string fileName)
            {
                var ext = Path.GetExtension(fileName);
                if (ext == PaymentExt)
                {
                    return Tags.TagType.Payment_FileType;
                }
                if (ext == ReceiptsExt)
                {
                    return Tags.TagType.Receipts_FileType;
                }
                if (ext == TrialBalanceExt)
                {
                    return Tags.TagType.TrialBal_FileType;
                }
                if (ext == JournalsExt)
                {
                    return Tags.TagType.Journal_FileType;
                }
                if (ext == SDBExt)
                {
                    return Tags.TagType.SDB_FileType;
                }
                if (ext == ReconcileExt)
                {
                    return Tags.TagType.BankReconcile_FileType;
                }

                return Tags.TagType.None;
            }
        }

        public enum RecordType
        {
            Task = 1,
            Client = 2,
            Email = 3,
            Ledger = 4,
        }

        public static string GetPassword()
        {
            string s = Utilities.EncryptionHelper.Decrypt(AppConstants.EmailKey, "418915");
            return s;
        }

        static string tempDir = null;
        public static void SetTempDirectory(string startupPath)
        {
            tempDir = Path.Combine(startupPath, "Temporary Documents");
            if (!Directory.Exists(tempDir))
            {
                DirectoryInfo dinf = Directory.CreateDirectory(tempDir);
            }
        }

        public static string TempDirectory
        {
            get
            {
                return tempDir;
            }
        }
    }
}
