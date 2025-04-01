using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AIPhotoboothDbContext _db;
        public UnitOfWork(AIPhotoboothDbContext db)
        {
            _db = db;
            Booth = new BoothRepository(_db);
            Coupon = new CouponRepository(_db);
            Location = new LocationRepository(_db);
            Order = new OrderRepository(_db);
            PaymentMethod = new PaymentMethodRepository(_db);
            PhotoHistory = new PhotoHistoryRepository(_db);
            Photo = new PhotoRepository(_db);
            Session = new SessionRepository(_db);
            TypeSession = new TypeSessionRepository(_db);
            User = new UserRepository(_db);
            PhotoStyle = new PhotoStyleRepository(_db);
            Sticker = new StickerRepository(_db);
            StickerStyle = new StickerStyleRepository(_db);

        }
        public IBoothRepository Booth { get; private set; }
        public ICouponRepository Coupon { get; private set; }
        public ILocationRepository Location { get; private set; }
        public IOrderRepository Order { get; private set; }
        public IPaymentMethodRepository PaymentMethod { get; private set; }
        public IPhotoHistoryRepository PhotoHistory { get; private set; }
        public IPhotoRepository Photo { get; private set; }
        public ISessionRepository Session { get; private set; }
        public ITypeSessionRepository TypeSession { get; private set; }
        public IUserRepository User { get; private set; }
        public IPhotoStyleRepository PhotoStyle { get; private set; }
        public IStickerRepository Sticker { get; private set; }
        public IStickerStyleRepository StickerStyle { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}