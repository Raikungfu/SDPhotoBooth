namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        //IAccountRepository Account { get; }
        void Save();
    }
}
