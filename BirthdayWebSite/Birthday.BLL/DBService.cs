using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.BLL
{
    public class DBService : IDisposable
    {

        public OleDbConnection conn = null;

        private string connectionString = System.Configuration.ConfigurationManager.AppSettings["Connectionstring"].ToString();
        //"Provider=Microsoft.Jet.OLEDB.4.0;Data Source= D:\\SagarData\\DB_BirthdayList.mdb" ;
        public DBService()
        {
            if (conn == null)
            {
                this.conn = new OleDbConnection(connectionString);
                conn.Open();
            }
        }
        public void Dispose()
        {
            conn.Close();
        }
    }
}
