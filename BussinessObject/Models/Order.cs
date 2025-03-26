using BussinessObject.Enums;

namespace BussinessObject.Models
{
    public class Order : BaseEntity
    {
        public long Code { get; set; } = new Random().NextInt64(1000000000, 9999999999);
        public OrderStatus? Status { get; set; } = OrderStatus.Pending;
        public decimal Amount { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? UserId { get; set; }
        public int? CouponId { get; set; }
        public int TransactionId { get; set; }
        public int TypeSessionId { get; set; }
        public int SessionId { get; set; }

        public User User { get; set; }
        public Coupon Coupon { get; set; }
        public Transaction Transaction { get; set; }
        public TypeSession TypeSession { get; set; }
        public Session Session { get; set; }
    }
}
