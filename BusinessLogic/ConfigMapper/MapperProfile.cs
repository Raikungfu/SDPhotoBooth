using AutoMapper;
using BusinessLogic.DTO.BoothDTO;
using BusinessLogic.DTO.CoordinateDTO;
using BusinessLogic.DTO.CouponDTO;
using BusinessLogic.DTO.FrameDTO;
using BusinessLogic.DTO.LevelMembershipDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.MembershipCardDTO;
using BusinessLogic.DTO.OrderDTO;
using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.DTO.PhotoDTO;
using BusinessLogic.DTO.PhotoHistoryDTO;
using BusinessLogic.DTO.PhotoStyleDTO;
using BusinessLogic.DTO.SessionCodeDTO;
using BusinessLogic.DTO.StickerDTO;
using BusinessLogic.DTO.StickerStyleDTO;
using BusinessLogic.DTO.TypeSessionDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.DTO.WalletDTO;
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
                    CreatedById = src.Location.CreatedById,
                    LastModifiedById = src.Location.LastModifiedById
                } : null));

            CreateMap<CouponRequestDTO, Coupon>();
            CreateMap<Coupon, CouponResponseDTO>();

            CreateMap<SessionRequestDTO, Session>();
            CreateMap<Session, SessionResponseDTO>();



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
                .ForMember(dest => dest.PaymentMethodName, opt => opt.MapFrom(src => src.Payment != null ? src.Payment.PaymentMethod.MethodName : null))
                .ForMember(dest => dest.LocationName, opt => opt.MapFrom(src => src.Session != null ? src.Session.PhotoHistory.Booth.Location.LocationName : null))
                .ForMember(dest => dest.BoothName, opt => opt.MapFrom(src => src.Session != null ? src.Session.PhotoHistory.Booth.BoothName : null))
                .ForMember(dest => dest.CouponCode, opt => opt.MapFrom(src => src.Coupon != null ? src.Coupon.Code : null))
                .ForMember(dest => dest.TypeSessionName, opt => opt.MapFrom(src => src.TypeSession.Name != null ? src.TypeSession.Name : null));
            
            CreateMap<PhotoStyle, PhotoStyleResponseDTO>();
            CreateMap<PhotoStyleRequestDTO, PhotoStyle>();
            CreateMap<PhotostyleWithFormFileRequestDTO, PhotoStyleRequestDTO>();

            CreateMap<PhotoHistory, PhotoHistoryResponseDTO>()
                 .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(p => new PhotoResponseDTO
                 {
                     Url = p.Url,
                     PhotoStyleName = p.PhotoStyle.Name
                 }).ToArray()));

            CreateMap<StickerRequestDTO, Sticker>();
            CreateMap<Sticker, StickerResponseDTO>()
                .ForMember(dest => dest.StickerStyleName, opt => opt.MapFrom(src => src.StickerStyle != null ? src.StickerStyle.StickerStyleName : null));

            CreateMap<StickerStyleRequestDTO, StickerStyle>();
            CreateMap<StickerStyle, StickerStyleResponseDTO>()
                .ForMember(dest => dest.Stickers, opt => opt.MapFrom(src => src.Stickers.Select(s => new StickerOnStickerStyleResponseDTO
                {
                    Id = s.Id,
                    Name = s.Name,
                    StickerUrl = s.StickerUrl
                })));

            CreateMap<FrameStyleRequestDTO, FrameStyle>();
            CreateMap<FrameStyle, FrameStyleResponseDTO>();

            CreateMap<FrameRequestDTO, Frame>();
            CreateMap<Frame, FrameResponseDTO>()
                .ForMember(dest => dest.FrameStyleName, opt => opt.MapFrom(src => src.FrameStyle != null ? src.FrameStyle.Name : null));
            CreateMap<FrameWithFormFileDTO, FrameRequestDTO>();
            CreateMap<Coordinate, CordianteOnFrameResponseDTO>();


            CreateMap<CoordinateRequestDTO, Coordinate>();
            CreateMap<Coordinate, CoordinateResponseDTO>()
                .ForMember(dest => dest.FrameName, opt => opt.MapFrom(src => src.Frame != null ? src.Frame.Name : null));

            CreateMap<CreateLevelMembershipRequestDTO, LevelMembership>();
            CreateMap<LevelMembership, LevelMembershipResponseDTO>();

            CreateMap<CreateMembershipCardRequestDTO, MembershipCard>();
            CreateMap<MembershipCard, MembershipCardResponseDTO>()
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => src.Customer != null ? new CustomerOnMembershipCard 
                {
                    Id = src.Customer.Id,
                    FullName = src.Customer.FullName,
                    Gender = src.Customer.Gender,
                    BirthDate = src.Customer.BirthDate,
                    Email = src.Customer.Email,
                    PhoneNumber = src.Customer.PhoneNumber
                } : null))
                .ForMember(dest => dest.LevelMemberShip, opt => opt.MapFrom(src => src.LevelMemberShip != null ? new LevelMembershipOnMembershipCard
                {
                    Id = src.LevelMemberShip.Id,
                    Name = src.LevelMemberShip.Name,
                    Point = src.LevelMemberShip.Point
                } : null));

            CreateMap<WalletRequestDTO, Wallet>();
            CreateMap<Wallet, WalletResponseDTO>();
        }
    }
}
