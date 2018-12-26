using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using DAL;

namespace DMS
{
    public static class UIExtentions
    {
        public static bool IsValidValue(this RadDropDownList drp)
        {
            if (drp.SelectedIndex >= 0)
            {
                string editedVal = drp.Text;
                var item = drp.Items.FirstOrDefault(x => x.Text == editedVal);
                return item != null;
            }
            return true;
        }
        
        #region RadDatePicker

        public static void ApplyDMSSettings(this RadDateTimePicker dp)
        {
            dp.NullDate = AppConstants.NullDate;
            dp.MinDate = AppConstants.NullDate;
            dp.MaxDate = AppConstants.MaxDate;
            dp.Opening += dp_Opening;
        }

        static void dp_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            RadDateTimePicker dp = (RadDateTimePicker)sender;
            if(dp.IsNull())
            {
                dp.Value = DateTime.Now;
            }
        }

        public static DateTime? GetNullValue(this RadDateTimePicker dp)
        {
            if (dp.Value == AppConstants.NullDate)
                return null;
            else
                return dp.Value;
        }

        public static bool IsNull(this RadDateTimePicker dp)
        {
            return dp.GetNullValue() == null;
        }

        public static void Clear(this RadDateTimePicker dp)
        {
            dp.Value = AppConstants.NullDate;
        }

        public static void SetDate(this RadDateTimePicker dp,DateTime? date)
        {
            if (date == null)
                dp.Value = AppConstants.NullDate;
            else
                dp.Value = date.Value;
        }

        #endregion

        #region Date

        public static DateTime GetNullValue(this DateTime? dt)
        {
            return (dt.HasValue)?dt.Value: AppConstants.NullDate;
        }

        public static DateTime? MonthAdjustment(this DateTime? dt, int months)
        {
            if (!dt.HasValue)
                return dt;

            return dt.Value.MonthAdjustment(months);
        }

        public static DateTime MonthAdjustment(this DateTime dt,int months)
        {
            if (dt == AppConstants.NullDate)
                return dt;

            #region Pre 
            //if (dt.Month == 4 || dt.Month == 6 || dt.Month == 9 || dt.Month == 11)
            //{
            //    dt = dt.AddMonths(months).AddDays(1);
            //}
            //else if (dt.Month == 2)
            //{
            //    if (DateTime.IsLeapYear(dt.Year))
            //        dt = dt.AddMonths(months).AddDays(1);
            //    else
            //        dt = dt.AddMonths(months).AddDays(2);
            //}
            //else
            //{
            //    dt = dt.AddMonths(months);
            //}
            //return dt;
            #endregion

            return AddMonthNew(dt, months);
        }

        #region New Impl

        static DateTime AddMonthNew(DateTime dt, int month)
        {
            DateTime newDate = dt.AddMonths(month);

            if (IsLastDateOfMonth(dt))
                newDate = LastDayOfMonthFromDateTime(newDate);
            return newDate;
        }

        public static DateTime LastDayOfMonthFromDateTime(DateTime dateTime)
        {
            DateTime firstDayOfTheMonth = new DateTime(dateTime.Year, dateTime.Month, 1);
            return firstDayOfTheMonth.AddMonths(1).AddDays(-1);
        }

        static bool IsLastDateOfMonth(DateTime dt)
        {
            if (dt.AddDays(1).Month > dt.Month)
                return true;
            else
                return false;
        }

        #endregion

        #endregion


        //5,418,752.00
        public static string ToMoney(this string str, char sign = char.MinValue)//insert comma after three digits starting from left
        {
            if (string.IsNullOrWhiteSpace(str))
                return str;

            int pointIndex = (str.LastIndexOf('.') != -1)? str.LastIndexOf('.') - 1: str.Length - 1; 

            string major = str.Substring(0, pointIndex + 1);

            List<char> lstStr = str.ToList<char>();
            int lastInd = major.Length % 3 > 0 ? major.Length % 3: 3;
            int majorStrLength = major.Length;

            while (lastInd < majorStrLength - 1)
            {
                lstStr.Insert(lastInd, ',');
                majorStrLength++;
                lastInd = (lastInd + 3) + 1;
            }

            if (sign != char.MinValue)
            {
                lstStr.Insert(0, ' ');
                lstStr.Insert(0, '$');
            }

            return string.Concat(lstStr);
        }

    }
}
