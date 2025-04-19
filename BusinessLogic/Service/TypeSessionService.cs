using AutoMapper;
using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.Service.IService;
using BusinessLogic.Utils;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;

namespace BusinessLogic.Service
{
    public class TypeSessionService : ITypeSessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICouponService _couponService;
        private readonly IMapper _mapper;
        public TypeSessionService(IUnitOfWork unitOfWork, IMapper mapper, ICouponService couponService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _couponService = couponService;
        }

        public async Task<int> GetCount()
        {
            var typeSessionCodes = await _unitOfWork.TypeSession.GetAllAsync();
            return typeSessionCodes.Count();
        }
        public async Task<TypeSessionResponseDTO> CreateTypeSessionCodeAsync(TypeSessionRequestDTO typeSessionCodeDTO)
        {
            var typeSessionCode = _mapper.Map<TypeSession>(typeSessionCodeDTO);
            _unitOfWork.TypeSession.Add(typeSessionCode);
            await _unitOfWork.SaveAsync();
            return _mapper.Map<TypeSessionResponseDTO>(typeSessionCode);
        }

        public async Task<bool> DeleteTypeSessionCodeAsync(int id)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            if (typeSessionCode == null)
            {
                return false;
            }
            _unitOfWork.TypeSession.Remove(typeSessionCode);
            await _unitOfWork.SaveAsync();
            return true;
        }

        public async Task<TypeSessionResponseDTO> GetTypeSessionCodeByIdAsync(int id)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(filter: e => e.Id == id);
            var typeSessionCodeDTO = _mapper.Map<TypeSessionResponseDTO>(typeSessionCode);
            return typeSessionCodeDTO;
        }

        public async Task<TypeSessionResponseDTO> GetTypeSessionCodeByNameAsync(string name)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(filter: e => e.Name == name);
            var typeSessionCodeDTO = _mapper.Map<TypeSessionResponseDTO>(typeSessionCode);
            return typeSessionCodeDTO;
        }

        public async Task<IEnumerable<TypeSessionResponseDTO>> SearchTypeSessionCodeByNameAsync(string name)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetAllAsync(filter: e => e.Name.ToLower().Contains(name.ToLower()));
            var typeSessionCodeDTO = _mapper.Map<IEnumerable<TypeSessionResponseDTO>>(typeSessionCode);
            return typeSessionCodeDTO;
        }

        public async Task<IEnumerable<TypeSessionResponseDTO>> GetTypeSessionCodesAsync(PaginationParams? pagination)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetAllAsync(pagination: pagination);
            return _mapper.Map<IEnumerable<TypeSessionResponseDTO>>(typeSessionCode);
        }

        public async Task<bool> UpdateTypeSessionCodeAsync(int id, TypeSessionRequestDTO typeSessionCodeDTO)
        {
            var typeSessionCode = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(e => e.Id == id);

            if (!string.IsNullOrEmpty(typeSessionCodeDTO.Name))
            {
                var isExist = await _unitOfWork.TypeSession.GetFirstOrDefaultAsync(e => e.Name == typeSessionCodeDTO.Name && e.Id != id);
                if (isExist != null)
                {
                    return false;
                }
                typeSessionCode.Name = typeSessionCodeDTO.Name;
            }
            if (typeSessionCodeDTO.ForMobile.HasValue)
            {
                if (typeSessionCodeDTO.ForMobile == false)
                {
                    var typeSessionProducts = await _unitOfWork.TypeSessionProduct.GetAllAsync(s => s.TypeSessionId == id);
                    foreach (var typeSessionProduct in typeSessionProducts)
                    {
                        _unitOfWork.TypeSessionProduct.Remove(id: typeSessionProduct.Id);
                    }
                    typeSessionCode.ForMobile = typeSessionCodeDTO.ForMobile.Value;
                }
                else
                {
                    var typeSessionProducts = await _unitOfWork.TypeSessionProduct.GetAllAsync(s => s.TypeSessionId == id);
                    if (!typeSessionProducts.Any())
                    {
                        var levelMemberships = await _unitOfWork.LevelMembership.GetAllAsync();
                        var coupons = await _couponService.GetCoupons(new PaginationParams());

                        var validCoupons = new List<CouponResponseDTO>();
                        foreach (var c in coupons)
                        {
                            try
                            {
                                _couponService.ValidateCoupon(c, typeSessionCode.Price);
                                validCoupons.Add(c);
                            }
                            catch { }
                        }

                        foreach (var level in levelMemberships)
                        {
                            var typeSessionProduct = new TypeSessionProduct
                            {
                                Name = $"{typeSessionCode.Name}_{level.Name}", 
                                ProductId = $"{ProductIdHelper.Normalize(typeSessionCode.Name)}_{ProductIdHelper.Normalize(level.Name)}", 
                                TypeSessionId = typeSessionCode.Id,
                                LevelMembershipId = level.Id
                            };
                            await _unitOfWork.TypeSessionProduct.AddAsync(typeSessionProduct);
                            await _unitOfWork.SaveAsync();
                        }


                        foreach (var coupon in validCoupons)
                        {
                            var typeSessionProduct = new TypeSessionProduct
                            {
                                Name = $"{typeSessionCode.Name}_{coupon.Name}",
                                ProductId = $"{ProductIdHelper.Normalize(typeSessionCode.Name)}_{ProductIdHelper.Normalize(coupon.Name)}",
                                TypeSessionId = typeSessionCode.Id,
                                CouponId = coupon.Id
                            };
                            await _unitOfWork.TypeSessionProduct.AddAsync(typeSessionProduct);
                            await _unitOfWork.SaveAsync();
                        }

                        typeSessionCode.ForMobile = typeSessionCodeDTO.ForMobile.Value;
                    }
                }
            }
            if(!string.IsNullOrEmpty(typeSessionCodeDTO.Description))
            {
                typeSessionCode.Description = typeSessionCodeDTO.Description;
            }
            if (typeSessionCodeDTO.Duration.HasValue)
            {
                typeSessionCode.Duration = typeSessionCodeDTO.Duration.Value;
            }
            if (typeSessionCodeDTO.Duration.HasValue && typeSessionCodeDTO.Price >= 0)
            {
                typeSessionCode.Price = typeSessionCodeDTO.Price.Value; 
            }
            if (typeSessionCodeDTO.Price.HasValue && typeSessionCodeDTO.Price >= 0)
            {
                typeSessionCode.Price = typeSessionCodeDTO.Price.Value;
            }
            if (typeSessionCodeDTO.AbleTakenNumber.HasValue && typeSessionCodeDTO.AbleTakenNumber >= 0)
            {
                typeSessionCode.AbleTakenNumber = typeSessionCodeDTO.AbleTakenNumber.Value;
            }

            _unitOfWork.TypeSession.Update(typeSessionCode);
            await _unitOfWork.SaveAsync();
            return true;
            //eyJhbGciOiJSUzI1NiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy54bWxzb2FwLm9yZy93cy8yMDA1LzA1L2lkZW50aXR5L2NsYWltcy9uYW1laWRlbnRpZmllciI6IjE4ZDliNDY4LWJkYjUtNDg2ZC1hNzgwLWYxMmJmOGNhNTVmYSIsImh0dHA6Ly9zY2hlbWFzLnhtbHNvYXAub3JnL3dzLzIwMDUvMDUvaWRlbnRpdHkvY2xhaW1zL25hbWUiOiJhZG1pbjEiLCJqdGkiOiJhYTlmYTg4NC1mN2E2LTQ1ZGQtODNjNi1kYWExMGU1YjExYTkiLCJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3JvbGUiOiJBZG1pbiIsImV4cCI6MTc0NzU1ODYwMiwiaXNzIjoiUmFpWXVnaSIsImF1ZCI6IlNhaW50In0.Y3VnoZ3BM0-1be8pnCzz4EDASvyR9MJ3AjbZDTVjIH2uLV4SH_FcRiZcVFI2Qut2k_s7fbQ2fJsbEVikpeJuFpoXGWVIO2tnsYRcqYoHWGuxh7zIDfJPCuKE8WL4AQ2E0_P1ETyHyt7Df3O4xNwc-c5JBDHHHUoWGaiElp8sPiAM0_7HIYnexJ25jGkHL5FAjQHxxWJtInDsWzXwDiMegd2OXSXUZ7J0wEvQTpTsTgNuOLB7_rilz1DlLRoT336YWblqbu3DbtpTPE32vPu9ssDT-5fJFumlSd2bjHI1uFbFIsytnbXDv0rrNjn1WV5AuvGKa_0IpAwEEioB7_jDpQ
        }
    }
}
