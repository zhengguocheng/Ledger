using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
   
   

    public class tblViewController : BaseController
    {
        public enum ViewTypes
        {
            Client,Users
        }

        const string ClientViewName = "VwClientTemplate";
        const string UsersViewName = "VwUser";

        public tblViewController()
        {
            this.EntitySetName = "tblViews";
        }

        public string GetViewName(ViewTypes type)
        {
            switch (type)
        	{
		        case ViewTypes.Client:
                    return ClientViewName;
                    break;
                case ViewTypes.Users:
                    return UsersViewName;
                    break;
                default:
                    return string.Empty;;
                    break;
	            
            }
        }

        public bool Save(tblView record)
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

        public bool Delete(tblView record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblView Find(long id)
        {
            tblView rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblViews.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblView> FetchAll()
        {
            List<tblView> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblViews.ToList();
            }

            return rec;
        }

        public tblView Find(ViewTypes type)
        {
            tblView rec = null;

            string viewName = GetViewName(type).ToLower().Trim();
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblViews.FirstOrDefault(x => x.ViewName.ToLower().Trim() == viewName);
            }
            return rec;
        }

    }

}
