using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BussinessObject
{
    public class Booth : BaseEntity
    {
        public int LocationId { get; set; }
        public string? BoothName { get; set; }
        public string? Description { get; set; }
        public bool Status { get; set; } = true;

        [ForeignKey("LocationId")]
        public Location Location { get; set; }

        public ICollection<PhotoHistory> PhotoHistories { get; set; }
    }
}