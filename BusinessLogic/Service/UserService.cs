using AutoMapper;
using BusinessLogic.DTO.DashboardDTO;
using BusinessLogic.DTO.LocationDTO;
using BusinessLogic.DTO.MembershipCardDTO;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.DTO.WalletDTO;
using BusinessLogic.Service.IService;
using BusinessLogic.Utils;
using BussinessObject.Enums;
using BussinessObject.Models;
using DataAccess.Extensions.Pagination;
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
        private readonly IWalletService _walletService;
        private readonly IMembershipCardService _membershipCardService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public UserService(IMapper mapper, UserManager<User> userManager, ILocationService locationService,
            IUnitOfWork unitOfWork, IWalletService walletService, IMembershipCardService membershipCardService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _locationService = locationService;
            _unitOfWork = unitOfWork;
            _walletService = walletService;
            _membershipCardService = membershipCardService;
        }

        public async Task<int> GetCount()
        {
            var users = await _userManager.Users.Where(u => !u.IsDeleted).CountAsync();
            return users;
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
            if(userRequestDTO.Role == UserType.Customer)
            {
                await _walletService.CreateWallet(user.Id, new WalletRequestDTO());
                await _membershipCardService.Create(new CreateMembershipCardRequestDTO() {
                    CustomerId = user.Id,
                    LevelMemberShipId = 1,
                    Points = 0,
                    IsActive = true
                });
            }
            
            var createdUser = await _userManager.Users
            .Include(u => u.MembershipCard)
            .ThenInclude(m => m.LevelMemberShip)
                .Include(u => u.Wallet)
            .FirstOrDefaultAsync(u => u.Id == user.Id);

            var userDTO = _mapper.Map<UserResponseDTO>(createdUser);
            userDTO.Role = userRequestDTO.Role.ToString();
            userDTO.Location = userRequestDTO.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(userRequestDTO.LocationId.Value)) : null;
            return userDTO;
        }

        public async Task<IEnumerable<UserResponseDTO>> GetUsers(PaginationParams? pagination)
        {
            var query = _userManager.Users
                .Include(u => u.Wallet)
                .Include(u => u.MembershipCard)
                    .ThenInclude(m => m.LevelMemberShip)
                .AsQueryable();

            if (pagination != null)
            {
                var (isPaged, pageNumber, pageSize) = pagination.Validate();
                if (isPaged)
                {
                    query = query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
                }
            }

            var users = await query.ToListAsync();

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
                Role = userRoles.GetValueOrDefault(u.Id),
                IsBanned = u.IsBanned,
                Location = _mapper.Map<LocationResponseDTO>(u.Location),
                Wallet = _mapper.Map<WalletResponseDTO>(u.Wallet),
                MembershipCard = _mapper.Map<MemberShipCardOnUserResponseDTO>(u.MembershipCard)
            }).ToList();
        }


        public async Task<UserResponseDTO> UserDetail(string email)
        {
            var user = await _userManager.Users
             .Include(u => u.MembershipCard)
             .ThenInclude(m => m.LevelMemberShip)
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
            userDTO.MembershipCard = _mapper.Map<MemberShipCardOnUserResponseDTO>(user.MembershipCard);
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

        public async Task<UserResponseDTO> UserDetailById(string Id)
        {
            var user = await _userManager.Users
             .Include(u => u.MembershipCard)
             .ThenInclude(m => m.LevelMemberShip)
                .Include(u => u.Wallet)
             .FirstOrDefaultAsync(u => u.Id == Id);
            if (user == null)
            {
                return null;
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            var roleOfUser = await _userManager.GetRolesAsync(user);
            userDTO.Role = roleOfUser.FirstOrDefault() ?? "No Role";
            userDTO.Location = user.LocationId.HasValue ? _mapper.Map<LocationResponseDTO>(await _locationService.GetLocation(user.LocationId.Value)) : null;
            userDTO.Wallet = _mapper.Map<WalletResponseDTO>(user.Wallet);
            userDTO.MembershipCard = _mapper.Map<MemberShipCardOnUserResponseDTO>(user.MembershipCard);
            return userDTO;
        }
    }
}
