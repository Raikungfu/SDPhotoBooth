using AutoMapper;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.PhotoHistoryDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class PhotoHistoryService : IPhotoHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _contextAccessor;

        public PhotoHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PhotoHistoryResponseDTO>> GetAll()
        {
            var photoHistories = await _unitOfWork.PhotoHistory.GetAllAsync(includeProperties: "Photos");

            var photoHistoryResponseDTO = _mapper.Map<IEnumerable<PhotoHistoryResponseDTO>>(photoHistories);
            return photoHistoryResponseDTO;
        }

        public async Task<PhotoHistoryResponseDTO> GetById(int id)
        {
            var ph = await _unitOfWork.PhotoHistory.GetFirstOrDefaultAsync(filter: e => e.Id == id, includeProperties: "Photos");

            var photoHistoryResponseDTO = _mapper.Map<PhotoHistoryResponseDTO>(ph);
            return photoHistoryResponseDTO;
        }

        public async Task<PhotoHistoryResponseDTO> CreateHistory(PhotoHistoryRequestDTO dto)
        {
            var existingPhotoHistory = await _unitOfWork.PhotoHistory
                .GetFirstOrDefaultAsync(filter: ph => ph.SessionCode == dto.SessionCode, includeProperties: "Photos");

            if (existingPhotoHistory != null)
            {
                existingPhotoHistory.BoothId = dto.BoothId;
                existingPhotoHistory.CustomerId = dto.CustomerId;
                existingPhotoHistory.LocationId = dto.LocationId;

                if (dto.Photo != null && dto.Photo.Any())
                {
                    foreach (var photo in dto.Photo)
                    {
                        var newPhoto = _mapper.Map<Photo>(photo);
                        newPhoto.PhotoHistoryId = existingPhotoHistory.Id;
                        existingPhotoHistory.Photos.Add(newPhoto);
                    }
                }

                _unitOfWork.PhotoHistory.Update(existingPhotoHistory);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<PhotoHistoryResponseDTO>(existingPhotoHistory);
            }
            else
            {
                var newPhotoHistory = _mapper.Map<PhotoHistory>(dto);
                newPhotoHistory.Photos = new List<Photo>();

                if (dto.Photo != null && dto.Photo.Any())
                {
                    foreach (var photo in dto.Photo)
                    {
                        var newPhoto = _mapper.Map<Photo>(photo);
                        newPhoto.PhotoHistory = newPhotoHistory;
                        newPhotoHistory.Photos.Add(newPhoto);
                    }
                }

                _unitOfWork.PhotoHistory.Add(newPhotoHistory);
                await _unitOfWork.SaveAsync();

                return _mapper.Map<PhotoHistoryResponseDTO>(newPhotoHistory);
            }
        }



        public async Task<int> DeleteHistory(int id)
        {
            var photoHistory = await _unitOfWork.PhotoHistory.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            _unitOfWork.PhotoHistory.Remove(photoHistory);
            await _unitOfWork.SaveAsync();
            return id;
        }

        public async Task<IEnumerable<PhotoHistoryResponseDTO>> GetByUser()
        {
            var customerId = _contextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(customerId))
            {
                throw new UnauthorizedAccessException("User chưa đăng nhập hoặc token không hợp lệ.");
            }

            var photoHistories = await _unitOfWork.PhotoHistory.GetAllAsync(filter: ph => ph.CustomerId == customerId, includeProperties: "Photos");

            return _mapper.Map<IEnumerable<PhotoHistoryResponseDTO>>(photoHistories);
        }
    }
}

