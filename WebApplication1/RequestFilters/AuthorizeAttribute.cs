using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System;
using WebApplication1.DTO.Authentication;

namespace WebApplication1.RequestFilters
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AuthorizeAttribute : Attribute, IAuthorizationFilter
    {
        private string roles;
        /// <summary></summary>
        public AuthorizeAttribute(string roles = null)
        {
            this.roles = roles;
        }

        /// <summary></summary>
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            // skip authorization if action is decorated with [AllowAnonymous] attribute
            bool allowAnonymous = context.ActionDescriptor.EndpointMetadata.OfType<AllowAnonymousAttribute>().Any();
            if (allowAnonymous)
                return;

            // authorization
            UserAuthorisationModel user = (UserAuthorisationModel)context.HttpContext.Items["User"];
            if (user == null)
            {
                context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
            }
            else
            {
                if (!string.IsNullOrEmpty(roles))
                {
                    string[] validateAgainst = roles.Split(",");
                    if (!user.Roles.Any(x => validateAgainst.Contains(x)))
                    {
                        context.Result = new JsonResult(new { message = "Unauthorized" }) { StatusCode = StatusCodes.Status401Unauthorized };
                    }
                }
            }

        }
    }
}
