using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{ 
    public class tblAnalysisCodeController : BaseController
    {
        public tblAnalysisCodeController()
        {
            this.EntitySetName = "tblAnalysisCodes";
        }

        public bool Save(tblAnalysisCode record)
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

        public bool Delete(tblAnalysisCode record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblAnalysisCode Find(long id)
        {
            tblAnalysisCode rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAnalysisCodes.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblAnalysisCode> FetchAll()
        {
            List<tblAnalysisCode> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAnalysisCodes.ToList();
            }

            return rec;
        }

        public List<tblAnalysisCode> FetchByYearEndID(long yrEndFolID)
        {
            List<tblAnalysisCode> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblAnalysisCodes.Where(x => x.YearEndFolderID == yrEndFolID).ToList();
            }

            return rec;
        }

    }
}
