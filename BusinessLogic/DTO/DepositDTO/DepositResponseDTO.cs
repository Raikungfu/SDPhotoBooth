using BussinessObject.Enums;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.DepositDTO
{
    public class DepositResponseDTO
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public int PaymentId { get; set; }
        public int WalletId { get; set; }
        public DepositStatus Status { get; set; } = DepositStatus.Pending;
        public string? PaymentMethodName { get; set; }
        public string? WalletName { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime LastModified { get; set; }
    }
}