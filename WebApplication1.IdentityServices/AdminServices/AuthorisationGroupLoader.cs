using Microsoft.EntityFrameworkCore;
using WebApplication1.EntityFramework.Identity;
using WebApplication1.EntityFramework;
using WebApplication1.IdentityServices.Services;
using WebApplication1.Models.Admin.AuthorisationGroup;
using WebApplication1.Models;
using Microsoft.Extensions.Logging;
using WebApplication1.Services;
using WebApplication1.Services.Utils;
using WebApplication1.Models.Exceptions;

namespace WebApplication1.IdentityServices.AdminServices
{
    public class AuthorisationGroupLoader : IAuthorisationGroupLoader
    {
        private readonly ILogger<AuthorisationGroupLoader> logger;
        private readonly ApplicationDbContext applicationDbContext;

        public AuthorisationGroupLoader(
                ILogger<AuthorisationGroupLoader> logger,
                ApplicationDbContext applicationDbContext
            )
        {
            this.logger = logger;
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<PagedResult<AuthorisationGroupsSearchResult>> SearchAsync()
        {
            // intentionally exclude admin group
            IQueryable<IdentityGroup> query = applicationDbContext.IdentityGroups.AsNoTracking().Where(x => x.Name != "Admin");

            PagedResult<AuthorisationGroupsSearchResult> result = new PagedResult<AuthorisationGroupsSearchResult>();
            result.Results = await query.Select(x => new AuthorisationGroupsSearchResult()
            {
                Id = x.Id,
                Name = x.Name
            }).OrderBy(x => x.Name).ToListAsync();
            result.TotalCount = await query.CountAsync();

            return result;
        }

        public async Task<AuthorisationGroupModel> GetByIdAsync(IOperationContext context, Guid id)
        {
            IdentityGroup group = await GetGroupAsync(context, id);
            return new AuthorisationGroupModel { Id = group.Id, Name = group.Name };
        }

        private async Task<IdentityGroup> GetGroupAsync(IOperationContext context, Guid id, List<string> includes = null)
        {
            IQueryable<IdentityGroup> query = applicationDbContext.IdentityGroups.Select(x => x);
            if (includes != null)
            {
                includes.ForEach(x => query = query.Include(x));
            }

            IdentityGroup group = await query.SingleOrDefaultAsync(x => x.Id == id);
            if (group == null)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"Group ({id}) is not found", () => throw new EntityNotFoundException($"Group ({id}) is not found"));
            }
            return group;
        }

        public async Task<AuthorisationGroupPermissionModel> GetGroupPermissionsAsync(IOperationContext context, Guid id)
        {
            IdentityGroup group = await GetGroupAsync(context, id, new List<string> { $"{nameof(IdentityGroup.Roles)}" });

            IEnumerable<Role> allRoles = await applicationDbContext.Roles.AsNoTracking().Where(x => x.Name != "Admin").ToListAsync();
            List<RoleSelection> roleSelections = new List<RoleSelection>();

            foreach (Role role in allRoles.Where(x => string.IsNullOrEmpty(x.ParentName)))
            {
                RoleSelection selection = new RoleSelection()
                {
                    Id = role.Id,
                    Selected = group.Roles.FirstOrDefault(x => x.Id == role.Id) == null ? false : true,
                    Name = role.Name,
                    Description = role.Description,
                    Children = GetRoleChildren(group.Roles.ToList(), role.Name, allRoles.ToList()),
                    HasParents = false

                };

                int count = 1;
                CountRoles(selection.Children, ref count);
                selection.RoleCount = count;

                roleSelections.Add(selection);
            }

            AuthorisationGroupPermissionModel model = new AuthorisationGroupPermissionModel()
            {
                Name = group.Name,
                Roles = roleSelections
            };

            return model;
        }

        private List<RoleSelection> GetRoleChildren(List<Role> groupRoles, string parentName, List<Role> allRoles)
        {
            return allRoles.Where(x => x.ParentName == parentName).Select(x => new RoleSelection()
            {
                Id = x.Id,
                Selected = groupRoles.FirstOrDefault(y => y.Id == x.Id) == null ? false : true,
                Name = x.Name,
                Description = x.Description,
                Children = GetRoleChildren(groupRoles, x.Name, allRoles),
                HasParents = true

            }).ToList();
        }

        private void CountRoles(List<RoleSelection> groupRoles, ref int count)
        {
            foreach (var role in groupRoles)
            {
                count++;
                if (role.Children.Any())
                {
                    CountRoles(role.Children, ref count);
                }
            }
        }
    }
}
