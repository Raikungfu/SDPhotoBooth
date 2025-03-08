using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO.CouponDTO
{
    public class FilterCouponDTO
    {
        public string? Code { get; set; }
        public decimal? Discount { get; set; }
        public decimal? DiscountPercent { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MaxUse { get; set; }
        public int? UsedAmount { get; set; }
        public decimal? MaxDiscount { get; set; }
        public decimal? MinOrder { get; set; }
        public bool? IsActive { get; set; }
    }
}
