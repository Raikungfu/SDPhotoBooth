using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ICouponRepository : IRepository<Coupon>
    {
        int Update(Coupon coupon);
        Task<int> UpdateAsync(Coupon coupon);
    }
}
