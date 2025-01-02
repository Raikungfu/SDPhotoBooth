using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class LevelMemberShip : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Duration { get; set; }
        public int? Point { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
