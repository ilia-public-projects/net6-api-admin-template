using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework;
using WebApplication1.EntityFramework.Identity;
using WebApplication1.IdentityServices.Services;
using WebApplication1.Models;
using WebApplication1.Models.Admin.User;
using WebApplication1.Services;
using WebApplication1.Services.Utils;

namespace WebApplication1.IdentityServices.AdminServices
{
    public class UserValidationService : IUserValidationService
    {
        private readonly ApplicationDbContext applicationDbContext;

        public UserValidationService(

                ApplicationDbContext applicationDbContext
            )
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task ValidateCreateAsync(UserCreateModel model)
        {
            WebApplication1Validator validator = new WebApplication1Validator();

            ValidatePassword(validator, model.Password, model.ConfirmPassword);

            bool nameExists = await applicationDbContext.Users.AsNoTracking().AnyAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower());
            if (nameExists)
            {
                validator.WriteError($"Name already exists");
            }

            bool emailExists = await applicationDbContext.Users.AnyAsync(x => x.Email.Trim().ToLower() == model.Email.Trim().ToLower());
            if (emailExists)
            {
                validator.WriteError($"Email already exists");
            }

            validator.Validate("Failed to create user. Validator error occured.");
        }

        public async Task ValidateUpdateAsync(Guid userId, UserUpdateModel model)
        {
            WebApplication1Validator validator = new WebApplication1Validator();

            User user = await ValidateUserAsync(validator, userId);

            bool nameExists = await applicationDbContext.Users.AnyAsync(x => x.Name.Trim().ToLower() == model.Name.Trim().ToLower() && x.Id != userId);
            if (nameExists)
            {
                validator.WriteError($"Name already exists");
            }

            bool emailExists = await applicationDbContext.Users.AnyAsync(x => x.Email.Trim().ToLower() == model.Email.Trim().ToLower() && x.Id != userId);
            if (emailExists)
            {
                validator.WriteError($"Email already exists");
            }

            validator.Validate("Failed to create user. Validator error occured.");
        }

        private async Task<User> ValidateUserAsync(WebApplication1Validator validator, Guid userId, List<string> includes = null)
        {
            IQueryable<User> query = applicationDbContext.Users.AsNoTracking().Select(x => x);
            if (includes != null && includes.Any())
            {
                foreach (string include in includes)
                {
                    query = query.Include(include);
                }
            }

            User user = await query.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                validator.WriteErrorAndValidate($"User does not exist");
            }

            return user;
        }

        public async Task ValidateChangePassword(IOperationContext context, Guid targetUserId, ChangePasswordModel model)
        {
            WebApplication1Validator validator = new WebApplication1Validator();

            // validate requesting user exists
            User requestingUser = await ValidateUserAsync(validator, context.UserId, new List<string> { $"{nameof(User.Roles)}" });

            // Validate requesting user is admin or is changing password for himself
            if (!requestingUser.Roles.Any(x => x.Name == "Admin") && targetUserId != context.UserId)
            {
                validator.WriteError("User is not authorized to change password for another user.");
            }

            // validate target user exists
            User targetUser = await ValidateUserAsync(validator, targetUserId);

            // Validate password meets requirements and matches confirm password
            ValidatePassword(validator, model.Password, model.ConfirmPassword);

            validator.Validate("Failed to change password. New password failed validation");
        }

        public async Task ValidateUserExistsAsync(IOperationContext context, Guid userId)
        {
            WebApplication1Validator validator = new WebApplication1Validator();

            User user = await ValidateUserAsync(validator, userId);

            validator.Validate("Failed to validate user exists");
        }   

        private void ValidatePassword(WebApplication1Validator validator, string password, string confirmPassword)
        {
            if (!PasswordValidationUtils.IsValidPassword(password))
            {
                validator.WriteError("Passwords must have at least one number. Passwords must have at least one lowercase ('a'-'z'). Passwords must have at least one uppercase ('A'-'Z'). Password must be atlist 6 characters long");
            }

            if (password != confirmPassword)
            {
                validator.WriteError($"Password does not match confirm password");
            }
        }

        public async Task ValidateChangePhotoAsync(IOperationContext context, Guid userId)
        {
            WebApplication1Validator validator = new WebApplication1Validator();

            // check if target user exists
            await ValidateUserAsync(validator, userId);

            if (context.UserId != userId)
            {
                // validate if user is admin
                User user = await ValidateUserAsync(validator, context.UserId, new List<string> { $"{nameof(User.Roles)}" });

                if (!user.Roles.Any(x => x.Name == "Admin"))
                {
                    validator.WriteErrorAndValidate($"You are not authorized to change photo of this user");
                }
            }

            validator.Validate();
        }
    }
}
