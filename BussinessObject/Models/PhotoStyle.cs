using BussinessObject.Enums;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class PhotoStyle : BaseEntityNoAudit
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public ICollection<Photo> Photos { get; set; } = new List<Photo>();
    }
}