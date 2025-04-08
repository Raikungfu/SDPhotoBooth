using BussinessObject.Enums;
using System.ComponentModel.DataAnnotations;

namespace BussinessObject.Models
{
    public class LevelMembership : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public int? Point { get; set; } = 0;

        public bool IsActive { get; set; } = true;

        public decimal? DiscountPercent { get; set; }

        public decimal? MaxDiscount { get; set; }

        public decimal? MinOrder { get; set; }

        public virtual ICollection<MembershipCard> MembershipCards { get; set; } = new List<MembershipCard>();
    }
}