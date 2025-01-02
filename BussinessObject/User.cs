using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class User : BaseEntityNoAudit
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string UserType { get; set; }
        public DateTime? BirthDate { get; set; }

        public decimal? Balance { get; set; }
        public string? Avatar { get; set; }
        
        public int? LocationId { get; set; }
        public int? MembershipCardId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        [ForeignKey("MembershipCardId")]
        public virtual ICollection<MembershipCard> MembershipCards { get; set; }

        public virtual ICollection<PhotoHistory> PhotoHistories { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }

        [NotMapped]
        public virtual ICollection<BaseEntity> CreatedEntities { get; set; }

        [NotMapped]
        public virtual ICollection<BaseEntity> UpdatedEntities { get; set; }
    }
}
