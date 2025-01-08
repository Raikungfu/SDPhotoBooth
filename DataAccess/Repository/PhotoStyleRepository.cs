using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class PhotoStyleRepository : Repository<PhotoStyle>, IPhotoStyleRepository
    {
        private readonly AIPhotoboothDbContext _db;
        public PhotoStyleRepository(AIPhotoboothDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(PhotoStyle photoStyle)
        {
            var objFromDb = _db.PhotoStyle.FirstOrDefault(s => s.Id == photoStyle.Id);
            objFromDb.Type = photoStyle.Type;
            objFromDb.Description = photoStyle.Description;
        }

        public Task UpdateAsync(PhotoStyle photoStyle)
        {
            var objFromDb = _db.PhotoStyle.FirstOrDefault(s => s.Id == photoStyle.Id);
            objFromDb.Type = photoStyle.Type;
            objFromDb.Description = photoStyle.Description;
            return Task.CompletedTask;
        }
    }
}
