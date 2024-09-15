using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplication1.Services.SignalR;

namespace WebApplication1.SignalR
{
    public static  class Module
    {
        public static void AddSignalRServices(this IServiceCollection services)
        {
            services.AddSingleton<ISignalRService, SignalRService>();
            services.AddSingleton<IConnectionManager, ConnectionManager>();
        }
    }
}
