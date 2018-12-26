using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblVATRateController : BaseController
    {
        public tblVATRateController()
        {
            this.EntitySetName = "tblVATRates";
        }

        public bool Save(tblVATRate record)
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

        public bool Delete(tblVATRate record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblVATRate Find(long id)
        {
            tblVATRate rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblVATRates.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public tblVATRate FindVatRateZero()
        {
            tblVATRate rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblVATRates.FirstOrDefault(x => x.Percentage == 0);
            }
            return rec;
        }


        public List<tblVATRate> FetchAll()
        {
            List<tblVATRate> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblVATRates.ToList();
            }

            return rec;
        }

        public List<tblVATRate> FetchByYearEndID(long yrEndFolID)
        {
            List<tblVATRate> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblVATRates.Where(x => x.YearEndFolderID == yrEndFolID).ToList();
            }

            return rec;
        }
    }
}
