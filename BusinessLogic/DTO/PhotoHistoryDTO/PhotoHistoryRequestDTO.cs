using BusinessLogic.DTO.PhotoDTO;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.PhotoHistoryDTO
{
    public class PhotoHistoryRequestDTO
    {
        public int? BoothId { get; set; }
        public string? CustomerId { get; set; }
        public int? LocationId { get; set; }
        public decimal?Amount { get; set; }
        public string? SessionCode { get; set; }
        public List<PhotoRequestDTO>? Photo { get; set; }
    }
}
