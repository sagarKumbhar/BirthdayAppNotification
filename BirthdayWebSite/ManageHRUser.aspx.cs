using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Birthday.BLL.EntityService;
using Birthday.Model;
using Birthday.ViewModel;
using Birthday.WebHelper;

public partial class ManageHRUser : BasePage
{
    Birthday.BLL.SessionService sessionser = new Birthday.BLL.SessionService();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            FillUserList();
            RebindDdlDepartment();
            RebindDdlProfile();
        }

    }

    private void RebindDdlProfile()
    {
        if (sessionser.CurrentUser != null)
        {
            List<UserProfile> lstDepartment = null;
            using (LoginService DepartmentSer = new LoginService())
            {
                lstDepartment = DepartmentSer.GetAllProfiles();
                ddlProfile.DataSource = lstDepartment;
                ddlProfile.DataTextField = "ProfileName";
                ddlProfile.DataValueField = "ID";
                ddlProfile.DataBind();
            }
            if (sessionser.CurrentUser.Profile.ProfileName != "IT administrator")
            {
                ddlProfile.SelectedValue = lstDepartment.Where(d => d.ProfileName == "HR User").FirstOrDefault().Id.ToString();
                ddlProfile.Enabled = false;
            }
        }
    }
    private void RebindDdlDepartment()
    {
        using (DepartmentService DepartmentSer = new DepartmentService())
        {
            List<Department> lstDepartment = DepartmentSer.GetAllDepartments();
            ddlDepartment.DataSource = lstDepartment;
            ddlDepartment.DataTextField = "ShortName";
            ddlDepartment.DataValueField = "ID";
            ddlDepartment.DataBind();
        }
    }

    private void FillUserList()
    {
        List<LoginUser> lstUser = new List<LoginUser>();
        using (LoginService loginSrv = new LoginService())
        {
            lstUser = loginSrv.GetAllLoginUsers();
        }
        object[] arrObj = new object[lstUser.Count];
        for (int i = 0; i < arrObj.Length; i++)
        {

            arrObj[i] = new
            {
                ID = (lstUser[i].Id).ToString(),
                UserCode = lstUser[i].UserCode,
                Password = lstUser[i].Password,
                Department = lstUser[i].Department.DepartmentName

            };
        }
        var json = new JavaScriptSerializer().Serialize(arrObj);//BindListTable('" + json + "');
        ScriptManager.RegisterStartupScript(this.Page, Page.GetType(), "updateDT", "BindListTable('" + json + "');", true);
    }


    protected void btnSaveData_Click(object sender, EventArgs e)
    {
        try
        {

            if (hidID.Value == "")//Create
            {
                LoginUser objEmp = new LoginUser();
                objEmp.UserCode = userCode.Value;
                objEmp.Password = password.Value;
                objEmp.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                objEmp.UserProfileId = Convert.ToInt32(ddlProfile.SelectedValue);

                using (LoginService lgService = new LoginService())
                {
                    int retVal = lgService.AddLoginUser(objEmp);
                    if (retVal == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key", "ShowAlert('danger','HR User already present with same usercode for same department.');", true);
                    }
                    else
                    {
                        FillUserList();
                    }
                }

            }
            else if (hidID.Value != "")
            {
                LoginUser objEmp = new LoginUser();
                objEmp.UserCode = userCode.Value;
                objEmp.Password = password.Value;
                objEmp.DepartmentId = Convert.ToInt32(ddlDepartment.SelectedValue);
                objEmp.UserProfileId = Convert.ToInt32(ddlProfile.SelectedValue); ;
                objEmp.Id = Convert.ToInt32(hidID.Value);

                using (LoginService lgService = new LoginService())
                {
                    int retVal = lgService.EditLoginUser(objEmp);
                    if (retVal == -1)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key", "ShowAlert('danger','HR User already present with same usercode for same department.');", true);
                    }
                    else
                    {
                        FillUserList();
                    }
                }
                hidID.Value = "";
            }

            upCreateLoginUser.Update();
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key", "ShowAlert('success','Saved Successfully.');ResetCreateBox();", true);

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this.Page, this.Page.GetType(), "key", "ShowAlert('danger','Error occurred in current operation!! - " + ex.Message + "');", true);
        }
    }

    [WebMethod]
    public static int AddDepartment(string DepartmentName, string shortName)
    {
        using (DepartmentService DepartmentService = new DepartmentService())
        {
            Department objDepartment = new Department();
            objDepartment.DepartmentName = DepartmentName;
            objDepartment.ShortName = shortName;

            return DepartmentService.AddDepartment(objDepartment);

        }
    }

    [WebMethod]
    public static string GetLoginUserById(string id)
    {
        LoginUser lgUser = null;
        using (LoginService lgSer = new LoginService())
        {
            lgUser = lgSer.GetLoginUserbyId(Convert.ToInt32(id));
        }
        object[] arrObj = new object[1];
        arrObj[0] = new
        {
            ID = lgUser.Id.ToString(),
            UserCode = lgUser.UserCode,
            Password = lgUser.Password,
            Department = lgUser.Department.DepartmentName,
            DepartmentId = lgUser.DepartmentId

        };
        var json = new JavaScriptSerializer().Serialize(arrObj);
        return json;
    }
    [WebMethod]
    public static string DeleteLoginUser(string id)
    {
        int retVal = -1;
        using (LoginService loginSer = new LoginService())
        {
            retVal = loginSer.DeleteLognUserbyId(Convert.ToInt32(id));
        }
        if (retVal > 0)
        {

            return "success";
        }
        else
        {
            return "danger";
        }
    }
    protected void btnAutoUpdate_Click(object sender, EventArgs e)
    {
        FillUserList();
        upDatatable.Update();
    }


    protected void UpdateDdl(object sender, EventArgs e)
    {
        RebindDdlDepartment();
        upCreateLoginUser.Update();
    }
}