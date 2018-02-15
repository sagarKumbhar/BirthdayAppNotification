using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Birthday.Model
{
    public class Employee:AbstractTemplate
    {
     
        [Required]
        public int EmployeeID { get; set; }
        public string Email { get; set; }
        public DateTime? Birthdate { get; set; }
        public DateTime? JoiningDate { get; set; }
        public DateTime? MarriageAnnivarsary { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        [Required, MaxLength(100)]
        public string EmployeeName { get; set; }
        public string PhotoLocation { get; set; }
    }
}
