using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Utilities;

namespace DAL
{
    public partial class tblDocumentTemplate : iEntityBase
    {
        public string UpdatedByName { get; set; }
        public string CreatedByName { get; set; }
        public byte[] TempByteData { get; set; }
    }

    public class DocumentTemplateController : BaseController, IDocUpdateSupport
    {
        public DocumentTemplateController()
        {
            this.EntitySetName = "tblDocumentTemplates";
        }

        public static string GetFilePath(tblDocumentTemplate record)
        {
            return Path.Combine(AppConstants.RpstFolderPath,AppConstants.WordTemplateDir, record.Name);
        }

        public bool Save(tblDocumentTemplate record)
        {
            if (record.Path == null)
                record.Path = string.Empty;
            if (!record.Name.EndsWith(AppConstants.WordDocExtention))
                record.Name += AppConstants.WordDocExtention;

            string path = GetFilePath(record);

            if (record.ID == 0)
            {
                Directory.CreateDirectory(AppConstants.WordTemplateDir);

                if(!string.IsNullOrWhiteSpace(FileHelper.WriteToFile(path, new byte[0])))
                {
                    return this.AddEntity(record);
                }
            }
            else
            {
                FileHelper.WriteToFile(path, record.TempByteData);
                return this.UpdateEntity(record);
            }
            return false;
        }

        public bool UpdateFileContent(long recordID, byte[] data)
        {
            tblDocumentTemplate itm = Find(recordID);
            itm.TempByteData = data;
            return Save(itm);
        }

        public byte[] GetFileContent(long id)
        {
            tblDocumentTemplate record = Find(id);

            return FileHelper.GetByteArray(GetFilePath(record));
        }

        public bool Delete(tblDocumentTemplate record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblDocumentTemplate Find(long id)
        {
            tblDocumentTemplate rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDocumentTemplates.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public tblDocumentTemplate FindByName(string name)
        {
            tblDocumentTemplate rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDocumentTemplates.FirstOrDefault(x => x.Name == name);
            }
            return rec;
        }

        public List<tblDocumentTemplate> FetchAll()
        {
            List<tblDocumentTemplate> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDocumentTemplates.ToList();
            }
            return rec;
        }

        public List<tblDocumentTemplate> FetchAllByViewType(tblViewController.ViewTypes type)
        {
            List<tblDocumentTemplate> rec = null;

            tblViewController vc = new tblViewController();
            tblView vw = vc.Find(type);

            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblDocumentTemplates.Where(x => x.ViewID== vw.ID).ToList<tblDocumentTemplate>();
            }

            return rec;
        }

        public List<tblDocumentTemplate> FetchView()
        {
            List<tblDocumentTemplate> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                var query = from t in context.tblDocumentTemplates
                            join cr in context.Users on t.CreatedBy equals cr.ID into tblCr from subCr in tblCr.DefaultIfEmpty()
                            join up in context.Users on t.UpdatedBy equals up.ID into tblUp from subUp in tblUp.DefaultIfEmpty()
                            
                            select new
                            {
                                ID = t.ID,
                                Name = t.Name,
                                Path = t.Path,
                                CreatedAT = t.CreatedAT,
                                CreatedBy = t.CreatedBy,
                                UpdatedAt = t.UpdatedAt,
                                UpdatedBy = t.UpdatedBy,

                                UpdatedByName = (subUp != null)? subUp.UserName: string.Empty,
                                CreatedByName = (subCr != null) ? subCr.UserName : string.Empty,
                            };

                rec = query.ToList().Select(t => new tblDocumentTemplate
                {
                    ID = t.ID,
                    Name = t.Name,
                    Path = t.Path,
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
