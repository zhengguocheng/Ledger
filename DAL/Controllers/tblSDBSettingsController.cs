using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblSDBSettingsController : BaseController
    {
        public tblSDBSettingsController()
        {
            this.EntitySetName = "tblSDBSettings";
        }

        public bool Save(tblSDBSetting record)
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

        public bool Delete(tblSDBSetting record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblSDBSetting Find(long id)
        {
            tblSDBSetting rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSDBSettings.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblSDBSetting> FetchAll()
        {
            List<tblSDBSetting> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSDBSettings.ToList();
            }
            return rec;
        }

        public List<tblSDBSetting> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblSDBSetting> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSDBSettings.Where(x => x.DocumentItemID == docItemID).ToList<tblSDBSetting>();
            }
            return rec;
        }

        public bool IsVATNull(tblSDBSetting settings)
        {
            return !(settings.VAT1.HasValue || settings.VAT2.HasValue || settings.VAT3.HasValue 
                || settings.VAT4.HasValue || settings.VAT5.HasValue || settings.VAT6.HasValue);
        }

    }
}
