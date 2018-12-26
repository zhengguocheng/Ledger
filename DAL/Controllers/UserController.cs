using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    
    public class UserController : BaseController
    {
        public UserController()
        {
            this.EntitySetName = "Users";
        }

        public bool Save(User record)
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

        public bool Delete(User record)
        {
            return DeleteEntity(record);
        }

        public User Find(long id)
        {
            User rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.Users.FirstOrDefault(x => x.ID == id);
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }

        public List<User> FetchAll()
        {
            List<User> rec = null;
            try
            {
                using (dbDMSEntities context = ContextCreater.GetContext())
                {
                    rec = context.Users.ToList();
                }
            }
            catch (Exception ecp)
            {
                throw ecp;
            }
            return rec;
        }
        
        public User Authenticate(string userName,string  password)
        {
            User rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.Users.FirstOrDefault(x => x.UserName.ToLower() == userName.ToLower() && x.Password == password);
            }
            return rec;
        }
    }

}
