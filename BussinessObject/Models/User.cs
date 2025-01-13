using BussinessObject.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class User : IdentityUser
    {
        public UserGender Gender { get; set; }

        public DateTime? BirthDate { get; set; }

        public decimal? Balance { get; set; }

        public string? Avatar { get; set; }

        public int? LocationId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public bool IsDeleted { get; set; } = false;

        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.Now;
        }

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

        public virtual ICollection<BoothStyle> CreatedBoothStyles { get; set; }
        public virtual ICollection<BoothStyle> LastModifiedBoothStyles { get; set; }
    }
}