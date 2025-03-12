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
        Task<IEnumerable<PhotoHistoryResponseDTO>> GetAll();
        Task<PhotoHistoryResponseDTO> GetById(int id);
        Task<IEnumerable<PhotoHistoryResponseDTO>> GetByUser();
        Task<PhotoHistoryResponseDTO> CreateHistory(PhotoHistoryRequestDTO dto);
        Task<int> DeleteHistory(int id);
    }

}
