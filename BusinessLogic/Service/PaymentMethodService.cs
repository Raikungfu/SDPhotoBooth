using AutoMapper;
using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class PaymentMethodService : IPaymentMethodService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PaymentMethodService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CreatePaymentMethod(PaymentMethodRequestDTO paymentMethodRequestDTO)
        {
            var paymentMethod = _mapper.Map<PaymentMethod>(paymentMethodRequestDTO);
            _unitOfWork.PaymentMethod.Add(paymentMethod);
            await _unitOfWork.SaveAsync();
            return paymentMethod.Id;
        }

        public async Task<int> DeletePaymentMethod(int id)
        {
            var paymentMethod = await _unitOfWork.PaymentMethod.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.PaymentMethod.Remove(paymentMethod);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<IEnumerable<PaymentMethodResponseDTO>> GetAllPaymentMethod()
        {
            var paymentMethods =await _unitOfWork.PaymentMethod.GetAllAsync();
            var paymentMethodResponseDTOs = _mapper.Map<IEnumerable<PaymentMethodResponseDTO>>(paymentMethods);
            return paymentMethodResponseDTOs;
        }

        public async Task<PaymentMethodResponseDTO> GetPaymentMethod(int id)
        {
            var paymentMethod = await _unitOfWork.PaymentMethod.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            var paymentMethodResponseDTO = _mapper.Map<PaymentMethodResponseDTO>(paymentMethod);
            return paymentMethodResponseDTO;
        }

        public async Task<PaymentMethodResponseDTO> GetPaymentMethod(string paymentMethodName)
        {
            var paymentMethod = await _unitOfWork.PaymentMethod.GetFirstOrDefaultAsync(filter: e => e.MethodName == paymentMethodName);
            var paymentMethodResponseDTO = _mapper.Map<PaymentMethodResponseDTO>(paymentMethod);
            return paymentMethodResponseDTO;
        }

        public async Task<PaymentMethodResponseDTO> UpdatePaymentMethod(int id, PaymentMethodRequestDTO paymentMethodRequestDTO)
        {
            var paymentMethod = await _unitOfWork.PaymentMethod.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            if (!string.IsNullOrEmpty(paymentMethodRequestDTO.MethodName))
            {
                paymentMethod.MethodName = paymentMethodRequestDTO.MethodName;
            }
            if (paymentMethodRequestDTO.IsActive.HasValue)
            {
                paymentMethod.IsActive = paymentMethodRequestDTO.IsActive.Value;
            }
            if (!string.IsNullOrEmpty(paymentMethodRequestDTO.Description))
            {
                paymentMethod.Description = paymentMethodRequestDTO.Description;
            }
            _unitOfWork.PaymentMethod.Update(paymentMethod);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PaymentMethodResponseDTO>(paymentMethod);
        }
    }
}
