using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO.DepositProductDTO
{
    public class DepositProductCreateRequestDTO
    {
        public string? Name { get; set; }
        [Required]
        public string ProductId { get; set; }
        public int? LevelMembershipId { get; set; }
        public int DepositId { get; set; }
        public int? CouponId { get; set; }
    }

    public class DepositProductUpdateRequestDTO
    {
        public string? Name { get; set; }
        public string? ProductId { get; set; }
        public int? LevelMembershipId { get; set; }
        public int? DepositId { get; set; }
        public int? CouponId { get; set; }
    }
}
