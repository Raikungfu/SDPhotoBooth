using BusinessLogic.DTO.TypeSessionDTO;

namespace BusinessLogic.Service.IService
{
    public interface ITypeSessionService
    {
        Task<TypeSessionResponseDTO> GetTypeSessionCodeByIdAsync(int id);
        Task<TypeSessionResponseDTO> GetTypeSessionCodeByNameAsync(string name);
        Task<IEnumerable<TypeSessionResponseDTO>> GetTypeSessionCodesAsync();
        Task<IEnumerable<TypeSessionResponseDTO>> FilterTypeSessionCode(int? duration, decimal? price, bool? isPrinting, int? ableTakenNumber);
        Task<TypeSessionResponseDTO> CreateTypeSessionCodeAsync(TypeSessionRequestDTO typeSessionCodeDTO);
        Task<bool> UpdateTypeSessionCodeAsync(int id, TypeSessionRequestDTO typeSessionCodeDTO);
        Task<bool> DeleteTypeSessionCodeAsync(int id);

    }
}
