using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Data;
using Microsoft.VisualBasic.FileIO;
using OfficeOpenXml;

namespace Utilities.Excel
{
    public class ExcelReader
    {
        public DataTable GetDataTableCSV(string strFilePath)
        {
            return test(strFilePath);

            //DataTable dt = new DataTable();
            //using (StreamReader sr = new StreamReader(strFilePath))
            //{
            //    string[] headers = sr.ReadLine().Split(',');
            //    foreach (string header in headers)
            //    {
            //        var colName = header;
            //        while (colName.Length < 3)
            //        {
            //            colName = "0" + colName;
            //        }
            //        dt.Columns.Add(colName);
            //    }
            //    while (!sr.EndOfStream)
            //    {
            //        string[] rows = sr.ReadLine().Split(',');
            //        DataRow dr = dt.NewRow();
            //        for (int i = 0; i < headers.Length; i++)
            //        {
            //            dr[i] = rows[i];
            //        }
            //        dt.Rows.Add(dr);
            //    }
            //}

            //return dt;
        }

        DataTable test(string fileName)
        {
            DataTable dt = new DataTable();

            TextFieldParser parser = new TextFieldParser(fileName);

            parser.HasFieldsEnclosedInQuotes = true;
            parser.SetDelimiters(",");
            
            string[] arrFields;

            while (!parser.EndOfData)
            {
                long lineNo = parser.LineNumber;
                arrFields = parser.ReadFields();

                if (lineNo == 1)
                {
                    foreach (string field in arrFields)
                    {
                        var colName = field;
                        while (colName.Length < 3)
                        {
                            colName = "0" + colName;
                        }
                        dt.Columns.Add(colName);
                    }
                }
                else
                {
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < arrFields.Length; i++)
                    {
                        dr[i] = arrFields[i];
                    }
                    dt.Rows.Add(dr);
                }

            }

            parser.Close();
            return dt;
        }

        public DataTable GetDataTable(string filePath,int sheetNo)
        {
            FileInfo newFile = new FileInfo(filePath);//new FileInfo(Server.MapPath("~/") + "test1.xlsx");
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // Openning first Worksheet of the template file i.e. 'Sample1.xlsx'
                ExcelWorksheet oSheet = package.Workbook.Worksheets[sheetNo];

                int totalRows = oSheet.Dimension.End.Row;
                int totalCols = oSheet.Dimension.End.Column;
                DataTable dt = new DataTable(oSheet.Name);
                DataRow dr = null;
                for (int i = 1; i <= totalRows; i++)
                {
                    if (i > 1) dr = dt.Rows.Add();
                    for (int j = 1; j <= totalCols; j++)
                    {
                        if (i == 1)
                            dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());//first rows must be columns
                        else if(oSheet.Cells[i, j].Value != null)
                            dr[j - 1] = oSheet.Cells[i, j].Value.ToString();
                    }
                }
                return dt;
            }
        }

        public DataTable WorksheetToDataTable(string filePath, int sheetNo = 1, int headerRow = 1)
        {
            FileInfo newFile = new FileInfo(filePath);//new FileInfo(Server.MapPath("~/") + "test1.xlsx");
            using (ExcelPackage package = new ExcelPackage(newFile))
            {
                // Openning first Worksheet of the template file i.e. 'Sample1.xlsx'
                ExcelWorksheet oSheet = package.Workbook.Worksheets[sheetNo];

                int totalRows = oSheet.Dimension.End.Row;
                int totalCols = oSheet.Dimension.End.Column;
                DataTable dt = new DataTable(oSheet.Name);
                DataRow dr = null;
                for (int i = headerRow; i <= totalRows; i++)
                {
                    if (i > headerRow) 
                        dr = dt.Rows.Add();
                    for (int j = 1; j <= totalCols; j++)
                    {
                        if (i == headerRow)
                        {
                            //if(oSheet.Cells[i, j].Value != null)
                                dt.Columns.Add(oSheet.Cells[i, j].Value.ToString());//first rows must be columns
                        }
                        else if (oSheet.Cells[i, j].Value != null)
                            dr[j - 1] = oSheet.Cells[i, j].Value.ToString();
                    }
                }
                return dt;
            }
        }
    }
}
