using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IPhotoRepository : IRepository<Photo>
    {
        int Update(Photo photo);
        Task<int> UpdateAsync(Photo photo);
    }
}
