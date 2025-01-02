using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class MembershipCard : BaseEntity
    {
        public int CustomerId { get; set; }
        public int LevelMemberShipId { get; set; }
        public int Points { get; set; }
        public DateTime Expired { get; set; } = DateTime.Now.AddYears(1);
        public string? Description { get; set; }
        public bool IsActive { get; set; } = true;

        [ForeignKey("CustomerId")]
        public User Customer { get; set; }

        [ForeignKey("LevelMemberShipId")]
        public LevelMemberShip LevelMemberShip { get; set; }
    }
}
