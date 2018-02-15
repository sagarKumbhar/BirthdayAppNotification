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
    public class LoginService : IDisposable
    {
        public bool isAuthenticate(string login, string pwd)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = @"SELECT L.* , d.DeptName,p.ProfileName FROM 
                        ((LoginUser L inner join Department d on L.DeptID=d.ID) inner join UserProfile p on L.UserProfileId=p.ID) where L.UserCode ='" + login + "' and L.Password='" + pwd + "' ;";
                    // 
                    //
                    // selects all content from table and adds it to datatable binded to datagridview
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    List<LoginUser> lstLoginUsers = null;

                    if (dt != null)
                    {
                        lstLoginUsers = new List<LoginUser>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            LoginUser usr = new LoginUser();
                            usr.DeptId = dr["DeptID"] != null ? Convert.ToInt32(dr["DeptID"]) : -1;
                            usr.DeptName = dr["DeptName"].ToString();
                            usr.UserCode = dr["UserCode"].ToString();
                            usr.UserProfileId = dr["UserProfileId"] != null ? Convert.ToInt32(dr["UserProfileId"]) : -1;
                            usr.ProfileName = dr["ProfileName"].ToString();
                            usr.Id = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                            usr.Password = dr["Password"].ToString();
                            lstLoginUsers.Add(usr);
                        }
                    }

                    var loginuser = lstLoginUsers.FirstOrDefault();
                    if (loginuser != null)
                    {
                        using (SessionService ss = new SessionService())
                        {
                            ss.CurrentUser = loginuser;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }

        public List<UserProfile> GetAllProfiles()
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT * FROM UserProfile;";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    List<UserProfile> lstprofiles= null;

                    if (dt != null)
                    {
                        lstprofiles = new List<UserProfile>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            UserProfile d = new UserProfile();

                            d.Code = dr["Code"].ToString();
                            d.ProfileName = dr["ProfileName"].ToString();
                            d.Id = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                            lstprofiles.Add(d);
                        }
                    }

                    return lstprofiles;
                }
            }
        }
        public int AddLoginUser(LoginUser loginUser)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string insertQuery = "INSERT INTO LoginUser ([UserCode],[Password],[UserProfileID],[DeptID]) VALUES ('" + loginUser.UserCode + "','" + loginUser.Password + "','" + loginUser.UserProfileId + "','" + loginUser.DeptId + "');";
                if (isUserPresent(loginUser.UserCode, loginUser.DeptId))
                {
                    return -1;

                }
                else
                {
                    using (OleDbCommand cmd = new OleDbCommand(insertQuery, dbservice.conn))
                    {
                        return cmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public bool isUserPresent(string userCode, int deptid)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string SelectQueryEmpID = "select * from LoginUser where DeptID=" + deptid + " and UserCode='" + userCode + "';";
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
        public int EditLoginUser(LoginUser LgUser)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string updateQuery = "UPDATE LoginUser SET [UserCode]='" + LgUser.UserCode + "', [Password]='" + LgUser.Password + "',[DeptID]=" + LgUser.DeptId + " WHERE id=" + LgUser.Id + ";";
                if (isUserPresent(LgUser.UserCode, LgUser.DeptId))
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
        public List<LoginUser> GetAllLoginUsers()
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    SessionService ss = new BLL.SessionService();
                    string selectQuery = "";
                    if (ss.CurrentUser.ProfileName == "IT administrator")
                    {
                        selectQuery = @"SELECT L.* , d.DeptName,p.ProfileName FROM
                                        ((LoginUser L inner join Department d on L.DeptID=d.ID) inner join UserProfile p on L.UserProfileId=p.ID);";
                    }
                    else
                    {
                        selectQuery = @"SELECT L.* , d.DeptName,p.ProfileName FROM
                                        ((LoginUser L inner join Department d on L.DeptID=d.ID) inner join UserProfile p on L.UserProfileId=p.ID ) where p.ProfileName <> 'IT administrator';";
                    }
                    // 
                    //
                    // selects all content from table and adds it to datatable binded to datagridview
                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    List<LoginUser> lstLoginUsers = null;

                    if (dt != null)
                    {
                        lstLoginUsers = new List<LoginUser>();
                        foreach (DataRow dr in dt.Rows)
                        {
                            LoginUser usr = new LoginUser();
                            usr.DeptId = dr["DeptID"] != null ? Convert.ToInt32(dr["DeptID"]) : -1;
                            usr.DeptName = dr["DeptName"].ToString();
                            usr.UserCode = dr["UserCode"].ToString();
                            usr.UserProfileId = dr["UserProfileId"] != null ? Convert.ToInt32(dr["UserProfileId"]) : -1;
                            usr.ProfileName = dr["ProfileName"].ToString();
                            usr.Id = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                            usr.Password = dr["Password"].ToString();
                            lstLoginUsers.Add(usr);
                        }
                    }

                    return lstLoginUsers;
                }
            }
        }
        public LoginUser GetLoginUserbyId(int id)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                using (DataTable dt = new DataTable())
                {
                    string selectQuery = "SELECT L.* , d.DeptName FROM LoginUser L inner join Department d on L.DeptID=d.ID where L.ID=" + id.ToString() + ";";

                    using (OleDbDataAdapter adapter = new OleDbDataAdapter(selectQuery, dbservice.conn))
                    {
                        adapter.Fill(dt);
                    }
                    LoginUser usr = null;
                    if (dt != null)
                    {
                        usr = new LoginUser();
                        foreach (DataRow dr in dt.Rows)
                        {


                            usr.DeptId = dr["DeptID"] != null ? Convert.ToInt32(dr["DeptID"]) : -1;
                            usr.DeptName = dr["DeptName"].ToString();
                            usr.UserCode = dr["UserCode"].ToString();
                            usr.UserProfileId = dr["UserProfileId"] != null ? Convert.ToInt32(dr["UserProfileId"]) : -1;
                            usr.Id = dr["ID"] != null ? Convert.ToInt32(dr["ID"]) : -1;
                            usr.Password = dr["Password"].ToString();

                        }
                    }
                    return usr;
                }
            }
        }
        public int DeleteLognUserbyId(int ID)
        {
            using (DBService dbservice = new BLL.DBService())
            {
                string DeleteQuery = "Delete from LoginUser where ID=" + ID;

                using (OleDbCommand cmd = new OleDbCommand(DeleteQuery, dbservice.conn))
                {
                    return cmd.ExecuteNonQuery();
                }
            }
        }
        public void Dispose()
        {
            //            throw new NotImplementedException();
        }
    }
}

