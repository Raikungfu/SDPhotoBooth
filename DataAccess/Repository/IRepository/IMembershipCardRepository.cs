using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IMembershipCardRepository : IRepository<MembershipCard>
    {
        int Update(MembershipCard membershipCard);
        Task<int> UpdateAsync(MembershipCard membershipCard);
    }
}
