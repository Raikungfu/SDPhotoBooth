using System.ComponentModel.DataAnnotations;

namespace BussinessObject.Models
{
    public class LevelMemberShip : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public decimal? Price { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Duration must be at least 1.")]
        public int? Duration { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Point must be non-negative.")]
        public int? Point { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<MembershipCard> MembershipCards { get; set; } = new List<MembershipCard>();
    }
}
