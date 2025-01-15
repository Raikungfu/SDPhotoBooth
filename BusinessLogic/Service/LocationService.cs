using AutoMapper;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class LocationService : ILocationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LocationService(IUnitOfWork unitOfWork, IMapper mapper) {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CreateLocation(LocationRequestDTO locationRequestDTO)
        {

            var location = _mapper.Map<Location>(locationRequestDTO);
            _unitOfWork.Location.Add(location);
            await _unitOfWork.SaveAsync();
            return location.Id;
        }

        public async Task<int> DeleteLocation(int id)
        {
            var location = await _unitOfWork.Location.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.Location.Remove(location);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<IEnumerable<LocationResponseDTO>> GetAllLocations()
        {
            var locations = await _unitOfWork.Location.GetAllAsync();
            var locationResponseDTOs = _mapper.Map<IEnumerable<LocationResponseDTO>>(locations); 
            return locationResponseDTOs;
        }

        public async Task<LocationResponseDTO> GetLocation(int id)
        {
            var location = await _unitOfWork.Location.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            var locationResponseDTO = _mapper.Map<LocationResponseDTO>(location);
            return locationResponseDTO;
        }

        public async Task<LocationResponseDTO> GetLocation(string locationName)
        {
            var location = await _unitOfWork.Location.GetFirstOrDefaultAsync(filter: l => l.LocationName == locationName);
            var locationResponseDTO = _mapper.Map<LocationResponseDTO>(location);
            return locationResponseDTO;
        }

        public async Task<LocationResponseDTO> UpdateLocation(int id, LocationRequestDTO locationRequestDTO)
        {
            var location = await _unitOfWork.Location.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            location = _mapper.Map(locationRequestDTO, location);
            _unitOfWork.Location.Update(location);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<LocationResponseDTO>(location);
        }
    }
}
