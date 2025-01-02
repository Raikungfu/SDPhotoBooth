using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject
{
    public class Transaction : BaseEntity
    {
        public int? UserId { get; set; }
        public int PaymentId { get; set; }
        public decimal Amount { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        [ForeignKey("PaymentId")]
        public virtual Payment Payment { get; set; }
    }
}
