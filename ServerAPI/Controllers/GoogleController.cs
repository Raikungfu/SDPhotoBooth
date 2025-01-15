using BussinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Helpers;
using BussinessObject.ViewModels;
using BussinessObject.Enums;
using ServerAPI.Services;

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
                string decryptedAccessToken = EncryptionService.DecryptData(tokenRequest.Token, _configuration["ENCRYPTION:ENCRYPTION_KEY"], _configuration["ENCRYPTION:ENCRYPTION_IV"]);

                TokenInfo tokenInfo = await GoogleTokenHelper.VerifyAccessTokenAsync(decryptedAccessToken, _configuration["Google:ClientId"]);

                if (tokenInfo != null)
                {
                    User user = _userManager.FindByEmailAsync(tokenInfo.Email).Result;

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
                            return Ok(new { message = "Token is valid", token = JwtHelper.GenerateJwtToken(user.Id, user.UserName, userRole, _configuration["Key:PrivateKey"]) });
                        }
                    }
                }
                else
                {
                    return Unauthorized(new { message = "Invalid token" + tokenRequest.Token });
                }
            }
            catch (Exception ex)
            {
                return Unauthorized(new { message = "Decryption failed", error = ex.Message });
            }
        }
    }
}
