
using ExcelDataReader;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLibs.Utils
{
    public class ExcelDriver
    {
        //Method to fetch data from excel sheet to a datatable
        private static DataTable ExcelToDataTable(string fileName, string sheetname)
        {
            //open file and returns as Stream
            /**(FileShare.Read) this is given to make the excel file readonly 
            i.e. it wont affect during parallel execution**/
            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.Read))
            {
                // Auto-detect format, supports:
                //  - Binary Excel files (2.0-2003 format; *.xls)
                //  - OpenXml Excel files (2007 format; *.xlsx, *.xlsb)
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {

                    //Return as DataSet
                    var result = excelReader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (data) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true
                        }
                    });

                    //Get all the Tables
                    DataTableCollection table = result.Tables;
                    //Store it in DataTable
                    DataTable resultTable = table[sheetname];

                    //return
                    return resultTable;
                }
            }
        }

        //Custom Class
        public class Datacollection
        {
            public int RowNumber { get; set; }
            public string ColName { get; set; }
            public string ColValue { get; set; }
        }

        //Method to Populate Data into Collections
        static List<Datacollection> dataCol = new List<Datacollection>();
        public static void PopulateInCollection(string fileName, string sheetname)
        {
            DataTable table = ExcelToDataTable(fileName, sheetname);

            //Iterate through the rows and columns of the Table
            for (int row = 1; row <= table.Rows.Count; row++)
            {
                for (int col = 0; col < table.Columns.Count; col++)
                {
                    Datacollection dtTable = new Datacollection()
                    {
                        RowNumber = row,
                        ColName = table.Columns[col].ColumnName,
                        ColValue = table.Rows[row - 1][col].ToString()
                    };
                    //Add all the details for each row
                    dataCol.Add(dtTable);
                }
            }
        }

        //Method to Read data from Collection
        public static string ReadData(int rowNumber, string columnName)
        {
            try
            {
                //Retriving Data using LINQ to reduce much of iterations
                string data = (from colData in dataCol
                               where colData.ColName == columnName && colData.RowNumber == rowNumber
                               select colData.ColValue).SingleOrDefault();

                //var datas = dataCol.Where(x => x.colName == columnName && x.rowNumber == rowNumber).SingleOrDefault().colValue;
                return data.ToString();
            }
            catch (Exception)
            {
                return null;
            }
        }

        //Reset the collection
        public static void ResetDataCollection()
        {
            dataCol.Clear();
        }
    }
}
