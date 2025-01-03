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
            //Account = new AccountRepository(_db);

        }
        //public IAccountRepository Account { get; private set; }

        public void Dispose()
        {
            _db.Dispose();
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}