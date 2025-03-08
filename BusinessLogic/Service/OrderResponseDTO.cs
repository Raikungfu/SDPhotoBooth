using BussinessObject.Enums;
using BussinessObject.Models;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.Service
{
    public class OrderResponseDTO
    {
        public int Id { get; set; }
        public string? UserId { get; set; }
        public long? OrderCode { get; set; }
        public int PaymentId { get; set; }
        public string PaymentName { get; set; }
        public OrderStatus? Status { get; set; }
        public decimal Amount { get; set; }
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public int? CouponId { get; set; }
        public int TypeSessionId { get; set; }
        public string TypeSessionName { get; set; }
        public string SessionId { get; set; }
        public string SessionCode { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public bool IsDeleted { get; set; }
        public string? CreatedById { get; set; }
        public string? LastModifiedById { get; set; }
    }
}