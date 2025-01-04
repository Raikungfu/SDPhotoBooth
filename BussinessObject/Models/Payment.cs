using System.ComponentModel.DataAnnotations;

namespace BussinessObject.Models
{
    public class Payment : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "MethodName cannot exceed 100 characters.")]
        public string MethodName { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
