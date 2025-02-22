using AutoMapper;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.PaymentMethod;
using BusinessLogic.DTO.UserDTO;
using BussinessObject.Models;

namespace BusinessLogic.ConfigMapper
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<LocationRequestDTO, Location>();
            CreateMap<Location, LocationResponseDTO>();

            CreateMap<PaymentMethodRequestDTO, PaymentMethod>();
            CreateMap<PaymentMethod, PaymentMethodResponseDTO>();

            CreateMap<User, UserResponseDTO>();
        }
    }
}
