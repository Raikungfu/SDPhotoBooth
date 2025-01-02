using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class PhotoHistory : BaseEntityNoAudit
    {
        public int? BoothId { get; set; }
        public int? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public decimal Amount { get; set; }
        public int SessionId { get; set; }

        [ForeignKey("SessionId")]
        public Session Session { get; set; }

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        [ForeignKey("BoothId")]
        public Booth Booth { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
