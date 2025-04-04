using AutoMapper;
using BusinessLogic.DTO.MembershipCardDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class MembershipCardService : IMembershipCardService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public MembershipCardService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<MembershipCardResponseDTO> Create(CreateMembershipCardRequestDTO membershipCardRequestDTO)
        {
            var membershipCard = _mapper.Map<MembershipCard>(membershipCardRequestDTO);
             await _unitOfWork.MembershipCard.AddAsync(membershipCard);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MembershipCardResponseDTO>(membershipCard);
        }

        public async Task<bool> Delete(int id)
        {
            var membershipCard = await _unitOfWork.MembershipCard.GetFirstOrDefaultAsync(s => s.Id == id);
            if (membershipCard == null)
            {
                return false;
            }
            _unitOfWork.MembershipCard.Remove(membershipCard);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<MembershipCardResponseDTO>> GetAll()
        {
            var membershipCards = await _unitOfWork.MembershipCard.GetAllAsync(includeProperties: "Customer,LevelMemberShip");
            return _mapper.Map<IEnumerable<MembershipCardResponseDTO>>(membershipCards);
        }

        public async Task<MembershipCardResponseDTO> GetByCustomerId(string customerId)
        {
            var membershipCard = await _unitOfWork.MembershipCard.GetFirstOrDefaultAsync(s => s.CustomerId == customerId, includeProperties: "Customer,LevelMemberShip");
            if (membershipCard == null)
            {
                return null;
            }
            return _mapper.Map<MembershipCardResponseDTO>(membershipCard);
        }

        public async Task<MembershipCardResponseDTO> GetById(int id)
        {
            var membershipCard = await _unitOfWork.MembershipCard.GetFirstOrDefaultAsync(s => s.Id == id, includeProperties: "Customer,LevelMemberShip");
            if (membershipCard == null)
            {
                return null;
            }
            return _mapper.Map<MembershipCardResponseDTO>(membershipCard);
        }

        public async Task<IEnumerable<MembershipCardResponseDTO>> GetByLevelMembershipId(int levelMembershipId)
        {
            var membershipCards = await _unitOfWork.MembershipCard.GetAllAsync(s => s.LevelMemberShipId == levelMembershipId, includeProperties: "Customer,LevelMemberShip");
            if (membershipCards == null)
            {
                return null;
            }
            return _mapper.Map<IEnumerable<MembershipCardResponseDTO>>(membershipCards);
        }

        public async Task<MembershipCardResponseDTO> Update(int id, UpdateMembershipCardRequestDTO membershipCardRequestDTO)
        {
            var membershipCard = await _unitOfWork.MembershipCard.GetFirstOrDefaultAsync(s => s.Id == id);
            
            if(membershipCardRequestDTO.CustomerId != null)
            {
                membershipCard.CustomerId = membershipCardRequestDTO.CustomerId;
            }
            if (membershipCardRequestDTO.LevelMemberShipId != null)
            {
                membershipCard.LevelMemberShipId = membershipCardRequestDTO.LevelMemberShipId.Value;
            }
            if (membershipCardRequestDTO.Points != null)
            {
                membershipCard.Points = membershipCardRequestDTO.Points.Value;
            }
            if (membershipCardRequestDTO.Expired != null)
            {
                membershipCard.Expired = membershipCardRequestDTO.Expired.Value;
            }
            if (membershipCardRequestDTO.Description != null)
            {
                membershipCard.Description = membershipCardRequestDTO.Description;
            }
            if (membershipCardRequestDTO.IsActive != null)
            {
                membershipCard.IsActive = membershipCardRequestDTO.IsActive.Value;
            }

            _unitOfWork.MembershipCard.Update(membershipCard);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<MembershipCardResponseDTO>(membershipCard);
        }
    }
}
