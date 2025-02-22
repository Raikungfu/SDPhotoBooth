using BussinessObject.Enums;

namespace BusinessLogic.DTO.UserDTO
{
    public class UpdateProfileDTO
    {
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public UserGender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Avatar { get; set; }
    }
}
