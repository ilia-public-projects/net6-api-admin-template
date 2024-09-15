using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.EntityFramework;
using WebApplication1.EntityFramework.Identity;
using WebApplication1.IdentityServices.Services;
using WebApplication1.Models;
using WebApplication1.Models.Admin;
using WebApplication1.Models.Admin.User;
using WebApplication1.Models.Exceptions;
using WebApplication1.Services;
using WebApplication1.Services.SignalR;
using WebApplication1.Services.Utils;
using BCryptNet = BCrypt.Net.BCrypt;

namespace WebApplication1.IdentityServices.AdminServices
{
    public class UserEntryService : IUserEntryService
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<UserEntryService> logger;
        private readonly IUserValidationService validationService;
        private readonly IAzureStorageService azureStorageService;
        private readonly IImageResizer imageResizer;
        private readonly ISignalRService signalRService;

        public UserEntryService(
            ApplicationDbContext applicationDbContext,
            ILogger<UserEntryService> logger,
            IUserValidationService validationService,
            IAzureStorageService azureStorageService,
            IImageResizer imageResizer,
            ISignalRService signalRService
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.logger = logger;
            this.validationService = validationService;
            this.azureStorageService = azureStorageService;
            this.imageResizer = imageResizer;
            this.signalRService = signalRService;
        }

        public async Task CreateUserAsync(IOperationContext context, UserCreateModel model)
        {
            await validationService.ValidateCreateAsync(model);
            try
            {
                User user = new User();
                user.Email = model.Email;
                user.Name = model.Name;
                user.IsActive = true;
                user.PasswordHash = BCryptNet.HashPassword(model.Password);

                applicationDbContext.Users.Add(user);
                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Failed to create user, message:{ex.Message}", () => throw new CreateEntityException(ex.Message, ex));
            }
        }

        public async Task ChangePasswordAsync(IOperationContext context, Guid? targetUserId, ChangePasswordModel model)
        {
            // when target user id is not provided, change password for current user
            Guid passwordOwnerId = targetUserId.HasValue ? targetUserId.Value : context.UserId;

            await validationService.ValidateChangePassword(context, passwordOwnerId, model);

            try
            {
                User targetUser = await applicationDbContext.Users.FirstAsync(x => x.Id == passwordOwnerId);

                targetUser.PasswordHash = BCryptNet.HashPassword(model.Password);
                await applicationDbContext.SaveChangesAsync();

            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"An error occured while changing password, exception: {ex.Message}", () => throw new UpdateEntityException(ex.Message, ex));
            }
        }

        public async Task UpdateUserAsync(IOperationContext context, Guid id, UserUpdateModel model)
        {
            await validationService.ValidateUpdateAsync(id, model);

            try
            {
                User user = await applicationDbContext.Users.FindAsync(id);
                user.Email = model.Email;
                user.Name = model.Name;
                user.IsActive = model.IsActive;

                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"An error occured while updating user, exception: {ex.Message}", () => throw new UpdateEntityException(ex.Message, ex));
            }
        }
        public async Task UpdateUserAuthorisationGroupsAsync(IOperationContext context, Guid targetUserId, List<Guid> selectedGroupIds)
        {
            await validationService.ValidateUserExistsAsync(context, targetUserId);

            try
            {
                User user = await applicationDbContext.Users
                        .Include(x => x.Groups)
                        .Include(x => x.Roles)
                        .FirstAsync(x => x.Id == targetUserId);

                await AddUserToGroupsAsync(user, selectedGroupIds);

                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Failed to update user authorisation groups, exception: {ex.Message}", () => throw new UpdateEntityException(ex.Message, ex));
            }
        }

        private async Task AddUserToGroupsAsync(User user, List<Guid> groupIds)
        {
            // clear existing groups and roles
            user.Groups.Clear();
            user.Roles.Clear();

            // get all groups and roles for selected groups
            List<IdentityGroup> groups = await applicationDbContext.IdentityGroups.Include(x => x.Roles).Where(x => groupIds.Contains(x.Id)).ToListAsync();

            // Add groups and roles to user
            List<Role> allRoles = new List<Role>();
            foreach (IdentityGroup group in groups)
            {
                user.Groups.Add(group);

                foreach (Role role in group.Roles)
                {
                    if (!allRoles.Any(x => x.Id == role.Id))
                    {
                        allRoles.Add(role);
                    }
                }
            }

            allRoles.ForEach(x => user.Roles.Add(x));
        }

        public async Task<string> ChangePhotoAsync(IOperationContext context, Guid? targetUserId, IFormFile photo)
        {
            Guid photoOwnerId = targetUserId.HasValue ? targetUserId.Value : context.UserId;

            await validationService.ValidateChangePhotoAsync(context, photoOwnerId);

            try
            {
                User user = await applicationDbContext.Users.FindAsync(photoOwnerId);

                string photoUriThumbnail = null;
                string photoUriRaw = null;
                if (photo != null)
                {
                    using (Stream photoStream = photo.OpenReadStream())
                    {
                        photoUriRaw = await azureStorageService.UploadStreamAsync(context, AdminAzureConstants.UserPhotoContainerName, photoStream, photo.ContentType);

                        // resize the image to 300*300 and upload to azure
                        byte[] resizedImage = imageResizer.ResizeImage(photoStream, ImageConstants.ProfilePhotoWidth, ImageConstants.ProfilePhotoHeight);
                        using (MemoryStream resizedImageStream = new MemoryStream(resizedImage))
                        {
                            photoUriThumbnail = await azureStorageService.UploadStreamAsync(context, AdminAzureConstants.UserPhotoContainerName, resizedImageStream, photo.ContentType);
                        }
                    }
                }

                user.PhotoUriThumbnail = photoUriThumbnail;
                user.PhotoUriRaw = photoUriRaw;

                await applicationDbContext.SaveChangesAsync();

                signalRService.UpdateUserPhotoAndShowActiveConnections(photoOwnerId, photoUriThumbnail);

                return photoUriThumbnail;

            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Failed to change user {photoOwnerId} photo, message: {ex.Message}", () => throw new UpdateEntityException(ex.Message, ex.InnerException));
            }

            return null;
        }

    }
}
