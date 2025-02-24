using AutoMapper;
using BusinessLogic.DTO.UserDTO;
using BusinessLogic.Service.IService;
using BussinessObject.Enums;
using BussinessObject.Models;
using BussinessObject.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using ServerAPI.Helpers;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IdentityController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IDistributedCache _cache;
        private readonly IEmailService _emailService;

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager,
            IConfiguration configuration, IMapper mapper, IDistributedCache cache, IEmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _mapper = mapper;
            _cache = cache;
            _emailService = emailService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password) && !user.IsDeleted)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (signInResult.Succeeded)
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles == null || !roles.Any())
                    {
                        return Unauthorized(new { message = "User has no roles assigned" });
                    }

                    if (!Enum.TryParse<UserType>(roles[0], out var userRole) || (userRole != UserType.Admin && userRole != UserType.Manager && userRole != UserType.Staff && UserType.Customer != userRole))
                    {
                        return Unauthorized(new { message = "You don't have permission to access!" });
                    }
                    else
                    {
                        return Ok(new { message = "Token is valid", token = JwtHelper.GenerateJwtToken(user.Id, user.UserName, userRole, _configuration["Jwt:PrivateKey"]) });
                    }
                }
            }

            return Unauthorized();
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            // Lấy token từ Header Authorization
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (string.IsNullOrEmpty(token))
            {
                return BadRequest(new { message = "Token không hợp lệ" });
            }

            // Đưa token vào blacklist trong Redis
            var expirationTime = DateTimeOffset.UtcNow.AddHours(5); // Token hết hạn sau 5 tiếng
            await _cache.SetStringAsync($"blacklist:{token}", "expired", new DistributedCacheEntryOptions
            {
                AbsoluteExpiration = expirationTime
            });

            // Đăng xuất khỏi Identity
            await _signInManager.SignOutAsync();

            return Ok(new { message = "Đăng xuất thành công" });
        }



        [HttpGet("profile")]
        [Authorize]
        public async Task<IActionResult> Profile()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var userDTO = _mapper.Map<UserResponseDTO>(user);
            userDTO.Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();
            return Ok(userDTO);
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound();
            }
            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            return BadRequest(result.Errors);
        }

        //update profile
        [HttpPatch("update-profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UpdateProfileDTO model)
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound(new { message = "User not found" });
            }

            if (!string.IsNullOrEmpty(model.Email))
            {
                var email = await _userManager.FindByEmailAsync(model.Email);
                if (email != null && email.Id != user.Id)
                {
                    return BadRequest(new { message = "Email already exists" });
                }
                user.Email = model.Email;

            }
            if (!string.IsNullOrEmpty(model.PhoneNumber))
            {
                var phoneNumber = await _userManager.FindByEmailAsync(model.PhoneNumber);
                if (phoneNumber != null && phoneNumber.Id != user.Id)
                {
                    return BadRequest(new { message = "Phone number already exists" });
                }
                user.PhoneNumber = model.PhoneNumber;
            }
            if (model.Gender.HasValue) user.Gender = model.Gender.Value;
            if (model.BirthDate.HasValue) user.BirthDate = model.BirthDate.Value;
            if (!string.IsNullOrEmpty(model.Avatar)) user.Avatar = model.Avatar;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Profile updated successfully" });
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email))
            {
                return BadRequest("Email is required.");
            }
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("User with this email does not exist.");
            }
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            var resetLink = $"https://yourwebsite.com/reset-password?email={request.Email}&token={token}";
            // Send email
            await _emailService.SendResetPasswordEmail(user.Email, resetLink);
            return Ok(new { message = "Reset password link has been sent to your email" });
        }


        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Token) || string.IsNullOrEmpty(request.NewPassword))
            {
                return BadRequest("All fields are required.");
            }

            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                return BadRequest("Invalid email.");
            }

            // Kiểm tra tính hợp lệ của token
            var isValidToken = await _userManager.VerifyUserTokenAsync(user,
                _userManager.Options.Tokens.PasswordResetTokenProvider,
                "ResetPassword",
                request.Token);

            if (!isValidToken)
            {
                return BadRequest("Invalid or expired token.");
            }

            // Đặt lại mật khẩu
            var resetPassResult = await _userManager.ResetPasswordAsync(user, request.Token, request.NewPassword);
            if (!resetPassResult.Succeeded)
            {
                return BadRequest(resetPassResult.Errors.Select(e => e.Description));
            }

            return Ok("Password has been reset successfully.");
        }


        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return Unauthorized();
        }
    }
}