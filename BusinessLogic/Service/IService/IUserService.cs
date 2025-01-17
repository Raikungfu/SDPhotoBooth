using BusinessLogic.DTO.UserDTO;
using BussinessObject.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service.IService
{
    public interface IUserService
    {
        Task<IEnumerable<UserResponseDTO>> GetAll(UserType userType);
        Task<UserResponseDTO> GetById(string id, UserType userType);
        Task<IEnumerable<UserResponseDTO>> GetByLocation(int locationId);
        Task<string> Create(UserRequestDTO userRequestDTO, UserType userType);
        Task<UserResponseDTO> Update(string id, UserRequestDTO userRequestDTO);
        Task<bool> Delete(string id);
    }
}