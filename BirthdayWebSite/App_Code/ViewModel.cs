using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Birthday.Model;

/// <summary>
/// Summary description for ViewModel
/// </summary>
namespace Birthday.ViewModel
{

    public class ViewModel
    {
        public ViewModel()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }
    public class EmployeeViewModel
    {
        public string Id { get; set; }
        public string EmployeeName { get; set; }
        public string Email { get; set; }
        public string Birthdate { get; set; }
        public string EmpID { get; set; }

        public Employee LoadModel(EmployeeViewModel vmObj)
        {
            Employee emp = new Employee();
            emp.EmployeeName = vmObj.EmployeeName;
            emp.EmployeeID = Convert.ToInt32(vmObj.EmpID);
            emp.Email = vmObj.Email;
            emp.Birthdate = vmObj.Birthdate != "" ? Convert.ToDateTime(vmObj.Birthdate) : (DateTime?)null;
            return emp;
        }
    }
    public class LoginUserViewModel
    {
        public string Usercode { get; set; }
        public string Password { get; set; }
        public int DepartmentID { get; set; }



        public LoginUser LoadModel(LoginUserViewModel usr)
        {
            return null;
        }

    }

    public class DepartmentViewModel
    {
        public string DepartmentName { get; set; }
        public string shortname { get; set; }


    }
}