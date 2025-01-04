using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IPaymentRepository : IRepository<Payment>
    {
        void Update(Payment payment);
        Task UpdateAsync(Payment payment);
    }
}
