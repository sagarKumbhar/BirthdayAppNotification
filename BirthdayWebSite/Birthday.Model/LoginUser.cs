using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public class LoginUser
    {
        public int Id { get; set; }
        public string UserCode { get; set; }
        public string Password { get; set; }
        public string DeptName { get; set; }
        public int DeptId { get; set; }
        public int UserProfileId { get; set; }
        public string ProfileName { get; set; }
    }
}
