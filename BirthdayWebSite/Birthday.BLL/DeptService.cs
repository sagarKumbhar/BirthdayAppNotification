using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Model;

namespace Birthday.BLL
{
    public class DeptService : IDisposable
    {
        public int AddDepartment(Department objDept)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string insertQuery = "INSERT INTO Department (DeptName,ShortName) VALUES ('" + objDept.DeptName + "','" + objDept.ShortName + "'); ";
                if (isDepartmentPresent(objDept.DeptName, objDept.ShortName))
                {
                    return -1;
                }
                else
                {
                    
                    using (OleDbCommand cmd = new OleDbCommand(insertQuery, dbservice.conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }

                    //Add distribution list while creating department

                }
            }
        }

        public bool isDepartmentPresent(string deptName, string ShortName)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string SelectQueryEmpID = "";

                SelectQueryEmpID = "select * from Department where DeptName='" + deptName + "' and ShortName='" + ShortName + "';";

                DataTable ds = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(SelectQueryEmpID, dbservice.conn))
                {
                    adapter.Fill(ds);
                }

                if (ds.Rows.Count == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        public void Dispose()
        {

        }
        public bool DeleteDepartment()
        {
            return false;
        }
        public List<Department> GetAllDepartments()
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT * FROM Department;";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    List<Department> lstDept = null;

                    if (dt != null)
                    {
                        lstDept = new List<Department>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            Department d = new Department();

                            d.DeptName = dr["DeptName"].ToString();
                            d.ShortName = dr["ShortName"].ToString();
                            d.Id = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                            lstDept.Add(d);
                        }
                    }

                    return lstDept;
                }
            }
        }
    }
}
