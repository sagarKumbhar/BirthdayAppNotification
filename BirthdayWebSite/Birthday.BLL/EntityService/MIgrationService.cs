using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Data;
using Birthday.Model;

namespace Birthday.BLL.EntityService
{
    public class MigrationService : IDisposable
    {
        public bool MigrateCurrentDataToSql(string filePath)
        {
            //OPen Access DB file
            //Migrate tables one by one
            //Copy Image of employees with renaming new ID

            OleDbConnection conn = null;

            string connectionString = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source= " + filePath;

            if (conn == null)
            {
                conn = new OleDbConnection(connectionString);
                conn.Open();
            }


            return true;
        }
        public void Dispose()
        {
            //            throw new NotImplementedException();
        }
    }
}

