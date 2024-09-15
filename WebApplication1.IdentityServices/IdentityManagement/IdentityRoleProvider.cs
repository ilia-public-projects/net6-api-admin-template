using WebApplication1.EntityFramework.Identity;
using WebApplication1.IdentityServices.Services;

namespace WebApplication1.IdentityServices.IdentityManagement
{
    public class IdentityRoleProvider: IIdentityRoleProvider
    {
        private List<Role> roles { get; set; }

        public IdentityRoleProvider(

            )
        {

        }

        public List<Role> GetApplicationRoles()
        {
            roles = new List<Role>();
            SetGlobalRoles(roles);



            return roles;
        }

        private void SetGlobalRoles(List<Role> roles)
        {

        }
    }
}
