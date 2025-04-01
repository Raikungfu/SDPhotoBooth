using BusinessLogic.DTO.StickerDTO;

namespace BusinessLogic.Service.IService
{
    public interface IStickerService
    {
        Task<IEnumerable<StickerResponseDTO>> GetAllStickers();
        Task<StickerResponseDTO> GetStickerById(int Id);
        Task<IEnumerable<StickerResponseDTO>> GetStickerByStyleId(int Id);
        Task<StickerResponseDTO> CreateSticker(StickerRequestDTO stickerRequestDTO);
        Task<StickerResponseDTO> UpdateSticker(int Id, StickerRequestDTO stickerRequestDTO);
        Task<StickerResponseDTO> DeleteSticker(int Id);
    }
}
