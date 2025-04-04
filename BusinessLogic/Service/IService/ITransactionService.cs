using BusinessLogic.DTO.TransactionDTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.IService
{
    public interface ITransactionService
    {
        Task<IEnumerable<TransactionResponseDTO>> GetAllTransactions();
        Task<TransactionResponseDTO> GetTransactionById(int Id);
        Task<TransactionResponseDTO> CreateTransaction(TransactionRequestDTO transactionRequestDTO);
    }
}
