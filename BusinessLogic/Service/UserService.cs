using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.DTO.WalletDTO;
using BusinessLogic.Service.IService;
using BusinessLogic.Utils;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Numerics;

namespace BusinessLogic.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILocationService _locationService;
        private readonly IWalletService _walletService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, UserManager<User> userManager, ILocationService locationService, IUnitOfWork unitOfWork, IWalletService walletService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _locationService = locationService;
            this._unitOfWork = unitOfWork;
            _walletService = walletService;
        }

        public async Task<UserResponseDTO> UserDetailById(string id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user == null)
            {
                return null;
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            var roleOfUser = await _userManager.GetRolesAsync(user);
            userDTO.Role = roleOfUser.FirstOrDefault() ?? "No Role";
            userDTO.Location = user.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(user.LocationId.Value)) : null;
            return userDTO;
        }
        public async Task<UserResponseDTO> BanUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            user.IsBanned = true;
            await _userManager.UpdateAsync(user);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> UnbanUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null;
            }
            user.IsBanned = false;
            await _userManager.UpdateAsync(user);
            return _mapper.Map<UserResponseDTO>(user);
        }

        public async Task<UserResponseDTO> CreateUser(UserRequestDTO userRequestDTO)
        {
            var isExist = await _userManager.FindByEmailAsync(userRequestDTO.Email);
            if (isExist != null)
            {
                return null;
            }
            var user = _mapper.Map<User>(userRequestDTO);
            if(userRequestDTO.Role != UserType.Staff)
            {
                user.LocationId = null;
            }

            var result = await _userManager.CreateAsync(user, userRequestDTO.Password);
            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create new user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }

            await _userManager.AddToRoleAsync(user, userRequestDTO.Role.ToString());
            await _walletService.CreateWallet(user.Id, new WalletRequestDTO());

            var createdUser = await _userManager.Users
            .Include(u => u.Wallet)
            .FirstOrDefaultAsync(u => u.Id == user.Id);

            var userDTO = _mapper.Map<UserResponseDTO>(createdUser);
            userDTO.Role = userRequestDTO.Role.ToString();
            userDTO.Location = userRequestDTO.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(userRequestDTO.LocationId.Value)) : null;
            return userDTO;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsers()
        {
            var users = await _userManager.Users
            .Include(u => u.Wallet)
            .ToListAsync();

            foreach (var user in users)
            {
                if (user.LocationId != null)
                {
                    var locationEntity = await _locationService.GetLocation(user.LocationId.Value);
                    user.Location = _mapper.Map<Location>(locationEntity);
                }
            }

            var userRoles = new Dictionary<string, string?>();
            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.FirstOrDefault();
            }

            return users.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Gender = u.Gender,
                BirthDate = u.BirthDate,
                Avatar = u.Avatar,
                Role = userRoles.ContainsKey(u.Id) ? userRoles[u.Id] : null,
                IsBanned = u.IsBanned,
                Location = _mapper.Map<LocationResponseDTO>(u.Location),
                Wallet = _mapper.Map<WalletResponseDTO>(u.Wallet)
            }).ToList();
        }


        public Task<UserResponseDTO> ResetPassword(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDTO> UserDetail(string email)
        {
            var user = await _userManager.Users
             .Include(u => u.Wallet)
             .FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
            {
                return null;
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            var roleOfUser = await _userManager.GetRolesAsync(user);
            userDTO.Role = roleOfUser.FirstOrDefault() ?? "No Role";
            userDTO.Location = user.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(user.LocationId.Value)) : null;
            userDTO.Wallet = _mapper.Map<WalletResponseDTO>(user.Wallet);
            return userDTO;
        }

        public async Task<UserResponseDTO> UserDetailByPhone(string phone)
        {
            var user = await _userManager.Users
             .Include(u => u.Wallet)
             .FirstOrDefaultAsync(u => u.PhoneNumber == phone);
            if (user == null)
            {
                return null;
            }

            var userDTO = _mapper.Map<UserResponseDTO>(user);
            var roleOfUser = await _userManager.GetRolesAsync(user);
            userDTO.Role = roleOfUser.FirstOrDefault() ?? "No Role";
            userDTO.Location = user.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(user.LocationId.Value)) : null;
            userDTO.Wallet = _mapper.Map<WalletResponseDTO>(user.Wallet);
            return userDTO;
        }

        public async Task<bool> ChangeRole(string email, UserType role)
        {
            var user = await _userManager.FindByEmailAsync(email);
            var currentRoles = await _userManager.GetRolesAsync(user);
            await _userManager.RemoveFromRolesAsync(user, currentRoles);
            await _userManager.AddToRoleAsync(user, role.ToString());
            return true;
        }

        public async Task<bool> MoveLocation(string email, int locationId)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return false;
            }
            user.LocationId = locationId;
            await _userManager.UpdateAsync(user);
            return true;
        }



        public async Task<TotalUserStaticResponseDTO> StaticUserCreated(StaticType staticType)
        {
            var totalUserStatic = new TotalUserStaticResponseDTO();
            (DateTime start, DateTime startPrev) = TimeRangeHelper.GetTimeRange(staticType);

            totalUserStatic.TotalUser = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= start && s.CreatedAt < start.Add(TimeRangeHelper.GetStepSize(staticType)) && !s.IsBanned && !s.IsDeleted);
            totalUserStatic.TotalUserPrev = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startPrev && s.CreatedAt < start && !s.IsBanned && !s.IsDeleted);

            return totalUserStatic;
        }
    }
}
