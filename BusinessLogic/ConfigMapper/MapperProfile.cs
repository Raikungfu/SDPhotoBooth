using AutoMapper;
using BusinessLogic.DTO.BoothDTO;
using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.PaymentMethod;
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
                .ForMember(dest => dest.Booth, opt => opt.MapFrom(src => src.Booth != null ? new BoothResponseDTO
                {
                    Id = src.Booth.Id,
                    BoothName = src.Booth.BoothName,
                    Description = src.Booth.Description,
                    Status = src.Booth.Status,
                    CreatedAt = src.Booth.CreatedAt,
                    LastModified = src.Booth.LastModified,
                    IsDeleted = src.Booth.IsDeleted,
                    CreatedById = src.Booth.CreatedById,
                    LastModifiedById = src.Booth.LastModifiedById,
                    Location = src.Booth.Location != null ? new LocationResponseDTO
                    {
                        Id = src.Booth.Location.Id,
                        LocationName = src.Booth.Location.LocationName,
                        Address = src.Booth.Location.Address,
                        CreatedAt = src.Booth.Location.CreatedAt,
                        LastModified = src.Booth.Location.LastModified,
                        IsDeleted = src.Booth.Location.IsDeleted,
                        CreatedById = src.Booth.Location.CreatedById,
                        LastModifiedById = src.Booth.Location.LastModifiedById
                    } : null
                } : null))
                .ForMember(dest => dest.TypeSession, opt => opt.MapFrom(src => src.TypeSession != null ? new TypeSessionResponseDTO
                {
                    Id = src.TypeSession.Id,
                    Name = src.TypeSession.Name,
                    Description = src.TypeSession.Description,
                    Duration = src.TypeSession.Duration,
                    Price = src.TypeSession.Price,
                    isPrinting = src.TypeSession.isPrinting,
                    AbleTakenNumber = src.TypeSession.AbleTakenNumber,
                    CreatedAt = src.TypeSession.CreatedAt,
                    LastModified = src.TypeSession.LastModified,
                    CreatedById = src.TypeSession.CreatedById,
                    LastModifiedById = src.TypeSession.LastModifiedById
                } : null))
                .ForMember(dest => dest.Order, opt => opt.MapFrom(src => src.Order != null ? new OrderResponseDTO
                {
                    Id = src.Order.Id,
                    Status = src.Order.Status,
                    Amount = src.Order.Amount,
                    Email = src.Order.Email,
                    Phone = src.Order.Phone,
                    CreatedAt = src.Order.CreatedAt,
                    LastModified = src.Order.LastModified,
                    IsDeleted = src.Order.IsDeleted,
                    CreatedById = src.Order.CreatedById,
                    LastModifiedById = src.Order.LastModifiedById
                } : null));




            CreateMap<OrderRequestDTO, Order>().ReverseMap();
            CreateMap<OrderResponseDTO, Order>().ReverseMap();

            CreateMap<Order, OrderResponseDTO>()
                .ForMember(dest => dest.User, opt => opt.MapFrom(src => src.User != null ? new UserResponseDTO
                {
                    Id = src.User.Id,
                    FullName = src.User.FullName,
                    UserName = src.User.UserName,
                    Email = src.User.Email,
                    PhoneNumber = src.User.PhoneNumber,
                    Gender = src.User.Gender,
                    BirthDate = src.User.BirthDate,
                    Avatar = src.User.Avatar,
                    Balance = src.User.Balance,
                    IsBanned = src.User.IsBanned,
                    Location = src.User.Location != null ? new LocationResponseDTO
                    {
                        Id = src.User.Location.Id,
                        LocationName = src.User.Location.LocationName,
                        Address = src.User.Location.Address,
                        CreatedAt = src.User.Location.CreatedAt,
                        LastModified = src.User.Location.LastModified,
                        IsDeleted = src.User.Location.IsDeleted,
                        CreatedById = src.User.Location.CreatedById,
                        LastModifiedById = src.User.Location.LastModifiedById
                    } : null
                } : null))
                .ForMember(dest => dest.Session, opt => opt.MapFrom(src => src.Session != null ? new SessionResponseDTO
                {
                    Code = src.Session.Code,
                    Expired = src.Session.Expired,
                    IsActive = src.Session.IsActive,
                    CreatedAt = src.Session.CreatedAt,
                    Booth = src.Session.Booth != null ? new BoothResponseDTO
                    {
                        Id = src.Session.Booth.Id,
                        BoothName = src.Session.Booth.BoothName,
                        Description = src.Session.Booth.Description,
                        Status = src.Session.Booth.Status,
                        CreatedAt = src.Session.Booth.CreatedAt,
                        LastModified = src.Session.Booth.LastModified,
                        IsDeleted = src.Session.Booth.IsDeleted,
                        CreatedById = src.Session.Booth.CreatedById,
                        LastModifiedById = src.Session.Booth.LastModifiedById,
                        Location = src.Session.Booth.Location != null ? new LocationResponseDTO
                        {
                            Id = src.Session.Booth.Location.Id,
                            LocationName = src.Session.Booth.Location.LocationName,
                            Address = src.Session.Booth.Location.Address,
                            CreatedAt = src.Session.Booth.Location.CreatedAt,
                            LastModified = src.Session.Booth.Location.LastModified,
                            IsDeleted = src.Session.Booth.Location.IsDeleted,
                            CreatedById = src.Session.Booth.Location.CreatedById,
                            LastModifiedById = src.Session.Booth.Location.LastModifiedById
                        } : null
                    } : null,
                    TypeSession = src.Session.TypeSession != null ? new TypeSessionResponseDTO
                    {
                        Id = src.Session.TypeSession.Id,
                        Name = src.Session.TypeSession.Name,
                        Description = src.Session.TypeSession.Description,
                        Duration = src.Session.TypeSession.Duration,
                        Price = src.Session.TypeSession.Price,
                        isPrinting = src.Session.TypeSession.isPrinting,
                        AbleTakenNumber = src.Session.TypeSession.AbleTakenNumber,
                        CreatedAt = src.Session.TypeSession.CreatedAt,
                        LastModified = src.Session.TypeSession.LastModified,
                        CreatedById = src.Session.TypeSession.CreatedById,
                        LastModifiedById = src.Session.TypeSession.LastModifiedById
                    } : null
                } : null))
                .ForMember(dest => dest.PaymentMethod, opt => opt.MapFrom(src => src.PaymentMethod != null ? new PaymentMethodResponseDTO
                {
                    Id = src.PaymentMethod.Id,
                    MethodName = src.PaymentMethod.MethodName,
                    Description = src.PaymentMethod.Description,
                    IsActive = src.PaymentMethod.IsActive,
                    CreatedAt = src.PaymentMethod.CreatedAt,
                    LastModified = src.PaymentMethod.LastModified,
                    IsDeleted = src.PaymentMethod.IsDeleted,
                    CreatedById = src.PaymentMethod.CreatedById,
                    LastModifiedById = src.PaymentMethod.LastModifiedById
                } : null))
                .ForMember(dest => dest.Coupon, opt => opt.MapFrom(src => src.Coupon != null ? new CouponResponseDTO
                {
                    Id = src.Coupon.Id,
                    Code = src.Coupon.Code,
                    Discount = src.Coupon.Discount,
                    IsActive = src.Coupon.IsActive,
                    CreatedAt = src.Coupon.CreatedAt,
                    LastModified = src.Coupon.LastModified,
                    IsDeleted = src.Coupon.IsDeleted,
                    CreatedById = src.Coupon.CreatedById,
                    LastModifiedById = src.Coupon.LastModifiedById
                } : null));
        }
    }
}
