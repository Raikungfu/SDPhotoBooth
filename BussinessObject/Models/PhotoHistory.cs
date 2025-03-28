using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject.Models
{
    public class PhotoHistory : BaseEntityNoAudit
    {
        public string? CustomerId { get; set; }

        public int? LocationId { get; set; }

        public int? BoothId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        [Required]
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session Session { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [ForeignKey("BoothId")]
        public Booth Booth { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
