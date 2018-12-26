using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DAL;
using System.IO;

using Utilities;
using System.Timers;

namespace DMS
{
    class ImportMatchedPDF
    {
        //static string ClientName = "ClientName".Trim().ToLower(), ClientNameVariations = "ClientNameVariations".Trim().ToLower(), TradingName = "TradingName".Trim().ToLower(), Refrence = "Refrence".Trim().ToLower(), NIN = "NIN".Trim().ToLower(), CompanyNumber = "CompanyNumber".Trim().ToLower(), UTR = "UTR".Trim().ToLower(), UTRVariation = "UTRVariation".Trim().ToLower(), VatRegNo = "VatRegNo".Trim().ToLower(), VatRegNoVariation = "VatRegNoVariation".Trim().ToLower();

        string FileName, FilePath;
        string ClientID, MactchedProperty;
        
        ClientController clntController;
        Repository repostry = new Repository(AppConstants.RecordType.Client);

        public ImportMatchedPDF(string fullPath)
        {
            clntController = new ClientController();
            FilePath = fullPath;
            Decrypt();
        }

        void Decrypt()
        {
            FileInfo file = new FileInfo(FilePath);
            
            string encName = Path.GetFileName(FilePath);

            ClientID = encName.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[0];
            
            MactchedProperty = encName.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[1];

            FileName = encName.Split("=".ToCharArray(), StringSplitOptions.RemoveEmptyEntries)[2];
        }

        string GetFolderName(string matchedPrp)
        {
            string fname = AppConstants.folPending;
            matchedPrp = matchedPrp.Trim().ToLower();

            if (matchedPrp == AppConstants.ClientName || matchedPrp == AppConstants.ClientNameVariations)
            {
                fname = AppConstants.folPending;
            }
            if (matchedPrp == AppConstants.TradingName)
            {
                fname = AppConstants.folPending;
            }
            if (matchedPrp == AppConstants.Refrence)
            {
                fname = AppConstants.folPending;
            }
            if (matchedPrp == AppConstants.NIN)
            {
                fname = AppConstants.folTax;
            }
            if (matchedPrp == AppConstants.UTR || matchedPrp == AppConstants.UTRVariation)
            {
                fname = AppConstants.folTax;
            }
            if (matchedPrp == AppConstants.CompanyNumber)
            {
                fname = AppConstants.folCompanies_House;             
            }
            if (matchedPrp == AppConstants.VatRegNo || matchedPrp == AppConstants.VatRegNoVariation)
            {
                fname = AppConstants.folVAT;
            }
            return fname;
        }

        public bool Import()
        {
            try
            {
                long id = Convert.ToInt64(ClientID);
                Client cl = clntController.Find(id);
                GlobalLogger.logger.LogMessage("Going to import file " + FileName);
                if (AddToRepository(FileName, FilePath, cl))
                {
                    GlobalLogger.logger.LogMessage("file imported.");
                    return true;
                }
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }
            return false;
        }

        //void AddToRepository(string newFileName, string path, Client clnt)
        //{
        //    tblDocumentItem fol = null;
        //    try
        //    {
        //        string folName = "Pending";
        //        fol = repostry.FindByName(folName, 0, clnt.ID);

        //        if (fol == null)
        //        {
        //            fol = new tblDocumentItem();
        //            fol.Name = folName;
        //            fol.Notes = "Folder contain matched PDF files.";
        //            fol.ParentID = 0;
        //            fol.ClientID = clnt.ID;
        //            repostry.AddFolder(fol);
        //        }
        //    }
        //    catch (Exception ecp)
        //    {
        //        GlobalLogger.logger.LogException(ecp);
        //    }

        //    if (fol != null && fol.ID > 0)
        //    {
        //        try
        //        {
        //            tblDocumentItem file = new tblDocumentItem();
        //            file.Name = newFileName;
        //            file.Notes = "Matched PDF.";
        //            file.ParentID = fol.ID;
        //            file.ClientID = clnt.ID;
        //            file.TempByteData = FileHelper.GetByteArray(path);

        //            repostry.AddFile(file);
        //        }
        //        catch (Exception ecp)
        //        {
        //            GlobalLogger.logger.LogException(ecp);
        //        }
        //    }

        //}

        bool AddToRepository(string newFileName, string path, Client clnt)
        {
            tblDocumentItem fol = null;
            try
            {
                string folName = GetFolderName(MactchedProperty);
                fol = repostry.FindByName(folName, clnt.ID);

                //create folder
                if (fol == null)
                {
                    fol = new tblDocumentItem();
                    fol.Name = folName;
                    
                    fol.ParentID = repostry.FindByName(clnt.Client_Name, clnt.ID).ID;//get client's parent folder
                    fol.RecordID = clnt.ID;
                    repostry.AddFolder(fol);
                }
            }
            catch (Exception ecp)
            {
                GlobalLogger.logger.LogException(ecp);
            }

            //create document
            if (fol != null && fol.ID > 0)
            {
                try
                {
                    tblDocumentItem file = new tblDocumentItem();
                    file.Name = newFileName;
                    file.Notes = "Matched PDF.";
                    file.ParentID = fol.ID;
                    file.RecordID = clnt.ID;
                    file.AddTag(Tags.TagType.Star);//Automatically star added document.
                    file.AddTag(Tags.TagType.PDFMatched);//Mark PDFMatched
                    file.TempByteData = FileHelper.GetByteArray(path);

                    repostry.AddFile(file);
                    return true;
                }
                catch (Exception ecp)
                {
                    GlobalLogger.logger.LogException(ecp);
                }
            }

            return false;
        }
    }

   
}
