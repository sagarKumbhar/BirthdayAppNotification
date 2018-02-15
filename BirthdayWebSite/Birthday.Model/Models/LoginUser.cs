using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public class LoginUser:AbstractTemplate
    {

        [Required, MaxLength(10)]
        public string UserCode { get; set; }

        [Required, MaxLength(10)]
        public string Password { get; set; }

        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }

        [ForeignKey("Profile")]
        public int UserProfileId { get; set; }
        public virtual UserProfile Profile { get; set; }
    }

}
