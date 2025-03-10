using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.UserDTO;
using BussinessObject.Enums;

namespace BusinessLogic.Service.IService
{
    public interface IUserService
    {
        //Filter
        Task<IEnumerable<UserResponseDTO>> Filter(UserFilterDTO filter);
        Task<UserResponseDTO> CreateUser(UserRequestDTO userRequestDTO);
        Task<UserResponseDTO> ResetPassword(string email, string newPassword);
        Task<UserResponseDTO> BanUser(string email);
        Task<UserResponseDTO> UnbanUser(string email);
        Task<UserResponseDTO> UserDetail(string email);
        Task<bool> ChangeRole(string email, UserType role);
        Task<bool> MoveLocation(string email, int locationId);
        Task<TotalUserStaticResponseDTO> StaticUserCreated(StaticType staticType);
    }
}
