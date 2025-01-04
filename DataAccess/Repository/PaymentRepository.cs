using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class PaymentRepository : Repository<Payment>, IPaymentRepository
    {
        private readonly AIPhotoboothDbContext _db;
        public PaymentRepository(AIPhotoboothDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Payment payment)
        {
            var objFromDb = _db.Payment.FirstOrDefault(s => s.Id == payment.Id);
            objFromDb.MethodName = payment.MethodName;
            objFromDb.Description = payment.Description;
            objFromDb.IsActive = payment.IsActive;
        }

        public Task UpdateAsync(Payment payment)
        {
            var objFromDb = _db.Payment.FirstOrDefault(s => s.Id == payment.Id);
            objFromDb.MethodName = payment.MethodName;
            objFromDb.Description = payment.Description;
            objFromDb.IsActive = payment.IsActive;
            return Task.CompletedTask;
        }
    }
}
