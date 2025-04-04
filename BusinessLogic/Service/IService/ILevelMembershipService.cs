using BusinessLogic.DTO.LevelMembershipDTO;

namespace BusinessLogic.Service.IService
{
    public interface ILevelMembershipService
    {
        Task<IEnumerable<LevelMembershipResponseDTO>> GetAllLevelMemberships();
        Task<LevelMembershipResponseDTO> GetLevelMembershipById(int id);
        Task<LevelMembershipResponseDTO> CreateLevelMembership(CreateLevelMembershipRequestDTO levelMembershipRequestDTO);
        Task<LevelMembershipResponseDTO> UpdateLevelMembership(int id, UpdateLevelMembershipRequestDTO levelMembershipRequestDTO);
        Task<bool> DeleteLevelMembership(int id);
    }
}
