using BussinessObject.Enums;

namespace BusinessLogic.DTO.UserDTO
{
    public class UserResponseDTO
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public UserGender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Avatar { get; set; }
        public decimal? Balance { get; set; }
        public string Role { get; set; }
    }
}
