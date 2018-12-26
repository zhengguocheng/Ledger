using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Novacode;
using System.Xml.Linq;

namespace Utilities.MS_Word
{
    public class MSWordHelper
    {
        public static bool RepalceText(string filePath, Dictionary<string, string> dicVales, string newFilePath = null)
        {
            using (DocX document = DocX.Load(filePath))
            {
                foreach (string key in dicVales.Keys)
                {
                    document.ReplaceText(key, dicVales[key]);
                }

                if (string.IsNullOrWhiteSpace(newFilePath))
                {
                    document.Save();
                }
                else
                {
                    document.SaveAs(newFilePath);
                }
                return true;
            }
        }

        public static bool Open(string filePath)
        {
            //using (DocX document = DocX.Load(filePath))
            //{
            //    XElement doc = document.Xml;
            //}
            return true;
        }

        void Wordhelper()
        {
            //this method is used to calculate 
        }
    }
}
