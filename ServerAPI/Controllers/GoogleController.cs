using BussinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Helpers;
using BussinessObject.ViewModels;
using BussinessObject.Enums;
using ServerAPI.Services;
using Azure.Core;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        [HttpPost("google/callback")]
        public async Task<IActionResult> GoogleCallback([FromBody] TokenRequest tokenRequest)
        {
            try
            {
                var payload = await GoogleTokenHelper.VerifyAccessTokenAsync(tokenRequest.Token, _configuration["GoogleOath2:WebClient"], _configuration["GoogleOath2:MobileClient"]);
                if (payload == null)
                {
                    return Unauthorized(new { message = "Invalid token" });
                }

                User user = _userManager.FindByEmailAsync(payload.Email).Result;

                if (user == null)
                {
                    return Unauthorized(new { message = "User not found" });
                }
                else
                {
                    var roles = await _userManager.GetRolesAsync(user);

                    if (roles == null || !roles.Any())
                    {
                        return Unauthorized(new { message = "User has no roles assigned" });
                    }

                    if (!Enum.TryParse<UserType>(roles[0], out var userRole) || (userRole != UserType.Admin && userRole != UserType.Manager && userRole != UserType.Staff))
                    {
                        return Unauthorized(new { message = "You don't have permission to access!" });
                    }
                    else
                    {
                        return Ok(new { message = "Token is valid", token = JwtHelper.GenerateJwtToken(user.Id, user.UserName, userRole, _configuration["Jwt:PrivateKey"]) });
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Decryption failed", error = ex.Message });
            }
        }
    }
}
