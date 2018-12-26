using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class ClientController: BaseController
    {
        public ClientController()
        {
            this.EntitySetName = "Clients";
        }

        public bool Save(Client record)
        {
            if (record.ID == 0)
            {
                return Add(record);
            }
            else
            {
                return Update(record);
            }
        }

        public bool Add(Client record)
        {
            if(!DuplicateRefrenceExist(record))
            {
                return this.AddEntity(record);
            }
            else
                return false;
        }

        public bool Update(Client record)
        {
            if (!DuplicateRefrenceExist(record))
            {
                return this.UpdateEntity(record);
            }
            else
                return false;
        }

        public bool DuplicateRefrenceExist(Client record)
        {
            List<Client> dupRefLst = Find(record.Reference);

            if (dupRefLst != null)
            {
                dupRefLst.RemoveAll(x => x.ID == record.ID);//remove current record from list
            }

            if (dupRefLst == null || dupRefLst.Count == 0)
            {
                return false;
            }
            else
            {
                string clientNames = string.Join(",", dupRefLst.Select(x => x.Client_Name).ToArray());
                string errorMsg = string.Format("The Reference you entered is already in use by {0}. Please enter unique Reference", clientNames);
                throw new Exception(errorMsg);
            }
            return true;
        }

        public bool Delete(Client record)
        {
            return this.AddEntity(record);
        }

        public Client Find(long id)
        {
            Client rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.Clients.FirstOrDefault(x => x.ID == id);
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<Client> Find(string Reference)
        {
            List<Client> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.Clients.Where(x => x.Reference == Reference).ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<Client> FetchAll(List<int> lstIDs)
        {
            List<Client> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    var qry = from clnt in context.Clients
                             where lstIDs.Contains(clnt.ID)
                             select clnt;

                    rec = qry.ToList<Client>();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<Client> FetchAll()
        {
            List<Client> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.Clients.ToList();
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
