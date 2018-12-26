using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class DataTableMapper
    {
        //GenericMethod<T>(T param) where T : BaseEntity
        // function that set the given object from the given data row
        public static void SetItemFromRow<T>(T item, DataRow row) where T : new()
        {
            // go through each column
            foreach (DataColumn c in row.Table.Columns)
            {
                // find the property for the column
                PropertyInfo p = item.GetType().GetProperty(c.ColumnName);

                //// if exists, set the value
                //if (p != null && row[c] != DBNull.Value)
                //{
                //    p.SetValue(item, row[c], null);
                //}

                //'shahbaz: I have changes the condition to allow process rows that value is DBNull
                if (p != null)
                {
                    if (row[c] != DBNull.Value)
                        p.SetValue(item, row[c], null);
                    else
                        p.SetValue(item, null, null);
                }
            }
        }

        // function that creates an object from the given data row
        public static T CreateItemFromRow<T>(DataRow row)
            where T : new()
        {
            // create a new object
            T item = new T();

            // set the item
            SetItemFromRow(item, row);

            // return 
            return item;
        }

        // function that creates a list of an object from the given data table
        //public static List<T> CreateListFromTable<T>(DataTable tbl)
        //    where T : new()
        //{
        //    // define return list
        //    List<T> lst = new List<T>();

        //    // go through each row
        //    foreach (DataRow r in tbl.Rows)
        //    {
        //        // add to the list
        //        lst.Add(CreateItemFromRow(row: r));
        //    }

        //    // return the list
        //    return lst;
        //}
    }
}
