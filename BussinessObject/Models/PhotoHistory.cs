using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class PhotoHistory : BaseEntityNoAudit
    {
        public int? BoothId { get; set; }

        public int? CustomerId { get; set; }

        public int? LocationId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        [Required]
        public string SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session Session { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [ForeignKey("BoothId")]
        public Booth Booth { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
