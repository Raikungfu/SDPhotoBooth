using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTO.StickerDTO
{
    public class StickerRequestDTO
    {
        [MaxLength(50)]
        public string? Name { get; set; }

        public string? StickerUrl { get; set; }

        public int? StickerStyleId { get; set; }
    }
}
