using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public abstract class BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public DateTime? CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastModified { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public int? CreatedById { get; set; }
        public int? LastModifiedById { get; set; }


        [NotMapped]
        [ForeignKey("CreatedById")]
        public virtual User CreatedBy { get; set; }

        [NotMapped]
        [ForeignKey("LastModifiedById")]
        public virtual User LastModifiedBy { get; set; }

        public void UpdateTimestamp()
        {
            LastModified = DateTime.Now;
        }
    }
}