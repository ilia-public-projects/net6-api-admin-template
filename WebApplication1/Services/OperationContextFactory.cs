using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using WebApplication1.Services;

namespace WebApplication1.API.Services
{
    /// <summary></summary>
    public class OperationContextFactory : IOperationContextFactory
    {
        /// <summary></summary>
        public IOperationContext CreateOperationContext(HttpRequest request)
        {
            OperationContext result = new OperationContext(request);

            return result;
        }

        public IOperationContext CreateBackgrounOperationContext(Guid? ownerId)
        {
            OperationContext result = new OperationContext(ownerId);
            return result;
        }

        private class OperationContext : IOperationContext
        {
            private string token;
            private Guid userId = Guid.Empty;
            private List<string> roles;
            private readonly string contextId = $"{Guid.NewGuid()}";


            public OperationContext(HttpRequest request)
            {
                ExtractTokenFromHeaders(request);
                ExtractUserInfoFromToken();
            }

            public OperationContext(Guid? ownerId)
            {
                if (ownerId.HasValue)
                {
                    userId = ownerId.Value;
                }
            }

            public string OperationId => contextId;

            public string Token => token;

            public Guid UserId => userId;
            public List<string> Roles => roles;

            private void ExtractTokenFromHeaders(HttpRequest request)
            {
                token = request.Headers.FirstOrDefault(x => string.Equals(x.Key, "Authorization", StringComparison.InvariantCultureIgnoreCase)).Value.FirstOrDefault();
                if (!string.IsNullOrWhiteSpace(token) && token.StartsWith("Bearer ", StringComparison.InvariantCultureIgnoreCase))
                {
                    token = token.Substring("Bearer ".Length);
                }
            }

            private void ExtractUserInfoFromToken()
            {
                if (!string.IsNullOrEmpty(token))
                {
                    JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                    JwtSecurityToken jwtSecurityToken = handler.ReadJwtToken(token);

                    userId = Guid.Parse(jwtSecurityToken.Claims.First(x => x.Type == "id").Value);
                    string rolesJson = jwtSecurityToken.Claims.First(x => x.Type == "role").Value;
                    roles = JsonConvert.DeserializeObject<List<string>>(rolesJson);
                }
            }
        }
    }
}
