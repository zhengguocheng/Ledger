using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class SettingsController : BaseController
    {
        List<tblSetting> cachedLst;

        public SettingsController()
        {
            this.EntitySetName = "tblSettings";
            cachedLst = FetchAll();
        }

        public bool Save(tblSetting record)
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

        public bool Delete(tblSetting record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblSetting Find(long id)
        {
            tblSetting rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSettings.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblSetting> FetchAll()
        {
            List<tblSetting> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSettings.ToList();
            }
            return rec;
        }

        public List<tblSetting> GetEnabledSettings()
        {
            List<tblSetting> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                var query = from t in context.tblSettings
                            where t.UseValue == true
                            select t;
                
                rec = query.ToList();
            }
            return rec;
        }

        public string GetSettings(string name)
        {
            if (cachedLst == null)
                cachedLst = FetchAll();

            string val = null;
            tblSetting itm = cachedLst.FirstOrDefault(x => string.Compare(x.Name.Trim(),name.Trim()) == 0);
            if (itm.UseValue && !string.IsNullOrEmpty(itm.Value))
                val = itm.Value.Trim();
            return val;
        }
    }

}
