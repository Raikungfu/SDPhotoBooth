using AutoMapper;
using BusinessLogic.DTO.TransactionDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class TransactionService : ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<TransactionResponseDTO> CreateTransaction(TransactionRequestDTO transactionRequestDTO)
        {
            var transaction = _mapper.Map<Transaction>(transactionRequestDTO);
            var newTransaction = _unitOfWork.Transaction.AddAsync(transaction);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TransactionResponseDTO>(newTransaction);
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetAllTransactions()
        {
            var transactions = await _unitOfWork.Transaction.GetAllAsync();
            return _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
        }

        public async Task<TransactionResponseDTO> GetTransactionById(int Id)
        {
            var transaction = await _unitOfWork.Transaction.GetFirstOrDefaultAsync(t => t.Id == Id);
            if (transaction == null)
            {
                return null;
            }
            return _mapper.Map<TransactionResponseDTO>(transaction);
        }
    }
}
