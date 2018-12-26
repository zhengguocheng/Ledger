using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Data;

namespace DAL
{
    public class BaseController
    {
        dbDMSEntities context;

        public string EntitySetName = null;
        public string PrimaryKeyName = null;

        protected bool AddEntity(EntityObject record)
        {
            using (context = ContextCreater.GetContext())
            {
                context.AddObject(EntitySetName, record);
                context.SaveChanges();
            }
            return true;
        }

        protected bool DeleteEntity(EntityObject record)
        {
            using (context = ContextCreater.GetContext())
            {
                context.Attach(record);
                context.DeleteObject(record);
                context.SaveChanges();
            }
            return true;
        }

        protected bool UpdateEntity(EntityObject record)
        {
            EntityKey key;
            object originalItem;
            using (context = ContextCreater.GetContext())
            {
                key = context.CreateEntityKey(record.EntityKey.EntitySetName, record);

                if (context.TryGetObjectByKey(key, out originalItem))
                {
                    context.ApplyCurrentValues(key.EntitySetName, record);
                }

                context.SaveChanges();

                return true;
            }
        }

        protected bool BatchUpdate(IEnumerable<EntityObject> recordList)
        {
            using (context = ContextCreater.GetContext())
            {
                foreach (var record in recordList)
                {
                    EntityKey key = context.CreateEntityKey(record.EntityKey.EntitySetName, record);

                    object originalItem;

                    if (context.TryGetObjectByKey(key, out originalItem))
                    {
                        context.ApplyCurrentValues(key.EntitySetName, record);
                    }
                }

                context.SaveChanges();
            }

            return true;
        }

        public bool ExecuteSQL(string sqlStatement)
        {
            using (context = ContextCreater.GetContext())
            {
                context.ExecuteStoreCommand(sqlStatement);
            }
            return true;
        }
    }
}
