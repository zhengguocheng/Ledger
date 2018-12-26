using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using DAL;
using System.IO;
using DMS.UIForms;
using System.Runtime.InteropServices;
using System.Timers;

namespace DMS.CustomClasses
{

    class ProcessInfo
    {
        public string DocumentName;
        public string UniqueName;
        public long DocumentID;
        public IDocUpdateSupport DocumentUpdator;
        public int ProcessID;
        public DateTime LastModifiedDate;

        public ProcessInfo()
        {
            LastModifiedDate = DateTime.Now;
        }
    }

    //public static class TempraryDocuments
    //{
    //    static List<ProcessInfo> filesToMonitor = new List<ProcessInfo>();
    //    static FileSystemWatcher fileSysWatcher = new FileSystemWatcher();

    //    static TempraryDocuments()
    //    {
    //        CleanTempFolder();
    //        CreateWatcher();
    //    }

    //    public static void CreateWatcher()
    //    {
    //        fileSysWatcher.Changed += new FileSystemEventHandler(watcher_FileChanged);
    //        fileSysWatcher.IncludeSubdirectories = true;
    //        fileSysWatcher.NotifyFilter = NotifyFilters.Attributes;
    //        fileSysWatcher.Path = AppConstants.TempDirectory;
    //        fileSysWatcher.EnableRaisingEvents = true;
    //    }

    //    public static void CleanTempFolder()
    //    {
    //        try
    //        {
    //            string directoryPath = AppConstants.TempDirectory;

    //            try
    //            {
    //                foreach (var fileName in Directory.GetFiles(directoryPath))
    //                {
    //                    File.Delete(fileName);
    //                }
    //            }
    //            catch (Exception ecp)
    //            {
    //                GlobalLogger.logger.LogException(ecp);
    //            }

    //            Directory.GetDirectories(directoryPath).ToList().ForEach(Directory.Delete);
    //        }
    //        catch (Exception ecp)
    //        {
    //            GlobalLogger.logger.LogException(ecp);
    //        }
    //    }

    //    public static List<string> TryDeleteTempFiles()
    //    {
    //        string directoryPath = AppConstants.TempDirectory;
    //        List<string> unDeletedLst = new List<string>();

    //        foreach (var fileName in Directory.GetFiles(directoryPath))
    //        {
    //            if (isValidFile(fileName) == false)
    //                continue;
                                
    //            if (fileName.ToLower().Contains("readonly"))//dont bother deleting readonly files. They are automatically deleted when system starts.
    //                continue;

    //            string newname = Path.Combine( Path.GetDirectoryName(fileName),"file to delete _ " + Path.GetFileName(fileName));
    //            try
    //            {
    //                File.Move(fileName, newname);
    //                File.Delete(newname);
    //            }
    //            catch (Exception ecp)
    //            {
    //                GlobalLogger.logger.LogException(ecp);
    //                unDeletedLst.Add(fileName);
    //            }
    //        }

    //        return unDeletedLst;
    //    }

    //    static bool isValidFile(string path)
    //    {
    //        if (path.Contains("~$")) //
    //            return false;

    //        if (!File.Exists(path)) //to avoid some temp files that need not be visible
    //            return false;

    //        if (Path.GetExtension(path) == ".tmp") //to avoid some temp files that need not be visible
    //            return false;    

    //        return true;
    //    }

    //    static void watcher_FileChanged(object sender, FileSystemEventArgs e)
    //    {
    //        if (e.ChangeType == WatcherChangeTypes.Changed )
    //        {
    //            if (isValidFile(e.FullPath) == false)
    //                return;
                
    //            string str = "File Path: "+e.FullPath + Environment.NewLine;
    //            str+= "File Name: "+e.Name;

    //            try
    //            {
    //                fileSysWatcher.EnableRaisingEvents = false;
    //                DocumentSaved(e.FullPath);
    //            }
                
    //            finally
    //            {
    //                fileSysWatcher.EnableRaisingEvents = true;
    //            }
    //        }
    //    }

    //    static string GetTempPath(string fileName)
    //    {
    //        return Path.Combine(AppConstants.TempDirectory,fileName);
    //    }

    //    //write file in temp document and monitor it for change
    //    public static void OpenMonitorFile(string docName,long docID, byte[] byteData, IDocUpdateSupport docUpdator)
    //    {
    //        string uniqName = Path.GetFileNameWithoutExtension(docName) + " - Version  " +DateTime.Now.Ticks.ToString() + Path.GetExtension(docName);
    //        FileHelper.WriteToTempFile(GetTempPath(uniqName), byteData);
    //        GlobalLogger.logger.LogMessage("Temporary Dir = " + AppConstants.TempDirectory);
    //        string path = GetTempPath(uniqName);

    //        Process p1 = new Process();
    //        p1.StartInfo.FileName = path;
    //        p1.Start();

    //        filesToMonitor.Add(new ProcessInfo { DocumentName = docName, DocumentID = docID, DocumentUpdator = docUpdator, UniqueName = uniqName });
    //    }

    //    //write file in temp folder
    //    public static string SaveToTempFolder(string docName, byte[] byteData)
    //    {
    //        string uniqName = Path.GetFileNameWithoutExtension(docName) + " - Readonly  " + DateTime.Now.Ticks.ToString() + Path.GetExtension(docName);
    //        FileHelper.WriteToTempFile(GetTempPath(uniqName), byteData);

    //        string path = GetTempPath(uniqName);
    //        return path;
    //    }

    //    static void DocumentSaved(string path)
    //    {
    //        try
    //        {
    //            FileInfo finf = new FileInfo(path);
    //            string uniqName = Path.GetFileName(path);

    //            ProcessInfo pinfo = filesToMonitor.FirstOrDefault(x => x.UniqueName == uniqName);
    //            if (pinfo == null)
    //            {
    //                return;// file should not be processed 
    //            }

    //            if (finf.CreationTime < finf.LastWriteTime)//Data LastWriteTime
    //            {
    //                byte[] byteData = FileHelper.GetByteArray(path);

    //                pinfo.DocumentUpdator.UpdateFileContent(pinfo.DocumentID, byteData);
    //                DisplayManager.DisplayDeskTopAlert(CustomMessages.GetCustomMessage(CustomMessages.UpdatedSuccessfully, pinfo.DocumentName));
    //            }
    //        }
    //        catch (Exception ecp) 
    //        {
    //            GlobalLogger.logger.LogException(ecp);
    //        }
    //    }
    //}

    public static class TempraryDocuments
    {
        static List<ProcessInfo> filesToMonitor = new List<ProcessInfo>();
        static FileSystemWatcher fileSysWatcher = new FileSystemWatcher();

        static TempraryDocuments()
        {
            CleanTempFolder();
            CreateWatcher();
        }

        public static void CreateWatcher()
        {
            try
            {
                fileSysWatcher.Changed += new FileSystemEventHandler(watcher_FileChanged);
                fileSysWatcher.IncludeSubdirectories = true;
                fileSysWatcher.NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.Attributes;
                fileSysWatcher.Path = AppConstants.TempDirectory;
                fileSysWatcher.EnableRaisingEvents = true;
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
        }

        public static void CleanTempFolder()
        {
            try
            {
                string directoryPath = AppConstants.TempDirectory;

                try
                {
                    foreach (var fileName in Directory.GetFiles(directoryPath))
                    {
                        File.Delete(fileName);
                    }
                }
                catch (Exception ecp)
                {
                    GlobalLogger.logger.LogException(ecp);
                }

                Directory.GetDirectories(directoryPath).ToList().ForEach(Directory.Delete);
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
        }

        public static List<string> TryDeleteTempFiles()
        {
            string directoryPath = AppConstants.TempDirectory;
            List<string> unDeletedLst = new List<string>();

            foreach (var fileName in Directory.GetFiles(directoryPath))
            {
                if (isValidFile(fileName) == false)
                    continue;

                if (fileName.ToLower().Contains("readonly"))//dont bother deleting readonly files. They are automatically deleted when system starts.
                    continue;

                string newname = Path.Combine(Path.GetDirectoryName(fileName), "file to delete _ " + Path.GetFileName(fileName));
                try
                {
                    File.Move(fileName, newname);
                    File.Delete(newname);
                }
                catch (Exception ecp)
                {
                    GlobalLogger.logger.LogException(ecp);
                    unDeletedLst.Add(fileName);
                }
            }

            return unDeletedLst;
        }

        static bool isValidFile(string path)
        {
            if (path.Contains("~$")) //
                return false;

            if (!File.Exists(path)) //to avoid some temp files that need not be visible
                return false;

            if (Path.GetExtension(path) == ".tmp") //to avoid some temp files that need not be visible
                return false;

            return true;
        }

        static void watcher_FileChanged(object sender, FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed)
            {
                //if (isValidFile(e.FullPath) == false)
                //    return;

                //find modified file
                ProcessInfo modifiedFile = null;
                foreach (var prcInfo in filesToMonitor)
                {
                    var path = GetTempPath(prcInfo.UniqueName);
                    var lastWriteTime = File.GetLastWriteTime(path);
                    if (lastWriteTime > prcInfo.LastModifiedDate)
                    {
                        modifiedFile = prcInfo;
                        break;
                    }
                }

                if (modifiedFile != null)
                {
                    var path = GetTempPath(modifiedFile.UniqueName);

                    string str = "File Path: " + path + Environment.NewLine;
                    str += "File Name: " + modifiedFile.UniqueName;

                    try
                    {
                        fileSysWatcher.EnableRaisingEvents = false;
                        DocumentSaved(path);
                        modifiedFile.LastModifiedDate = DateTime.Now;
                    }

                    finally
                    {
                        fileSysWatcher.EnableRaisingEvents = true;
                    }
                }
            }
        }

        static string GetTempPath(string fileName)
        {
            return Path.Combine(AppConstants.TempDirectory, fileName);
        }

        //write file in temp document and monitor it for change
        public static void OpenMonitorFile(string docName, long docID, byte[] byteData, IDocUpdateSupport docUpdator)
        {
            string uniqName = Path.GetFileNameWithoutExtension(docName) + " - Version  " + DateTime.Now.Ticks.ToString() + Path.GetExtension(docName);
            FileHelper.WriteToTempFile(GetTempPath(uniqName), byteData);
            GlobalLogger.logger.LogMessage("Temporary Dir = " + AppConstants.TempDirectory);
            string path = GetTempPath(uniqName);

            Process p1 = new Process();
            p1.StartInfo.FileName = path;
            p1.Start();

            filesToMonitor.Add(new ProcessInfo { DocumentName = docName, DocumentID = docID, DocumentUpdator = docUpdator, UniqueName = uniqName });
        }

        //write file in temp folder
        public static string SaveToTempFolder(string docName, byte[] byteData)
        {
            string uniqName = Path.GetFileNameWithoutExtension(docName) + " - Readonly  " + DateTime.Now.Ticks.ToString() + Path.GetExtension(docName);
            FileHelper.WriteToTempFile(GetTempPath(uniqName), byteData);

            string path = GetTempPath(uniqName);
            return path;
        }

        static void DocumentSaved(string path)
        {
            try
            {
                FileInfo finf = new FileInfo(path);
                string uniqName = Path.GetFileName(path);

                ProcessInfo pinfo = filesToMonitor.FirstOrDefault(x => x.UniqueName == uniqName);
                if (pinfo == null)
                {
                    return;// file should not be processed 
                }

                if (finf.CreationTime < finf.LastWriteTime)//Data LastWriteTime
                {
                    byte[] byteData = FileHelper.GetByteArray(path);

                    pinfo.DocumentUpdator.UpdateFileContent(pinfo.DocumentID, byteData);
                    DisplayManager.DisplayDeskTopAlert(CustomMessages.GetCustomMessage(CustomMessages.UpdatedSuccessfully, pinfo.DocumentName));
                }
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
        }
    }

}
