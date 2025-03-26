using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class Transaction : BaseEntity
    {
        public bool IsSuccess { get; set; }
        public decimal Amount { get; set; }
        public int WalletId { get; set; }
        public int OrderId { get; set; }
        public int PaymentMethodId { get; set; }
        [ForeignKey("WalletId")]
        public Wallet Wallet { get; set; }
        [ForeignKey("OrderId")]
        public Order Order { get; set; }
        [ForeignKey("PaymentMethodId")]
        public PaymentMethod PaymentMethod { get; set; }
    }
}
