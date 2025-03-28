using BusinessLogic.DTO.BoothDTO;

namespace BusinessLogic.Service.IService
{
    public interface IBoothService
    {
        Task<IEnumerable<BoothResponseDTO>> GetBooths();
        Task<BoothResponseDTO> GetBooth(int id);
        Task<BoothResponseDTO> GetBooth(string boothName);
        Task<BoothResponseDTO> CreateBooth(BoothRequestDTO boothRequestDTO);
        Task<BoothResponseDTO> UpdateBooth(int id, BoothRequestDTO boothRequestDTO);
        Task<bool> DeleteBooth(int id);
        Task<IEnumerable<BoothResponseDTO>> GetBoothByLocation(int locationId);
    }
}
