using System.ComponentModel.DataAnnotations;

namespace BussinessObject.Models
{
    public class PhotoStyle : BaseEntityNoAudit
    {
        [Required]
        public string Name { get; set; }

        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}