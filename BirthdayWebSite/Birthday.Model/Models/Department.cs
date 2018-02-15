using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public class Department:AbstractTemplate
    {
      
        [Required, MaxLength(50)]
        public string DepartmentName { get; set; }

        [Required, MaxLength(15)]
        public string ShortName { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }

        public virtual ICollection<LoginUser> LoginUsers { get; set; }

        public virtual ICollection<DistributionList> DistributionList { get; set; }
    }
}
