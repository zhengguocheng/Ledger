using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using OfficeOpenXml;
using System.Data;
using OfficeOpenXml.Style;
using System.IO;

namespace Utilities.Excel
{
    public class ExcelExport
    {
     
        public static bool Export(DataSet ds, string outputFilePath)
        {
            using (ExcelPackage p = new ExcelPackage())
            {
                //Here setting some document properties
                p.Workbook.Properties.Author = "Shahbaz Khurram";
                p.Workbook.Properties.Title = "Office Open XML Sample";

                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    var dt = ds.Tables[i];

                    //Create a sheet

                    var sheetName = "Sheet" + (i + 1).ToString();
                    p.Workbook.Worksheets.Add(sheetName);

                    ExcelWorksheet ws = p.Workbook.Worksheets[sheetName];
                    ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
                    ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet


                    #region Add Headings to Excel Sheet

                    int colIndex = 1;
                    int rowIndex = 1;

                    foreach (DataColumn dc in dt.Columns) //Creating Headings
                    {
                        var cell = ws.Cells[rowIndex, colIndex];

                        //Setting the background color of header cells to Gray
                        var fill = cell.Style.Fill;
                        fill.PatternType = ExcelFillStyle.Solid;
                        fill.BackgroundColor.SetColor(Color.LightBlue);

                        //Setting Top/left,right/bottom borders.
                        var border = cell.Style.Border;
                        border.Bottom.Style =
                            border.Top.Style =
                            border.Left.Style =
                            border.Right.Style = ExcelBorderStyle.Thin;

                        //Setting Value in cell
                        cell.Value = dc.Caption;

                        colIndex++;
                    }

                    #endregion

                    #region Add Data to Excel Sheet

                    foreach (DataRow dr in dt.Rows) // Adding Data into rows
                    {
                        colIndex = 1;
                        rowIndex++;
                        foreach (DataColumn dc in dt.Columns)
                        {
                            var cell = ws.Cells[rowIndex, colIndex];
                            //Setting Value in cell
                            cell.Value = dr[dc.ColumnName];

                            //Setting borders of cell
                            var border = cell.Style.Border;
                            border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
                            colIndex++;
                        }
                    }

                    #endregion

                }


                #region Save Excel to file


                //Create output file
                Byte[] bin = p.GetAsByteArray();
                string file = outputFilePath;
                try
                {
                    File.Delete(file);
                }
                catch { }
                File.WriteAllBytes(file, bin);

                #endregion

                return true;
            }
        }

        //public static bool ExportFirstDataTable(DataSet ds, string outputFilePath)
        //{
        //    using (ExcelPackage p = new ExcelPackage())
        //    {
        //        //Here setting some document properties
        //        p.Workbook.Properties.Author = "Shahbaz Khurram";
        //        p.Workbook.Properties.Title = "Office Open XML Sample";

        //        //Create a sheet
        //        p.Workbook.Worksheets.Add("Export");
        //        ExcelWorksheet ws = p.Workbook.Worksheets[1];
        //        ws.Name = "Export From Tracking System"; //Setting Sheet's name
        //        ws.Cells.Style.Font.Size = 11; //Default font size for whole sheet
        //        ws.Cells.Style.Font.Name = "Calibri"; //Default Font name for whole sheet

        //        DataTable dt = ds.Tables[0];

        //        #region Add Headings to Excel Sheet

        //        int colIndex = 1;
        //        int rowIndex = 1;

        //        foreach (DataColumn dc in dt.Columns) //Creating Headings
        //        {
        //            var cell = ws.Cells[rowIndex, colIndex];

        //            //Setting the background color of header cells to Gray
        //            var fill = cell.Style.Fill;
        //            fill.PatternType = ExcelFillStyle.Solid;
        //            fill.BackgroundColor.SetColor(Color.Gray);

        //            //Setting Top/left,right/bottom borders.
        //            var border = cell.Style.Border;
        //            border.Bottom.Style =
        //                border.Top.Style =
        //                border.Left.Style =
        //                border.Right.Style = ExcelBorderStyle.Thin;

        //            //Setting Value in cell
        //            cell.Value = dc.Caption;

        //            colIndex++;
        //        }

        //        #endregion

        //        #region Add Data to Excel Sheet

        //        foreach (DataRow dr in dt.Rows) // Adding Data into rows
        //        {
        //            colIndex = 1;
        //            rowIndex++;
        //            foreach (DataColumn dc in dt.Columns)
        //            {
        //                var cell = ws.Cells[rowIndex, colIndex];
        //                //Setting Value in cell
        //                cell.Value = dr[dc.ColumnName];

        //                //Setting borders of cell
        //                var border = cell.Style.Border;
        //                border.Left.Style = border.Right.Style = ExcelBorderStyle.Thin;
        //                colIndex++;
        //            }
        //        }

        //        #endregion

        //        #region Save Excel to file


        //        //Create output file
        //        Byte[] bin = p.GetAsByteArray();
        //        string file = outputFilePath;
        //        try
        //        {
        //            File.Delete(file);
        //        }
        //        catch { }
        //        File.WriteAllBytes(file, bin);

        //        #endregion

        //        return true;
        //    }
        //}

    }
}
