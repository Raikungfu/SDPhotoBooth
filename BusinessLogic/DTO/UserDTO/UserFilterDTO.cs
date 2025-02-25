using BussinessObject.Enums;

namespace BusinessLogic.DTO.UserDTO
{
    public class UserFilterDTO
    {
        public List<UserType>? Roles { get; set; }
        public string? Email { get; set; }
        public UserGender? Gender { get; set; }
        public string? Birth { get; set; }
        public string? LocationName { get; set; }
        public bool? IsBanned { get; set; }
    }

}
