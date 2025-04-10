using BusinessLogic.Service.IService;
using Microsoft.Extensions.Caching.Distributed;
using System.IdentityModel.Tokens.Jwt;

namespace ServerAPI.Middlewares
{
    public class RoleValidationMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IDistributedCache _cache;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public RoleValidationMiddleware(RequestDelegate next, IDistributedCache cache, IServiceScopeFactory serviceScopeFactory)
        {
            _next = next;
            _cache = cache;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            if (!string.IsNullOrEmpty(token))
            {
                var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(token);
                var userId = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;

                if (!string.IsNullOrEmpty(userId))
                {
                    using (var scope = _serviceScopeFactory.CreateScope())
                    {
                        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();

                        var currentUser = await userService.UserDetailById(userId);
                        var currentRole = currentUser.Role;

                        var tokenRole = jwtToken?.Claims?.FirstOrDefault(c => c.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role")?.Value;

                        if (!string.Equals(currentRole, tokenRole, StringComparison.OrdinalIgnoreCase))
                        {
                            await _cache.SetStringAsync($"blacklist:{token}", "true", new DistributedCacheEntryOptions
                            {
                                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1) 
                            });

                            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
                            await context.Response.WriteAsync("Role của người dùng đã thay đổi, vui lòng đăng nhập lại.");
                            return;
                        }
                    }
                }
            }
            await _next(context);
        }
    }
}
