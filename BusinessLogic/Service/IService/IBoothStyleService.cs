using BusinessLogic.DTO.BoothStyleDTO;

namespace BusinessLogic.Service.IService
{
    public interface IBoothStyleService
    {
        Task<int> CreateBoothStyle(BoothStyleRequestDTO boothStyleRequestDTO);
        Task<BoothStyleResponseDTO> UpdateBoothStyle(int id, BoothStyleRequestDTO boothStyleRequestDTO);
        Task<int> DeleteBoothStyle(int id);
        Task<BoothStyleResponseDTO> GetBoothStyle(int id);
        Task<IEnumerable<BoothStyleResponseDTO>> GetAllBoothStyles();
    }
}
