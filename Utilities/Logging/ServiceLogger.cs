using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Utilities
{
    public class ServiceLogger
    {
        public static string logDirPath { get; set; }
        public static void LogMessage(string message)
        {
            try
            {
                //string logDirPath = AgentService.WinSer.Default.LogDirectory.TrimEnd(@"\".ToCharArray());
                string fileName = string.Format(logDirPath + @"\DMS_Service_Log {0}.txt", DateTime.Now.ToString("dd_MM_yyyy"));

                if(!Directory.Exists(logDirPath))
                    Directory.CreateDirectory(logDirPath);

                if (!File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                string text = Environment.NewLine + "---  " + DateTime.Now.ToString() + "   ---" + Environment.NewLine + message;

                File.AppendAllText(fileName, text);
            }
            catch(Exception ecp)
            {

            }
        }

        public static void LogException(Exception ex, string extraMessage)
        {
            //LogMessage("Error: " + ex.Message
            //    + Environment.NewLine
            //    + Environment.StackTrace
            //    + (string.IsNullOrEmpty(extraMessage) ? string.Empty : Environment.NewLine + extraMessage));

            string message = string.Empty;
            try
            {
                message = string.IsNullOrEmpty(extraMessage) ? ex.Message : extraMessage + Environment.NewLine + ex.Message;
                if (ex.StackTrace != null)
                    message += message + Environment.NewLine + ex.StackTrace;
            }
            catch { }
            
            LogMessage("Error Message : " + message);
        }

        public static void LogException(Exception ex)
        {
            LogException(ex, string.Empty);
        }
    }

    public class GlobalLogger
    {
        public static void LogMessage(string message)
        {
            try
            {
                string fileName = string.Format(@".\GlobalLog_{0}.txt", DateTime.Now.ToString("dd_MM_yyyy"));

                if (!File.Exists(fileName))
                {
                    FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                    fs.Close();
                }

                string text = Environment.NewLine + "---  " + DateTime.Now.ToString() + "   ---" + Environment.NewLine + message;

                File.AppendAllText(fileName, text);
            }
            catch (Exception ecp)
            {

            }
        }

        public static void LogException(Exception ex, string extraMessage)
        {
            string message = string.Empty;
            try
            {
                message = string.IsNullOrEmpty(extraMessage) ? ex.Message : extraMessage + Environment.NewLine + ex.Message;
                if (ex.StackTrace != null)
                    message += message + Environment.NewLine + ex.StackTrace;
            }
            catch { }

            LogMessage("Error Message : " + message);
        }

        public static void LogException(Exception ex)
        {
            LogException(ex, string.Empty);
        }
    }
}
