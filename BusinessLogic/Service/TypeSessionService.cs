using AutoMapper;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Service
{
    public class TypeSessionService : ITypeSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public TypeSessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<TypeSessionResponseDTO> CreateTypeSessionCodeAsync(TypeSessionRequestDTO typeSessionCodeDTO)
        {
            var typeSessionCode = _mapper.Map<TypeSession>(typeSessionCodeDTO);
            _unitOfWork.TypeSession.Add(typeSessionCode);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TypeSessionResponseDTO>(typeSessionCode);
        }

        public async Task<bool> DeleteTypeSessionCodeAsync(int id)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            if (typeSessionCode == null)
            {
                return false;
            }
            _unitOfWork.TypeSession.Remove(typeSessionCode);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<TypeSessionResponseDTO> GetTypeSessionCodeByIdAsync(int id)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            var typeSessionCodeDTO = _mapper.Map<TypeSessionResponseDTO>(typeSessionCode);
            return typeSessionCodeDTO;
        }

        public async Task<TypeSessionResponseDTO> GetTypeSessionCodeByNameAsync(string name)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(filter: e => e.Name == name);
            var typeSessionCodeDTO = _mapper.Map<TypeSessionResponseDTO>(typeSessionCode);
            return typeSessionCodeDTO;
        }

        public async Task<IEnumerable<TypeSessionResponseDTO>> GetTypeSessionCodesAsync()
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetAllAsync();
            var typeSessionCodeDTO = _mapper.Map<IEnumerable<TypeSessionResponseDTO>>(typeSessionCode);
            return typeSessionCodeDTO;
        }

        public async Task<bool> UpdateTypeSessionCodeAsync(int id, TypeSessionRequestDTO typeSessionCodeDTO)
        {
            if (typeSessionCodeDTO == null)
            {
                return false;
            }

            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(e => e.Id == id);
            if (typeSessionCode == null)
            {
                return false;
            }

            if (!string.IsNullOrEmpty(typeSessionCodeDTO.Name))
            {
                var isExist = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(e => e.Name == typeSessionCodeDTO.Name && e.Id != id);
                if (isExist != null)
                {
                    return false;
                }
                typeSessionCode.Name = typeSessionCodeDTO.Name;
            }

            typeSessionCode.Description = typeSessionCodeDTO.Description ?? typeSessionCode.Description;
            typeSessionCode.Duration = typeSessionCodeDTO.Duration ?? typeSessionCode.Duration;
            typeSessionCode.Price = typeSessionCodeDTO.Price >= 0 ? typeSessionCodeDTO.Price : typeSessionCode.Price;
            typeSessionCode.isPrinting = typeSessionCodeDTO.isPrinting;
            typeSessionCode.AbleTakenNumber = typeSessionCodeDTO.AbleTakenNumber >= 0 ? typeSessionCodeDTO.AbleTakenNumber : typeSessionCode.AbleTakenNumber;

            _unitOfWork.TypeSession.Update(typeSessionCode);
            await _unitOfWork.SaveAsync();
            return true;
        }

    }
}
