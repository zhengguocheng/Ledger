using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using unvell.ReoGrid;
using unvell.ReoGrid.Actions;
using unvell.ReoGrid.CellTypes;
using unvell.ReoGrid.DataFormat;
using unvell.ReoGrid.Events;

namespace DMS.CustomClasses
{
    public class LedgerGridData
    {
        public List<tblVATRate> tblVATRateList;
        public List<tblChartAccount> tblNominalCodeList;
        public List<string> lstVatDisplayText;
        public List<string> lstNominalDisplayText;
        
        public LedgerGridData(long yrEndFolID)
        {
            tblVATRateList = new tblVATRateController().FetchByYearEndID(yrEndFolID);
            tblVATRateList = tblVATRateList.OrderBy(x => x.Code).ToList<tblVATRate>();

            tblNominalCodeList = new tblChartAccountController().FetchByYearEndID(yrEndFolID);
            tblNominalCodeList = tblNominalCodeList.OrderBy(x => x.Code).ToList<tblChartAccount>();
        }

        #region Dropdown
        public List<string> GetVatDisplayTextList()
        {
            if (lstVatDisplayText == null)
            {
                lstVatDisplayText = new List<string>();
                foreach (var item in tblVATRateList)
                {
                    lstVatDisplayText.Add(item.Code);
                }
            }

            return lstVatDisplayText;
        }

        public string FetchVatDisplayText(object vatID)
        {
            if (vatID != null && vatID != DBNull.Value)
            {
                var obj = tblVATRateList.FirstOrDefault(x => x.ID == (int)vatID);
                if (obj != null)
                    return obj.Code;
            }
            return string.Empty;
        }
        public object FetchVatValue(object displayText)
        {
            if (displayText != null)
            {
                var obj = tblVATRateList.FirstOrDefault(x => x.Code == displayText.ToString());
                if (obj != null)
                    return obj.ID;
            }

            return DBNull.Value;

        }

        public List<string> GetNominalDisplayTextList()
        {
            if (lstNominalDisplayText == null)
            {
                lstNominalDisplayText = new List<string>();
                foreach (var item in tblNominalCodeList)
                {
                    lstNominalDisplayText.Add(item.Code);
                }
            }

            return lstNominalDisplayText;
        }

        public string FetchNominalDisplayText(object nominalID)
        {
            try
            {
                var obj = tblNominalCodeList.FirstOrDefault(x => x.ID == (int)nominalID);
                if (obj != null)
                    return obj.Code;
                else
                    return string.Empty;
            }
            catch{}
            
            return string.Empty;

        }

        public string FetchNominalDescription(object nominalCode)
        {
            try
            {
                if (nominalCode != null)
                {
                    var obj = tblNominalCodeList.FirstOrDefault(x => x.Code == nominalCode.ToString().Trim());
                    if (obj != null)
                        return obj.Description;
                    else
                        return string.Empty;
                }
            }
            catch { }

            return string.Empty;

        }

        public object FetchNominalValue(object displayText)
        {
            if (displayText != null)
            {
                var strCode = displayText.ToString();
                if(strCode.Contains("."))
                {
                    float f = Convert.ToSingle(displayText.ToString());
                    strCode = f.ToString("N0");
                }

                var obj = tblNominalCodeList.FirstOrDefault(x => string.Equals(x.Code,strCode.ToString().Trim()));
                if (obj != null)
                    return obj.ID;

                if (obj == null && strCode.Length == 1)
                    strCode = "00" + strCode;
                if (obj == null && strCode.Length == 2)
                    strCode = "0" + strCode;

                obj = tblNominalCodeList.FirstOrDefault(x => string.Equals(x.Code, strCode.ToString().Trim()));
                if (obj != null)
                    return obj.ID;

            }
             
            return DBNull.Value;
        }

        #endregion

        #region Calculations
        public string CalculateVat(object code, object objGross)
        {
            string val = string.Empty;
            if (code != null && objGross != null)
            {
                try
                {
                    var gross = Convert.ToDecimal(objGross);
                    var obj = tblVATRateList.FirstOrDefault(x => x.Code == code.ToString());
                    var vatRate = obj.Percentage / 100;
                    decimal result = gross * vatRate / (1 + vatRate);
                    val = result.ToString("N2");
                }
                catch (Exception ecp) { }
            }
            return val;
        }

        public static string CalculateNet(object oVat, object oGross)
        {
            string val = string.Empty;
            
            if (oVat == null)
                oVat = 0;
            
            if (oGross != null)
            {
                try
                {
                    var gross = Convert.ToDecimal(oGross);
                    var vat = AppConverter.ToDecimal(oVat,0);
                    decimal result = gross - vat;
                    val = result.ToString("N2");
                }
                catch (Exception ecp) { }
            }
            return val;
        }

        #endregion

        public static DataTable GetDataTable(string tableName, long docItemID)
        {
            string query = string.Format("select * from {0} where DocumentItemID = {1};", tableName,docItemID);
            var ds = DBHelper.ExecuteSQL(query);
            return ds.Tables[0];
        }

        public static string ToDateString(string str)
        {
            try
            {
                if (!string.IsNullOrEmpty(str))
                {
                    DateTime d;
                    if(DateTime.TryParse(str, out d))
                    {
                        return d.ToString(AppConstants.DateFormatShort);
                    }
                    
                }
            }
            catch { }
            return string.Empty;
        }
    }
}
