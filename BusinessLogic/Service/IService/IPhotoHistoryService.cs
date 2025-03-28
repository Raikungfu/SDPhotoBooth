using BusinessLogic.DTO.PhotoHistoryDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.IService
{
    public interface IPhotoHistoryService
    {
        Task<string[]> GetListPhotos(int sessionId);
        Task<string[]> GetListPhotosFromSessionCode(string sessionCode);
        Task<IEnumerable<PhotoHistoryResponseDTO>> GetListPhotoHistoryAsync(string userId);
        Task<PhotoHistoryResponseDTO> GetPhotoHistoryAsync(int id, string userId);
    }
}