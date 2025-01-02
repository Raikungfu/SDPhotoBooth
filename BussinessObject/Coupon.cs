using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Coupon : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Code { get; set; }
        public decimal Discount { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public int? MaxUse { get; set; }
        public int? MaxUsePerUser { get; set; }
        public int? MaxDiscount { get; set; }
        public int? MinOrder { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
