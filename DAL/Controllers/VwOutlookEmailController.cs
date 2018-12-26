using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class VwOutlookEmailController: BaseController
    {
        public List<VwOutlookEmail> FetchAll()
        {
            List<VwOutlookEmail> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.VwOutlookEmails.ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }
    }
}
