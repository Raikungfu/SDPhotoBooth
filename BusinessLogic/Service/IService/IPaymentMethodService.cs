using BusinessLogic.DTO.PaymentMethod;

namespace BusinessLogic.Service.IService
{
    public interface IPaymentMethodService
    {
        Task<int> CreatePaymentMethod(PaymentMethodRequestDTO paymentMethodRequestDTO);
        Task<PaymentMethodResponseDTO> UpdatePaymentMethod(int id, PaymentMethodRequestDTO paymentMethodRequestDTO);
        Task<int> DeletePaymentMethod(int id);
        Task<PaymentMethodResponseDTO> GetPaymentMethod(int id);
        Task<PaymentMethodResponseDTO> GetPaymentMethod(string paymentMethodName);
        Task<IEnumerable<PaymentMethodResponseDTO>> GetAllPaymentMethod();
        Task<int> DisablePaymentMethod(int id);
    }
}
