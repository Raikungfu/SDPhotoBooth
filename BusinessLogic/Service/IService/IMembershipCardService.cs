using BusinessLogic.DTO.MembershipCardDTO;

namespace BusinessLogic.Service.IService
{
    public interface IMembershipCardService
    {
        Task<IEnumerable<MembershipCardResponseDTO>> GetAll();
        Task<MembershipCardResponseDTO> GetById(int id);
        Task<MembershipCardResponseDTO> GetByCustomerId(string customerId);
        Task<IEnumerable<MembershipCardResponseDTO>> GetByLevelMembershipId(int levelMembershipId);
        Task<MembershipCardResponseDTO> Create(CreateMembershipCardRequestDTO membershipCardRequestDTO);
        Task<MembershipCardResponseDTO> Update(int id, UpdateMembershipCardRequestDTO membershipCardRequestDTO);
        Task<bool> Delete(int id);

    }
}
