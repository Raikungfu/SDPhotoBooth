using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class Order : BaseEntity
    {
        
        public string? UserId { get; set; }

        [Required]
        public int PaymentId { get; set; }

        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        public string? Email { get; set; }

        public string? Phone { get; set; }

        [Required]
        public string SessionId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("SessionId")]
        public virtual Session Session { get; set; }

        [ForeignKey("PaymentId")]
        public virtual PaymentMethod PaymentMethod { get; set; }
    }
}
