using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DefaultFolders
    {

        string[] folderNames = { AppConstants.folAccounts,AppConstants.folCompliant,AppConstants.folTax,
                                   AppConstants.folVAT,AppConstants.folPAYE,AppConstants.folCompanies_House,
                                   AppConstants.folSundries,AppConstants.folPending, AppConstants.folNotes };

        public bool CreateFolders(int clientID, string[] arrfolNames, string parentName, AppConstants.RecordType type)
        {

            if (type == AppConstants.RecordType.Client || type == AppConstants.RecordType.Ledger)
            {
                Repository rep = new Repository(type);

                List<tblDocumentItem> lst = rep.FetchRootNode(clientID);

                if (lst != null && lst.Count > 0)//Root node allready exist;
                {
                    return false;
                }

                //create node with client name
                tblDocumentItem p = new tblDocumentItem();
                p.ParentID = 0;
                p.RecordID = clientID;
                p.Name = parentName;
                rep.AddFolder(p);

                foreach (string fname in arrfolNames)
                {
                    tblDocumentItem f = new tblDocumentItem();
                    f.ParentID = p.ID;
                    f.RecordID = clientID;
                    f.Name = fname;
                    rep.AddFolder(f);
                }
            }

            return true;
        }

        public bool CreateDefaultFolders(int clientID, string parentName, AppConstants.RecordType type)
        {
            return CreateFolders(clientID, folderNames, parentName, type);
        }

        //public bool CreateEndYearFolders(int clientID, DateTime stDate, DateTime endDate)
        //{
            //List<string> lstYearEnd = new List<string>();
            //DateTime date = stDate;

            //while (date <= endDate)
            //{
            //    lstYearEnd.Add(date.ToString("dd MMM yyyy"));
            //    date = DateHelper.AddMonthNew(date, 12);
            //}

            //Repository rep = new Repository(AppConstants.RecordType.Ledger);

            //tblDocumentItem rootNode = rep.FetchRootNode(clientID)[0];
            //if (rep.FetchChildren(rootNode).Count == 0)//if there are no yearend folders then create them
            //{
            //    foreach (string fname in lstYearEnd)
            //    {
            //        tblDocumentItem f = new tblDocumentItem();
            //        f.ParentID = rootNode.ID;
            //        f.RecordID = clientID;
            //        f.Name = fname;
            //        f.AddTag(Tags.TagType.YearEndFolder);
            //        if (rep.AddFolder(f))
            //        {
            //            string[] ledgerfolderNames = { AppConstants.LedgerFolder.Payment, AppConstants.LedgerFolder.Receipts, 
            //                                             AppConstants.LedgerFolder.Transfer, AppConstants.folReconcile, AppConstants.folSTB };
            //            foreach (var item in ledgerfolderNames)
            //            {
            //                tblDocumentItem d = new tblDocumentItem();
            //                d.ParentID = f.ID;
            //                d.RecordID = clientID;
            //                d.Name = item;
            //                rep.AddFolder(d);
            //            }
            //        }
            //    }
            //}
            //return true;
        //}

        //public bool CreateEndYearFolders(int clientID)
        //{

        //    DeadlineTracking itm = DMSQueries.Find_DeadlineTracking(clientID);
        //    if (itm.AccountingYearEndDate.HasValue)
        //    {
        //        List<string> lstYearEnd = new List<string>();
        //        DateTime date = itm.AccountingYearEndDate.Value;

        //        while (date.Year <= DateTime.Now.Year)
        //        {
        //            lstYearEnd.Add(date.ToString("dd MMM yyyy"));
        //            date = DateHelper.AddMonthNew(date, 12);
        //        }

        //        Repository rep = new Repository(AppConstants.RecordType.Ledger);

        //        tblDocumentItem rootNode = rep.FetchRootNode(clientID)[0];
        //        if (rep.FetchChildren(rootNode).Count == 0)//if there are no yearend folders then create them
        //        {
        //            foreach (string fname in lstYearEnd)
        //            {
        //                tblDocumentItem f = new tblDocumentItem();
        //                f.ParentID = rootNode.ID;
        //                f.RecordID = clientID;
        //                f.Name = fname;
        //                rep.AddFolder(f);
        //            }
        //        }
        //        return true;
        //    }
        //    return false;
        //}


        public bool CreateAllClientFolders()
        {
            ClientController con = new ClientController();
            List<Client> lst = con.FetchAll();
            
            foreach (Client cl in lst)
            {
                CreateDefaultFolders(cl.ID, (cl.Reference + " - " + cl.Client_Name), AppConstants.RecordType.Client);
            }

            return true;
        }

        public List<string> ModifyFolderNames()
        {
            List<string> lstError = new List<string>();

            Repository rep = new Repository(AppConstants.RecordType.Client);

            ClientController con = new ClientController();
            List<Client> lst = con.FetchAll();

            foreach (Client cl in lst)
            {
                try
                {
                    List<tblDocumentItem> lstRootNode = rep.FetchRootNode(cl.ID);
                    if (lstRootNode != null && lstRootNode.Count > 0)
                    {
                        tblDocumentItem p = lstRootNode[0];
                        
                        if (!cl.Client_Name.StartsWith(cl.Reference))
                        {
                            p.Name = cl.Reference + " - " + cl.Client_Name;
                            p.EncryptedName = p.Name;
                            rep.UpdateFolder(p);
                        }
                    }
                }
                catch (Exception ecp)
                {
                    lstError.Add(ecp.Message);
                }
            }
          
            return lstError;
        }
    }
}
