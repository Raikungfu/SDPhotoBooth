using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class BoothStyleRepository : Repository<BoothStyle>, IBoothStyleRepository
    {
        private readonly AIPhotoboothDbContext _db;
        public BoothStyleRepository(AIPhotoboothDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(BoothStyle boothStyle)
        {
            var objFromDb = _db.BoothStyle.FirstOrDefault(s => s.Id == boothStyle.Id);
            if (objFromDb != null)
            {
                objFromDb.StyleName = boothStyle.StyleName;
                objFromDb.Description = boothStyle.Description;
            }
        }
        public async Task UpdateAsync(BoothStyle boothStyle)
        {
            var objFromDb = await _db.BoothStyle.FirstOrDefaultAsync(s => s.Id == boothStyle.Id);
            if (objFromDb != null)
            {
                objFromDb.StyleName = boothStyle.StyleName;
                objFromDb.Description = boothStyle.Description;
            }
        }
    }
}
