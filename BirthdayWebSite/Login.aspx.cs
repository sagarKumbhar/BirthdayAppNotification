using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using Birthday.BLL.EntityService;
using Birthday.Model;
using Birthday.WebHelper;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    [WebMethod]
    public static string AuthenticateUser(string UserID, string Password)
    {
        bool isauth = false;
        using (LoginService lgservice = new LoginService())
        {
            isauth = lgservice.isAuthenticate(UserID, Password);
        }
        if (isauth)
        {
            return "success";
        }
        else
        {
            return "failed";
        }
        //if (UserID=="sagar" && Password=="test")
        //{
        //    LoginUser objUser = new LoginUser();
        //    objUser.Id = 1;
        //    objUser.DepartmentId = 1;
        //    objUser.UserProfileId = 1;
        //    objUser.UserCode = "IT Admin";

        //    SessionService ss = new SessionService();
        //    ss.CurrentUser = objUser;
        //    return "ManageUser.aspx";
        //}
        //else
        //{
        //    return "failed";
        //}
    }

    [WebMethod]
    public static string test(string UserID,string Password)
    {
        return "failed";
        //bool isauth = false;
        //using (LoginService lgservice = new LoginService())
        //{
        //    isauth = lgservice.isAuthenticate(UserID, Password);
        ////}
        //if (isauth)
        //{
        //    return "success";
        //}
        //else
        //{
        //    return "failed";
        //}
            
    }
}