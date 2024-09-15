using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.EntityFramework.Identity.Repositories;

namespace WebApplication1.EntityFramework
{
    public static class Module
    {
        public static void AddEFServices(this IServiceCollection services)
        {
            services.AddIdentityRepositories();
        }
    }
}
