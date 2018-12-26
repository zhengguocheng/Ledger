using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{ 
    public class tblSupplierController : BaseController
    {
        public tblSupplierController()
        {
            this.EntitySetName = "tblSuppliers";
        }

        public bool Save(tblSupplier record)
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

        public bool Delete(tblSupplier record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblSupplier Find(long id)
        {
            tblSupplier rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSuppliers.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public tblSupplier Find(string name)
        {
            tblSupplier rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSuppliers.FirstOrDefault(x => x.Name == name.Trim());
            }
            return rec;
        }

        public List<tblSupplier> FetchAll()
        {
            List<tblSupplier> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblSuppliers.ToList();
            }

            return rec;
        }

        public List<VwSupplier> FetchView()
        {
            List<VwSupplier> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.VwSuppliers.Where(x => x.ID > 0).ToList();
            }

            return rec;
        }

        public void Add_If_NotExist(tblExcelSheet exclRecord)
        {
            var supName = exclRecord.Description;
            if(!string.IsNullOrWhiteSpace(supName))
            {
                supName = supName.Trim();
                var s = Find(exclRecord.Description);
                if (s == null)
                {
                    var sup = new tblSupplier();
                    sup.Name = supName;

                    if(exclRecord.NominalCode.HasValue)
                    {
                        //set Nominl Code
                        tblChartAccountController c = new tblChartAccountController();
                        var pNom = c.FetchParentNomCode(exclRecord.NominalCode.Value);
                        if(pNom != null)
                        {
                            sup.NominalCodeID = pNom.ID;
                        }
                    }
                    
                    Save(sup);
                }
            }
            
        }

    }
}
