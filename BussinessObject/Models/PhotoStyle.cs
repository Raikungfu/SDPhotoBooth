using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class PhotoStyle : BaseEntity
    {
        [Required]
        [StringLength(100, ErrorMessage = "Type cannot exceed 100 characters.")]
        public Style Type { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string? Description { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}
