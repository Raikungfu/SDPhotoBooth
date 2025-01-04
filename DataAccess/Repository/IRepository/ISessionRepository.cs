using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ISessionRepository : IRepository<Session>
    {
        string Update(Session session);
        Task<string> UpdateAsync(Session session);
    }
}
