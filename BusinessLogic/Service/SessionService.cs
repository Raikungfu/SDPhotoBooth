using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.SessionCodeDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public SessionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<SessionResponseDTO> CreateSession(SessionRequestDTO sessionRequestDTO)
        {
            if (await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.Id == sessionRequestDTO.BoothId) == null ||
                await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Id == sessionRequestDTO.OrderId) == null)
            {
                return null;
            }

            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Id == sessionRequestDTO.OrderId);
            if (order.Status != OrderStatus.Completed)
            {
                return null;
            }

            var existingSession = await _unitOfWork.Session.GetFirstOrDefaultAsync(
                s => s.OrderId == sessionRequestDTO.OrderId,
                includeProperties: "Order,Booth,Booth.Location");

            if (existingSession != null)
            {
                return _mapper.Map<SessionResponseDTO>(existingSession);
            }

            var newSession = _mapper.Map<Session>(sessionRequestDTO);
            newSession.TypeSessionId = order.TypeSessionId;
            await _unitOfWork.Session.AddAsync(newSession);
            await _unitOfWork.SaveAsync();

            var sessionWithDetails = await _unitOfWork.Session.GetFirstOrDefaultAsync(
                s => s.Code == newSession.Code,
                includeProperties: "Order,Booth,Booth.Location"
            );

            return _mapper.Map<SessionResponseDTO>(sessionWithDetails);
        }

        public async Task<IEnumerable<SessionResponseDTO>> GetAllSessions()
        {
            var allSession = await _unitOfWork.Session.GetAllAsync(includeProperties: "Order,Booth,Booth.Location");
            return _mapper.Map<IEnumerable<SessionResponseDTO>>(allSession);
        }

        public async Task<SessionResponseDTO> GetSessionByCode(string code)
        {
            var session = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Code == code, includeProperties: "Order,Booth,Booth.Location");
            return _mapper.Map<SessionResponseDTO>(session);
        }

        public async Task<bool> UpdateStatusSession(string code, bool status)
        {
            var session = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Code == code);
            session.IsActive = status;
            await _unitOfWork.SaveAsync();
            return true;
        }
    }
}