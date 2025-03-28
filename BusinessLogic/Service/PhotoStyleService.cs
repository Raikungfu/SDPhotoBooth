using AutoMapper;
using BusinessLogic.DTO.PhotoStyleDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class PhotoStyleService : IPhotoStyleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public PhotoStyleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<PhotoStyleResponseDTO> CreatePhotoStyle(PhotoStyleRequestDTO photoStyle)
        {
            var isExist = await _unitOfWork.PhotoStyle.GetFirstOrDefaultAsync(t => t.Name == photoStyle.Name && !t.IsDeleted);
            if (isExist != null)
            {
                return null;
            }
            else
            {
                var photoStyleObj = _mapper.Map<PhotoStyle>(photoStyle);
                await _unitOfWork.PhotoStyle.AddAsync(photoStyleObj);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<PhotoStyleResponseDTO>(photoStyleObj);
            }
        }
        public async Task<PhotoStyleResponseDTO> DeletePhotoStyle(int id)
        {
            var photoStyle = await _unitOfWork.PhotoStyle.GetFirstOrDefaultAsync(t => t.Id == id);
            if (photoStyle == null)
            {
                return null;
            }
            _unitOfWork.PhotoStyle.Remove(photoStyle);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<PhotoStyleResponseDTO>(photoStyle);
        }

        public async Task<IEnumerable<PhotoStyleResponseDTO>> GetAllPhotoStyles()
        {
            var photoStyles = await _unitOfWork.PhotoStyle.GetAllAsync();
            return _mapper.Map<IEnumerable<PhotoStyleResponseDTO>>(photoStyles);
        }

        public async Task<PhotoStyleResponseDTO> GetPhotoStyleById(int id)
        {
            var photoStyle = await _unitOfWork.PhotoStyle.GetFirstOrDefaultAsync(t => t.Id == id);
            if (photoStyle == null)
            {
                return null;
            }
            return _mapper.Map<PhotoStyleResponseDTO>(photoStyle);
        }

        public async Task<PhotoStyleResponseDTO> GetPhotoStyleByName(string name)
        {
            var photoStyle = await _unitOfWork.PhotoStyle.GetFirstOrDefaultAsync(t => t.Name == name);
            if (photoStyle == null)
            {
                return null;
            }
            return _mapper.Map<PhotoStyleResponseDTO>(photoStyle);
        }

        public async Task<PhotoStyleResponseDTO> UpdatePhotoStyle(int id, PhotoStyleRequestDTO photoStyle)
        {
            var isExistPhotoName = await _unitOfWork.PhotoStyle.GetFirstOrDefaultAsync(t => t.Name == photoStyle.Name && !t.IsDeleted && t.Id != id);
            if (isExistPhotoName != null)
            {
                return null;
            }
            var photoStyleObj = await _unitOfWork.PhotoStyle.GetFirstOrDefaultAsync(t => t.Id == id);
            photoStyleObj.Name = photoStyle.Name;
            photoStyleObj.Description = photoStyle.Description;
            photoStyleObj.ImageUrl = photoStyle.ImageUrl;
            photoStyleObj.LastModified = DateTime.Now;
            try
            {
                _unitOfWork.PhotoStyle.Update(photoStyleObj);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<PhotoStyleResponseDTO>(photoStyleObj);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    } 
}
