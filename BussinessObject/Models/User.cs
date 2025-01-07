using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class User : BaseEntityNoAudit
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string PasswordHash { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required]
        public string UserType { get; set; }

        public DateTime? BirthDate { get; set; }

        public decimal? Balance { get; set; }
        public string? Avatar { get; set; }

        public int? LocationId { get; set; }

        [ForeignKey("LocationId")]
        public virtual Location Location { get; set; }

        public virtual ICollection<MembershipCard> MembershipCards { get; set; }

        public virtual ICollection<PhotoHistory> PhotoHistories { get; set; }

        public virtual ICollection<Order> Orders { get; set; }

        public virtual ICollection<Booth> CreatedBooths { get; set; }

        public virtual ICollection<Booth> LastModifiedBooths { get; set; }

        public virtual ICollection<Location> CreatedLocations { get; set; }

        public virtual ICollection<Location> LastModifiedLocations { get; set; }

        public virtual ICollection<TypeSession> CreatedTypeSessions { get; set; }

        public virtual ICollection<TypeSession> LastModifiedTypeSessions { get; set; }

        public virtual ICollection<Coupon> CreatedCoupons { get; set; }

        public virtual ICollection<Coupon> LastModifiedCoupons { get; set; }

        public virtual ICollection<PhotoStyle> CreatedPhotoStyles { get; set; }

        public virtual ICollection<PhotoStyle> LastModifiedPhotoStyles { get; set; }

        public virtual ICollection<PaymentMethod> CreatedPaymentMethods { get; set; }

        public virtual ICollection<PaymentMethod> LastModifiedPaymentMethods { get; set; }

        public virtual ICollection<LevelMemberShip> CreatedLevelMemberships { get; set; }

        public virtual ICollection<LevelMemberShip> LastModifiedLevelMemberships { get; set; }
    }
}
