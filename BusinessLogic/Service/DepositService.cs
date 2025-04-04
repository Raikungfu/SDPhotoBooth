using AutoMapper;
using BusinessLogic.DTO.DepositDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class DepositService : IDepositService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public DepositService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<DepositResponseDTO> CreateDeposit(DepositRequestDTO depositRequestDTO, string userId)
        {
            var wallet = await _unitOfWork.Wallet.GetFirstOrDefaultAsync(t => t.CustomerId == userId);
            if (wallet == null)
            {
                return null;
            }
            var depositObj = _mapper.Map<Deposit>(depositRequestDTO);

            depositObj.WalletId = wallet.Id;
            await _unitOfWork.Deposit.AddAsync(depositObj);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DepositResponseDTO>(depositObj);
        }

        public async Task<DepositResponseDTO> GetDeposit(int id)
        {
            var deposit = await _unitOfWork.Deposit.GetFirstOrDefaultAsync(t => t.Id == id);
            if (deposit == null)
            {
                return null;
            }
            return _mapper.Map<DepositResponseDTO>(deposit);
        }

        public async Task<DepositResponseDTO> DeleteDeposit(int id)
        {
            var deposit = await _unitOfWork.Deposit.GetFirstOrDefaultAsync(t => t.Id == id);
            if (deposit == null)
            {
                return null;
            }
            _unitOfWork.Deposit.Remove(deposit);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DepositResponseDTO>(deposit);
        }

        public async Task<IEnumerable<DepositResponseDTO>> GetAllDeposits()
        {
            var deposits = await _unitOfWork.Deposit.GetAllAsync();
            return _mapper.Map<IEnumerable<DepositResponseDTO>>(deposits);
        }

        public async Task<DepositResponseDTO> UpdateDeposit(int id, DepositRequestDTO depositRequestDTO)
        {
            var deposit = await _unitOfWork.Deposit.GetFirstOrDefaultAsync(t => t.Id == id);
            if (deposit == null)
            {
                return null;
            }

            deposit.Amount = depositRequestDTO.Amount;
            deposit.Status = depositRequestDTO.Status;

            _unitOfWork.Deposit.Update(deposit);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DepositResponseDTO>(deposit);
        }

        public async Task<DepositResponseDTO> UpdateDepositStatusById(int id, DepositStatus depositStatus)
        {
            var deposit = await _unitOfWork.Deposit.GetFirstOrDefaultAsync(t => t.Id == id);
            if (deposit == null)
            {
                return null;
            }

            deposit.Status = depositStatus;
            _unitOfWork.Deposit.Update(deposit);

            if (depositStatus == DepositStatus.Success)
            {
                var wallet = await _unitOfWork.Wallet.GetFirstOrDefaultAsync(t => t.Id == deposit.WalletId);
                if (wallet != null)
                {
                    wallet.Balance += deposit.Amount;
                    _unitOfWork.Wallet.Update(wallet);
                }
            }
            await _unitOfWork.SaveAsync();
            return _mapper.Map<DepositResponseDTO>(deposit);
        }
    }
}
