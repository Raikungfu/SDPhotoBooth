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

        public async Task<SessionResponseDTO> CreateSession(long orderCode)
        {
            if (await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Code == orderCode) == null)
            {
                return null;
            }

            var order = await _unitOfWork.Order.GetFirstOrDefaultAsync(o => o.Code == orderCode, includeProperties:"TypeSession");
            if (order.Status != OrderStatus.Completed)
            {
                return null;
            }

            var existingSession = await _unitOfWork.Session.GetFirstOrDefaultAsync(
                s => s.OrderId == order.Id);

            if (existingSession != null)
            {
                return _mapper.Map<SessionResponseDTO>(existingSession);
            }

            var newSession = new Session();
            newSession.OrderId = order.Id;

            await _unitOfWork.Session.AddAsync(newSession);
            await _unitOfWork.SaveAsync();

            var sessionWithDetails = await _unitOfWork.Session.GetFirstOrDefaultAsync(
                s => s.Code == newSession.Code);

            return _mapper.Map<SessionResponseDTO>(sessionWithDetails);
        }

        public async Task<IEnumerable<SessionResponseDTO>> GetAllSessions()
        {
            var allSession = await _unitOfWork.Session.GetAllAsync();
            return _mapper.Map<IEnumerable<SessionResponseDTO>>(allSession);
        }

        public async Task<IEnumerable<SessionResponseDTO>> SearchSessionByCode(string code)
        {
            var session = await _unitOfWork.Session.GetAllAsync(s => s.Code.ToLower().Contains(code.ToLower()));
            return _mapper.Map<IEnumerable<SessionResponseDTO>>(session);
        }

        public async Task<SessionResponseDTO> GetSessionById(int Id)
        {
            var session = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Id == Id);
            return _mapper.Map<SessionResponseDTO>(session);
        }

        public async Task<SessionResponseDTO> GetSessionByCode(string code)
        {
            var session = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Code == code);
            return _mapper.Map<SessionResponseDTO>(session);
        }

        public async Task<SessionResponseDTO> UseSession(string code, int? boothId)
        {
            var session = await _unitOfWork.Session.GetFirstOrDefaultAsync(s => s.Code == code, includeProperties:"Order,Order.TypeSession");

            if (!session.IsActive)
            {
                session.IsActive = true;
                session.Expired = DateTime.Now.AddMinutes(session.Order.TypeSession.Duration + 1);

                // Create photo history
                var photoHistory = new PhotoHistory() {
                    CustomerId = session.Order.UserId,
                    SessionId = session.Id,
                    BoothId = boothId,
                };

                await _unitOfWork.PhotoHistory.AddAsync(photoHistory);
                await _unitOfWork.SaveAsync();
                return _mapper.Map<SessionResponseDTO>(session);
            }
            else
            {
                if (session.Expired < DateTime.Now)
                {
                    return null;
                }
            }
            return _mapper.Map<SessionResponseDTO>(session);
        }
    }
}