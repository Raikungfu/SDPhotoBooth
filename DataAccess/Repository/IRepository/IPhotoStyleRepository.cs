using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IPhotoStyleRepository : IRepository<PhotoStyle>
    {
        int Update(PhotoStyle photoStyle); 
        Task<int> UpdateAsync(PhotoStyle photoStyle);
    }
}
