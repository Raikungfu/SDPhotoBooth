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
    public class DepositRequestDTO
    {
        public decimal Amount { get; set; }
        public int PaymentMethodId { get; set; }
        public DepositStatus Status { get; set; } = DepositStatus.Pending;
        public string? Description { get; set; } = null;
    }
}
