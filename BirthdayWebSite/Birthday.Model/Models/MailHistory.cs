using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public class MailHistory
    {
        public int Id { get; set; }
        public DateTime MailSentDate { get; set; }
        public int MailSentCount { get; set; }
        public int DepartmentId { get; set; }
        [ForeignKey("Department")]
        public virtual Department Department { get; set; }
    }
}
