using BusinessLogic.DTO.LocationDTO;

namespace BusinessLogic.Service.IService
{
    public interface ILocationService
    {
        Task<int> CreateLocation(LocationRequestDTO locationRequestDTO);
        Task<LocationResponseDTO> UpdateLocation(int id, LocationRequestDTO locationRequestDTO);
        Task<int> DeleteLocation(int id);
        Task<LocationResponseDTO> GetLocation(int id);
        Task<IEnumerable<LocationResponseDTO>> GetLocation(string locationName);
        Task<IEnumerable<LocationResponseDTO>> GetAllLocations();
    }
}
