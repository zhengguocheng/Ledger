using System;
using System.Linq;
using System.Windows.Forms;

using System.IO;
using DMS;

namespace WordControls
{
    public partial class DocBrowser : UserControlBase
    {
        private System.Windows.Forms.WebBrowser webBrowser1;

        delegate void ConvertDocumentDelegate(string fileName);

        public DocBrowser()
        {
            InitializeComponent();
            this.Caption = "";
            // Create the webBrowser control on the UserControl. 
            // This code was moved from the designer for cut and paste
            // ease. 
            webBrowser1 = new System.Windows.Forms.WebBrowser();

            webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            webBrowser1.Location = new System.Drawing.Point(0, 0);
            webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            webBrowser1.Name = "webBrowser1";
            //webBrowser1.Size = new System.Drawing.Size(532, 514);
            webBrowser1.TabIndex = 0;

            Controls.Add(webBrowser1);

            // set up an event handler to delete our temp file when we're done with it. 
        }

        string tempFileName = null;

        private void LoadDocument(string fileName)
        {
            webBrowser1.DocumentCompleted += webBrowser1_DocumentCompleted;

            // Call ConvertDocument asynchronously. 
            ConvertDocumentDelegate del = new ConvertDocumentDelegate(ConvertDocument);

            // Call DocumentConversionComplete when the method has completed. 
            del.BeginInvoke(fileName, DocumentConversionComplete, null);
        }

        void ConvertDocument(string fileName)
        {
            //object m = System.Reflection.Missing.Value;
            //object oldFileName = (object)fileName;
            //object readOnly = (object)false;
            //Microsoft.Office.Interop.Word.Application ac = null;

            //try
            //{
            //    // First, create a new Microsoft.Office.Interop.Word.ApplicationClass.
            //    ac = new Microsoft.Office.Interop.Word.Application();

            //    // Now we open the document.
            //    Document doc = ac.Documents.Open(ref oldFileName, ref m, ref readOnly,
            //        ref m, ref m, ref m, ref m, ref m, ref m, ref m,
            //         ref m, ref m, ref m, ref m, ref m, ref m);

            //    // Create a temp file to save the HTML file to. 
            //    tempFileName = GetTempFile("html");

            //    // Cast these items to object.  The methods we're calling 
            //    // only take object types in their method parameters. 
            //    object newFileName = (object)tempFileName;

            //    // We will be saving this file as HTML format. 
            //    object fileType = (object)WdSaveFormat.wdFormatHTML;

            //    // Save the file. 
            //    doc.SaveAs(ref newFileName, ref fileType,
            //        ref m, ref m, ref m, ref m, ref m, ref m, ref m,
            //        ref m, ref m, ref m, ref m, ref m, ref m, ref m);

            //}
            //finally
            //{
            //    // Make sure we close the application class. 
            //    if (ac != null)
            //        ac.Quit(ref readOnly, ref m, ref m);
            //}
        }

        void DocumentConversionComplete(IAsyncResult result)
        {
            // navigate to our temp file. 
            webBrowser1.Navigate(tempFileName);
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                if (tempFileName != string.Empty)
                {
                    // delete the temp file we created. 
                    File.Delete(tempFileName);

                    // set the tempFileName to an empty string. 
                    tempFileName = string.Empty;
                }
            }
            catch(Exception ecp) 
            {
                GlobalLogger.logger.LogException(ecp);
            }
            finally 
            {
                webBrowser1.DocumentCompleted -= webBrowser1_DocumentCompleted;
            }
        }

        string GetTempFile(string extension)
        {
            // Uses the Combine, GetTempPath, ChangeExtension, 
            // and GetRandomFile methods of Path to 
            // create a temp file of the extension we're looking for. 
            return Path.Combine(Path.GetTempPath(),
                Path.ChangeExtension(Path.GetRandomFileName(), extension));
        }

        public void Preview(string fileName)
        {
            if (IsValidFile(fileName))
            {
                string ext = Path.GetExtension(fileName);
              
                if (string.Compare(ext, extDoc, true) == 0)
                {
                    LoadDocument(fileName);
                }
                else
                {
                    LoadFile(fileName);
                }
            }
            else
            {
                throw new Exception("File of this type cannot be previewd.");
            }
            
        }

        string extPDF = ".pdf";
        string extXlx = ".xls";
        string extXlsx = ".xlsx";
        string extTxt = ".txt";
        string extDoc = ".docx";
        string extPng = ".png";
        string extJPG = ".jpg";
        string extJpeg = ".jpeg";
        string extBmp = ".bmp";
        string extHTML = ".html";

        public bool IsValidFile(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            if (string.Compare(ext, extPDF, true) == 0 || string.Compare(ext, extXlx, true) == 0 ||
                string.Compare(ext, extXlsx, true) == 0 || string.Compare(ext, extTxt, true) == 0 || 
                string.Compare(ext, extDoc, true) == 0  || string.Compare(ext, extPng, true) == 0 ||
                string.Compare(ext, extJPG, true) == 0 || string.Compare(ext, extJpeg, true) == 0 ||
                string.Compare(ext, extBmp, true) == 0 || string.Compare(ext, extHTML, true) == 0) 
            {
                return true;
            }

            return false;
        }


        private void LoadFile(string fileName)
        {
            //for excel http://stackoverflow.com/questions/15381922/c-sharp-when-opening-a-xls-file-in-the-web-browser-it-opens-an-instance-of-exce
            //webBrowser1.Navigate(new Uri("file:///" + @"d:\t.xlsx"), false);
            
            Uri uri = new Uri("file:///" + fileName);
            webBrowser1.Navigate(uri, false);
        }

        public void DisposeWebBrowserControl()
        {
            if (webBrowser1 != null)
                webBrowser1.Dispose();
        }

        public void ShowBorder(bool val)
        {
            this.BorderStyle =  val? System.Windows.Forms.BorderStyle.FixedSingle: System.Windows.Forms.BorderStyle.None;
        }
    }
}