using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.DTO.LevelMembershipDTO;

namespace BusinessLogic.DTO.DepositProductDTO
{
    public class DepositProductResponseDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public string? CreatedById { get; set; }
        public string? LastModifiedById { get; set; }
        public string? ProductId { get; set; }
        public int? LevelMembershipId { get; set; }
        public int? DepositId { get; set; }
        public int? CouponId { get; set; }
        public LevelMembershipResponseDTO? LevelMembership { get; set; }
        public DepositResponseDTO Deposit { get; set; }
        public CouponResponseDTO? Coupon { get; set; }
        public decimal? Discount => CountDiscount();

        private decimal? CountDiscount()
        {
            if (Deposit == null || (LevelMembership == null && Coupon == null))
            {
                return null;
            }

            decimal discount = 0;
            if (LevelMembership != null)
            {
                if (Deposit.Amount >= LevelMembership.MinOrder)
                {
                    discount = Deposit.Amount * LevelMembership.DiscountPercent ?? 0;
                    return Math.Min(discount, LevelMembership.MaxDiscount ?? int.MaxValue);
                }
            }
            else if (Coupon != null)
            {
                if (Deposit.Amount >= Coupon.MinOrder)
                {
                    discount = Coupon.DiscountPercent != null ? Deposit.Amount * Coupon.DiscountPercent ?? 0 : Coupon.Discount ?? 0;
                    return Math.Min(discount, Coupon.MaxDiscount ?? int.MaxValue);
                }
            }

            return discount;
        }
    }
}
