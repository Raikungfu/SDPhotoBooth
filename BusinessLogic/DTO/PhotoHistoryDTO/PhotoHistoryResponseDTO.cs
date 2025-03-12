using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLogic.DTO.NewFolder;

namespace BusinessLogic.DTO.PhotoHistoryDTO
{
    public class PhotoHistoryResponseDTO
    {
        public int Id { get; set; }
        public int? BoothId { get; set; }
        public string? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public decimal Amount { get; set; }
        public string SessionCode { get; set; }
        public List<PhotoResponseDTO> Photos { get; set; } = new List<PhotoResponseDTO>();
    }
}
