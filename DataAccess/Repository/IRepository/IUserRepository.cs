using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IUserRepository : IRepository<User>
    {
        int Update(User user);
        Task<int> UpdateAsync(User user);
    }
}
