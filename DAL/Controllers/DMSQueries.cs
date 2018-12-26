using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data;

namespace DAL
{
    public class DMSQueries : BaseController
    {
        public static DeadlineTracking Find_DeadlineTracking(long clientID)
        {
            DeadlineTracking rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.DeadlineTrackings.FirstOrDefault(x => x.ClientID == clientID);
            }
            return rec;
        }
    }

    
}
