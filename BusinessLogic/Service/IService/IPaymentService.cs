using BusinessLogic.DTO.PaymentDTO;
using BusinessLogic.DTO.PaymentMethod;
using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.IService
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentResponseDTO>> GetAllPayments();
        Task<PaymentResponseDTO> GetPaymentById(int id);
        Task<PaymentResponseDTO> GetPaymentByCusId(string cusId);
        Task<PaymentResponseDTO> CreatePaymentForOrder(PaymentRequestDTO paymentRequest);
        Task<PaymentResponseDTO> CreatePaymentForDeposit(PaymentRequestDTO paymentRequest);
        Task<PaymentResponseDTO> UpdatePaymentStatusByOrderId(int orderId, PaymentStatus paymentStatus);
        Task<PaymentResponseDTO> UpdatePaymentStatusByDepositId(int depositId, PaymentStatus paymentStatus);
    }
}
