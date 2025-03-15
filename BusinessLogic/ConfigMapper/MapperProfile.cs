using AutoMapper;
using BusinessLogic.DTO.BoothDTO;
using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.DTO.PhotoDTO;
using BusinessLogic.DTO.PhotoHistoryDTO;
using BusinessLogic.DTO.PhotoStyleDTO;
using BusinessLogic.DTO.SessionCodeDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service;
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

            CreateMap<SessionRequestDTO, Session>();
            CreateMap<Session, SessionResponseDTO>()
                .ForMember(dest => dest.BoothName, opt => opt.MapFrom(src => src.Booth != null ? src.Booth.BoothName : null))
                .ForMember(dest => dest.TypeSessionName, opt => opt.MapFrom(src => src.TypeSession != null ? src.TypeSession.Name : null))
                .ForMember(dest => dest.OrderId, opt => opt.MapFrom(src => src.Order.Id));




            CreateMap<OrderRequestDTO, Order>().ReverseMap();
            CreateMap<OrderResponseDTO, Order>().ReverseMap();

            CreateMap<Order, OrderResponseDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User != null ? new UserResponseOnOrderDTO
                {
                    Id = src.User.Id,
                    Email = src.User.Email,
                    PhoneNumber = src.User.PhoneNumber
                } : null))
                .ForMember(dest => dest.SessionCode, opt => opt.MapFrom(src => src.Session != null ? src.Session.Code : null))
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.PaymentMethod != null ? src.PaymentMethod.MethodName : null))
                .ForMember(dest => dest.CouponCode, opt => opt.MapFrom(src => src.Coupon != null ? src.Coupon.Code : null));

            CreateMap<PhotoStyle, PhotoStyleResponseDTO>();
            CreateMap<PhotoStyleRequestDTO, PhotoStyle>();
            CreateMap<PhotoHistory, PhotoHistoryResponseDTO>()
                 .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(p => new PhotoResponseDTO
                 {
                     Url = p.Url,
                     PhotoStyleName = p.PhotoStyle.Name
                 }).ToArray()));
        }
    }
}
