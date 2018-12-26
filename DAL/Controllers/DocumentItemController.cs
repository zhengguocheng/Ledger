using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data;

namespace DAL
{
    public class DocumentItemController : BaseController
    {
        TagsController tagCnt = new TagsController();


        public DocumentItemController()
        {
            this.EntitySetName = "tblDocumentItems";
        }

        internal bool Add(tblDocumentItem record)
        {
            if (record.RecordTypeID < 1)
            {
                throw new Exception("RecordTypeID must be greater then 0.");
            }
            record.IsDeleted = false;
            record.CreatedAT = DateTime.Now;
            record.UpdatedAt = DateTime.Now;
            return this.AddEntity(record);
        }

        public bool Update(tblDocumentItem record)
        {
            record.UpdatedAt = DateTime.Now;
            return this.UpdateEntity(record);
        }

        internal bool BatchUpdate(List<tblDocumentItem> recordList, bool changeUpdateDate = true)
        {
            if (changeUpdateDate)
            {
                DateTime updatedAt = DateTime.Now;
                recordList.ForEach(x => x.UpdatedAt = updatedAt);
            }
            IEnumerable<EntityObject> el = recordList.Cast<EntityObject>();
            return this.BatchUpdate(el);
        }

        public bool Delete(tblDocumentItem record)
        {
            return this.DeleteEntity(record);
        }

        internal tblDocumentItem Find(long id)
        {
            tblDocumentItem rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblDocumentItems.FirstOrDefault(x => x.ID == id && x.IsDeleted == false);
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public tblDocumentItem Find(string encryptedName)
        {
            tblDocumentItem rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblDocumentItems.FirstOrDefault(x => x.EncryptedName == encryptedName && x.IsDeleted == false);
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        internal List<tblDocumentItem> FetchAll()
        {
            List<tblDocumentItem> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblDocumentItems.Where(x => x.IsDeleted == false).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<tblDocumentItem> FetchDeletedItems()
        {
            List<tblDocumentItem> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.tblDocumentItems.Where(x => x.IsDeleted == true).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }


        //public void LoadTagList(List<tblDocumentItem> docList)
        //{
        //    List<tblTag> allLst = tagCnt.FetchAll();

        //    foreach (tblDocumentItem doc in docList)
        //    {
        //        doc.GetTagIDs();
        //        doc.TagList = (allLst.Where(
        //                           x => doc.TagIDs.Contains(x.ID)
        //                          )
        //             ).ToList<tblTag>();
        //    }
        //}

        internal List<tblDocumentItem> FetchChildrens(tblDocumentItem parent)
        {
            List<tblDocumentItem> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = (List<tblDocumentItem>)(context.tblDocumentItems.Select(x => x.IsDeleted != false && x.ParentID == parent.ID));
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        internal tblDocumentItem FindByName(string docName, long parentID, long clientID, int recTypeID)
        {
            tblDocumentItem doc = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                doc = context.tblDocumentItems.FirstOrDefault(x => x.ParentID == parentID && x.Name == docName && x.RecordID == clientID && x.RecordTypeID == recTypeID && x.IsDeleted == false);
            }
            return doc; 
        }

        internal tblDocumentItem FindByName(string docName, long clientID, int recTypeID)//return first element having same name
        {
            List<tblDocumentItem> rec = null;

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDocumentItems.Where(x => x.Name == docName && x.RecordID == clientID && x.RecordTypeID == recTypeID && x.IsDeleted == false).ToList();
            }

            return (rec.Count > 0) ? rec.OrderBy(x => x.ID).ToList()[0] : null;
        }


        internal bool DocumentExist(string docName, long parentID, long clientID, int recTypeID)
        {
            return FindByName(docName, parentID,clientID,recTypeID) != null;
        }
    }
}
