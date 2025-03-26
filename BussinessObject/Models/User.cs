using BussinessObject.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }
        public UserGender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Avatar { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
        public bool IsDeleted { get; set; } = false;
        public bool IsBanned { get; set; } = false;
        public int? LocationId { get; set; }
        public int WalletId { get; set; }

        public virtual Location? Location { get; set; }
        public virtual Wallet Wallet { get; set; }
        public virtual ICollection<MembershipCard> MembershipCards { get; set; }
        public virtual ICollection<PhotoHistory> PhotoHistories { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<LevelMemberShip> CreatedLevelMemberships { get; set; }
        public virtual ICollection<LevelMemberShip> LastModifiedLevelMemberships { get; set; }
        public virtual ICollection<MembershipCard> CreatedMembershipCards { get; set; }
        public virtual ICollection<MembershipCard> LastModifiedMembershipCards { get; set; }
        public virtual ICollection<Booth> CreatedBooths { get; set; }
        public virtual ICollection<Booth> LastModifiedBooths { get; set; }
        public virtual ICollection<Location> CreatedLocations { get; set; }
        public virtual ICollection<Location> LastModifiedLocations { get; set; }
        public virtual ICollection<TypeSession> CreatedTypeSessions { get; set; }
        public virtual ICollection<TypeSession> LastModifiedTypeSessions { get; set; }
        public virtual ICollection<Coupon> CreatedCoupons { get; set; }
        public virtual ICollection<Coupon> LastModifiedCoupons { get; set; }
        public virtual ICollection<PaymentMethod> CreatedPaymentMethods { get; set; }
        public virtual ICollection<PaymentMethod> LastModifiedPaymentMethods { get; set; }
        public virtual ICollection<Order> CreatedOrders { get; set; }
        public virtual ICollection<Order> LastModifiedOrders { get; set; }

        public void UpdateTimestamp()
        {
            UpdatedAt = DateTime.Now;
        }
    }
}