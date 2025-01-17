using AutoMapper;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Service
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetAll(UserType userType)
        {
            var users = await _unitOfWork.User.GetAllAsync(u => !u.IsDeleted);
            var userList = new List<UserResponseDTO>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                if (roles.Contains(userType.ToString()))
                {
                    var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
                    userResponseDTO.Role = Enum.GetName(typeof(UserType), userType);
                    userList.Add(userResponseDTO);
                }
            }

            return userList;
        }

        public async Task<UserResponseDTO> GetById(string id, UserType userType)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) return null;

            var roles = await _userManager.GetRolesAsync(user);
            if (roles.Contains(userType.ToString()))
            {
                var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
                userResponseDTO.Role = Enum.GetName(typeof(UserType), userType);
                return userResponseDTO;
            }

            return null;
        }

        public async Task<string> Create(UserRequestDTO userRequestDTO, UserType userType)
        {
            var user = _mapper.Map<User>(userRequestDTO);
            user.CreatedAt = DateTime.UtcNow;
            user.UpdatedAt = DateTime.UtcNow;

            var result = await _userManager.CreateAsync(user);
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(userType.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(userType.ToString()));
                }
                await _userManager.AddToRoleAsync(user, userType.ToString());
                return user.Id;
            }

            throw new Exception("Failed to create user.");
        }

        public async Task<UserResponseDTO> Update(string id, UserRequestDTO userRequestDTO)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) return null;
            if (!string.IsNullOrEmpty(userRequestDTO.UserName))
            {
                user.UserName = userRequestDTO.UserName;
            }
            if (!string.IsNullOrEmpty(userRequestDTO.Email))
            {
                user.Email = userRequestDTO.Email;
            }
            if (userRequestDTO.Gender.HasValue)
            {
                user.Gender = userRequestDTO.Gender.Value;
            }
            if (userRequestDTO.BirthDate.HasValue)
            {
                user.BirthDate = userRequestDTO.BirthDate.Value;
            }
            if (userRequestDTO.Balance.HasValue)
            {
                user.Balance = userRequestDTO.Balance.Value;
            }
            if (!string.IsNullOrEmpty(userRequestDTO.Avatar))
            {
                user.Avatar = userRequestDTO.Avatar;
            }

            user.UpdatedAt = DateTime.UtcNow;

            var currentRoles = await _userManager.GetRolesAsync(user);
            if (!currentRoles.Contains(userRequestDTO.Role))
            {
                await _userManager.RemoveFromRolesAsync(user, currentRoles);
                await _userManager.AddToRoleAsync(user, userRequestDTO.Role);
            }
            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync(); 
            var userResponseDTO = _mapper.Map<UserResponseDTO>(user);
            userResponseDTO.Role = userRequestDTO.Role;

            return userResponseDTO;
        }

        public async Task<bool> Delete(string id)
        {
            var user = await _unitOfWork.User.GetFirstOrDefaultAsync(u => u.Id == id && !u.IsDeleted);
            if (user == null) return false;

            user.IsDeleted = true;
            user.UpdatedAt = DateTime.UtcNow;

            _unitOfWork.User.Update(user);
            await _unitOfWork.SaveAsync();

            return true;
        }
    }

}