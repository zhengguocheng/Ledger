using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace DAL.CustomClasses
{
    /*
    public class FileHelper
    {

        public static byte[] GetByteArray(string filePath)
        {
            //Initialize byte array with a null value initially.
            byte[] data = null;

            //Use FileInfo object to get file size.
            FileInfo fInfo = new FileInfo(filePath);
            long numBytes = fInfo.Length;

            //Open FileStream to read file
            FileStream fStream = new FileStream(filePath, FileMode.Open, FileAccess.Read);

            //Use BinaryReader to read file stream into byte array.
            BinaryReader br = new BinaryReader(fStream);

            //When you use BinaryReader, you need to supply number of bytes to read from file.
            //In this case we want to read entire file. So supplying total number of bytes.
            data = br.ReadBytes((int)numBytes);
            return data;
        }


        public static string WriteToFile(string filePath, byte[] arr)
        {
            string dirPath = Path.GetDirectoryName(filePath);
            DirectoryInfo din = new DirectoryInfo(dirPath);
            if (!din.Exists)
                Directory.CreateDirectory(dirPath);

            using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(arr, 0, arr.Length);
                return filePath;
            }
            return null;
        }
    }*/
}
