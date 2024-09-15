using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Models.Admin.User;
using WebApplication1.Services;

namespace WebApplication1.IdentityServices.Services
{
    public interface IUserValidationService
    {
        Task ValidateChangePassword(IOperationContext context, Guid targetUserId, ChangePasswordModel model);
        Task ValidateChangePhotoAsync(IOperationContext context, Guid userId);
        Task ValidateCreateAsync(UserCreateModel model);
        Task ValidateUpdateAsync(Guid userId, UserUpdateModel model);
        Task ValidateUserExistsAsync(IOperationContext context, Guid userId);
    }
}
