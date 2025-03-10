using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.SessionCodeDTO;

namespace BusinessLogic.Service.IService
{
    public interface ISessionService
    {
        Task<SessionResponseDTO> CreateSession(SessionRequestDTO sessionRequestDTO);
        Task<SessionResponseDTO> GetSessionByCode(string code);
        Task<IEnumerable<SessionResponseDTO>> GetAllSessions();
        Task<bool> UpdateStatusSession(string code, bool status);
    }
}