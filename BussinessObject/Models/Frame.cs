using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class Frame : BaseEntity
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        [Required]
        public string FrameUrl { get; set; }

        [Required]
        public int FrameStyleId { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "SlotCount must be at least 1.")]
        public int SlotCount { get; set; }

        [ForeignKey("FrameStyleId")]
        public virtual FrameStyle FrameStyle { get; set; }

        public virtual ICollection<Coordinate> Coordinates { get; set; } = new List<Coordinate>();
    }
}
