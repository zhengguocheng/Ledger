using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class LedgerRepository : Repository
    {
        public LedgerRepository()
            : base(AppConstants.RecordType.Ledger)
        {

        }

        //Fetch all documents related to given email
        public List<tblDocumentItem> FetchAllDocs(long emailID)
        {
            List<tblDocumentItem> doc = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                doc = context.tblDocumentItems.Where(x => x.RecordID == emailID
                    && x.RecordTypeID == (int)AppConstants.RecordType.Email
                    && x.IsDeleted == false).ToList();
            }
            return doc;
        }

        public List<tblDocumentItem> FetchAllYearEnd(long parentID)
        {
            List<tblDocumentItem> docs = null;

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                docs = context.tblDocumentItems.Where(x => x.ParentID == parentID
                    && x.RecordTypeID == (int)AppConstants.RecordType.Ledger
                    && x.YearEndDate != null
                    && x.IsDeleted == false).ToList();
            }

            docs = docs.Where(x => x.ItemTag.Contains(Tags.TagValue(Tags.TagType.YearEndFolder))).ToList();
            return docs;
        }

        public List<tblDocumentItem> FetchAllYearEndByClient(long clientID)
        {
            List<tblDocumentItem> docs = null;

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                docs = context.tblDocumentItems.Where(x => x.RecordID == clientID
                    && x.RecordTypeID == (int)AppConstants.RecordType.Ledger
                    && x.YearEndDate != null
                    && x.IsDeleted == false).ToList();
            }

            docs = docs.Where(x => x.ItemTag.Contains(Tags.TagValue(Tags.TagType.YearEndFolder))).ToList();
            return docs;
        }


        public string GetFolderYearEnd(long docItemID)
        {
            try
            {
                tblDocumentItem doc = GetYearEndFolder(docItemID);

                //if (doc.YearEndDate.HasValue)
                //    return doc.YearEndDate.Value.ToString(AppConstants.DateFormatYearEnd);

                //string[] arr = doc.Name.Split("-".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                //string date = arr[arr.Length - 1].Trim();

                var date = doc.YearEndDate.Value.ToString(AppConstants.DateFormatYearEnd);

                return date;
            }
            catch (Exception ecp)
            {
                throw new Exception("Cannot load Year End date.");
            }
        }

        public tblDocumentItem GetYearEndFolder(long docItemID)
        {
            tblDocumentItem doc = null;

            while (true)
            {
                doc = Find(docItemID);//get current doc

                if (doc.IsTagged(doc.ItemTag, Tags.TagType.YearEndFolder))
                    break;
                else
                    docItemID = Find(doc.ParentID).ID;
            }

            return doc;
        }

        public bool IsChildOf(long docItemID, string parentName)//work for only Ledger folders 1 level beneath YearEnd
        {
            tblDocumentItem itm = Find(docItemID);
            tblDocumentItem parent = Find(itm.ParentID);

            while (parent != null)
            {
                if (string.Compare(parent.Name, parentName, true) == 0)
                {
                    var objFol = Find(parent.ParentID);//Get one level up
                    if (objFol.IsLedgerYearEnd())//check if it is YearEnd
                        return true;
                }
                if (parent.IsTagged(parent.ItemTag, Tags.TagType.YearEndFolder))
                    return false;
                else
                    parent = Find(parent.ParentID);
            }

            return false;
        }

        public bool IsChildOf(long docItemID, Tags.TagType tag)
        {
            tblDocumentItem itm = Find(docItemID);
            tblDocumentItem parent = Find(itm.ParentID);

            while (parent != null)
            {
                if (parent.IsTagged(parent.ItemTag, tag))
                    return true;
                if (parent.IsTagged(parent.ItemTag, Tags.TagType.YearEndFolder))
                    return false;
                else
                    parent = Find(parent.ParentID);
            }

            return false;
        }

        //return prev yearend folder against any items current year end. If items current year end is 31/3/2015 this function would return yearend folder 31/3/2014
        public tblDocumentItem PreviousYearEndFolder(long docItemID)
        {
            tblDocumentItem docPreYeEnd = null;

            tblDocumentItem docYrEnd = GetYearEndFolder(docItemID);

            if (docYrEnd != null)
            {
                DateTime? prvYrEnd = docYrEnd.YearEndDate.Value.AddYears(-1);

                var docList = FetchAllYearEnd(docYrEnd.ParentID);

                docPreYeEnd = docList.FirstOrDefault(x => x.YearEndDate == prvYrEnd);
            }
            return docPreYeEnd;
        }


    }
}
