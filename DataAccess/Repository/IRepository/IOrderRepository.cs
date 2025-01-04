using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IOrderRepository : IRepository<Order>
    {
        int Update(Order order);
        Task<int> UpdateAsync(Order order);
    }
}
