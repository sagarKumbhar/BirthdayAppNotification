using System;
using System.Web.UI;
using Birthday.BLL;
using Birthday.BLL.EntityService;
using Birthday.Model;
using Birthday.WebHelper;

public partial class ManageDistribution : BasePage
{
    SessionService sessionser = new SessionService();
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            FillDetails();
        }
    }

    private void FillDetails()
    {
        using (EmployeeService empSer = new EmployeeService())
        {
            DistributionList dsList = empSer.GetDistributionListByDepartmentId(sessionser.CurrentUser.DepartmentId);
            if (dsList != null)
            {
                chkIncludeAddedUsers.Checked = dsList.IsIncludeUsers;
                txtUSers.Text = dsList.Recepients;
            }
            else
            {
                chkIncludeAddedUsers.Checked = true;
            }
            lblDepartment.Text = sessionser.CurrentUser.Department.DepartmentName;

        }
    }

    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        using (EmployeeService empSer = new EmployeeService())
        {
            DistributionList dsList = new DistributionList();
            dsList.IsIncludeUsers = chkIncludeAddedUsers.Checked;
            dsList.Recepients = txtUSers.Text;
            dsList.DepartmentId = sessionser.CurrentUser.DepartmentId;
            empSer.EditDistributionList(dsList);

        }
    }
}