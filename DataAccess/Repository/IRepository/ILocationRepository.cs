using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ILocationRepository : IRepository<Location>
    {
        int Update(Location location); 
        Task<int> UpdateAsync(Location location);
    }
}
