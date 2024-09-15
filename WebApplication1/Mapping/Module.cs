using WebApplication1.API.Mapping.Admin;

namespace WebApplication1.API.Mapping
{
    public static class Module
    {
        public static void AddAutoMapperProfiles(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(typeof(UserProfile));
        }
    }
}
