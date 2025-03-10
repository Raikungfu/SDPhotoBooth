using BusinessLogic.DTO.CouponDTO;

namespace BusinessLogic.Service.IService
{
    public interface ICouponService
    {
        Task<IEnumerable<CouponResponseDTO>> Filter(FilterCouponDTO filter);
        Task<CouponResponseDTO> GetCoupon(string code);
        Task<CouponResponseDTO> GetCouponById(int id);
        Task<CouponResponseDTO> CreateCoupon(CouponRequestDTO couponRequestDTO);
        Task<CouponResponseDTO> UpdateCoupon(string code, CouponRequestDTO couponRequestDTO);
        Task<bool> DeleteCoupon(string id);

    }
}
