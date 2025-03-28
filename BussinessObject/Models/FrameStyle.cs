using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class FrameStyle : BaseEntity
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        public string? ImageUrl { get; set; }

        public virtual ICollection<Frame> Frames { get; set; } = new List<Frame>();
    }
}
