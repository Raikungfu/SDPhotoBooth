using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class MembershipCard : BaseEntity
    {
        [Required]
        public int CustomerId { get; set; }

        [Required]
        public int LevelMemberShipId { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Points must be a non-negative value.")]
        public int Points { get; set; }

        [Required]
        public DateTime Expired { get; set; } = DateTime.Now.AddYears(1);

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [ForeignKey("LevelMemberShipId")]
        public LevelMembership LevelMembership { get; set; }
    }
}
