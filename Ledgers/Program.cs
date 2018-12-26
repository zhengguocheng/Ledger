using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using DMS.UIForms;
using Ledgers.UIForms;
using DMS;
using FrontEnd;
using DAL;
using Ledgers.Properties;

namespace Ledgers
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new LedgerMainForm());

            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(UserControlBase.UnhandledThreadException);
            LoadAppConstants();

            DisplayManager.MainForm = new Ledgers.UIForms.LedgerMainForm();
            DisplayManager.LoadControl(new UcLogin());
            
            Application.Run(DisplayManager.MainForm);
        }

        static void LoadAppConstants()
        {
            AppConstants.IsLedger = true;
            string macname = System.Environment.MachineName;

            if (string.Compare(macname, "shahbazkhurram", true) == 0 || string.Compare(macname, "VC1", true) == 0)
            {
                AppConstants.ConnString = "name=dbDMSEntitieslocal";
            }
            else
            {
                AppConstants.IsDeployed = true;
                AppConstants.SendEmails = true;
                AppConstants.RpstFolderPath = @"\\server01\repository$";
                AppConstants.ConnString = "name=dbDMSEntities";
            }

            SettingsController cnt = new SettingsController();
            //AppConstants.RpstFolderPath = cnt.GetSettings("RpstFolderPath") ?? Settings.Default.RpstFolderPath;
            //AppConstants.EmailKey = cnt.GetSettings("EmailKey") ?? Settings.Default.EmailKey;
            //AppConstants.DMSEmail = cnt.GetSettings("DMSEmail") ?? Settings.Default.DMSEmail;
            AppConstants.LogDirPath = cnt.GetSettings("LogDirPath") ?? Settings.Default.LogDirPath;
            AppConstants.IsDeployed = cnt.GetSettings("IsDeployed") != null ? bool.Parse(cnt.GetSettings("IsDeployed")) : Settings.Default.IsDeployed;
            //AppConstants.SendEmails = cnt.GetSettings("SendEmails") != null ? bool.Parse(cnt.GetSettings("SendEmails")) : Settings.Default.SendEmails;

            #region this line is commented temporarly. Modify MatchedFolderPath value in db to '.\Matched PDF' and uncomment this line
            //AppConstants.MatchedFolderPath = cnt.GetSettings("MatchedFolderPath") ?? DMSSettings.Default.MatchedFolderPath;
            //AppConstants.MatchedFolderPath = Settings.Default.MatchedFolderPath;
            #endregion

            //AppConstants.PDFAnalyzerExePath = cnt.GetSettings("PDFAnalyzerExePath") ?? Settings.Default.PDFAnalyzerExePath;
        }

    }
}
