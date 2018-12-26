using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMS.CustomClasses
{
    public class GridException
    {
        public enum ErrorType { SplitParentInvalid, ColumnNotFound, InvalidCalculation, InvalidRows };


        public const string SplitParentInvalid = "Row number {0} which is 'Split Parent Row' is invalid. Please correct the error.";
        public const string ColumnNotFound = "Specified column not found.";
        public const string InvalidCalculation = "Calculation is not valid for row {0}. Please correct the error.";
        public const string InvalidRows = "Row number {0} is not valid. Please correct error.";

        public static Exception GetException(ErrorType typ, int row = 0)
        {
            string msg = string.Empty;

            switch (typ)
            {
                case ErrorType.SplitParentInvalid:
                    msg = string.Format(SplitParentInvalid, row);
                    break;
                case ErrorType.ColumnNotFound:
                    msg = ColumnNotFound;
                    break;
                case ErrorType.InvalidCalculation:
                    msg = string.Format(InvalidCalculation, row);
                    break;
                case ErrorType.InvalidRows:
                    msg = string.Format(InvalidRows, row);
                    break;
                default:
                    break;
            }

            return new Exception(msg);
        }
    }
}
