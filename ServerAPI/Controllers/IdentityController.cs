using BussinessObject.Enums;
using BussinessObject.Models;
using BussinessObject.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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

        public IdentityController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var signInResult = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);

                if (signInResult.Succeeded)
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

            return Unauthorized();
        }

        [HttpGet("access-denied")]
        public IActionResult AccessDenied()
        {
            return Unauthorized();
        }
    }
}