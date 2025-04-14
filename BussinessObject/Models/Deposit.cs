using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BussinessObject.Models
{
    public class Deposit : BaseEntityNoAudit
    {
        public decimal Amount { get; set; }

        public int WalletId { get; set; }

        public DepositStatus Status { get; set; } = DepositStatus.Pending;

        public virtual Payment Payment { get; set; }

        [ForeignKey("WalletId")]
        public virtual Wallet Wallet { get; set; } = null!;
        public virtual ICollection<DepositProduct> DepositProducts { get; set; } = new List<DepositProduct>();
    }
}
