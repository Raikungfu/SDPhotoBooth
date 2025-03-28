using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class StickerStyle : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string StickerStyleName { get; set; }

        [MaxLength(100)]
        public string? Description { get; set; }

        public virtual ICollection<Sticker> Stickers { get; set; } = new List<Sticker>();
    }

}
