using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ExecuteCode
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
            LoadAppConstants();
            Application.Run(new Form1());
        }

        static void LoadAppConstants()
        {
            AppConstants.IsLedger = true;

            

            string macname = System.Environment.MachineName;

            AppConstants.SetTempDirectory(Application.StartupPath);

            if (string.Compare(macname, "shahbazkhurram", true) == 0 || string.Compare(macname, "VC1", true) == 0)
            {
                AppConstants.ConnString = "name=dbDMSEntitieslocal";
            }
            else
            {
                AppConstants.IsDeployed = true;
                AppConstants.SendEmails = true;
                AppConstants.RpstFolderPath = @"\\192.168.1.11\repository$";
                AppConstants.ConnString = "name=dbDMSEntities";
            }

            SettingsController cnt = new SettingsController();

            AppConstants.IsDeployed = true;

            //string s = AppConstants.GetPassword();
            #region this line is commented temporarly. Modify MatchedFolderPath value in db to '.\Matched PDF' and uncomment this line
            //AppConstants.MatchedFolderPath = cnt.GetSettings("MatchedFolderPath") ?? DMSSettings.Default.MatchedFolderPath;
            #endregion

        }
    }
}
