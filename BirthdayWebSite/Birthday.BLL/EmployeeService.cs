using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Model;

namespace Birthday.BLL
{
    public class EmployeeService : IDisposable
    {
        public int AddEmployee(Employee emp)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string insertQuery = "INSERT INTO Employee (EmployeeName,EmployeeID,Email,Birthdate,DepartmentID) VALUES ('" + emp.EmployeeName + "','" + emp.EmployeeID + "','" + emp.Email + "','" + emp.Birthdate + "','" + emp.DepartmentId + "');";
                string selectQuery = "Select @@Identity";
                int id = -1;
                if (isEmployeePresent(emp.EmployeeID, emp.Email, 0, "create"))
                {
                    return id;

                }
                else
                {
                    using (OleDbCommand cmd = new OleDbCommand(insertQuery, dbservice.conn))
                    {
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = selectQuery;
                        id = (int)cmd.ExecuteScalar();
                        return id;
                    }
                }
            }
        }
        public int EditEmployee(Employee emp)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string updateQuery = "UPDATE Employee SET [EmployeeName]='" + emp.EmployeeName + "', [EmployeeID]=" + emp.EmployeeID + ",[Email]='" + emp.Email + "',[Birthdate]='" + emp.Birthdate + "' WHERE id=" + emp.Id + ";";
                if (isEmployeePresent(emp.EmployeeID, emp.Email, emp.Id, "update"))
                {
                    return -1;

                }
                else
                {
                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, dbservice.conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public int UpdateEmployeePhotoLocation(string path, int id)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string updateQuery = "UPDATE Employee SET [PhotoLocation]='" + path + "'WHERE id=" + id + ";";

                using (OleDbCommand cmd = new OleDbCommand(updateQuery, dbservice.conn))
                {
                    return cmd.ExecuteNonQuery();
                }

            }
        }
        public bool isEMployeeIDChanged(int ID, int pEmpID)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string SelectQueryEmpID = "select EmployeeID from Employee where ID=" + ID + ";";
                DataTable ds = new DataTable();
                using (OleDbDataAdapter adapter = new OleDbDataAdapter(SelectQueryEmpID, dbservice.conn))
                {
                    adapter.Fill(ds);
                }

                if (ds.Rows.Count > 0)
                {
                    int empID = ds.Rows[0]["EmployeeID"] != null ? Convert.ToInt32(ds.Rows[0]["EmployeeID"]) : -1;
                    if (empID != pEmpID)
                    {
                        return true;
                    }
                    else
                        return false;
                }
                else
                    return false;
            }
        }
        public bool isEmployeePresent(int EmpID, string Email, int id, string action)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string SelectQueryEmpID = "";
                if (action == "create")
                    SelectQueryEmpID = "select * from Employee where EmployeeID=" + EmpID + " or Email='" + Email + "';";
                else if (action == "update")
                    SelectQueryEmpID = "select * from Employee where (EmployeeID=" + EmpID + " or Email='" + Email + "') and ID not in(" + id + ");";
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
        public int DeleteEmployee(int EmpID)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string DeleteQuery = "Delete from Employee where ID=" + EmpID;

                using (OleDbCommand cmd = new OleDbCommand(DeleteQuery, dbservice.conn))
                {
                    int retVal = cmd.ExecuteNonQuery();
                    if (retVal > 0)
                    {
                        string photoPath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PhotoLocation"].ToString(), EmpID.ToString() + ".jpg");
                        if (!File.Exists(photoPath))
                        {
                            photoPath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PhotoLocation"].ToString(), EmpID.ToString() + ".png");
                            if (!File.Exists(photoPath))
                            {
                                photoPath = Path.Combine(System.Configuration.ConfigurationManager.AppSettings["PhotoLocation"].ToString(), EmpID.ToString() + ".jpeg");
                                if (File.Exists(photoPath))
                                {
                                    File.Delete(photoPath);
                                }
                            }
                            else
                            {
                                File.Delete(photoPath);
                            }
                        }
                        else
                        {
                            File.Delete(photoPath);
                        }
                    }

                    return retVal;
                }
            }
        }

        public List<Employee> GetAllEmployeeByDepartmentID(int DepartmentID)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT * FROM Employee where DepartmentID=" + DepartmentID + ";";
                    return FillEmployeeList(dbservice, dt, selectQuery);
                }
            }

        }


        public List<Employee> GetAllEmployee()
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT * FROM Employee;";

                    return FillEmployeeList(dbservice, dt, selectQuery);
                }
            }
        }

        public Employee GetEmployeebyId(int id)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT * FROM Employee where ID=" + id.ToString() + ";";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    Employee usr = null;
                    if (dt != null)
                    {
                        usr = new Employee();
                        foreach (DataRow dr in dt.Rows)
                        {

                            usr.DepartmentId = dr["DepartmentID"] != null ? Convert.ToInt32(dr["DepartmentID"]) : -1;
                            usr.EmployeeName = dr["EmployeeName"].ToString();
                            usr.EmployeeID = dr["EmployeeID"] != null ? Convert.ToInt32(dr["EmployeeID"]) : -1;
                            usr.ID = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                            usr.Email = dr["Email"].ToString();
                            usr.Birthdate = dr["Birthdate"] != null ? Convert.ToDateTime(dr["Birthdate"]) : (DateTime?)null;
                            usr.PhotoLocation = dr["PhotoLocation"].ToString();

                        }
                    }
                    return usr;
                }
            }
        }

        private static List<Employee> FillEmployeeList(DBService dbservice, DataTable dt, string selectQuery)
        {
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
            {
                adapter.Fill(dt);
            }
            List<Employee> lstEmployee = null;

            if (dt != null)
            {
                lstEmployee = new List<Employee>();
                foreach (DataRow dr in dt.Rows)
                {
                    Employee usr = new Employee();
                    usr.DepartmentId = dr["DepartmentID"] != null ? Convert.ToInt32(dr["DepartmentID"]) : -1;
                    usr.EmployeeName = dr["EmployeeName"].ToString();
                    usr.EmployeeID = dr["EmployeeID"] != null ? Convert.ToInt32(dr["EmployeeID"]) : -1;
                    usr.ID = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                    usr.Email = dr["Email"].ToString();
                    usr.Birthdate = dr["Birthdate"] != null ? Convert.ToDateTime(dr["Birthdate"]) : (DateTime?)null;
                    usr.PhotoLocation = dr["PhotoLocation"].ToString();
                    lstEmployee.Add(usr);
                }
            }
            return lstEmployee;
        }

        public bool MigrateExcelFileData(DataTable dtEmployeeInfo, int DepartmentID,ref string msg)
        {
            try
            {
                msg = "";
                if (dtEmployeeInfo.Rows.Count > 0)
                {
                    foreach (DataRow dtaRowEmp in dtEmployeeInfo.Rows)
                    {
                        if (dtaRowEmp["EmployeeName"].ToString() != string.Empty || dtaRowEmp["EmployeeID"].ToString() != string.Empty ||
                            dtaRowEmp["Email"].ToString() != string.Empty || dtaRowEmp["BirthDate"].ToString() != string.Empty)
                        {
                            Employee emp = new Employee();
                            emp.EmployeeName = dtaRowEmp["EmployeeName"].ToString();
                            emp.EmployeeID = Convert.ToInt32(dtaRowEmp["EmployeeID"].ToString());
                            emp.Email = dtaRowEmp["Email"].ToString();
                            emp.Birthdate = Convert.ToDateTime(dtaRowEmp["BirthDate"].ToString());
                            emp.DepartmentId = DepartmentID;
                            int ret = AddEmployee(emp);
                            if (ret == -1)
                            {
                                msg += "Wrong/Duplicate records for Emp Name:" + emp.EmployeeName +"$";
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public DistributionList GetDistributionListByDepartmentId(int DepartmentId)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT DS.*, d.DepartmentName FROM DistributionList DS inner join Department d on DS.DepartmentId=d.ID  where  DepartmentId=" + DepartmentId.ToString() + ";";
                    //SELECT L.* , d.DepartmentName FROM LoginUser L inner join Department d on L.DepartmentID=d.ID where L.ID=" + id.ToString() + ";";
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    DistributionList usr = null;
                    if (dt.Rows.Count > 0)
                    {
                        usr = new DistributionList();
                        foreach (DataRow dr in dt.Rows)
                        {

                            usr.DepartmentId = dr["DepartmentID"] != null ? Convert.ToInt32(dr["DepartmentID"]) : -1;
                            usr.Recepients = dr["Recepients"].ToString();
                            usr.Department.DepartmentName = dr["DepartmentName"].ToString();
                            usr.IsIncludeUsers = Convert.ToBoolean(dr["IsIncludeUsers"]);
                            usr.ID = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;

                        }
                    }
                    return usr;
                }
            }
        }

        public int EditDistributionList(DistributionList dsList)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                DistributionList ds = GetDistributionListByDepartmentId(dsList.DepartmentId);
                if (ds != null)
                {
                    string updateQuery = "UPDATE DistributionList SET [Recepients]='" + dsList.Recepients + "', [IsIncludeUsers]=" + dsList.IsIncludeUsers + " WHERE Departmentid=" + dsList.DepartmentId + ";";
                    using (OleDbCommand cmd = new OleDbCommand(updateQuery, dbservice.conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    string insertQuery = "INSERT INTO DistributionList (Recepients,IsIncludeUsers,DepartmentID) VALUES ('" + dsList.Recepients + "'," + dsList.IsIncludeUsers + "," + dsList.DepartmentId + ");";


                    using (OleDbCommand cmd = new OleDbCommand(insertQuery, dbservice.conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }

                }
            }

        }

        public void Dispose()
        {

        }
    }
}
