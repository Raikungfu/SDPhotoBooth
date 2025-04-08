using AutoMapper;
using BusinessLogic.DTO.TransactionDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
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

        public async Task<int> GetCount()
        {
            var transactions = await _unitOfWork.Transaction.GetAllAsync();
            return transactions.Count();
        }

        public async Task<TransactionResponseDTO> CreateTransaction(TransactionRequestDTO transactionRequestDTO)
        {
            var transaction = _mapper.Map<Transaction>(transactionRequestDTO);
            var newTransaction = _unitOfWork.Transaction.AddAsync(transaction);
            await _unitOfWork.SaveAsync();

            return _mapper.Map<TransactionResponseDTO>(newTransaction);
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetAllTransactions(PaginationParams? pagination)
        {
            var transactions = await _unitOfWork.Transaction.GetAllAsync(
                includeProperties: "Payment,Payment.PaymentMethod",
                pagination: pagination
            );

            return _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
        }

        public async Task<IEnumerable<TransactionResponseDTO>> GetTransactionByCusId(string cusId)
        {
            var transactions = await _unitOfWork.Transaction.GetAllAsync(
                t =>
                    (t.Payment.Order != null && t.Payment.Order.CustomerId == cusId) ||
                    (t.Payment.Deposit != null && t.Payment.Deposit.Wallet.CustomerId == cusId),
                includeProperties: "Payment.PaymentMethod,Payment.Order,Payment.Deposit.Wallet"
            );

            return _mapper.Map<IEnumerable<TransactionResponseDTO>>(transactions);
        }


        public async Task<TransactionResponseDTO> GetTransactionById(int Id)
        {
            var transaction = await _unitOfWork.Transaction.GetFirstOrDefaultAsync(t => t.Id == Id, includeProperties: "Payment.PaymentMethod,Payment");
            if (transaction == null)
            {
                return null;
            }
            return _mapper.Map<TransactionResponseDTO>(transaction);
        }
    }
}
