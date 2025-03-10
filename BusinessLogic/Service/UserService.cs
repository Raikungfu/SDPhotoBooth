using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BusinessLogic.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;
        private readonly ILocationService _locationService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, UserManager<User> userManager, ILocationService locationService, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _userManager = userManager;
            _locationService = locationService;
            this._unitOfWork = unitOfWork;
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
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, userRequestDTO.Role.ToString());
            }

            if (!result.Succeeded)
            {
                throw new Exception($"Failed to create new user: {string.Join(", ", result.Errors.Select(e => e.Description))}");
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            userDTO.Role = userRequestDTO.Role.ToString();
            userDTO.Location = userRequestDTO.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(userRequestDTO.LocationId.Value)) : null;
            return userDTO;
        }

        public async Task<IEnumerable<UserResponseDTO>> Filter(UserFilterDTO filter)
        {
            var users = await _userManager.Users.ToListAsync();
            foreach (var user in users)
            {
                if (user.LocationId != null)
                {
                    var locationEntity = await _locationService.GetLocation(user.LocationId.Value);
                    user.Location = _mapper.Map<Location>(locationEntity);
                }
            }

            var query = _userManager.Users.AsQueryable();

            if (filter.Roles != null && filter.Roles.Any())
            {
                var selectedRoles = filter.Roles.ToHashSet();
                var usersWithSelectedRoles = new List<string>();

                foreach (var user in users)
                {
                    var roles = (await _userManager.GetRolesAsync(user))
                                .Select(role => Enum.TryParse<UserType>(role, out var userType) ? userType : (UserType?)null)
                                .Where(role => role.HasValue)
                                .Select(role => role!.Value)
                                .ToList();

                    if (roles.Any(role => selectedRoles.Contains(role)))
                    {
                        usersWithSelectedRoles.Add(user.Id);
                    }
                }
                query = query.Where(u => usersWithSelectedRoles.Contains(u.Id));
            }

            // Lọc theo Email
            if (!string.IsNullOrEmpty(filter.Email))
            {
                query = query.Where(u => u.Email.Contains(filter.Email));
            }

            // Lọc theo Gender
            if (filter.Gender.HasValue)
            {
                query = query.Where(u => u.Gender == filter.Gender.Value);
            }

            // Lọc theo ngày sinh
            if (!string.IsNullOrEmpty(filter.Birth))
            {
                if (int.TryParse(filter.Birth, out int birthYear))
                {
                    query = query.Where(u => u.BirthDate.HasValue && u.BirthDate.Value.Year == birthYear);
                }
            }

            // Lọc theo LocationName nếu user thuộc vai trò "Staff"
            LocationResponseDTO? location = null;
            if (!string.IsNullOrEmpty(filter.LocationName) && filter.Roles != null && filter.Roles.Contains(UserType.Staff))
            {
                location = await _locationService.GetLocation(filter.LocationName);
            }
            if (location != null)
            {
                query = query.Where(u => u.LocationId == location.Id);
            }

            // Lọc theo IsBanned
            if (filter.IsBanned.HasValue)
            {
                query = query.Where(u => u.IsBanned == filter.IsBanned.Value);
            }

            if (!string.IsNullOrEmpty(filter.LocationName))
            {
                query = query.Where(u => u.Location.LocationName == filter.LocationName);
            }

            // Lấy danh sách Users sau khi đã lọc
            var usersList = await query.ToListAsync();

            // Lấy danh sách vai trò của Users
            var userRoles = new Dictionary<string, string?>();
            foreach (var user in usersList)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles.FirstOrDefault();
            }
            // Map dữ liệu vào DTO
            return usersList.Select(u => new UserResponseDTO
            {
                Id = u.Id,
                FullName = u.FullName,
                UserName = u.UserName,
                Email = u.Email,
                PhoneNumber = u.PhoneNumber,
                Gender = u.Gender,
                BirthDate = u.BirthDate,
                Avatar = u.Avatar,
                Balance = u.Balance,
                Role = userRoles.ContainsKey(u.Id) ? userRoles[u.Id] : null,
                IsBanned = u.IsBanned,
                Location = _mapper.Map<LocationResponseDTO>(u.Location)
            }).ToList();
        }


        public Task<UserResponseDTO> ResetPassword(string email, string newPassword)
        {
            throw new NotImplementedException();
        }

        public async Task<UserResponseDTO> UserDetail(string email)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
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

            var today = DateTime.Today;
            var usersInRole = await _userManager.GetUsersInRoleAsync("Customer");
            var userIds = usersInRole.Select(u => u.Id).ToList();

            switch (staticType)
            {
                case StaticType.Day:
                    totalUserStatic.TotalUser = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= today && s.CreatedAt < today.AddDays(1) && userIds.Contains(s.Id));
                    totalUserStatic.TotalUserPrev = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= today.AddDays(-1) && s.CreatedAt < today && userIds.Contains(s.Id));
                    break;

                case StaticType.Week:
                    var startOfWeek = today.AddDays(-(int)today.DayOfWeek);
                    var startOfPrevWeek = startOfWeek.AddDays(-7);
                    totalUserStatic.TotalUser = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startOfWeek && userIds.Contains(s.Id));
                    totalUserStatic.TotalUserPrev = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startOfPrevWeek && s.CreatedAt < startOfWeek && userIds.Contains(s.Id));
                    break;

                case StaticType.Month:
                    var startOfMonth = new DateTime(today.Year, today.Month, 1);
                    var startOfPrevMonth = startOfMonth.AddMonths(-1);
                    totalUserStatic.TotalUser = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startOfMonth && userIds.Contains(s.Id));
                    totalUserStatic.TotalUserPrev = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startOfPrevMonth && s.CreatedAt < startOfMonth && userIds.Contains(s.Id));
                    break;

                case StaticType.Year:
                    var startOfYear = new DateTime(today.Year, 1, 1);
                    var startOfPrevYear = startOfYear.AddYears(-1);
                    totalUserStatic.TotalUser = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startOfYear && userIds.Contains(s.Id));
                    totalUserStatic.TotalUserPrev = await _unitOfWork.User.CountAsync(s => s.CreatedAt >= startOfPrevYear && s.CreatedAt < startOfYear && userIds.Contains(s.Id));
                    break;
            }

            return totalUserStatic;
        }

    }
}
