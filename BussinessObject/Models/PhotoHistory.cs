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

        public string? CustomerId { get; set; }

        public int? LocationId { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Amount must be a non-negative value.")]
        public decimal Amount { get; set; }

        public string SessionCode { get; set; }

        [ForeignKey(nameof(SessionCode))]
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
