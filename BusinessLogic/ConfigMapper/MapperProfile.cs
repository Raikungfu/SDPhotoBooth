using AutoMapper;
using BusinessLogic.DTO.BoothDTO;
using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.UserDTO;
using BussinessObject.Models;

namespace BusinessLogic.ConfigMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LocationRequestDTO, Location>();
            CreateMap<Location, LocationResponseDTO>().ReverseMap();

            CreateMap<PaymentMethodRequestDTO, PaymentMethod>();
            CreateMap<PaymentMethod, PaymentMethodResponseDTO>();

            CreateMap<UserRequestDTO, User>();
            CreateMap<User, UserResponseDTO>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location != null ? new LocationResponseDTO
            {
                Id = src.Location.Id,
                LocationName = src.Location.LocationName,
                Address = src.Location.Address,
                CreatedAt = src.Location.CreatedAt,
                LastModified = src.Location.LastModified,
                IsDeleted = src.Location.IsDeleted,
                CreatedById = src.Location.CreatedById,
                LastModifiedById = src.Location.LastModifiedById
            } : null)); 

            CreateMap<TypeSessionRequestDTO, TypeSession>();
            CreateMap<TypeSession, TypeSessionResponseDTO>();

            CreateMap<BoothRequestDTO, Booth>();
            CreateMap<Booth, BoothResponseDTO>()
                .ForMember(dest => dest.Location, opt => opt.MapFrom(src => src.Location != null ? new LocationResponseDTO
                {
                    Id = src.Location.Id,
                    LocationName = src.Location.LocationName,
                    Address = src.Location.Address,
                    CreatedAt = src.Location.CreatedAt,
                    LastModified = src.Location.LastModified,
                    IsDeleted = src.Location.IsDeleted,
                    CreatedById = src.Location.CreatedById,
                    LastModifiedById = src.Location.LastModifiedById
                } : null));

            CreateMap<CouponRequestDTO, Coupon>();
            CreateMap<Coupon, CouponResponseDTO>();
        }
    }
}
