using Microsoft.Extensions.DependencyInjection;
using WebApplication1.IdentityServices.Services;

namespace WebApplication1.IdentityServices.AdminServices
{
    public static class Module
    {
        public static void AddAdminServices(this IServiceCollection services)
        {
            services.AddTransient<IUserEntryService, UserEntryService>();
            services.AddTransient<IUserValidationService, UserValidationService>();
            services.AddTransient<IUserLoader, UserLoader>();

            services.AddTransient<IAuthorisationGroupEntryService, AuthorisationGroupEntryService>();
            services.AddTransient<IAuthorisationGroupLoader, AuthorisationGroupLoader>();
        }
    }
}
