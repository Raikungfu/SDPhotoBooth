using AutoMapper;
using BusinessLogic.DTO.PhotoHistoryDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
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

        public PhotoHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string[]> GetListPhotos(string sesssionCode)
        {
            var photoHistory = await _unitOfWork.PhotoHistory.GetFirstOrDefaultAsync(includeProperties: "Photos", filter: o => o.SessionCode == sesssionCode);
            if (photoHistory == null)
            {
                return null;
            }

            return photoHistory.Photos.Select(p => p.Url).ToArray();
        }

        public async Task<PhotoHistoryResponseDTO> GetPhotoHistoryAsync(int id, string userId)
        {
            var photoHistory = await _unitOfWork.PhotoHistory.GetFirstOrDefaultAsync(includeProperties: "Photos,Photos.PhotoStyle", filter: o => o.Id == id && o.CustomerId == userId);
            if (photoHistory == null)
            {
                return null;
            }

            var photoHistoryResponseDTO = _mapper.Map<PhotoHistoryResponseDTO>(photoHistory);
            return photoHistoryResponseDTO;
        }

        public async Task<IEnumerable<PhotoHistoryResponseDTO>> GetListPhotoHistoryAsync(string userId)
        {
            var photoHistories = await _unitOfWork.PhotoHistory.GetAllAsync(filter: o => o.CustomerId == userId);
            return photoHistories.Select(photoHistory => new PhotoHistoryResponseDTO
            {
                Id = photoHistory.Id,
                CreatedAt = photoHistory.CreatedAt,
                UpdatedAt = photoHistory.UpdatedAt,
                Amount = photoHistory.Amount,
                SessionCode = photoHistory.SessionCode
            });
        }
    }
}
