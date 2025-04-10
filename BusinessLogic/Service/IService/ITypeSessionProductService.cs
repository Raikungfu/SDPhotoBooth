using BusinessLogic.DTO.TypeSessionProductDTO;
using DataAccess.Extensions.Pagination;

namespace BusinessLogic.Service.IService
{
    public interface ITypeSessionProductService
    {
        Task<IEnumerable<TypeSessionProductResponseDTO>> GetAllAsync(PaginationParams? pagination);
        Task<TypeSessionProductResponseDTO> GetByIdAsync(int id);
        Task<IEnumerable<TypeSessionProductResponseDTO>> GetByTypeSessionIdAsync(int typeSessionId);
        Task<IEnumerable<TypeSessionProductResponseDTO>> GetByProductIdAsync(string productId);
        Task<IEnumerable<TypeSessionProductResponseDTO>> GetByLevelMembershipIdAsync(int levelMembershipId);
        Task<TypeSessionProductResponseDTO> CreateAsync(TypeSessionProductCreateRequestDTO typeSessionProduct);
        Task<TypeSessionProductResponseDTO> UpdateAsync(int id, TypeSessionProductUpdateRequestDTO typeSessionProduct);
        Task<bool> DeleteAsync(int id);
    }
}
