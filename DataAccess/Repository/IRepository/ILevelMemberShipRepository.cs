using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ILevelMemberShipRepository : IRepository<LevelMemberShip>
    {
        int Update(LevelMemberShip levelMemberShip);
        Task<int> UpdateAsync(LevelMemberShip levelMemberShip);
    }
}
