using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        int Update(Payment payment);
        Task<int> UpdateAsync(Payment payment);
    }
}
