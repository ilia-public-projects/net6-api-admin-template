using Microsoft.Extensions.DependencyInjection;
using WebApplication1.IdentityServices.Services;

namespace WebApplication1.IdentityServices.IdentityManagement
{
    public static class Module
    {
        public static void AddIdentityServices(this IServiceCollection services)
        {
            services.AddTransient<IIdentitySeeder, IdentitySeeder>();
            services.AddSingleton<IIdentityRoleProvider, IdentityRoleProvider>();
        }
    }
}
