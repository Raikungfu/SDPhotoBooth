namespace BussinessObject.Models
{
    public class Coupon : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MaxUse { get; set; }
        public int? UsedAmount { get; set; } = 0;
        public decimal? MaxDiscount { get; set; }
        public decimal? MinOrder { get; set; }
        public bool IsActive { get; set; } = true;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }

}
