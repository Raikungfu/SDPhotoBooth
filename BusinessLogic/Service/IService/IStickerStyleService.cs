using BusinessLogic.DTO.StickerStyleDTO;

namespace BusinessLogic.Service.IService
{
    public interface IStickerStyleService
    {
        Task<IEnumerable<StickerStyleResponseDTO>> GetAllStickerStyles();
        Task<StickerStyleResponseDTO> GetStickerStyleById(int Id);
        Task<StickerStyleResponseDTO> CreateStickerStyle(StickerStyleRequestDTO stickerStyleRequestDTO);
        Task<StickerStyleResponseDTO> UpdateStickerStyle(int Id, StickerStyleRequestDTO stickerStyleRequestDTO);
        Task<StickerStyleResponseDTO> DeleteStickerStyle(int Id);
    }
}
