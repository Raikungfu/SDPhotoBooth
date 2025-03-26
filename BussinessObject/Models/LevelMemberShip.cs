using BussinessObject.Enums;

namespace BussinessObject.Models
{
    public class LevelMemberShip : BaseEntity
    {
        public Level Name { get; set; }
        public string Description { get; set; }

        public decimal? Price { get; set; }
        public int? Duration { get; set; }
        public int? Point { get; set; }
        public bool IsActive { get; set; } = true;
        public decimal? DiscountPercent { get; set; }
        public decimal? MaxDiscount { get; set; }
        public decimal? MinOrder { get; set; }

        public ICollection<MembershipCard> MembershipCards { get; set; } = new List<MembershipCard>();
    }
}
