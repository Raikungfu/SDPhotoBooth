using BusinessLogic.DTO.BoothStyleDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Mapster;

namespace BusinessLogic.Service
{
    public class BoothStyleService : IBoothStyleService
    {
        private readonly IUnitOfWork _unitOfWork;
        public BoothStyleService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<int> CreateBoothStyle(BoothStyleRequestDTO boothStyleRequestDTO)
        {
            var boothStyle = boothStyleRequestDTO.Adapt<BoothStyle>();
            _unitOfWork.BoothStyle.Add(boothStyle);
            await _unitOfWork.SaveAsync();
            return boothStyle.Id;
        }

        public async Task<int> DeleteBoothStyle(int id)
        {
            var boothStyle = await _unitOfWork.BoothStyle.GetAsync(id);
            _unitOfWork.BoothStyle.Remove(boothStyle);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<IEnumerable<BoothStyleResponseDTO>> GetAllBoothStyles()
        {
            var boothStyles = await _unitOfWork.BoothStyle.GetAllAsync(filter: b => !b.IsDeleted);
            var boothStyleResponseDTOs = boothStyles.Adapt<IEnumerable<BoothStyleResponseDTO>>();
            return boothStyleResponseDTOs;
        }

        public async Task<BoothStyleResponseDTO> GetBoothStyle(int id)
        {
            var boothStyle = await _unitOfWork.BoothStyle.GetAsync(id);
            if (boothStyle == null || boothStyle.IsDeleted)
            {
                return null;
            }
            var boothStyleResponseDTO = boothStyle.Adapt<BoothStyleResponseDTO>();
            return boothStyleResponseDTO;
        }

        public async Task<BoothStyleResponseDTO> UpdateBoothStyle(int id, BoothStyleRequestDTO boothStyleRequestDTO)
        {
            var boothStyle = await _unitOfWork.BoothStyle.GetAsync(id);
            boothStyle = boothStyleRequestDTO.Adapt(boothStyle);
            _unitOfWork.BoothStyle.Update(boothStyle);
            await _unitOfWork.SaveAsync();
            return boothStyle.Adapt<BoothStyleResponseDTO>();
        }
    }
}
