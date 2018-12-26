using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.IO;

using Utilities;
using System.Timers;

namespace DMS
{
   
    public class ProcessMatchedPDF
    {
        //static string folderPath;
        static Timer impTimer;

        private static void ImportAll()
        {
            try
            {
                string[] arrfiles = FileHelper.GetAllFiles("pdf", AppConstants.MatchedFolderPath);
                GlobalLogger.logger.LogMessage("Folder to search for matched pdf = " + AppConstants.MatchedFolderPath);
                GlobalLogger.logger.LogMessage("total files to import = " + arrfiles.Length);

                foreach (string filePath in arrfiles)
                {
                    ImportMatchedPDF imp = new ImportMatchedPDF(filePath);
                    if (imp.Import())
                        File.Delete(filePath);
                }
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
        }

        public static void StartJob()
        {
            Start();
        }

        static void Start()
        {
            impTimer = new Timer(10 * 1000);
            impTimer.Elapsed += new ElapsedEventHandler(impTimer_Elapsed);
            impTimer.Enabled = true;
        }

        static void impTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            GlobalLogger.logger.LogMessage("PDF importing timer elapsed.");
            impTimer.Enabled = false;
            ImportAll();
            impTimer.Enabled = true;
        }


    }
}
