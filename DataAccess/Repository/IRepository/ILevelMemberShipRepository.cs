using BussinessObject.Models;

namespace DataAccess.Repository.IRepository
{
    public interface ILevelMemberShipRepository : IRepository<LevelMemberShip>
    {
        void Update(LevelMemberShip levelMemberShip);
        Task UpdateAsync(LevelMemberShip levelMemberShip);
    }
}
