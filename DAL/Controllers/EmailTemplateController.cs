using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    
    /*
    public class clsEmailTemplateType
    {
        const string TaskVal = "task", ClientVal = "client";

        public enum EnumType
        {
            Task, Client
        }

        public static List<string> GetTemplateList()
        {
            List<string> lst = new List<string>() { TaskVal,ClientVal };
            return lst;
        }

        public static string GetTemplateValue(EnumType type)
        {
            string val = string.Empty;
            switch (type)
            {
                case EnumType.Task:
                    val = TaskVal;
                    break;
                case EnumType.Client:
                    val = ClientVal;
                    break;
                default:
                    val = null;
                    break;
            }
            return val;
        }

        public static EnumType GetTemplateEnum(string val)
        {
            
            switch (val)
            {
                case TaskVal:
                    return EnumType.Task;
                    break;
                case ClientVal:
                    return EnumType.Client;
                    break;
                default:
                    throw new Exception("Invalid value.");
                    break;
            }
            
        }
    }
    */
    public partial class tblEmailTemplate : iEntityBase
    {
        public string UpdatedByName { get; set; }
        public string CreatedByName { get; set; }

        //public clsEmailTemplateType.EnumType TemplateTypeEnum 
        //{
        //    get
        //    {
        //        return clsEmailTemplateType.GetTemplateEnum(this.TemplateType);
        //    }
        //    set
        //    {
        //        this.TemplateType = clsEmailTemplateType.GetTemplateValue(value);
        //    }
        //}
        
    }

    public class EmailTemplateController : BaseController
    {
        public EmailTemplateController()
        {
            this.EntitySetName = "tblEmailTemplates";
        }

        public bool Save(tblEmailTemplate record)
        {
            if (record.ID == 0)
            {
                return this.AddEntity(record);
            }
            else
            {
                return this.UpdateEntity(record);
            }
        }

        public bool Delete(tblEmailTemplate record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblEmailTemplate Find(long id)
        {
            tblEmailTemplate rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblEmailTemplates.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblEmailTemplate> FetchAll()
        {
            List<tblEmailTemplate> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblEmailTemplates.ToList();
            }

            return rec;
        }

        public List<tblEmailTemplate> FetchAllByViewType(tblViewController.ViewTypes type)
        {
            List<tblEmailTemplate> rec = null;

            tblViewController vc = new tblViewController();
            tblView vw = vc.Find(type);

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblEmailTemplates.Where(x => x.ViewID== vw.ID).ToList<tblEmailTemplate>();
            }

            return rec;
        }

        public List<tblEmailTemplate> FetchView()
        {
            List<tblEmailTemplate> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
               
                var query = from t in context.tblEmailTemplates
                            join cr in context.Users on t.CreatedBy equals cr.ID into tblCr from subCr in tblCr.DefaultIfEmpty()
                            join up in context.Users on t.UpdatedBy equals up.ID into tblUp from subUp in tblUp.DefaultIfEmpty()
                            
                            select new
                            {
                                ID = t.ID,
                                Name = t.Name,
                                Body = t.Body,
                                CreatedAT = t.CreatedAT,
                                CreatedBy = t.CreatedBy,
                                UpdatedAt = t.UpdatedAt,
                                UpdatedBy = t.UpdatedBy,

                                UpdatedByName = (subUp != null)? subUp.UserName: string.Empty,
                                CreatedByName = (subCr != null) ? subCr.UserName : string.Empty,
                            };

                rec = query.ToList().Select(t => new tblEmailTemplate
                {
                    ID = t.ID,
                    Name = t.Name,
                    Body = t.Body,
                    CreatedAT = t.CreatedAT,
                    CreatedBy = t.CreatedBy,
                    UpdatedAt = t.UpdatedAt,
                    UpdatedBy = t.UpdatedBy,
                    CreatedByName = t.CreatedByName,
                    UpdatedByName =  t.UpdatedByName,

                }).ToList();
            }
            return rec;
        }

        
    }

}
