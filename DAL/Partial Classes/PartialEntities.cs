using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Xml;

namespace DAL
{
    public interface iDocumentItemUI
    {
        Bitmap DocImg { get; set; }
        Bitmap StarImg { get; set; }
        bool IsFolder { get; set; }
        string Name { get; set; }
        bool IsStarred();
        bool IsLedgerYearEnd();
        bool? IsLocked();
    }

    public partial class Client
    {
        public List<string> GetEmailList(ref string commaSepVal)//fill commaSepVal with comma seperated email string
        {
            string[] arr = this.EmailAddress1.Split(new string[] { Environment.NewLine,"," }, StringSplitOptions.RemoveEmptyEntries);
            commaSepVal = string.Join(",",arr);
            return arr.ToList();
        }
    }

    public partial class tblDocumentItem : iDocumentItemUI
    {
        public byte[] TempByteData { get; set; }
        public Bitmap DocImg { get; set; }
        public Bitmap StarImg { get; set; }
        TagsController tagCnt = new TagsController();

        public bool IsRootNode 
        {
            get
            {
                return (this.ParentID == 0);
            }
        }

        public bool IsSelected { get; set; }

        #region Commented Tag List Implementation
        //public List<tblTag> TagList { get; set; }
        
        //private List<int> _TagIDs; 
        //public List<int> TagIDs 
        //{
        //    get
        //    {
        //        if (_TagIDs == null)
        //        {
        //            _TagIDs = GetTagIDs();
        //        }
        //        return _TagIDs;
        //    }
        //}

        //public List<tblTag> GetTagList()
        //{
            
        //    List<tblTag> allLst = tagCnt.FetchAll();
            
        //    TagList = (allLst.Where(
        //                            x => TagIDs.Contains(x.ID)
        //                           )
        //              ).ToList<tblTag>();
        //    return TagList;
        //}

        //public void GenerateTagsString()
        //{
        //    StringBuilder strTag = new StringBuilder();
        //    TagIDs.ForEach(x => strTag.Append( "(" + x + ")" ));
        //    this.ItemTag = strTag.ToString();
        //}

        //public List<int> GetTagIDs()
        //{
        //    List<int> ids = new List<int>();
        //    if (!string.IsNullOrEmpty(this.ItemTag))
        //    {
        //        string[] strIDs = this.ItemTag.Split(new string[] { "(", ")" }, StringSplitOptions.RemoveEmptyEntries);
                
        //        foreach (string item in strIDs)
        //        {
        //            ids.Add(Convert.ToInt32(item));
        //        }
        //    }
        //    return ids;
        //}
        
        //void SaveTags()
        //{
        //    DocumentItemController dc = new DocumentItemController();
        //    GenerateTagsString();
        //    dc.Update(this);
        //}
        #endregion

        public void ToggleStar()
        {
            DocumentItemController dc = new DocumentItemController();
            this.ItemTag = Tags.ToggleTag(this.ItemTag, Tags.TagType.Star);
            dc.Update(this);
        }

        public bool? IsLocked()
        {
            var id = FindLockedBy();
            return id > 0;
        }

        public int? FindLockedBy()
        {
            tblItemInfoController dc = new tblItemInfoController();
            var coll = dc.FetchAllByDocumentItemID(this.ID);
            if (coll.Count > 0)
            {
                return coll[0].LockedByUserID;
            }
            return null;
        }

        public void UpdateLock(int? lockedBy)
        {
            tblItemInfoController dc = new tblItemInfoController();
            var coll = dc.FetchAllByDocumentItemID(this.ID);
            if (coll.Count > 0)
            {
                coll[0].LockedByUserID = lockedBy;
                dc.Save(coll[0]);
            }
        }

        public void AddTag(Tags.TagType type)
        {
            this.ItemTag = Tags.AddTag(this.ItemTag, type);
        }

        public bool IsTagged(string tagString, Tags.TagType type)
        {
            return (!string.IsNullOrEmpty(tagString)) && tagString.Contains(Tags.TagValue(type));
        }

        public bool IsStarred()
        {
            return Tags.IsTagged(this.ItemTag, Tags.TagType.Star);
        }

        public bool IsLedgerYearEnd()
        {
            return Tags.IsTagged(this.ItemTag, Tags.TagType.YearEndFolder);
        }
    }


    public partial class tblRecycleBin : iDocumentItemUI
    {
        public byte[] TempByteData { get; set; }
        public Bitmap DocImg { get; set; }
        public Bitmap StarImg { get; set; }
        TagsController tagCnt = new TagsController();

        public bool IsRootNode
        {
            get
            {
                return (this.ParentID == 0);
            }
        }

        public bool IsSelected { get; set; }

        //public void ToggleStar()
        //{
        //    DocumentItemController dc = new DocumentItemController();
        //    this.ItemTag = Tags.ToggleTag(this.ItemTag, Tags.TagType.Star);
        //    dc.Update(this);
        //}

        //public void AddTag(Tags.TagType type)
        //{
        //    this.ItemTag = Tags.AddTag(this.ItemTag, type);
        //}

        //public bool IsTagged(string tagString, Tags.TagType type)
        //{
        //    return (!string.IsNullOrEmpty(tagString)) && tagString.Contains(Tags.TagValue(type));
        //}

        public bool? IsLocked()
        {
            return null;
        }

        public bool IsStarred()
        {
            return Tags.IsTagged(this.ItemTag, Tags.TagType.Star);
        }

        public bool IsLedgerYearEnd()
        {
            return Tags.IsTagged(this.ItemTag, Tags.TagType.YearEndFolder);
        }
    }

    public partial class VwDocumentList : iDocumentItemUI
    {
        public Bitmap DocImg { get; set; }
        public Bitmap StarImg { get; set; }

        //public bool IsFolder { get; set; }
        //public string Name { get; set; }

        public bool IsStarred()
        {
            return Tags.IsTagged(this.ItemTag,Tags.TagType.Star);
        }


        public bool? IsLocked()
        {
            return null;
        }

        public bool IsLedgerYearEnd()
        {
            return Tags.IsTagged(this.ItemTag, Tags.TagType.YearEndFolder);
        }
    }

    public partial class tblOutlookEmail 
    {
        public bool IsSent{ get; set; }//speify if this email is sentby ivan.

        public Bitmap DocImg { get; set; }
        public Bitmap StarImg { get; set; }
    }
   
    public partial class tblTask
    {
        public void GetEmailContent(ref string subject, ref string body, ref string receiver) 
        {
            subject = string.Format("Task: {0}.", this.Title.Trim());
            
            UserController userCntrl = new UserController();
            User assTo = userCntrl.Find(this.AssignedTo.Value);
            receiver = assTo.Email;

            /* Email Formatting
            StringWriter writer = new StringWriter();
            XmlTextWriter xml = new XmlTextWriter(writer);
            xml.Formatting = Formatting.Indented;
            xml.WriteElementString("b", String.Format("Hi {0},", assTo.FirstName));
            xml.wri("br");
            xml.WriteEndElement();
            xml.WriteElementString("p", "First part of the email body goes here");
            xml.Flush();
            */

            body = @"<b>Hi {FirstName},</b><br/>
You are assigned following task at Document Management System.
<br/><br/>
<b>Title</b>: {Title}
<br/><br/>
<b>Detail</b>: {Detail}
<br/><br/>
Please login to Document Management System to view full detail.
<br/><br/>
<b>Thanks.</b>";
            body = body.Replace("{FirstName}", assTo.FirstName);
            body = body.Replace("{Title}", this.Title);
            body = body.Replace("{Detail}", this.Detail);
            
            
        }
    }

    public partial class tblEmail
    {
        public string RecordCaption { get; set; }
    }

}
