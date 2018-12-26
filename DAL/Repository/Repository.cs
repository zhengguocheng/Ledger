using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Utilities;
using System.Data.SqlClient;

namespace DAL
{
    public class Repository : IDocUpdateSupport
    {
        DocumentItemController cntrl = new DocumentItemController();
        public AppConstants.RecordType recordType;
        int recordTypeID;

        //public Repository()
        //{
        //}

        //protected Repository()//for use of subclass EmailRepository
        //{
        //}

        public Repository(AppConstants.RecordType pTableType)
        {
            recordType = pTableType;
            recordTypeID = (int)recordType;
        }

        internal string GetFilePath(string encrytedName)
        {
            return Path.Combine(AppConstants.RpstFolderPath, encrytedName);
        }

        public byte[] GetFileContent(long id)
        {
            tblDocumentItem record = Find(id);

            if (record.IsFolder)
            {
                throw new Exception("No content found as selected item is a folder.");
            }
            else
            {
                //return FileHelper.GetByteArray(GetFilePath(record.EncryptedName));

                //this change is made after using record.Path in outlookemail document
                string path = null;

                if (string.IsNullOrEmpty(record.Path))
                    path = GetFilePath(record.EncryptedName);
                else
                    path = Path.Combine(AppConstants.RpstFolderPath, record.Path, record.EncryptedName);

                return FileHelper.GetByteArray(path);
            }
        }

        #region Crud Methods

        public string CreatePhysicalFolder(string folderName)
        {
            if (string.IsNullOrEmpty(folderName))
            {
                throw new Exception("Name cannot be empty.");
            }
            DirectoryInfo din = new DirectoryInfo(Path.Combine(AppConstants.RpstFolderPath, folderName));
            if (!din.Exists)
            {
                din.Create();
            }
            return din.FullName;
        }

        public bool AddFolder(tblDocumentItem record)
        {
            if (string.IsNullOrEmpty(record.Name))
            {
                throw new Exception("Name cannot be empty.");
            }
            if (!DocumentExist(record.Name, record.ParentID, record.RecordID))
            {
                record.EncryptedName = record.Name;
                record.IsFolder = true;
                record.RecordTypeID = recordTypeID;
                return cntrl.Add(record);
            }
            else
            {
                throw new Exception("Folder with same name already exist.");
            }
        }

        public bool UpdateFolder(tblDocumentItem record)
        {
            if (string.IsNullOrEmpty(record.Name))
            {
                throw new Exception("Name cannot be empty.");
            }
            if (record.IsFolder)
            {
                return cntrl.Update(record);
            }
            return false;
        }

        //public bool AddFile(tblDocumentItem record)
        //{
        //    if (string.IsNullOrEmpty(record.Name))
        //    {
        //        throw new Exception("Name cannot be empty.");
        //    }
        //    if (!DocumentExist(record.Name, record.ParentID, record.RecordID))
        //    {
        //        record.EncryptedName = DateTime.Now.Ticks.ToString() + ".dms";
        //        record.RecordTypeID = recordTypeID;

        //        try
        //        {
        //            if (!string.IsNullOrEmpty(FileHelper.WriteToFile(GetFilePath(record.EncryptedName), record.TempByteData)))
        //            {
        //                return cntrl.Add(record);
        //            }
        //        }
        //        catch (Exception ecp)
        //        {
        //            throw ecp; 
        //        }
        //    }
        //    else
        //    {
        //        throw new Exception("File with same name already exist.");
        //    }
        //    return false;
        //}

        public bool AddFile(tblDocumentItem record, string physicalFolderName = null)
        {
            record.EncryptedName = DateTime.Now.Ticks.ToString() + ".dms";
            record.RecordTypeID = recordTypeID;

            string filePath = null;

            if (string.IsNullOrEmpty(physicalFolderName))//in case No physicalFolderName is passed.
            {
                filePath = GetFilePath(record.EncryptedName);
            }
            else
            {
                record.Path = physicalFolderName;
                string folderPath = CreatePhysicalFolder(physicalFolderName);//create physicalFolderName first
                filePath = Path.Combine(folderPath, record.EncryptedName);//combine physicalFolderName with filename
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                if (string.IsNullOrEmpty(record.Name))
                {
                    throw new Exception("Name cannot be empty.");
                }
                if (!DocumentExist(record.Name, record.ParentID, record.RecordID))
                {
                    try
                    {
                        //string fpath = Path.Combine(path, record.EncryptedName);
                        if (!string.IsNullOrEmpty(FileHelper.WriteToFile(filePath, record.TempByteData)))
                        {
                            return cntrl.Add(record);
                        }
                    }
                    catch (Exception ecp)
                    {
                        throw ecp;
                    }
                }
                else
                {
                    throw new Exception("File with same name already exist.");
                }
            }
            return false;
        }

        //No physical file is created in repository folder. only database record is added
        public bool AddVirtualFile(tblDocumentItem record, string physicalFolderName = null)
        {
            record.EncryptedName = DateTime.Now.Ticks.ToString() + ".dms";
            record.RecordTypeID = recordTypeID;
            record.IsVirtual = true;
            string filePath = null;

            if (string.IsNullOrEmpty(physicalFolderName))//in case No physicalFolderName is passed.
            {
                filePath = GetFilePath(record.EncryptedName);
            }
            else
            {
                record.Path = physicalFolderName;
                string folderPath = CreatePhysicalFolder(physicalFolderName);//create physicalFolderName first
                filePath = Path.Combine(folderPath, record.EncryptedName);//combine physicalFolderName with filename
            }

            if (!string.IsNullOrEmpty(filePath))
            {
                if (string.IsNullOrEmpty(record.Name))
                {
                    throw new Exception("Name cannot be empty.");
                }
                if (!DocumentExist(record.Name, record.ParentID, record.RecordID))
                {
                    try
                    {
                        return cntrl.Add(record);
                    }
                    catch (Exception ecp)
                    {
                        throw ecp;
                    }
                }
                else
                {
                    throw new Exception("File with same name already exist.");
                }
            }
            return false;
        }

        public bool UpdateFile(tblDocumentItem record)
        {
            if (string.IsNullOrEmpty(record.Name))
            {
                throw new Exception("Name cannot be empty.");
            }

            //if no byte array is provided then update only database record
            if (record.TempByteData == null || record.IsVirtual == true)
                return cntrl.Update(record);

            if (!string.IsNullOrEmpty(FileHelper.WriteToFile(GetFilePath(record.EncryptedName), record.TempByteData)))
            {
                return cntrl.Update(record);
            }
            return false;
        }

        public bool UpdateFileContent(long recordID, byte[] data)
        {
            tblDocumentItem itm = Find(recordID);
            itm.TempByteData = data;
            return UpdateFile(itm);
        }



        public bool BatchUpdate(List<tblDocumentItem> recordList, bool changeUpdateDate = true)
        {
            return cntrl.BatchUpdate(recordList, changeUpdateDate);
        }

        #endregion

        #region Recycle Bin & Delete

        tblItemInfoController itemInfoCnt = new tblItemInfoController();
        RecycleBinController cntRB = new RecycleBinController();

        //Include Parent record as well
        public void FindAllChildRecursive(tblDocumentItem record, List<tblDocumentItem> lstChild)
        {
            if (record.IsFolder)
            {
                var lst = this.FetchChildren(record);
                foreach (var item in lst)
                {
                    FindAllChildRecursive(item, lstChild);
                }
            }

            lstChild.Add(record);
        }
        bool IsFileLocked(tblDocumentItem record)
        {
            var lstInfo = itemInfoCnt.FetchAllByDocumentItemID(record.ID);
            if (lstInfo != null && lstInfo.Count > 0)
            {
                var itemInfo = lstInfo[0];
                if (itemInfo.LockedByUserID.HasValue)
                    return true;
            }
            return false;
        }

        public List<tblDocumentItem> SearchLockedFilesAndFolders(tblDocumentItem record)
        {
            List<tblDocumentItem> lstAllChilds = new List<tblDocumentItem>();
            FindAllChildRecursive(record, lstAllChilds);

            List<tblDocumentItem> lstLockedFileFolders = new List<tblDocumentItem>();

            foreach (var item in lstAllChilds)
            {
                if (IsFileLocked(item))
                {
                    lstLockedFileFolders.Add(item);

                    //Also add all parent folders 
                    var lstParents = GetAllParentFolders(item);

                    foreach (var parent in lstParents)
                    {
                        if (!lstLockedFileFolders.Exists(x => x.ID == parent.ID))
                        {
                            lstLockedFileFolders.Add(parent);
                        }
                    }
                }
            }

            return lstLockedFileFolders;
        }

        public bool MoveToRecycleBin(tblDocumentItem recordToDelete, List<string> lstLockedNames = null)
        {
            if (!recordToDelete.IsRootNode) //dont delete root node.
            {
                if (recordType == AppConstants.RecordType.Ledger)
                {
                    #region Load Locked Files

                    var lstLockedFilesFolders = SearchLockedFilesAndFolders(recordToDelete);
                    if (lstLockedNames != null)
                    {
                        foreach (var item in lstLockedFilesFolders)
                        {
                            if (!item.IsFolder)
                                lstLockedNames.Add(item.Name);
                        }
                    }

                    #endregion

                    var lstAllChilds = new List<tblDocumentItem>();

                    FindAllChildRecursive(recordToDelete, lstAllChilds);

                    foreach (var child in lstAllChilds)
                    {
                        //check locked files
                        if (!lstLockedFilesFolders.Exists(x => x.ID == child.ID))
                        {
                            #region commented
                            //tblRecycleBin rb = new tblRecycleBin();
                            //rb.ID = child.ID;
                            //rb.Name = child.Name;
                            //rb.IsFolder = child.IsFolder;
                            //rb.ParentID = child.ParentID;
                            //rb.Path = child.Path;
                            //rb.EncryptedName = child.EncryptedName;
                            //rb.Notes = child.Notes;
                            //rb.RecordTypeID = child.RecordTypeID;
                            //rb.RecordID = child.RecordID;
                            //rb.OrderNO = child.OrderNO;
                            //rb.CreatedAT = child.CreatedAT;
                            //rb.CreatedBy = child.CreatedBy;
                            //rb.UpdatedAt = child.UpdatedAt;
                            //rb.UpdatedBy = child.UpdatedBy;
                            //rb.IsDeleted = child.IsDeleted;
                            //rb.ItemTag = child.ItemTag;
                            //rb.IsVirtual = child.IsVirtual;
                            //rb.YearEndDate = child.YearEndDate;

                            //rb.MovedDate = DateTime.Now;
                            //rb.Path = FindFullPath(child);

                            //if (cntRB.Save(rb))
                            //{
                            //    cntrl.Delete(child);
                            //}
                            #endregion
                            child.IsDeleted = true;
                            child.Path = FindFullPath(child);
                            child.UpdatedAt = DateTime.Now;
                            cntrl.Update(child);
                        }
                    }
                }
            }
            return false;
        }

        public bool RestoreFromRecycleBin(tblDocumentItem recordToDelete)
        {
            if (!recordToDelete.IsRootNode) //dont delete root node.
            {
                if (recordType == AppConstants.RecordType.Ledger)
                {
                    var lstAllChilds = new List<tblDocumentItem>();

                    FindAllChildRecursive(recordToDelete, lstAllChilds);

                    foreach (var child in lstAllChilds)
                    {
                        //check locked files
                        if (child.IsDeleted)
                        {
                            child.IsDeleted = false;
                            child.Path = null;
                            child.UpdatedAt = DateTime.Now;
                            cntrl.Update(child);
                        }
                    }

                    return true;
                }
            }
            return false;
        }


        public bool DeleteRecursive(tblDocumentItem recordToDelete, bool checkLockedFiles, List<string> lstLockedNames = null)
        {
            #region commented
            //if (!record.IsRootNode) //dont delete root node.
            //{
            //    if (recordType == AppConstants.RecordType.Ledger)
            //    {
            //        var lstLocked = SearchLockedFilesAndFolders(record);
            //        if (lstLockedNames != null)
            //        {
            //            foreach (var item in lstLocked)
            //            {
            //                if (!item.IsFolder)
            //                    lstLockedNames.Add(item.Name);
            //            }
            //        }

            //        DeleteRecursive(record, lstLocked);
            //        DBHelper.ExecuteSP_NoRead(SPNames.SpRemoveDeletedItem, new SqlParameter("@docItemID", record.ID));
            //    }
            //    else
            //    {
            //        record.IsDeleted = true;
            //        cntrl.Update(record);
            //    }
            //}
            //return false;
            #endregion

            if (!recordToDelete.IsRootNode) //dont delete root node.
            {
                if (recordType == AppConstants.RecordType.Ledger)
                {
                    List<tblDocumentItem> lstLockedFilesFolders = new List<tblDocumentItem>();

                    #region Load Locked Files
                    if (checkLockedFiles)
                    {
                        lstLockedFilesFolders = SearchLockedFilesAndFolders(recordToDelete);

                        if (lstLockedNames != null)
                        {
                            foreach (var item in lstLockedFilesFolders)
                            {
                                if (!item.IsFolder)
                                    lstLockedNames.Add(item.Name);
                            }
                        }
                    }
                    #endregion

                    var lstAllChilds = new List<tblDocumentItem>();

                    FindAllChildRecursive(recordToDelete, lstAllChilds);

                    foreach (var child in lstAllChilds)
                    {
                        //check locked files
                        if (!lstLockedFilesFolders.Exists(x => x.ID == child.ID))
                        {
                            cntrl.Delete(child);
                            DBHelper.ExecuteSP_NoRead(SPNames.SpRemoveDeletedItem, new SqlParameter("@docItemID", child.ID));
                        }
                    }

                    return true;
                }
            }

            return false;

        }



        //public void DeleteRecursive(tblDocumentItem record, List<tblDocumentItem> lstLocked)
        //{
        //    if (record.IsFolder)
        //    {
        //        var lst = this.FetchChildren(record);
        //        foreach (var item in lst)
        //        {
        //            DeleteRecursive(item, lstLocked);
        //        }
        //    }

        //    //check locked files
        //    if (lstLocked.Exists(x => x.ID == record.ID))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        record.IsDeleted = true;
        //        cntrl.Update(record);
        //    }

        //}

        #endregion

        #region copy, move

        public bool Move(tblDocumentItem itm, long newParentID)
        {
            if (itm.IsRootNode)
            {
                throw new Exception("Root node cannot be moved.");
            }

            tblDocumentItem prnt = Find(newParentID);

            itm.ParentID = newParentID;
            itm.RecordID = prnt.RecordID;
            itm.RecordTypeID = recordTypeID;//prnt.RecordTypeID will not work when ledger document is moved to some client root node because root node always have recordtypeid = 2(client);

            return cntrl.Update(itm);
        }

        public bool Copy(tblDocumentItem copyFromItm, tblDocumentItem copyToItm)
        {
            if (!copyToItm.IsFolder)
                throw new Exception("Cannot copy to a file. Please select a folder.");

            tblDocumentItem newItm = new tblDocumentItem();

            newItm.Name = copyFromItm.Name;
            newItm.CreatedAT = newItm.UpdatedAt = DateTime.Now;
            newItm.Notes = "Copied from " + copyFromItm.Name;

            newItm.RecordID = copyToItm.RecordID;
            newItm.ParentID = copyToItm.ID;
            newItm.RecordTypeID = copyToItm.RecordTypeID;

            newItm.TempByteData = GetFileContent(copyFromItm.ID);
            newItm.IsFolder = false;

            return AddFile(newItm);
        }

        #endregion

        #region Search methods

        public void CleanRepository()
        {
            List<tblDocumentItem> lst = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    lst = context.tblDocumentItems.Where(x => x.RecordTypeID == 2 && x.IsDeleted == true && x.IsFolder == false).ToList();
                    foreach (var file in lst)
                    {
                        try
                        {
                            string path = GetFilePath(file.EncryptedName);

                            bool deleted = false;
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                                deleted = true;
                            }
                            else
                                deleted = true;

                            if (deleted)
                            {
                                context.Detach(file);//detach the entity before calling delete function
                                cntrl.Delete(file);
                            }
                        }
                        catch (Exception ecp)
                        {
                            GlobalLogger.LogException(ecp);
                        }
                    }
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
        }

        public tblDocumentItem Find(long id)
        {
            return cntrl.Find(id);
        }

        public List<tblDocumentItem> FetchAll()
        {
            return cntrl.FetchAll();
        }

        public List<tblDocumentItem> FetchChildren(tblDocumentItem rec)
        {
            if (!rec.IsFolder)
            {
                throw new Exception("Item is not a folder.");
            }
            else
            {
                List<tblDocumentItem> lst = null;
                try
                {
                    using (dbDMSEntities context = ContextCreater.GetContext())
                    {
                        lst = context.tblDocumentItems.Where(x => x.RecordID == rec.RecordID && x.RecordTypeID == recordTypeID && x.ParentID == rec.ID && x.IsDeleted != true).ToList();
                    }
                }
                catch (Exception ecp)
                {
                    throw ecp;
                }
                return lst;
            }
        }

        public List<tblDocumentItem> FetchChildren(long parentID)
        {
            if (parentID == 0)
            {
                return FetchAllRooteNotes();
            }
            else
                return FetchChildren(Find(parentID));
        }

        //Root node is common between DMS ans ledger 
        public List<tblDocumentItem> FetchAllRooteNotes()
        {
            List<tblDocumentItem> lst = null;

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                //Root node is common between DMS ans ledger 
                lst = context.tblDocumentItems.Where(x => x.ParentID == 0
                                        && (x.RecordTypeID == (int)AppConstants.RecordType.Client || x.RecordTypeID == (int)AppConstants.RecordType.Ledger)//
                                        && x.IsDeleted != true).ToList();
                return lst;
            }
        }

        //fetch clients root node
        //Root node is common between DMS ans ledger 
        public List<tblDocumentItem> FetchRootNode(long clientID)
        {
            List<tblDocumentItem> lst = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    lst = context.tblDocumentItems.Where(x => x.RecordID == clientID
                                                        && (x.RecordTypeID == (int)AppConstants.RecordType.Client || x.RecordTypeID == (int)AppConstants.RecordType.Ledger)//Root node is common between DMS ans ledger 
                                                        && x.ParentID == 0 && x.IsDeleted != true).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return lst;
        }

        public List<tblDocumentItem> FetchSiblings(tblDocumentItem rec)
        {
            List<tblDocumentItem> lst = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    lst = context.tblDocumentItems.Where(x => x.RecordID == rec.RecordID && x.RecordTypeID == recordTypeID && x.ParentID == rec.ParentID && x.IsDeleted != true).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return lst;
        }

        public List<tblDocumentItem> FetchSiblings(long recID)
        {
            tblDocumentItem itm = Find(recID);
            return FetchSiblings(itm);
        }

        public tblDocumentItem FindByName(string docName, long parentID, long clientID)
        {
            return cntrl.FindByName(docName, parentID, clientID, recordTypeID);
        }

        public tblDocumentItem FindByName(string docName, long clientID)
        {
            return cntrl.FindByName(docName, clientID, recordTypeID);
        }

        public bool DocumentExist(string docName, long parentID, long clientID)
        {
            return cntrl.DocumentExist(docName, parentID, clientID, recordTypeID);
        }

        public string FindFullPath(tblDocumentItem itm)
        {
            if (itm == null)
                return string.Empty;
            string seperator = @" > ";
            string fullPath = itm.Name;

            while (!itm.IsRootNode)
            {
                itm = Find(itm.ParentID);
                fullPath = itm.Name + seperator + fullPath;
            }

            return fullPath;
        }

        public List<tblDocumentItem> GetAllParentFolders(tblDocumentItem itm)
        {
            var lst = new List<tblDocumentItem>();

            if (itm == null)
                return lst;

            while (!itm.IsRootNode)
            {
                itm = Find(itm.ParentID);
                lst.Add(itm);
            }

            return lst;
        }

        public List<tblDocumentItem> FindRecursive(long parentFolderID, Tags.TagType tag, List<tblDocumentItem> lstResult)
        {
            var lst = FetchChildren(parentFolderID);

            foreach (var child in lst)
            {
                if (child.IsTagged(child.ItemTag, tag) && child.IsDeleted != true)
                {
                    lstResult.Add(child);
                }
            }

            foreach (var child in lst)
            {
                if (child.IsFolder)
                {
                    FindRecursive(child.ID, tag, lstResult);
                }
            }

            return lstResult;
        }

        #endregion

        #region View Methods

        public VwDocumentList FindView(long docID)
        {
            VwDocumentList rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.VwDocumentLists.FirstOrDefault(x => x.ID == docID && x.IsDeleted == false);
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<VwDocumentList> FetchAllView(int clientID, long parentID)
        {
            List<VwDocumentList> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.VwDocumentLists.Where(x => x.RecordTypeID == clientID && x.RecordTypeID == recordTypeID && x.ParentID == parentID && x.IsDeleted != true).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<VwDocumentList> FindAllTagged(Tags.TagType type)
        {
            List<VwDocumentList> lst = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    string tagValue = Tags.TagValue(type);
                    lst = context.VwDocumentLists.Where(x => x.ItemTag.Contains(tagValue) && x.RecordTypeID == recordTypeID && x.IsDeleted != true).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return lst;
        }

        #endregion


    }
}
