using BusinessLogic.DTO.DepositProductDTO;
using BusinessLogic.DTO.TypeSessionProductDTO;
using DataAccess.Extensions.Pagination;

namespace BusinessLogic.Service.IService
{
    public interface IDepositProductService
    {
        Task<IEnumerable<DepositProductResponseDTO>> GetAllAsync(PaginationParams? pagination);
        Task<IEnumerable<DepositProductResponseDTO>> GetAllIncludeAsync();
        Task<DepositProductResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<DepositProductResponseDTO>> GetByDepositIdAsync(int depositId);
        Task<IEnumerable<DepositProductResponseDTO>> GetByProductIdAsync(string productId);
        Task<IEnumerable<DepositProductResponseDTO>> GetByLevelMembershipIdAsync(int levelMembershipId);
        Task<IEnumerable<DepositProductResponseDTO>> GetByCouponAndDepositIdAsync(int couponId, int depositId);
        Task<DepositProductResponseDTO> CreateAsync(DepositProductCreateRequestDTO depositProduct);
        Task<DepositProductResponseDTO> UpdateAsync(int id, DepositProductUpdateRequestDTO depositProduct);
        Task<bool> DeleteAsync(int id);
    }
}
