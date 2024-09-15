using Microsoft.AspNetCore.Http;
using WebApplication1.Models.Admin.User;
using WebApplication1.Services;

namespace WebApplication1.IdentityServices.Services
{
    public interface IUserEntryService
    {
        Task ChangePasswordAsync(IOperationContext context, Guid? targetUserId, ChangePasswordModel model);
        Task<string> ChangePhotoAsync(IOperationContext context, Guid? targetUserId, IFormFile photo);
        Task CreateUserAsync(IOperationContext context, UserCreateModel model);
        Task UpdateUserAsync(IOperationContext context, Guid id, UserUpdateModel model);
        Task UpdateUserAuthorisationGroupsAsync(IOperationContext context, Guid targetUserId, List<Guid> selectedGroupIds);
    }
}
