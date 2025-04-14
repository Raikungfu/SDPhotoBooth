using AutoMapper;
using BusinessLogic.DTO.DepositProductDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class DepositProductService : IDepositProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepositProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DepositProductResponseDTO> CreateAsync(DepositProductCreateRequestDTO depositProduct)
        {
            var depositProductEntity = _mapper.Map<DepositProduct>(depositProduct);
            await _unitOfWork.DepositProduct.AddAsync(depositProductEntity);
            await _unitOfWork.SaveAsync();
            var depositProductDTO = _mapper.Map<DepositProductResponseDTO>(depositProductEntity);
            return depositProductDTO;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var depositProduct = await _unitOfWork.DepositProduct.GetFirstOrDefaultAsync(s => s.Id == id);
            if (depositProduct == null)
            {
                return false;
            }
            _unitOfWork.DepositProduct.Remove(depositProduct);
            await _unitOfWork.SaveAsync();
            return true;

        }

        public async Task<IEnumerable<DepositProductResponseDTO>> GetAllAsync(PaginationParams? pagination)
        {
            var depositProducts = await _unitOfWork.DepositProduct.GetAllAsync(pagination: pagination);
            var depositProductDTOs = _mapper.Map<IEnumerable<DepositProductResponseDTO>>(depositProducts);
            return depositProductDTOs;

        }

        public async Task<IEnumerable<DepositProductResponseDTO>> GetAllIncludeAsync()
        {
            var depositProducts = await _unitOfWork.DepositProduct.GetAllAsync(includeProperties: "LevelMembership,Deposit,Coupon");
            var depositProductDTOs = _mapper.Map<IEnumerable<DepositProductResponseDTO>>(depositProducts);
            return depositProductDTOs;
        }

        public async Task<DepositProductResponseDTO> GetByIdAsync(int id)
        {
            var depositProduct = await _unitOfWork.DepositProduct.GetFirstOrDefaultAsync(s => s.Id == id);
            if (depositProduct == null)
            {
                return null;
            }
            var depositProductDTO = _mapper.Map<DepositProductResponseDTO>(depositProduct);
            return depositProductDTO;
        }

        
        public async Task<IEnumerable<DepositProductResponseDTO>> GetByProductIdAsync(string productId)
        {
            var depositProducts = await _unitOfWork.DepositProduct.GetAllAsync(s => s.ProductId == productId, includeProperties: "LevelMembership,Deposit");
            var depositProductDTOs = _mapper.Map<IEnumerable<DepositProductResponseDTO>>(depositProducts);
            return depositProductDTOs;
        }

    }
}
