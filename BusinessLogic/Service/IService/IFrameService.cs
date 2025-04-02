using BusinessLogic.DTO.FrameDTO;

namespace BusinessLogic.Service.IService
{
    public interface IFrameService
    {
        Task<IEnumerable<FrameResponseDTO>> GetFrames();
        Task<FrameResponseDTO> GetFrame(int id);
        Task<IEnumerable<FrameResponseDTO>> GetFrameByFrameStyleId(int frameStyleId);
        Task<IEnumerable<FrameResponseDTO>> SearchFrame(string frameName);
        Task<FrameResponseDTO> CreateFrame(FrameRequestDTO frameRequestDTO);
        Task<FrameResponseDTO> UpdateFrame(int id, FrameRequestDTO frameRequestDTO);
        Task<bool> DeleteFrame(int id);
    }
}
