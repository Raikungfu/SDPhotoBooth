using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.DTO.StickerDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.IService
{
    public interface IDepositService
    {
        Task<IEnumerable<DepositResponseDTO>> GetAllDeposits();
        Task<DepositResponseDTO> CreateDeposit(DepositRequestDTO stickerRequestDTO, string userId);
        Task<DepositResponseDTO> UpdateDeposit(int id, DepositRequestDTO stickerRequestDTO);
        Task<DepositResponseDTO> DeleteDeposit(int id);
    }
}
