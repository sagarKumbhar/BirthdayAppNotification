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
    public class LoginService : IDisposable
    {
        public bool isAuthenticate(string login, string pwd)
        {
            using (AppContext ctx = new AppContext())
            {
                var LoginUser = ctx.LoginUsers.Include(a => a.Department).Include(a => a.Profile).Where(a => a.UserCode == login && a.Password == pwd).FirstOrDefault();
                if (LoginUser != null)
                {
                    using (SessionService ss = new SessionService())
                    {
                        ss.CurrentUser = LoginUser;
                    }
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public List<UserProfile> GetAllProfiles()
        {

            using (AppContext ctx = new AppContext())
            {
                var lstProfiles = ctx.UserProfiles.ToList();
                return lstProfiles;
            }
        }
        public int AddLoginUser(Model.LoginUser loginUser)
        {
            using (AppContext ctx = new AppContext())
            {
                bool isUserPresent = ctx.LoginUsers.Any(a => a.DepartmentId == loginUser.DepartmentId && a.UserCode == loginUser.UserCode);
                if (!isUserPresent)
                {
                    ctx.LoginUsers.Add(loginUser);
                    ctx.SaveChanges();
                    return loginUser.Id;
                }
                else
                {
                    return -1;
                }

            }
        }
        public int EditLoginUser(LoginUser LgUser)
        {
            using (AppContext ctx = new AppContext())
            {
                var user = ctx.LoginUsers.Where(a => a.Id == LgUser.Id).FirstOrDefault();

                if (user != null)
                {
                    bool retChk = ctx.LoginUsers.Any(a => (a.UserCode == LgUser.UserCode && a.DepartmentId == LgUser.DepartmentId) && a.Id != LgUser.Id);
                    if (!retChk)
                    {
                        user.UserCode = LgUser.UserCode;
                        user.Password = LgUser.Password;
                        user.DepartmentId = LgUser.DepartmentId;
                        return ctx.SaveChanges();
                    }
                    else
                    {
                        return -1;
                    }
                }
                else
                {
                    return -1;
                }
            }
        }
        public List<LoginUser> GetAllLoginUsers()
        {
            using (AppContext ctx = new AppContext())
            {
                SessionService ss = new BLL.SessionService();
                if (ss.CurrentUser.Profile.ProfileName == "IT administrator")
                {
                    return ctx.LoginUsers.Include(a => a.Department).Include(a => a.Profile).ToList();
                }
                else
                {
                    var ret = ctx.LoginUsers.Include(a => a.Department).Include(a => a.Profile).Where(a => a.Profile.ProfileName != "IT administrator").ToList();
                    return ret;
                }
            }

        }
        public LoginUser GetLoginUserbyId(int id)
        {
            using (AppContext ctx = new AppContext())
            {
                var LoginUser = ctx.LoginUsers.Include(a => a.Department).Include(a => a.Profile).Where(a => a.Id == id).FirstOrDefault();
                return LoginUser;
            }
        }
        public int DeleteLognUserbyId(int ID)
        {
            using (AppContext ctx = new AppContext())
            {
                var LoginUser = ctx.LoginUsers.Where(a => a.Id == ID).FirstOrDefault();
                ctx.LoginUsers.Remove(LoginUser);
                return ctx.SaveChanges();
            }
        }
        public void Dispose()
        {
            //            throw new NotImplementedException();
        }
    }
}

