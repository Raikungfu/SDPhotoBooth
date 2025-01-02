using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Photo : BaseEntityNoAudit
    {
        public int PhotoHistoryId { get; set; }
        public string Url { get; set; }
        public DateTime CreatedAt { get; set; }
        public int PhotoStyleId { get; set; }

        [ForeignKey("PhotoStyleId")]
        public PhotoStyle PhotoStyle { get; set; }

        [ForeignKey("PhotoHistoryId")]
        public PhotoHistory PhotoHistory { get; set; }
    }
}
