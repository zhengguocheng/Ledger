using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class VwDocumentList_Controller: BaseController
    {
        
        

        //public static void Testing()
        //{
        //    SelectEntity<MyObject>(r => r.MyObjectId == 1);
        //}

        //public static T SelectEntity<T>(Expression<Func<T, bool>> expression) where T : EntityObject
        //{
        //    MyContext db = new MyContext();
        //    return db.CreateObjectSet<T>().SingleOrDefault(expression);
        //}

        public VwDocumentList Find(long docID)
        {
            VwDocumentList rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.VwDocumentLists.FirstOrDefault(x => x.ID == docID);
            }
            return rec;
        }
    }
}
