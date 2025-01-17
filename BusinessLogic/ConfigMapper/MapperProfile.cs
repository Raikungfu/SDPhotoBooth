using AutoMapper;
using BusinessLogic.DTO.BoothStyleDTO;
using BusinessLogic.DTO.LocationDTO;
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
            CreateMap<BoothStyleRequestDTO, BoothStyle>();
            CreateMap<BoothStyle, BoothStyleResponseDTO>();
            CreateMap<User, UserResponseDTO>();
            CreateMap<UserRequestDTO, User>();
        }
    }
}
