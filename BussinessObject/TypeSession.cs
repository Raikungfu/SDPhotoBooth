using System.ComponentModel.DataAnnotations;

namespace BussinessObject
{

    public class TypeSession : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Duration must be a non-negative value.")]
        public int? Duration { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "AbleTakenNumber must be a non-negative value.")]
        public int? AbleTakenNumber { get; set; }

        public ICollection<Session> Sessions { get; set; } = new List<Session>();
    }

}