using AutoMapper;
using BusinessLogic.DTO.BoothDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class BoothService : IBoothService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BoothService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BoothResponseDTO> CreateBooth(BoothRequestDTO boothRequestDTO)
        {
            var isExist = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.BoothName == boothRequestDTO.BoothName);
            if (isExist != null)
            {
                return null;
            }
            var boothDTO = _mapper.Map<Booth>(boothRequestDTO);
            await _unitOfWork.Booth.AddAsync(boothDTO);
            await _unitOfWork.SaveAsync();
            var booth = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.Id == boothDTO.Id, includeProperties: "Location");
            var boothResponseDTO = _mapper.Map<BoothResponseDTO>(booth);
            return boothResponseDTO;
        }

        public async Task<bool> DeleteBooth(int id)
        {
            var booth = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.Id == id);
            _unitOfWork.Booth.Remove(booth);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<BoothResponseDTO>> Filter(int? locationId, string? boothName, bool? status)
        {
            var booths = await _unitOfWork.Booth.GetAllAsync(includeProperties: "Location");
            if (locationId != null)
            {
                booths = booths.Where(b => b.LocationId == locationId);
            }
            if (!string.IsNullOrEmpty(boothName))
            {
                booths = booths.Where(b => b.BoothName.Contains(boothName));
            }
            if (status != null)
            {
                booths = booths.Where(b => b.Status == status);
            }
            var boothResponseDTOs = _mapper.Map<IEnumerable<BoothResponseDTO>>(booths);
            return boothResponseDTOs;
        }

        public async Task<BoothResponseDTO> GetBooth(int id)
        {
            var booth = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.Id == id, includeProperties: "Location");
            if (booth == null)
            {
                return null;
            }
            var boothResponseDTO = _mapper.Map<BoothResponseDTO>(booth);
            return boothResponseDTO;
        }

        public async Task<BoothResponseDTO> GetBooth(string boothName)
        {
            var booth = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.BoothName == boothName, includeProperties: "Location");
            if (booth == null)
            {
                return null;
            }
            var boothResponseDTO = _mapper.Map<BoothResponseDTO>(booth);
            return boothResponseDTO;
        }

        public async Task<BoothResponseDTO> UpdateBooth(int id, BoothRequestDTO boothRequestDTO)
        {
            var booth = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.Id == id, includeProperties:"Location");

            if (!string.IsNullOrEmpty(boothRequestDTO.BoothName))
            {
                booth.BoothName = boothRequestDTO.BoothName;
            }
            if (!string.IsNullOrEmpty(boothRequestDTO.Description))
            {
                booth.Description = boothRequestDTO.Description;
            }
            if (boothRequestDTO.LocationId.HasValue)
            {
                var isExistLocation = await _unitOfWork.Location.GetFirstOrDefaultAsync(l => l.Id == boothRequestDTO.LocationId);
                if (isExistLocation == null)
                {
                    return null;
                }
                booth.LocationId = boothRequestDTO.LocationId ?? booth.LocationId;
            }
            if (boothRequestDTO.Status.HasValue)
            {
                booth.Status = boothRequestDTO.Status ?? booth.Status;
            }
            await _unitOfWork.SaveAsync();
            var boothResponseDTO = _mapper.Map<BoothResponseDTO>(booth);
            return boothResponseDTO;
        }
    }
}
