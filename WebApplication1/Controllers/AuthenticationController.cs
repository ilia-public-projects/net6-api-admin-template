using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers;
using WebApplication1.DTO.Authentication;
using WebApplication1.DTO;
using WebApplication1.Services;
using WebApplication1.API.Extensions;

namespace WebApplication1.API.Controllers
{
    /// <summary></summary>
    [ApiController]
    public class AuthenticationController : BaseController
    {
        private readonly IAuthenticationService authenticationService;

        /// <summary></summary>
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            this.authenticationService = authenticationService;
        }

        /// <summary></summary>
        [HttpPost]
        [Route("oauth/token")]
        [AllowAnonymous]
        public async Task<APIResponse<PostLoginResponse>> LoginAsync([FromBody] PostLoginRequest request)
        {
            IOperationContext operationContext = HttpContext.GetOperationContext();

            string token = await authenticationService.LoginUserAsync(operationContext, request.Email, request.Password);
            return new APIResponse<PostLoginResponse>(new PostLoginResponse(token));
        }
    }
}
