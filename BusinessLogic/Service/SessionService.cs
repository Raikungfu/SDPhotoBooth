using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.SessionCodeDTO;
using BusinessLogic.Service.IService;
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
            var isTypeSessionExist = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(ts => ts.Id == sessionRequestDTO.TypeSessionId);
            if (isTypeSessionExist == null)
            {
                return null;
            }

            var isBoothExist = await _unitOfWork.Booth.GetFirstOrDefaultAsync(b => b.Id == sessionRequestDTO.BoothId);
            if (isBoothExist == null)
            {
                return null;
            }

            var isOrderExist = await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Id == sessionRequestDTO.OrderId);
            if (isOrderExist == null)
            {
                return null;
            }

            var session = new Session();
            session = _mapper.Map<Session>(sessionRequestDTO);
            await _unitOfWork.Session.AddAsync(session);
            await _unitOfWork.SaveAsync();

            var sessionForDTO = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Code == session.Code, includeProperties: "Order,TypeSession,Booth,Booth.Location");
            return _mapper.Map<SessionResponseDTO>(sessionForDTO);
        }

        public async Task<IEnumerable<SessionResponseDTO>> GetAllSessions()
        {
            var allSession = await _unitOfWork.Session.GetAllAsync(includeProperties: "Order,TypeSession,Booth,Booth.Location");
            return _mapper.Map<IEnumerable<SessionResponseDTO>>(allSession);
        }

        public async Task<SessionResponseDTO> GetSessionByCode(string code)
        {
            var session = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Code == code, includeProperties: "Order,TypeSession,Booth,Booth.Location");
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