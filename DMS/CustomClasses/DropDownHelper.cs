using DAL;
using DMS.CustomClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Telerik.WinControls.UI;

namespace DMS
{
    public class DropDownHelper
    {
        public static string EmptyValue = "-1";
        public const string ValueSeperator = "-";
        public static void CommonSetting(RadDropDownList drp)
        {
            drp.DisplayMember = "DisplayText";
            drp.ValueMember = "Value";

            drp.DropDownStyle = Telerik.WinControls.RadDropDownStyle.DropDown;
            drp.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            drp.DropDownListElement.AutoCompleteSuggest.SuggestMode = SuggestMode.Contains;//contains search
            drp.DropDownListElement.AutoCompleteAppend.LimitToList = true;

        }

        public static void Bind(RadDropDownList drp, List<DropDownItem> lst)
        {
            //drp.SortStyle = Telerik.WinControls.Enumerations.SortStyle.Ascending;
            lst.Sort((x, y) => x.DisplayText.CompareTo(y.DisplayText));
            drp.DataSource = lst;
            CommonSetting(drp);
        }

        public static List<DropDownItem> Bind(RadDropDownList drp, DataTable dt, string valueMember, params string[] arrDisplayMem)
        {
            List<DropDownItem> lst = new List<DropDownItem>();
            lst.Add(new DropDownItem(string.Empty, EmptyValue));
            foreach (DataRow row in dt.Rows)
            {
                #region Calculate Text Value
                string displayTxt = string.Empty;
                foreach (var colName in arrDisplayMem)
                {
                    string str = row[colName].ToString();
                    if (!string.IsNullOrWhiteSpace(str))
                        displayTxt += " " + str;//dont use any seperator like "-" or "_". It stops autocomplettion
                }

                #endregion

                DropDownItem itm = new DropDownItem(displayTxt.Trim(), row[valueMember].ToString());

                lst.Add(itm);
            }
            Bind(drp, lst);
            return lst;
        }

        public static string DisplayDefault(string str, bool sign = false)
        {
            try
            {
                if (str == EmptyValue)
                    return string.Empty;
                else
                    return str;

            }
            catch (Exception ecp)
            {
                return str;
            }
        }

        public static void SelectByValue(RadDropDownList drp, string val)
        {
            drp.SelectedValue = val;
        }
        public static string GetSelectedValue(RadDropDownList drp)
        {
            var itm = GetSelectedItem(drp);
            return itm.Value;
        }
        public static string GetSelectedText(RadDropDownList drp)
        {
            var itm = GetSelectedItem(drp);
            return itm.DisplayText.Trim();
        }
        public static DropDownItem GetSelectedItem(RadDropDownList drp)
        {
            List<DropDownItem> lst = (List<DropDownItem>)drp.DataSource;
            var selItem = lst.FirstOrDefault(x => x.DisplayText == drp.Text);

            if (selItem != null)
                return selItem;
            else
            {
                var itm = (DropDownItem)drp.SelectedItem.DataBoundItem; //this dont work if user dont press enter after selecting text. 
                return itm;
            }
        }

        

        public static bool IsTextExist(RadDropDownList drp)
        {
            var itm = drp.FindItemExact(drp.Text.Trim(), false);
            if (itm == null)
                return false;
            else
                return true;
        }


        public static void CorrectInvalidSearchTerm(RadDropDownList drp)//this method select nearset possible item in dropdown if user enters invalid search term
        {
            
            var enteredTxt = drp.Text;

            List<DropDownItem> lst = (List<DropDownItem>)drp.DataSource;
            lst.Sort((x, y) => x.DisplayText.CompareTo(y.DisplayText));

            var selItem = lst.FirstOrDefault(x => x.DisplayText.Contains(enteredTxt.Trim()));

            if (selItem == null || selItem.Value == EmptyValue)
            {
                if(IsAllowNull(drp))
                    selItem = lst[0];//select first item. which is empty
                else
                    selItem = lst[1];//select first non empty item
            }

            SelectByValue(drp, selItem.Value);
            drp.Text = selItem.DisplayText;//set dropdown text.
        }
        public static void SelectNull(RadDropDownList drp)
        {
            SelectByValue(drp, EmptyValue);
        }

        public static bool IsEmpty(RadDropDownList drp)
        {
            return GetSelectedValue(drp) == EmptyValue;
        }

        public static void LoadDistinct(RadDropDownList drp)
        {
            List<DropDownItem> lst = (List<DropDownItem>)drp.DataSource;
            var obj = from x in lst
                      group x by x.Value into gx
                      select gx.First();
            drp.DataSource = obj.ToList<DropDownItem>();
        }

        #region Custom Binding

        public static void SetAllowNull(RadDropDownList drp, bool val)
        {
            drp.Tag = val;
        }

        public static bool IsAllowNull(RadDropDownList drp)
        {
            try
            {
                return Convert.ToBoolean(drp.Tag);
            }
            catch
            {
                return false;
            }
        }


        public static List<DropDownItem> BindNominalCode(RadDropDownList drp, long yrEndFolID, bool allowNull = false)
        {
            string qry = string.Format("select ID,Code +' {0} '+ [Description] as CombinedText from tblChartAccount where YearEndFolderID = {1}", ValueSeperator, yrEndFolID);
            var dtChart = DBHelper.ExecuteSQL(qry).Tables[0];
            var lst = Bind(drp, dtChart, "ID", "CombinedText");

            SetAllowNull(drp, allowNull);

            #region enable user enter search term
            drp.DropDownListElement.AutoCompleteAppend.LimitToList = false;//this is to enable user enter search term
            drp.Leave += new System.EventHandler(drpNominalCode_Leave);
            drp.DropDownListElement.Focus();
            #endregion

            return lst;
        }

        private static void drpNominalCode_Leave(object sender, EventArgs e)
        {
            var drpNominalCode = (RadDropDownList)sender;
            DropDownHelper.CorrectInvalidSearchTerm(drpNominalCode);
        }
        public static List<DropDownItem> BindVATCode(RadDropDownList drp, long yrEndFolID)
        {
            string qry = string.Format("select ID,Code +' {0} '+ [Type] as CombinedText from tblVATRate where YearEndFolderID = {1}", ValueSeperator, yrEndFolID);
            var dtChart = DBHelper.ExecuteSQL(qry).Tables[0];
            return Bind(drp, dtChart, "ID", "CombinedText");
        }

        public static List<DropDownItem> BindDescription(RadDropDownList drp)
        {
            var qry = string.Format("SELECT [Description] FROM tblDescription where [Description]  not like '%{0}%'", AppConstants.splitValue);
            var dt = DBHelper.ExecuteSQL(qry).Tables[0];
            var lst = Bind(drp, dt, "Description", "Description");
            return lst;
        }

        public static List<DropDownItem> BindSupplier(RadDropDownList drp)
        {
            var qry = string.Format("select ID,Name from tblSupplier where Name not like '%{0}%'", AppConstants.splitValue);
            var dt = DBHelper.ExecuteSQL(qry).Tables[0];
            var lst = Bind(drp, dt, "ID", "Name");
            return lst;
        }

        public static List<DropDownItem> BindClientYearEnd(RadDropDownList drp)
        {
            var qry = "SELECT Distinct [Client Name],RecordID FROM dbo.VwDocumentList where YearEndDate is not null";
            var dt = DBHelper.ExecuteSQL(qry).Tables[0];
            var lst = Bind(drp, dt, "RecordID", "Client Name");
            return lst;
        }
        public static List<DropDownItem> BindYearEnd(RadDropDownList drp, string clientID)
        {
            string qry = string.Format(@"SELECT Name,ID FROM tblDocumentItem 
                                        where YearEndDate is not null 
                                        and (IsDeleted = 0 or IsDeleted is null)  
                                        and RecordID = {0}", clientID);
            var dtChart = DBHelper.ExecuteSQL(qry).Tables[0];
            return Bind(drp, dtChart, "ID", "Name");
        }

        #endregion
    }

}
