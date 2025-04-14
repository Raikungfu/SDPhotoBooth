using BussinessObject.Models;
using DataAccess.Data;
using DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repository
{
    public class DepositProductRepository : Repository<DepositProduct>, IDepositProductRepository
    {
        private readonly AIPhotoboothDbContext _context;
        public DepositProductRepository(AIPhotoboothDbContext context) : base(context)
        {
            _context = context;
        }

        public void Update(DepositProduct depositProduct)
        {
            var objFromDb = _context.DepositProduct.FirstOrDefault(s => s.Id == depositProduct.Id);
            objFromDb.DepositId = depositProduct.DepositId;
            objFromDb.ProductId = depositProduct.ProductId;
            objFromDb.CouponId = depositProduct.CouponId;
            objFromDb.LevelMembershipId = depositProduct.LevelMembershipId;
        }

        public async Task UpdateAsync(DepositProduct depositProduct)
        {
            var objFromDb = await _context.DepositProduct.FirstOrDefaultAsync(s => s.Id == depositProduct.Id);
            if (objFromDb != null)
            {
                objFromDb.DepositId = depositProduct.DepositId;
                objFromDb.ProductId = depositProduct.ProductId;
                objFromDb.CouponId = depositProduct.CouponId;
                objFromDb.LevelMembershipId = depositProduct.LevelMembershipId;
            }
        }
    }
}
