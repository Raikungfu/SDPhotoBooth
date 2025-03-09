using BussinessObject.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class Order : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public new long Id { get; set; } = new Random().NextInt64(1000000000, 9999999999);

        public string? UserId { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }

        public OrderStatus? Status { get; set; } = OrderStatus.Pending;

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        public int? CouponId { get; set; }

        [Required]
        public int TypeSessionId { get; set; }

        public string? SessionId { get; set; }

        [ForeignKey("TypeSessionId")]
        public virtual TypeSession TypeSession { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }

        [ForeignKey("PaymentId")]
        public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("CouponId")]
        public virtual Coupon Coupon { get; set; }
    }
}
