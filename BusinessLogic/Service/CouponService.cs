using AutoMapper;
using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;

namespace BusinessLogic.Service
{
    public class CouponService : ICouponService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CouponService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public async Task<CouponResponseDTO> CreateCoupon(CouponRequestDTO couponRequestDTO)
        {
            var coupon = _mapper.Map<Coupon>(couponRequestDTO);
            await _unitOfWork.Coupon.AddAsync(coupon);
            await _unitOfWork.SaveAsync();
            var couponResponseDTO = _mapper.Map<CouponResponseDTO>(coupon);
            return couponResponseDTO;
        }

        public async Task<bool> DeleteCoupon(string code)
        {
            var coupon = await _unitOfWork.Coupon.GetFirstOrDefaultAsync(c => c.Code == code);
            _unitOfWork.Coupon.Remove(coupon);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<IEnumerable<CouponResponseDTO>> GetCoupons()
        {
            var coupon = await _unitOfWork.Coupon.GetAllAsync();
            var couponResponseDTO = _mapper.Map<IEnumerable<CouponResponseDTO>>(coupon);
            return couponResponseDTO;

        }

        public async Task<CouponResponseDTO> GetCoupon(string code)
        {
            var coupon = await _unitOfWork.Coupon.GetFirstOrDefaultAsync(c => c.Code == code);
            if (coupon == null)
            {
                return null;
            }
            var couponResponseDTO = _mapper.Map<CouponResponseDTO>(coupon);
            return couponResponseDTO;
        }

        public async Task<CouponResponseDTO> GetCouponById(int id)
        {
            var coupon = await _unitOfWork.Coupon.GetFirstOrDefaultAsync(c => c.Id == id);
            if (coupon == null)
            {
                return null;
            }
            var couponResponseDTO = _mapper.Map<CouponResponseDTO>(coupon);
            return couponResponseDTO;
        }

        public async Task<CouponResponseDTO> UpdateCoupon(string code, CouponRequestDTO couponRequestDTO)
        {
            var existingCoupon = await _unitOfWork.Coupon.GetFirstOrDefaultAsync(c => c.Code == code);

            if (!string.IsNullOrEmpty(couponRequestDTO.Name))
            {
                existingCoupon.Name = couponRequestDTO.Name;
            }
            if (!string.IsNullOrEmpty(couponRequestDTO.Description))
            {
                existingCoupon.Description = couponRequestDTO.Description;
            }
            if (!string.IsNullOrEmpty(couponRequestDTO.Code))
            {
                existingCoupon.Code = couponRequestDTO.Code;
            }
            if (couponRequestDTO.Discount.HasValue)
            {
                existingCoupon.Discount = couponRequestDTO.Discount.Value;
            }
            if (couponRequestDTO.DiscountPercent.HasValue)
            {
                existingCoupon.DiscountPercent = couponRequestDTO.DiscountPercent.Value;
            }
            if (couponRequestDTO.StartDate.HasValue)
            {
                existingCoupon.StartDate = couponRequestDTO.StartDate.Value;
            }
            if (couponRequestDTO.EndDate.HasValue)
            {
                existingCoupon.EndDate = couponRequestDTO.EndDate.Value;
            }
            if (couponRequestDTO.MaxUse.HasValue)
            {
                existingCoupon.MaxUse = couponRequestDTO.MaxUse.Value;
            }
            if (couponRequestDTO.UsedAmount.HasValue)
            {
                existingCoupon.UsedAmount = couponRequestDTO.UsedAmount.Value;
            }
            if (couponRequestDTO.MaxDiscount.HasValue)
            {
                existingCoupon.MaxDiscount = couponRequestDTO.MaxDiscount.Value;
            }
            if (couponRequestDTO.MinOrder.HasValue)
            {
                existingCoupon.MinOrder = couponRequestDTO.MinOrder.Value;
            }
            if (couponRequestDTO.IsActive.HasValue)
            {
                existingCoupon.IsActive = couponRequestDTO.IsActive.Value;
            }

            _unitOfWork.Coupon.Update(existingCoupon);
            await _unitOfWork.SaveAsync();
            var couponResponseDTO = _mapper.Map<CouponResponseDTO>(existingCoupon);
            return couponResponseDTO;
        }
    }
}
