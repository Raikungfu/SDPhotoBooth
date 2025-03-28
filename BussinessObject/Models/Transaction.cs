using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class Transaction : BaseEntityNoAudit
    {
        public bool IsSuccess { get; set; } = false;

        [Required]
        public decimal Amount { get; set; }

        public int WalletId { get; set; }

        public virtual Payment? Payment { get; set; }

        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; }
    }
}
