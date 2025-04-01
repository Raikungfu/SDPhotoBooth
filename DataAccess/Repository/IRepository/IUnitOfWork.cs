namespace DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        IBoothRepository Booth { get; }
        ICouponRepository Coupon { get; }
        ILocationRepository Location { get; }
        IOrderRepository Order { get; }
        IPaymentMethodRepository PaymentMethod { get; }
        IPhotoHistoryRepository PhotoHistory { get; }
        IPhotoRepository Photo { get; }
        ISessionRepository Session { get; }
        ITypeSessionRepository TypeSession { get; }
        IPhotoStyleRepository PhotoStyle { get; }
        IUserRepository User { get; }
        IStickerRepository Sticker { get; }

        IStickerStyleRepository StickerStyle { get; }
        void Save();
        Task SaveAsync();
    }
}
