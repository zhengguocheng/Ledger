using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DMS
{
    public class DataSetSchema
    {
        public static void WriteSchema(DataSet ds,string dsName,string filePath)
        {
            // Set the name of the DataSet which will be useful later
            ds.DataSetName = dsName;

            // Can be used to write the of the DataSet
            ds.WriteXmlSchema(filePath+".xsd");

        }


    }
}
