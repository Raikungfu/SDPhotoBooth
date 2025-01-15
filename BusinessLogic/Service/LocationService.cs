using BusinessLogic.DTO.BoothStyleDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Mapster;

namespace BusinessLogic.Service
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;   
        public LocationService(IUnitOfWork unitOfWork) {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateLocation(LocationRequestDTO locationRequestDTO)
        {
            var location = locationRequestDTO.Adapt<Location>();
            _unitOfWork.Location.Add(location);
            await _unitOfWork.SaveAsync();
            return location.Id;
        }

        public async Task<int> DeleteLocation(int id)
        {
            var location = await _unitOfWork.Location.GetAsync(id);
            _unitOfWork.Location.Remove(location);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<IEnumerable<LocationResponseDTO>> GetAllLocations()
        {
            var locations = await _unitOfWork.Location.GetAllAsync(filter: l => !l.IsDeleted);
            var locationResponseDTOs = locations.Adapt<IEnumerable<LocationResponseDTO>>();
            return locationResponseDTOs;
        }

        public async Task<LocationResponseDTO> GetLocation(int id)
        {
            var location = await _unitOfWork.Location.GetAsync(id);
            if (location == null || location.IsDeleted)
            {
                return null;
            }
            var locationResponseDTO = location.Adapt<LocationResponseDTO>();
            return locationResponseDTO;
        }

        public async Task<LocationResponseDTO> GetLocation(string locationName)
        {
            var location = await _unitOfWork.Location.GetFirstOrDefaultAsync(filter: l => l.LocationName == locationName);
            if (location == null || location.IsDeleted)
            {
                return null;
            }
            var locationResponseDTO = location.Adapt<LocationResponseDTO>();
            return locationResponseDTO;
        }

        public async Task<LocationResponseDTO> UpdateLocation(int id, LocationRequestDTO locationRequestDTO)
        {
            var location = await _unitOfWork.Location.GetAsync(id);
            location = locationRequestDTO.Adapt(location);
            _unitOfWork.Location.Update(location);
            await _unitOfWork.SaveAsync();
            return location.Adapt<LocationResponseDTO>();
        }
    }
}
