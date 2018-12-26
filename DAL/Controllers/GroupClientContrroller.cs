using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GroupClientController : BaseController
    {
        public GroupClientController()
        {
            this.EntitySetName = "tblGroupClients";
        }

        public bool Insert(int clientID, int groupID)
        {
            if (!Exist(clientID, groupID))
            {
                tblGroupClient obj = new tblGroupClient();
                obj.ClientID = clientID;
                obj.GroupID = groupID;
                return this.AddEntity(obj);
            }
            return false;
        }

        public bool Save(tblGroupClient record)
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

        public bool Delete(tblGroupClient record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public bool Delete(long clientID, long groupID)
        {
            string qry = string.Format("DELETE FROM tblGroupClient WHERE ClientID  = {0} and GroupID = {1};", clientID, groupID); 
            return ExecuteSQL(qry);
        }

        
        public tblGroupClient Find(long id)
        {
            tblGroupClient rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroupClients.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public bool Exist(int ClientId, int GroupID)
        {
            tblGroupClient rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroupClients.FirstOrDefault(x => x.GroupID == GroupID && x.ClientID == ClientId);
            }
            return (rec != null && rec.ID > 0);
        }

        public List<tblGroupClient> FetchAll()
        {
            List<tblGroupClient> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroupClients.ToList();
            }

            return rec;
        }

        public List<tblGroupClient> FetchByGroupID(int grpID)
        {
            List<tblGroupClient> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGroupClients.Where(x => x.GroupID == grpID).ToList<tblGroupClient>();
            }

            return rec;
        }
    }

}
