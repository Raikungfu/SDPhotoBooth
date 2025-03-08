using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.DTO.SessionCodeDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.UserDTO;
using BussinessObject.Enums;
using BussinessObject.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Service
{
    public class OrderResponseDTO
    {
        public long Id { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal Amount { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedById { get; set; }
        public string? LastModifiedById { get; set; }
        public UserResponseDTO? User { get; set; }
        public SessionResponseDTO? Session { get; set; }
        public PaymentMethodResponseDTO? PaymentMethod { get; set; }
        public CouponResponseDTO? Coupon { get; set; }
    }
}