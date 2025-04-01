using BusinessLogic.DTO.SessionCodeDTO;

namespace BusinessLogic.Service.IService
{
    public interface ISessionService
    {
        Task<SessionResponseDTO> CreateSession(long orderCode);
        Task<IEnumerable<SessionResponseDTO>> SearchSessionByCode(string code);
        Task<IEnumerable<SessionResponseDTO>> GetAllSessions();
        Task<SessionResponseDTO> GetSessionById(int Id);
        Task<SessionResponseDTO> GetSessionByCode(string code);
        Task<SessionResponseDTO> UseSession(string code, int? boothId);
    }
}