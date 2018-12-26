using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using Utilities.MS_Word;
using Utilities;

namespace DAL
{
    public class DocMerge
    {
        public static Dictionary<string, string> GetTokenDictionary(tblDocumentTemplate template, long clientID)
        {
            
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (template == null || template.ID == 0)
                return dic;



            using (dbDMSEntities context = ContextCreater.GetContext())
            {
                if (template != null)
                {
                    //string viewName = template.tblView.ViewName;

                    context.tblDocumentTemplates.Attach(template);

                    SqlCommand cmd = new SqlCommand(@"select * from dbo.VwClientTemplate where ID = @clientID;");
                    cmd.Parameters.AddWithValue("clientID", clientID);

                    DataSet ds = ContextCreater.ExecuteDataSet(cmd);

                    if (!ds.IsNullOrEmpty())
                    {
                        DataRow dr = ds.Tables[0].Rows[0];

                        List<tblToken> tokenLst = template.tblView.tblTokens.ToList<tblToken>();
                        foreach (tblToken token in tokenLst)
                        {
                            dic.Add(token.TokenName, dr[token.TokenColumn].ToString());
                        }
                    }
                }
            }
            
            return dic;
        }

        public static byte[] ExtractDocument(tblDocumentTemplate template, long clientID)
        {
            byte[] byteData = null;

            string path = DocumentTemplateController.GetFilePath(template);
            string newFilePath = Path.Combine(AppConstants.TempDirectory, DateTime.Now.Ticks.ToString() + AppConstants.WordDocExtention);

            Dictionary<string, string> dic = GetTokenDictionary(template, clientID);


            if (MSWordHelper.RepalceText(path, dic, newFilePath))
            {
                try
                {
                    byteData = FileHelper.GetByteArray(newFilePath);
                }
                finally
                {
                    File.Delete(newFilePath);
                }
            }

            return byteData;
        }
    }
}
