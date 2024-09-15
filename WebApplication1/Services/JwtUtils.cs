using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebApplication1.DTO.Authentication;
using WebApplication1.Models.Common;

namespace WebApplication1.Services
{
    public class JwtUtils : IJwtUtils
    {
        private readonly ILogger<JwtUtils> logger;
        private AppSettings appSettings;

        /// <summary></summary>
        public JwtUtils(ILogger<JwtUtils> logger, IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
            this.logger = logger;
        }

        /// <summary></summary>
        public UserAuthorisationModel ValidateToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;

                Guid userId = Guid.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                string rolesJson = jwtToken.Claims.First(x => x.Type == "role").Value;

                return new UserAuthorisationModel { Id = userId, Roles = JsonConvert.DeserializeObject<string[]>(rolesJson) };

            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }

        public string GenerateToken(UserAuthorisationModel user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Convert.FromBase64String(appSettings.Secret);

            var subject = new ClaimsIdentity();
            subject.AddClaim(new Claim("id", user.Id.ToString()));
            subject.AddClaim(new Claim("email", user.Email));
            subject.AddClaim(new Claim("name", user.Name));
            subject.AddClaim(new Claim("role", JsonConvert.SerializeObject(user.Roles)));
            subject.AddClaim(new Claim("photoUri", user.PhotoUri ?? ""));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = subject,
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
