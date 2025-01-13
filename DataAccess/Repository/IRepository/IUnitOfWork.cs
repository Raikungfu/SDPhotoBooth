namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBoothRepository Booth { get; }
        ICouponRepository Coupon { get; }
        ILevelMemberShipRepository LevelMemberShip { get; }
        ILocationRepository Location { get; }
        IMembershipCardRepository MembershipCard { get; }
        IOrderRepository Order { get; }
        IPaymentMethodRepository Payment { get; }
        IPhotoHistoryRepository PhotoHistory { get; }
        IPhotoRepository Photo { get; }
        IPhotoStyleRepository PhotoStyle { get; }
        ISessionRepository Session { get; }
        ITypeSessionRepository TypeSession { get; }
        IUserRepository User { get; }
        IBoothStyleRepository BoothStyle { get; }
        void Save();
    }
}
