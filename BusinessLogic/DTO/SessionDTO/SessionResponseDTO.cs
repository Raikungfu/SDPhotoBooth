using BusinessLogic.DTO.BoothDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.Service;

namespace BusinessLogic.DTO.SessionCodeDTO
{
    public class SessionResponseDTO
    {
        public string Code { get; set; }

        public DateTime? Expired { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public TypeSessionResponseDTO? TypeSession { get; set; }

        public BoothResponseDTO? Booth { get; set; }

        public OrderResponseDTO? Order { get; set; }

    }
}