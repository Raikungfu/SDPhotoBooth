using BusinessLogic.DTO.FrameDTO;

namespace BusinessLogic.Service.IService
{
    public interface IFrameStyleService
    {
        Task<IEnumerable<FrameStyleResponseDTO>> GetFrameStyles();
        Task<FrameStyleResponseDTO> GetFrameStyle(int id);
        Task<IEnumerable<FrameStyleResponseDTO>> SearchFrameStyle(string frameStyleName);
        Task<FrameStyleResponseDTO> CreateFrameStyle(FrameStyleRequestDTO frameStyleRequestDTO);
        Task<FrameStyleResponseDTO> UpdateFrameStyle(int id, FrameStyleRequestDTO frameStyleRequestDTO);
        Task<bool> DeleteFrameStyle(int id);
    }
}
