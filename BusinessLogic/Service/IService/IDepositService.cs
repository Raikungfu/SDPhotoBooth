using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.DTO.StickerDTO;
using BussinessObject.Enums;
using BussinessObject.Models;
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
        Task<DepositResponseDTO> CreateDeposit(DepositRequestDTO depositRequestDTO, string userId);
        Task<DepositResponseDTO> UpdateDeposit(int id, DepositRequestDTO depositRequestDTO);
        Task<DepositResponseDTO> UpdateDepositStatusById(int id, DepositStatus depositStatus);
        Task<DepositResponseDTO> DeleteDeposit(int id);
    }
}
