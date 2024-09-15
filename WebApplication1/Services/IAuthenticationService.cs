using WebApplication1.Models.Common;

namespace WebApplication1.Services
{
    public interface IAuthenticationService
    {
        Task<string> LoginUserAsync(IOperationContext context, string email, string password);
    }
}
