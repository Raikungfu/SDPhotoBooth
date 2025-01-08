using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        private readonly AIPhotoboothDbContext _db;
        public OrderRepository(AIPhotoboothDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Order order)
        {
            var objFromDb = _db.Order.FirstOrDefault(s => s.Id == order.Id);
            objFromDb.UserId = order.UserId;
            objFromDb.PaymentId = order.PaymentId;
            objFromDb.Amount = order.Amount;
            objFromDb.SessionId = order.SessionId;
        }

        public Task UpdateAsync(Order order)
        {
            var objFromDb = _db.Order.FirstOrDefault(s => s.Id == order.Id);
            objFromDb.UserId = order.UserId;
            objFromDb.PaymentId = order.PaymentId;
            objFromDb.Amount = order.Amount;
            objFromDb.SessionId = order.SessionId;
            return Task.CompletedTask;
        }
    }
}
