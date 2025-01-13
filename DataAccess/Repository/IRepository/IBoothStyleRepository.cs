using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface IBoothStyleRepository : IRepository<BoothStyle>
    {
        void Update(BoothStyle boothStyle);
        Task UpdateAsync(BoothStyle boothStyle);
    }
}
