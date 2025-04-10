using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class TypeSessionProduct : BaseEntity
    {
        public string? Name { get; set; }

        [Required]
        public string ProductId { get; set; } 

        [Required]
        public int LevelMembershipId { get; set; }

        [ForeignKey("LevelMembershipId")]
        public virtual LevelMembership LevelMembership { get; set; }

        public int TypeSessionId { get; set; }

        [ForeignKey("TypeSessionId")]
        public virtual TypeSession TypeSession { get; set; }
    }
}
