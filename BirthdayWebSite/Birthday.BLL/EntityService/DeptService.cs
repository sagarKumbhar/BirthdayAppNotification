using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Birthday.Data;
using Birthday.Model;
/*
   using (AppContext ctx = new AppContext())
            {
     
     */
namespace Birthday.BLL.EntityService
{
    public class DepartmentService : IDisposable
    {
        public int AddDepartment(Department objDepartment)
        {
            using (AppContext ctx = new AppContext())
            {
                bool isDepartmentPresent = ctx.Departments.Any(a => a.DepartmentName == objDepartment.DepartmentName || a.ShortName == objDepartment.ShortName);
                if (!isDepartmentPresent)
                {
                    ctx.Departments.Add(objDepartment);
                    return ctx.SaveChanges();
                }
                else
                    return -1;
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
            using (AppContext ctx = new AppContext())
            {
                var Departments = ctx.Departments.ToList();
                return Departments;
            }
        }
    }
}
