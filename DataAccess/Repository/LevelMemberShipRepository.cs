using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class LevelMemberShipRepository : Repository<LevelMemberShip>, ILevelMemberShipRepository
    {
        private readonly AIPhotoboothDbContext _context;
        public LevelMemberShipRepository(AIPhotoboothDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(LevelMemberShip levelMemberShip)
        {
            var objFromDb = _context.LevelMemberShip.FirstOrDefault(s => s.Id == levelMemberShip.Id);
            objFromDb.Name = levelMemberShip.Name;
            objFromDb.Description = levelMemberShip.Description;
            objFromDb.Price = levelMemberShip.Price;
            objFromDb.IsActive = levelMemberShip.IsActive;
            objFromDb.Duration = levelMemberShip.Duration;
            objFromDb.Point = levelMemberShip.Point;
        }

        public Task UpdateAsync(LevelMemberShip levelMemberShip)
        {
            var objFromDb = _context.LevelMemberShip.FirstOrDefault(s => s.Id == levelMemberShip.Id);
            objFromDb.Name = levelMemberShip.Name;
            objFromDb.Description = levelMemberShip.Description;
            objFromDb.Price = levelMemberShip.Price;
            objFromDb.IsActive = levelMemberShip.IsActive;
            objFromDb.Duration = levelMemberShip.Duration;
            objFromDb.Point = levelMemberShip.Point;
            return Task.CompletedTask;
        }
    }
}
