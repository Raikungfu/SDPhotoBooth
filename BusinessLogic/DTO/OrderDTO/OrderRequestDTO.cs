using BussinessObject.Enums;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.OrderDTO
{
    public class OrderRequestDTO
    {
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public decimal? Amount { get; set; }
        public string? CouponCode { get; set; }
        public int TypeSessionId { get; set; }
        public int PaymentMethodId { get; set; }
        public string? SessionId { get; set; }

        public string? UserId { get; set; }
        public int? CouponId { get; set; }
        public OrderStatus? Status { get; set; } = OrderStatus.Pending;
    }


    public class OrderStatusCodeRequestDTO
    {
        [Required]
        public long OrderCode { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }

    public class OrderStatusRequestDTO
    {
        [Required]
        public int OrderId { get; set; }
        [Required]
        public OrderStatus Status { get; set; }
    }
}
