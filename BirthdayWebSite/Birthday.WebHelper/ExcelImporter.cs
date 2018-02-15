using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aspose.Cells;

namespace Birthday.WebHelper
{
    public class ExcelImporter
    {
        /// <summary>
        /// Read the work sheet of the file
        /// </summary>
        /// <returns>Data table</returns>
        public DataTable ReadandUpload(string fileLoc)
        {
            Workbook workbook = new Workbook(fileLoc);

            // Obtaining the reference of the newly added worksheet by passing its sheet index
            Worksheet worksheet = workbook.Worksheets[0];
            try
            {
                string sheetName = worksheet.Name + "$";
                DataTable sheetData = new DataTable();
                string connString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileLoc + ";Extended Properties=Excel 12.0;";
                using (OleDbConnection conn = new OleDbConnection(connString))
                {
                    conn.Open();

                    // retrieve the data using data adapter
                    OleDbDataAdapter sheetAdapter = new OleDbDataAdapter("select * from [" + sheetName + "]", conn);
                    sheetAdapter.Fill(sheetData);
                }

                return sheetData;
            }
            catch (Exception ex)
            {
               
                return new DataTable();
            }
        }
    }
}
