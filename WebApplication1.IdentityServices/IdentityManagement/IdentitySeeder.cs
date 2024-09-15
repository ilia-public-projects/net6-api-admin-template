using Microsoft.Extensions.Logging;
using WebApplication1.EntityFramework;
using WebApplication1.EntityFramework.Identity;
using WebApplication1.IdentityServices.Services;
using BCryptNet = BCrypt.Net.BCrypt;
namespace WebApplication1.IdentityServices.IdentityManagement
{
    public class IdentitySeeder: IIdentitySeeder
    {
        private readonly ILogger<IdentitySeeder> logger;
        private readonly ApplicationDbContext applicationDbContext;
        private readonly IIdentityRoleProvider identityRoleProvider;

        public IdentitySeeder(
            ILogger<IdentitySeeder> logger,
            ApplicationDbContext applicationDbContext,
            IIdentityRoleProvider identityRoleProvider
            )
        {
            this.logger = logger;
            this.applicationDbContext = applicationDbContext;
            this.identityRoleProvider = identityRoleProvider;
        }

        public void SeedIdentity()
        {
            try
            {
                IdentityGroup adminGroup = AddAuthGroupsIfRequired();
                AddAdminUser(adminGroup);
                CreateMissingRoles();
            }
            catch (Exception ex)
            {
                logger.LogError(ex, $"An error occured while seeding identity, exception: {ex.Message}");
            }

        }

        private IdentityGroup AddAuthGroupsIfRequired()
        {
            var dbGroups = applicationDbContext.IdentityGroups.ToList();

            var adminGroup = dbGroups.FirstOrDefault(x => x.Name == "Admin");
            if (adminGroup == null)
            {
                adminGroup = new IdentityGroup() { Name = "Admin" };
                applicationDbContext.IdentityGroups.Add(adminGroup);
                applicationDbContext.SaveChanges();
            }

            var userGroup = dbGroups.FirstOrDefault(x => x.Name == "Users");
            if (userGroup == null)
            {
                userGroup = new IdentityGroup() { Name = "Users" };
                applicationDbContext.IdentityGroups.Add(userGroup);
                applicationDbContext.SaveChanges();
            }

            return adminGroup;
        }

        private void AddAdminUser(IdentityGroup identityGroup)
        {
            var adminUser = applicationDbContext.Users.FirstOrDefault(x => x.Name == "Admin");
            if (adminUser == null)
            {
                var admin = new User()
                {
                    Name = "Admin",
                    IsActive = true,
                    Email = "email@email.com",
                    PasswordHash = BCryptNet.HashPassword("password")
                };

                var adminRole = new Role() { Name = "Admin", Description = "Global access" };
                admin.Roles.Add(adminRole);
                admin.Groups.Add(identityGroup);
                applicationDbContext.Users.Add(admin);
                identityGroup.Roles.Add(adminRole);

                applicationDbContext.SaveChanges();
            }
        }

        private void CreateMissingRoles()
        {
            var roles = identityRoleProvider.GetApplicationRoles();
            var dbroles = applicationDbContext.Roles.ToList();
            var rolesToAdd = new List<Role>();

            foreach (var role in roles)
            {
                if (!dbroles.Any(x => x.Name == role.Name))
                {
                    rolesToAdd.Add(role);
                }
            }

            if (rolesToAdd.Any())
            {
                foreach (var role in rolesToAdd)
                {
                    applicationDbContext.Roles.Add(role);
                }
                applicationDbContext.SaveChanges();
            }
        }
    }
}
