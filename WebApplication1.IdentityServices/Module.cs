using Microsoft.Extensions.DependencyInjection;
using WebApplication1.IdentityServices.AdminServices;
using WebApplication1.IdentityServices.IdentityManagement;

namespace WebApplication1.IdentityServices
{
    public static class Module
    {
        public static void AddIdentityAndAdminServices(this IServiceCollection services)
        {
            services.AddAdminServices();
            services.AddIdentityServices();
        }

    }
}