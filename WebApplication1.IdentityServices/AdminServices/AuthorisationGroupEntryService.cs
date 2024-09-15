using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using WebApplication1.EntityFramework.Identity;
using WebApplication1.EntityFramework;
using WebApplication1.IdentityServices.Services;
using WebApplication1.Services.Utils;
using WebApplication1.Services;
using WebApplication1.Models.Exceptions;

namespace WebApplication1.IdentityServices.AdminServices
{
    public class AuthorisationGroupEntryService : IAuthorisationGroupEntryService
    {
        private readonly ILogger<AuthorisationGroupEntryService> logger;
        private readonly ApplicationDbContext applicationDbContext;

        public AuthorisationGroupEntryService(
                ILogger<AuthorisationGroupEntryService> logger,
                ApplicationDbContext applicationDbContext
            )
        {
            this.logger = logger;
            this.applicationDbContext = applicationDbContext;
        }


        public async Task CreateAsync(IOperationContext context, string name)
        {
            bool nameExists = await applicationDbContext.IdentityGroups.AnyAsync(x => x.Name.Trim().ToLower() == name.Trim().ToLower());
            if (nameExists)
            {
                ErrorUtils.LogAndThrowException(context, logger, "Name already exists", () => throw new WebApplication1ValidationException("Failed to create auth group", new List<string> { "Name already exists" }));
            }

            try
            {
                IdentityGroup group = new IdentityGroup { Name = name };
                applicationDbContext.IdentityGroups.Add(group);
                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"An error occured while creating authorisation groups, exception: {ex.Message}", () => throw new CreateEntityException(ex.Message, ex));
            }
        }

        public async Task UpdateAsync(IOperationContext context, Guid id, string name)
        {
            bool nameExists = await applicationDbContext.IdentityGroups.AnyAsync(x => x.Name.Trim().ToLower() == name.Trim().ToLower() && x.Id != id);
            if (nameExists)
            {
                ErrorUtils.LogAndThrowException(context, logger, "Name already exists", () => throw new WebApplication1ValidationException("Failed to update auth group", new List<string> { "Name already exists" }));
            }

            IdentityGroup group = await applicationDbContext.IdentityGroups.FindAsync(id);

            try
            {
                group.Name = name.Trim();
                await applicationDbContext.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                ErrorUtils.LogAndThrowException(context, logger, $"An error occured while updating authorisation groups, exception: {ex.Message}", () => throw new UpdateEntityException(ex.Message, ex));
            }
        }

        public async Task UpdateGroupPermissionsAsync(IOperationContext context, Guid groupId, List<Guid> selectedRoleIds)
        {
            using (var transaction = await applicationDbContext.Database.BeginTransactionAsync())
            {
                IdentityGroup group = await applicationDbContext.IdentityGroups
                    .Include(x => x.Roles)
                        .ThenInclude(x => x.Users)
                    .Include(x => x.Users)
                    .FirstOrDefaultAsync(x => x.Id == groupId);

                if (group == null)
                {
                    ErrorUtils.LogAndThrowException(context, logger, $"Authorisation group ({groupId}) not found.", () => throw new EntityNotFoundException($"Authorisation group ({groupId}) not found."));
                }

                try
                {
                    await ClearGroupRolesAndSaveAsync(group);

                    List<Role> allRoles = await applicationDbContext.Roles.ToListAsync();
                    List<Guid> allSelectedRoleIds = AddParentsRoleIdsFor(allRoles, selectedRoleIds);
                    if (selectedRoleIds.Any())
                    {
                        await AddRolesToGroupAsync(group, allSelectedRoleIds);
                    }

                    await applicationDbContext.SaveChangesAsync();

                    await transaction.CommitAsync();
                }
                catch (DbUpdateException ex)
                {
                    await transaction.RollbackAsync();

                    ErrorUtils.LogAndThrowException(context, logger, $"An error occured while updating authorisation group permissions, exception: {ex.Message}", () => throw new UpdateEntityException(ex.Message, ex));
                }
            }


        }

        private async Task ClearGroupRolesAndSaveAsync(IdentityGroup group)
        {
            List<User> groupUsers = await applicationDbContext.Users.Include(x => x.Groups).Include($"{nameof(User.Groups)}.{nameof(IdentityGroup.Roles)}").ToListAsync();
            foreach (Role role in group.Roles)
            {
                Guid currentRoleId = role.Id;
                foreach (User user in groupUsers)
                {
                    // Is the user a member of any other groups with this role?
                    int groupsWithRole = user.Groups.Count(x => x.Roles.Any(y => y.Id == currentRoleId));

                    // This will be 1 if the current group is the only one:
                    if (groupsWithRole == 1)
                    {
                        user.Roles.Remove(role);
                    }
                }
            }
            group.Roles.Clear();
            await applicationDbContext.SaveChangesAsync();
        }

        private List<Guid> AddParentsRoleIdsFor(List<Role> roles, List<Guid> selectedRoleIds)
        {
            var result = new List<Guid>();
            if (selectedRoleIds != null)
            {
                foreach (var roleId in selectedRoleIds)
                {
                    AddRoleIdToResult(roles, result, roleId);
                }
            }
            return result;
        }

        private void AddRoleIdToResult(List<Role> roles, List<Guid> result, Guid roleId)
        {
            result.Add(roleId);
            var role = roles.First(x => x.Id == roleId);
            if (!string.IsNullOrWhiteSpace(role.ParentName))
            {
                var parent = roles.First(x => x.Name == role.ParentName);
                if (!result.Any(x => x == parent.Id))
                {
                    result.Add(parent.Id);
                }

                AddRoleIdToResult(roles, result, parent.Id);
            }
        }

        private async Task AddRolesToGroupAsync(IdentityGroup group, List<Guid> roleIds)
        {
            List<Role> roles = await applicationDbContext.Roles.Where(x => roleIds.Contains(x.Id)).ToListAsync();

            // add roles to group
            foreach (Role role in roles)
            {
                if (!group.Roles.Any(x => x.Id == role.Id))
                {
                    group.Roles.Add(role);
                }
            }

            // assign users in the group to each role

            foreach (User user in group.Users)
            {
                List<Role> rolesToAdd = new List<Role>();
                foreach (Role role in roles)
                {
                    if (!user.Roles.Any(x => x.Id == role.Id))
                    {
                        rolesToAdd.Add(role);
                    }
                }

                rolesToAdd.ForEach(x => user.Roles.Add(x));
            }
        }
    }
}
