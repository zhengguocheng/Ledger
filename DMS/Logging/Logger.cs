using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Security.AccessControl;
using System.Security.Principal;

namespace DMS.Logging
{
    /*
    public class Logger
    {
        private static readonly string destinationFilePath;

        static Logger()
        {
            string exeDir = Path.GetDirectoryName(Application.ExecutablePath);
            string dirName = Path.Combine(exeDir, "Logs");

            System.IO.Directory.CreateDirectory(dirName);
            GiveUserPermission(dirName);

            string fileName = "Log " + DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year;
            destinationFilePath = dirName + "\\" + fileName + ".txt";

        }


        //Check the permissions of the current user to create files in the directory passed in the parameters and if it doesn't have it, 
        //it grants the permission to the user permission to create files
        private static void GiveUserPermission(string directoryPath)
        {
            FileSystemAccessRule rule = GetDirectoryPermissions(WindowsIdentity.GetCurrent().Name, directoryPath);

            if (rule == null || !rule.FileSystemRights.ToString().Contains("Write"))
            {
                DirectoryInfo myDirectoryInfo = new DirectoryInfo(directoryPath);

                DirectorySecurity myDirectorySecurity = myDirectoryInfo.GetAccessControl();
                myDirectorySecurity.RemoveAccessRuleAll(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.ReadAndExecute, AccessControlType.Allow));
                myDirectorySecurity.AddAccessRule(new FileSystemAccessRule(WindowsIdentity.GetCurrent().Name, FileSystemRights.Write, AccessControlType.Allow));

                myDirectoryInfo.SetAccessControl(myDirectorySecurity);
            }

        }

        private static FileSystemAccessRule GetDirectoryPermissions(string user, string folderPath)
        {
            if (!Directory.Exists(folderPath))
            {
                return (null);
            }

            try
            {

                string identityReference = user;
                DirectorySecurity dirSecurity = Directory.GetAccessControl(folderPath);
                foreach (FileSystemAccessRule fsRule in dirSecurity.GetAccessRules(true, true, typeof(NTAccount)))
                {
                    if (fsRule.IdentityReference.Value.ToLower() == identityReference.ToLower())
                    {
                        return (fsRule);
                    }
                }
            }

            catch (UnauthorizedAccessException)
            {
                return null;
            }

            return (null);
        }

        public static void LogMessage(string message)
        {
            string text = Environment.NewLine + "---   " + DateTime.Now.ToString() + "   ---" + Environment.NewLine + message;
            File.AppendAllText(destinationFilePath, text);
        }

        public static void LogException(Exception ex, string extraMessage)
        {
            LogMessage("Error: " + ex.Message
                + Environment.NewLine
                + Environment.StackTrace
                + (string.IsNullOrEmpty(extraMessage) ? string.Empty : Environment.NewLine + extraMessage));
        }

        public static void LogException(Exception ex)
        {
            LogException(ex, string.Empty);
        }

        public void LogInfo(string str)
     *  {
     *      LogData(str,false);
     *  }

    }
    */
}
