using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ITypeSessionRepository : IRepository<TypeSession>
    {
        int Update(TypeSession typeSession);
        Task<int> UpdateAsync(TypeSession typeSession);
    }
}
