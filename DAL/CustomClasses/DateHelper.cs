using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DAL
{
    public class DateHelper
    {
        public static DateTime AddMonthNew(DateTime dt, int month)
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
    }
}
