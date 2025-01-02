using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class PhotoStyle : BaseEntity
    {
        public string Type { get; set; }
        public string? Description { get; set; }

        public ICollection<Photo> Photos { get; set; }
    }
}
