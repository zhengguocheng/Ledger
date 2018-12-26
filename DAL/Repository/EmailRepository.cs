using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class EmailRepository : Repository
    {
        public EmailRepository() : base(AppConstants.RecordType.Email)
        {
           
        }

        public tblDocumentItem FetchBody(long emailID)
        {
            List<tblDocumentItem> docs = FetchAllDocs(emailID);
            tblDocumentItem doc = docs.FirstOrDefault(x => x.ItemTag.Contains(Tags.TagValue(Tags.TagType.Email)));
            return doc;
        }

        public List<tblDocumentItem> FetchAttachments(long emailID)
        {
            List<tblDocumentItem> docs = FetchAllDocs(emailID);
            docs = docs.Where(x => x.ItemTag.Contains(Tags.TagValue(Tags.TagType.EmailAttachment))).ToList();
            return docs;
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

        

    }
}
