using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO.Authentication;
using WebApplication1.EntityFramework;
using WebApplication1.Models.Exceptions;
using WebApplication1.Services.Utils;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebApplication1.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly ILogger<AuthenticationService> logger;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IJwtUtils jwtUtils;

        public AuthenticationService(
            ILogger<AuthenticationService> logger,
            ApplicationDbContext applicationDbContext,
            IJwtUtils jwtUtils
            )
        {
            this.logger = logger;
            this.applicationDbContext = applicationDbContext;
            this.jwtUtils = jwtUtils;
        }

        /// <summary></summary>
        public async Task<string> LoginUserAsync(IOperationContext context, string email, string password)
        {
            EntityFramework.Identity.User user = await applicationDbContext.Users.Include(x => x.Roles).AsNoTracking()
                .Where(x => x.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"User ({email}) does not exist", () => throw new WebApplication1UnauthorizedException());
            }

            if (!BCryptNet.Verify(password, user.PasswordHash))
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Invalid password ({password}) for user ({email})", () => throw new WebApplication1UnauthorizedException());
            }

            if (!user.IsActive)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"User ({email}) is not active.", () => throw new WebApplication1UnauthorizedException());
            }

            try
            {
                UserAuthorisationModel result = new UserAuthorisationModel
                {
                    Email = user.Email,
                    Name = user.Name,
                    Id = user.Id,
                    Roles = user.Roles.Select(x => x.Name).ToArray(),
                    PhotoUri = user.PhotoUriThumbnail
                };
                string token = jwtUtils.GenerateToken(result);
                return token;
            }
            catch (Exception ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Error occured generating token for user ({email}) and password ({password}).", () => throw new WebApplication1UnauthorizedException(ex.Message, ex));
            }

            return null;
        }
    }
}
