using Microsoft.Extensions.Options;
using WebApplication1.Models.Common;
using WebApplication1.Services;

namespace WebApplication1.Middleware
{
    public class TokenMiddleware
    {
        private readonly RequestDelegate next;
        private readonly AppSettings appSettings;

        public TokenMiddleware(RequestDelegate next, IOptions<AppSettings> appSettings)
        {
            this.next = next;
            this.appSettings = appSettings.Value;
        }

        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            DTO.Authentication.UserAuthorisationModel user = jwtUtils.ValidateToken(token ?? "");
            if (user != null)
            {
                // attach user to context on successful jwt validation
                context.Items["User"] = user;
            }

            await next(context);
        }
    }
}
