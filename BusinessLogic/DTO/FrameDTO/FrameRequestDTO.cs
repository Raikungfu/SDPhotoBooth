using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO.FrameDTO
{
    public class FrameRequestDTO : BaseFrameDTO
    {
        public string? FrameUrl { get; set; }
    }
    public class FrameWithFormFileDTO : BaseFrameDTO
    {
        public IFormFile? FrameFile { get; set; }
    }
    public class BaseFrameDTO
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        public int? FrameStyleId { get; set; }

        public int? SlotCount { get; set; }
    }
}
