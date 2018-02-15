using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public class DistributionList
    {
        [Key]
        public int ID { get; set; }
        public string Recepients { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department Department { get; set; }
        public bool IsIncludeUsers { get; set; }


    }
}
