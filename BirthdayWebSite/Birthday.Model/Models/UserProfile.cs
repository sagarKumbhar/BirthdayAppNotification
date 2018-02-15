using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public class UserProfile
    {
        [Key]
        public int Id { get; set; }
        public string Code { get; set; }
        public string ProfileName { get; set; }
    }
}
