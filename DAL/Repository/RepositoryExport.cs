using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities;
using System.Diagnostics;

namespace DAL
{
    public class RepositoryExport
    {
        Repository rep = new Repository(AppConstants.RecordType.Client);
        string parentFolder;

        public void Export(string folderPath)
        {
            parentFolder = folderPath;

            List<tblDocumentItem> rootNodes = rep.FetchAllRooteNotes();

            foreach (tblDocumentItem itm in rootNodes)
                ExportFolder(itm,parentFolder);
        }

        void ExportFolder(tblDocumentItem itm,string parentPath)
        {
            string legalName = itm.Name;
            legalName.Trim(" ".ToCharArray());
            legalName.Trim(".".ToCharArray());
            legalName.Replace("<", string.Empty);
            legalName.Replace(">", string.Empty);
            legalName.Replace(":", string.Empty);
            legalName.Replace("\"", string.Empty);
            legalName.Replace("/", string.Empty);
            legalName.Replace("|", string.Empty);
            legalName.Replace("?", string.Empty);
            legalName.Replace("*", string.Empty);
            legalName.Replace("*", string.Empty);
            
            string itmPath = string.Empty;

            try
            {
                foreach (char item in Path.InvalidPathChars.ToArray())
                {
                    legalName.Replace(item.ToString(), string.Empty);
                }

                itmPath = Path.Combine(parentPath, legalName);

                if (itm.IsFolder)
                {
                    DirectoryInfo din = new DirectoryInfo(itmPath);
                    if (!din.Exists)
                        Directory.CreateDirectory(itmPath);
                }
                else
                {
                    //if (!File.Exists(itmPath)) this is commented for overwriting updated files
                    {
                        string repPath = rep.GetFilePath(itm.EncryptedName);
                        byte[] arr = FileHelper.GetByteArray(repPath);
                        FileHelper.WriteToFile(itmPath, arr);//overwrite if file allready exist.
                        return;
                    }
                }
            }
            catch (Exception ecp)
            {
                ServiceLogger.LogException(ecp);
                return;//this is recursive method. if there is an error exporting file then return so other files can be tried to export.

            }
            List<tblDocumentItem> lstChildren = rep.FetchChildren(itm);

            foreach (tblDocumentItem doc in lstChildren)
                ExportFolder(doc,itmPath);
        }

        void StartSkyDrive()
        {
            //if (Process.GetProcessesByName("SkyDrive.exe").Length == 0)
            //{
            //    Process.Start(@"C:\Users\shahbaz khurram\AppData\Local\Microsoft\SkyDrive\SkyDrive.exe");
            //}
        }

    }



}

