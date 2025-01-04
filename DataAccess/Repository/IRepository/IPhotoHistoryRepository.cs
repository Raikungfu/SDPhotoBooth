using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IPhotoHistoryRepository : IRepository<PhotoHistory>
    {
        int Update(PhotoHistory photoHistory);
        Task<int> UpdateAsync(PhotoHistory photoHistory);
    }
}
