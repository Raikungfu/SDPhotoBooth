using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.DTO.UserDTO
{
    public class UserRequestDTO
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public UserGender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public decimal? Balance { get; set; }
        public string? Avatar { get; set; }
        public string? Role { get; set; }
    }
}
