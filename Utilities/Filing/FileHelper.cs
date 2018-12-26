using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;

namespace Utilities
{
    static public class FileHelper
    {
        public static byte[] GetByteArray(string filePath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(filePath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            using (FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                //Use BinaryReader to read file stream into byte array.
                using (BinaryReader br = new BinaryReader(fStream))
                {
                    //When you use BinaryReader, you need to supply number of bytes to read from file.
                    //In this case we want to read entire file. So supplying total number of bytes.
                    data = br.ReadBytes((int)numBytes);
                }
            }
            return data;
        }

        public static string ReadFile(string filePath)
        {
            return System.IO.File.ReadAllText(filePath);
        }

        public static string WriteToFile(string filePath, byte[] arr)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            DirectoryInfo din = new DirectoryInfo(dirPath);
            if (!din.Exists)
                Directory.CreateDirectory(dirPath);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(arr, 0, arr.Length);
                return filePath;
            }
            return null;
        }

        //Deletes the folder at path and everything in it
        static public void EraseOldTraces(string path)
        {
            try
            {
                //Validates that path exists
                if (Directory.Exists(path))
                {
                    //Loops on all folders in that path and deletes all files in each folder then deletes that folder
                    foreach (var sfolder in Directory.GetDirectories(path))
                    {
                        //Call a function to delete all files inside the folder name that was passed
                        deleteAllFiles(sfolder);
                        Directory.Delete(sfolder);
                    }
                    try
                    {
                        deleteAllFiles(path);
                        Directory.Delete(path);
                    }
                    catch (Exception ex)
                    {
                        //Mylogger.GlobalLogger.AddEvent(ex, "Error occured while deleting all the file in " + path);
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Mylogger.GlobalLogger.AddEvent(ex);
            }
        }

        //Takes a folder location and deletes all the files inside
        private static void deleteAllFiles(string path)
        {
            try
            {
                //Loops on all files in that path
                foreach (var sFile in Directory.GetFiles(path))
                {
                    File.Delete(sFile);
                }
            }
            catch (Exception ex)
            {
                throw ex;
                //Mylogger.GlobalLogger.AddEvent(ex);
            }
        }

        //Get a list of all files with a certain extension inside a certain folder
        static public string[] GetAllFiles(string extension, string folderName)
        {
            string[] allFiles = null;

            try
            {
                allFiles = Directory.GetFiles(folderName, "*." + extension);
            }
            catch (Exception ex)
            {
                throw ex;
                //Mylogger.GlobalLogger.AddEvent(ex);
            }
            return allFiles;
        }

        //Get a list of all files with a certain extension inside a certain folder
        static public int GetCountOfFiles(string extension, string folderName)
        {
            return GetAllFiles(extension, folderName).Length;
        }

        static public void CreateFile(string path, string content)
        {
            File.AppendAllText(path, content);
        }

        static public void MoveFile(string srcFilePath, string desFilePath)
        {
            if (File.Exists(srcFilePath))
            {
                if (File.Exists(desFilePath))
                {
                    File.Delete(desFilePath);
                }
                File.Move(srcFilePath, desFilePath);
            }
        }

        static public void CreateDirectory(string srcFilePath)
        {
            Directory.CreateDirectory(srcFilePath);
            GiveUserPermission(srcFilePath);
        }

        //Check the permissions of the current user to create files in the directory passed in the parameters and if it doesn't have it, 
        //it grants the permission to the user permission to create files
        public static void GiveUserPermission(string directoryPath)
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

        public static FileSystemAccessRule GetDirectoryPermissions(string user, string folderPath)
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

       
    }
}
