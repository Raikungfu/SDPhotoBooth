using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;

namespace DataAccess.Repository
{
    public class MembershipCardRepository : Repository<MembershipCard>, IMembershipCardRepository
    {
        private readonly AIPhotoboothDbContext _context;
        public MembershipCardRepository(AIPhotoboothDbContext context) : base(context)
        {
            _context = context;
        }
        public void Update(MembershipCard membershipCard)
        {
            var objFromDb = _context.MembershipCard.FirstOrDefault(s => s.Id == membershipCard.Id);
            objFromDb.CustomerId = membershipCard.CustomerId;
            objFromDb.LevelMemberShipId = membershipCard.LevelMemberShipId;
            objFromDb.Points = membershipCard.Points;
            objFromDb.Expired = membershipCard.Expired;
            objFromDb.Description = membershipCard.Description;
            objFromDb.IsActive = membershipCard.IsActive;
        }

        public Task UpdateAsync(MembershipCard membershipCard)
        {
            var objFromDb = _context.MembershipCard.FirstOrDefault(s => s.Id == membershipCard.Id);
            objFromDb.CustomerId = membershipCard.CustomerId;
            objFromDb.LevelMemberShipId = membershipCard.LevelMemberShipId;
            objFromDb.Points = membershipCard.Points;
            objFromDb.Expired = membershipCard.Expired;
            objFromDb.Description = membershipCard.Description;
            objFromDb.IsActive = membershipCard.IsActive;
            return Task.CompletedTask;
        }
    }
}
