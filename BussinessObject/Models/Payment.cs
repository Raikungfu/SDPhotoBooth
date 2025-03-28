using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class Payment : BaseEntityNoAudit 
    {
        public int? OrderId { get; set; }

        public int? TransactionId { get; set; }

        [Required]
        public int PaymentMethodId { get; set; }

        public bool IsSuccess { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        [ForeignKey("TransactionId")]
        public virtual Transaction Transaction { get; set; }

        [ForeignKey("PaymentMethodId")]
        public virtual PaymentMethod PaymentMethod { get; set; }

        [ForeignKey("OrderId")]
        public virtual Order Order { get; set; }
    }
}