using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.EntityFramework;
using WebApplication1.EntityFramework.Identity;
using WebApplication1.IdentityServices.Services;
using WebApplication1.Models.Admin.User;
using WebApplication1.Models;
using WebApplication1.Services;
using WebApplication1.Services.Utils;
using WebApplication1.Models.Exceptions;

namespace WebApplication1.IdentityServices.AdminServices
{
    public class UserLoader : IUserLoader
    {
        private readonly ApplicationDbContext applicationDbContext;
        private readonly ILogger<UserLoader> logger;
        private readonly IUserValidationService validationService;
        private readonly IAzureStorageService azureStorageService;
        private readonly IImageResizer imageResizer;

        public UserLoader(
            ApplicationDbContext applicationDbContext,
            ILogger<UserLoader> logger,
            IAzureStorageService azureStorageService,
            IImageResizer imageResizer
            )
        {
            this.applicationDbContext = applicationDbContext;
            this.logger = logger;
            this.azureStorageService = azureStorageService;
            this.imageResizer = imageResizer;
        }

        public async Task<PagedResult<UserSearchResult>> SearchUsersAsync(UserSearchCriteria criteria)
        {
            IQueryable<User> query = applicationDbContext.Users.AsNoTracking().Where(x => x.Name != "Admin").Select(x => x);
            if (!string.IsNullOrWhiteSpace(criteria.Email)) query = query.Where(x => x.Email.Trim().ToLower().Contains(criteria.Email.Trim().ToLower()));
            if (!string.IsNullOrWhiteSpace(criteria.Name)) query = query.Where(x => x.Name.Trim().ToLower().Contains(criteria.Name.Trim().ToLower()));
            if (criteria.IncludeInactive.HasValue && !criteria.IncludeInactive.Value) query = query.Where(x => x.IsActive);

            var result = new PagedResult<UserSearchResult>();
            result.Results = await query.Select(x => new UserSearchResult
            {
                Id = x.Id,
                IsActive = x.IsActive,
                Email = x.Email,
                Name = x.Name,
                ThumbnailUri = x.PhotoUriThumbnail

            }).OrderBy(x => x.Name).Skip(criteria.Skip).Take(criteria.PageSize).ToListAsync();
            result.TotalCount = await query.CountAsync();
            return result;
        }

        public async Task<UserModel> GetByIdAsync(IOperationContext context, Guid id)
        {
            User user = await applicationDbContext.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"User ({id}) not found.", () => throw new EntityNotFoundException($"User ({id}) not found."));
            }
            UserModel result = new UserModel
            {
                IsActive = user.IsActive,
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                PhotoUriThumbnail = user.PhotoUriThumbnail,
                PhotoUriRaw = user.PhotoUriRaw
            };

            return result;
        }

        public async Task<UserAuthorisationGroupsModel> GetUserAuthorisationGroupsAsync(IOperationContext context, Guid id)
        {
            User user = await applicationDbContext.Users.AsNoTracking()
                .Include(x => x.Groups)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"User ({id}) not found.", () => throw new EntityNotFoundException($"User ({id}) not found."));
            }

            IEnumerable<IdentityGroup> allGroups = await applicationDbContext.IdentityGroups.AsNoTracking().OrderBy(x => x.Name).ToListAsync();

            UserAuthorisationGroupsModel result = new UserAuthorisationGroupsModel();
            result.Name = user.Name;

            foreach (IdentityGroup group in allGroups)
            {
                result.Groups.Add(new UserAuthorisationGroupModel
                {
                    GroupId = group.Id,
                    GroupName = group.Name,
                    IsSelected = user.Groups.Any(x => x.Id == group.Id)

                });
            }

            return result;
        }

        public async Task<IEnumerable<UserModel>> GetAllUsersAsync()
        {
            List<UserModel> result = await applicationDbContext.Users.AsNoTracking().Select(x => new UserModel
            {
                Id = x.Id,
                Name = x.Name,
                IsActive = x.IsActive,
                Email = x.Email,
            }).OrderBy(x => x.Name).ToListAsync();

            return result;
        }
    }
}
