using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class GenericListType
    {
        public enum TypeEnum
        {
            AccountGroup = 1
        }

        public static string GetName(TypeEnum type)
        {
            string name = string.Empty;
            switch (type)
            {
                case GenericListType.TypeEnum.AccountGroup:
                    name = "Account Group";
                    break;
            }
            return name;
        }

    }

    public class tblGenericListController : BaseController
    {
        public tblGenericListController()
        {
            this.EntitySetName = "tblGenericLists";
        }

        public bool Save(tblGenericList record)
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

        public bool Delete(tblGenericList record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblGenericList Find(long id)
        {
            tblGenericList rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGenericLists.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblGenericList> FetchAll()
        {
            List<tblGenericList> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGenericLists.ToList();
            }

            return rec;
        }

        private List<tblGenericList> FetchByTypeID(int TypeID)//set method private so prohibit using this method directly
        {
            List<tblGenericList> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblGenericLists.Where(x => x.TypeID == TypeID).ToList();
            }
            return rec;
        }

        public List<tblGenericList> FetchByType(GenericListType.TypeEnum type)
        {
            int typeID = (int)type;
            return FetchByTypeID(typeID);
        }

        public bool ItemExist(GenericListType.TypeEnum type, string itemName)
        {
            int typeID = (int)type;
            List<tblGenericList> lst = FetchByTypeID(typeID);
            return lst.Any(x => string.Compare(x.Name,itemName,true) == 0);
        }

        public tblGenericList SearchItem(GenericListType.TypeEnum type, string itemName)
        {
            int typeID = (int)type;
            List<tblGenericList> lst = FetchByTypeID(typeID);
            tblGenericList itm = lst.FirstOrDefault(x => string.Compare(x.Name, itemName, true) == 0);
            return itm;
        }

    }
}
