using BusinessLogic.DTO.CouponDTO;

namespace BusinessLogic.Service.IService
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponResponseDTO>> GetCoupons();
        Task<CouponResponseDTO> GetCoupon(string code);
        Task<IEnumerable<CouponResponseDTO>> GetCoupons(string code);
        Task<CouponResponseDTO> GetCouponById(int id);
        Task<CouponResponseDTO> CreateCoupon(CouponRequestDTO couponRequestDTO);
        Task<CouponResponseDTO> UpdateCoupon(int id, CouponRequestDTO couponRequestDTO);
        Task<bool> DeleteCoupon(int id);
        bool IsValidCoupon(CouponResponseDTO coupon, decimal price);
    }
}
