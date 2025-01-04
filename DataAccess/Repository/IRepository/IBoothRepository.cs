using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IBoothRepository : IRepository<Booth>
    {
        int Update(Booth booth);
        Task<int> UpdateAsync(Booth booth);

    }
}
