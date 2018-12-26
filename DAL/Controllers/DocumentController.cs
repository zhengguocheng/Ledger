using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Objects.DataClasses;

namespace DAL
{
    //public class DocumentController : BaseController
    //{

    //    //public long Add(tblDocument record)
    //    //{
    //    //    try
    //    //    {
    //    //        //EntityObject obj = (EntityObject)record;

    //    //        using (context = new dbDMSEntities())
    //    //        {
    //    //            context.AddObject("tblDocuments", record);
    //    //            //context.CreateObjectSet().AddObject(record);
    //    //            context.SaveChanges();
    //    //        }
    //    //        return record.ID;
    //    //    }
    //    //    catch (Exception ecp)
    //    //    {
    //    //        return -1;
    //    //    }
    //    //}

    //    //public long Delete(tblDocument record)
    //    //{
    //    //    try
    //    //    {
    //    //        using (context = new dbDMSEntities())
    //    //        {
    //    //            context.DeleteObject(record);
    //    //            context.SaveChanges();
    //    //        }
    //    //        return record.ID;
    //    //    }
    //    //    catch (Exception ecp)
    //    //    {
    //    //        return -1;
    //    //    }
    //    //}

    //    //public bool UpdateRecord(tblDocument record)
    //    //{
    //    //    EntityKey key;
    //    //    object originalItem;
    //    //    try
    //    //    {
    //    //        using (context = new dbDMSEntities())
    //    //        {
    //    //            key = context.CreateEntityKey(record.EntityKey.EntitySetName, record);

    //    //            if (context.TryGetObjectByKey(key, out originalItem))
    //    //            {
    //    //                context.ApplyCurrentValues(key.EntitySetName, record);
    //    //            }

    //    //            context.SaveChanges();

    //    //            return true;
    //    //        }
    //    //    }
    //    //    catch (Exception ecp)
    //    //    {
    //    //        return false;
    //    //    }
    //    //}

    //    public DocumentController()
    //    {
    //        this.EntitySetName = "tblDocuments";
    //    }

    //    public bool Add(tblDocument record)
    //    {
    //        return this.AddEntity(record);
    //    }

    //    public bool Update(tblDocument record)
    //    {
    //        return this.UpdateEntity(record);
    //    }
        
    //    public bool Delete(tblDocument record)
    //    {
    //        return this.DeleteEntity(record);
    //    }

    //    public tblDocument Find(long id)
    //    {
    //        tblDocument rec = null;
    //        try
    //        {
    //            using (dbDMSEntities context = ContextCreater.GetContext())
    //            {
    //                rec = context.tblDocuments.FirstOrDefault(x => x.ID == id);
    //            }
    //        }
    //        catch (Exception ecp)
    //        {
    //            throw ecp;
    //        }
    //        return rec;
    //    }

    //    public List<tblDocument> FetchAll()
    //    {
    //        List<tblDocument> rec = null;
    //        try
    //        {
    //            using (dbDMSEntities context = ContextCreater.GetContext())
    //            {
    //                rec = context.tblDocuments.ToList();
    //            }
    //        }
    //        catch (Exception ecp)
    //        {
    //            throw ecp;
    //        }
    //        return rec;
    //    }

    //    public tblDocument FindByName(string docName,long clntID)
    //    {
    //        tblDocument doc = null;
    //        using (dbDMSEntities context = ContextCreater.GetContext())
    //        {
    //            doc = context.tblDocuments.FirstOrDefault(x => x.ClientID == clntID && x.DocName == docName);
    //        }
    //        return doc; 
    //    }

    //    public bool DocumentExist(string docName, long clntID)
    //    {
    //        return FindByName(docName,clntID) != null;
    //    }
    //}
}
