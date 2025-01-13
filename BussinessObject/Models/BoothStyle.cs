using System.ComponentModel.DataAnnotations;

namespace BussinessObject.Models
{
    public class BoothStyle : BaseEntity
    {
        [Required]
        [StringLength(100)]
        public string StyleName { get; set; }
        [StringLength(500)]
        public string? Description { get; set; }
        public ICollection<Booth> Booths { get; set; } = new List<Booth>();
        public ICollection<PhotoStyle> PhotoStyles { get; set; } = new List<PhotoStyle>();

    }
}
