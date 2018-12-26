//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using DAL;
//using PDFConvertLibrary;
//using System.IO;
//using System.Windows.Forms;
//using Utilities;

//namespace DMS
//{
//    public enum MatchProperty
//    {
//        ClientName, ClientNameVariations, TradingName, Refrence, NIN, CompanyNumber, UTR, UTRVariation, VatRegNo, VatRegNoVariation
//    }


//    public class MatchingClient
//    {
//        public string ClientID { set; get; }
//        public List<MatchProperty> ListMatchProperty { set; get; }
//        public int MatchStrength { set; get; }

//        public MatchingClient()
//        {
//            ListMatchProperty = new List<MatchProperty>();
//        }

        
//    }

//    public class PDFContent
//    {
//        public string FilePath { set; get; }
//        public string FileName { set; get; }
//        public string FileNameWithoutExt { set; get; }
//        public string Content { set; get; }
//        public string ImagePath { set; get; }
//        public List<MatchingClient> MatchingClients { set; get; }

//        public PDFContent(string pdfPath)
//        {
//            this.FilePath = pdfPath;
//            this.FileName = Path.GetFileName(pdfPath);
//            this.FileNameWithoutExt = Path.GetFileNameWithoutExtension(pdfPath);
//            this.ImagePath = Path.Combine(GetExtractedImageDirectory(), FileNameWithoutExt + "." + AppConstants.ExtractedImageFormat);//Path.Combine(Constants.ExtractedImageDirectory , FileNameWithoutExt +"."+ Constants.ExtractedImageFormat);
//            this.MatchingClients = new List<MatchingClient>();
//        }

//        public bool IsMatchFound()
//        {
//            return (MatchingClients.Count > 0);
//        }

//        public static string GetExtractedImageDirectory()
//        {
//            //string dr = @"D:\Projects\oDesk\PDF New\PDF Analyzer\PDF Analyzer\bin\Debug";
//            //return dr;
//            string strPath = Path.Combine(Directory.GetParent(Application.ExecutablePath).FullName, "ExtImage");
//            FileHelper.CreateDirectory(strPath);
//            return strPath;
//        }

//    }
//}
