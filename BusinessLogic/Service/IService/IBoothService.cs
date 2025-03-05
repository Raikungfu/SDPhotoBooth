using BusinessLogic.DTO.BoothDTO;

namespace BusinessLogic.Service.IService
{
    public interface IBoothService
    {
        Task<IEnumerable<BoothResponseDTO>> Filter(int? locationId, string? boothName, bool? status);
        Task<BoothResponseDTO> GetBooth(int id);
        Task<BoothResponseDTO> GetBooth(string boothName);
        Task<BoothResponseDTO> CreateBooth(BoothRequestDTO boothRequestDTO);
        Task<BoothResponseDTO> UpdateBooth(int id, BoothRequestDTO boothRequestDTO);
        Task<bool> DeleteBooth(int id);
    }
}
