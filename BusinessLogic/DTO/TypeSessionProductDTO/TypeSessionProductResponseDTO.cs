
using BusinessLogic.DTO.LevelMembershipDTO;
using BusinessLogic.DTO.TypeSessionDTO;

namespace BusinessLogic.DTO.TypeSessionProductDTO
{
    public class TypeSessionProductResponseDTO
    {
        public int? Id { get; set; }
        public string? Name { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? LastModified { get; set; }
        public string? CreatedById { get; set; }
        public string? LastModifiedById { get; set; }
        public string? ProductId { get; set; }
        public int? LevelMembershipId { get; set; }
        public int? TypeSessionId { get; set; }
        public LevelMembershipResponseDTO? LevelMembership { get; set; }

        public TypeSessionResponseDTO typeSession { get; set; }
    }
}
