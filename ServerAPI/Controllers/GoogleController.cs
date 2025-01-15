using BussinessObject.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ServerAPI.Helpers;
using BussinessObject.ViewModels;

namespace ServerAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IConfiguration _configuration;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        [HttpPost("google/callback")]
        public async Task<IActionResult> GoogleCallback([FromBody] TokenRequest tokenRequest)
        {
            try
            {
                string decryptedAccessToken = GoogleTokenHelper.DecryptToken(tokenRequest.Token, _configuration["ENCRYPTION:ENCRYPTION_KEY"], _configuration["ENCRYPTION:ENCRYPTION_IV"]);

                TokenInfo tokenInfo = await GoogleTokenHelper.VerifyAccessTokenAsync(decryptedAccessToken, _configuration["Google:ClientId"]);

                if (tokenInfo != null)
                {
                    return Ok(new { message = "Token is valid", user = tokenInfo });
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
