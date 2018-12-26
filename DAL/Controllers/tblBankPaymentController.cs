using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class tblBankPaymentController : BaseController
    {
        public tblBankPaymentController()
        {
            this.EntitySetName = "tblBankPayments";
        }

        public bool Save(tblBankPayment record)
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

        public bool Delete(tblBankPayment record)
        {
            return this.DeleteEntity(record);
        }

        public bool Delete(long id)
        {
            return this.DeleteEntity(Find(id));
        }

        public tblBankPayment Find(long id)
        {
            tblBankPayment rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankPayments.FirstOrDefault(x => x.ID == id);
            }
            return rec;
        }

        public List<tblBankPayment> FetchAll()
        {
            List<tblBankPayment> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankPayments.ToList();
            }
            return rec;
        }

        public List<tblBankPayment> FetchAllByDocumentItemID(long docItemID)
        {
            List<tblBankPayment> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankPayments.Where(x => x.DocumentItemID == docItemID).ToList<tblBankPayment>();
            }
            return rec;
        }

        public List<tblBankPayment> FetchNonZeroVat(long docItemID, int zeroVatRateID)
        {
            List<tblBankPayment> rec = null;
            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                rec = context.tblBankPayments.Where(x => x.DocumentItemID == docItemID && x.VATRateID != zeroVatRateID).ToList<tblBankPayment>();
            }
            return rec;
        }
    }
}
