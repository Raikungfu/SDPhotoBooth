using AutoMapper;
using BusinessLogic.DTO.BoothStyleDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class BoothStyleService : IBoothStyleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public BoothStyleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<int> CreateBoothStyle(BoothStyleRequestDTO boothStyleRequestDTO)
        {
            var boothStyle = _mapper.Map<BoothStyle>(boothStyleRequestDTO);
            _unitOfWork.BoothStyle.Add(boothStyle);
            await _unitOfWork.SaveAsync();
            return boothStyle.Id;
        }

        public async Task<int> DeleteBoothStyle(int id)
        {
            var boothStyle = await _unitOfWork.BoothStyle.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.BoothStyle.Remove(boothStyle);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<IEnumerable<BoothStyleResponseDTO>> GetAllBoothStyles()
        {
            var boothStyles = await _unitOfWork.BoothStyle.GetAllAsync();
            var boothStyleResponseDTOs = _mapper.Map<IEnumerable<BoothStyleResponseDTO>>(boothStyles);
            return boothStyleResponseDTOs;
        }

        public async Task<BoothStyleResponseDTO> GetBoothStyle(int id)
        {
            var boothStyle = await _unitOfWork.BoothStyle.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            var boothStyleResponseDTO = _mapper.Map<BoothStyleResponseDTO>(boothStyle);
            return boothStyleResponseDTO;
        }

        public async Task<BoothStyleResponseDTO> UpdateBoothStyle(int id, BoothStyleRequestDTO boothStyleRequestDTO)
        {
            var boothStyle = await _unitOfWork.BoothStyle.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            boothStyle = _mapper.Map(boothStyleRequestDTO, boothStyle);
            _unitOfWork.BoothStyle.Update(boothStyle);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<BoothStyleResponseDTO>(boothStyle);
        }
    }
}
