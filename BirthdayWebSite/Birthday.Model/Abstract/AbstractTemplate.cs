using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Birthday.Model
{
    public abstract class AbstractTemplate
    {
        [Key]
        public int Id { get; set; }
        public DateTime? CreatedDate { get; set; }
        [ForeignKey("CreatedByUser")]
        public int CreatedBy { get; set; }
        public virtual LoginUser CreatedByUser { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        [ForeignKey("ModifiedByUser")]
        public int ModifiedBy { get; set; }
        public virtual LoginUser ModifiedByUser { get; set; }
    }
}
