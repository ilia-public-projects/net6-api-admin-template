using Microsoft.Extensions.DependencyInjection;
using WebApplication1.Services;

namespace WebApplication1.Common
{
    public static class Module
    {
        public static void AddCommonServices(this IServiceCollection services)
        {
            services.AddSingleton<IExcelGenerator, ExcelGenerator>();
            services.AddSingleton<IRestService, RestService>();
            services.AddSingleton<IAzureStorageService, AzureStorageService>();
            services.AddSingleton<IAzureServiceBusMessageSender, AzureServiceBusMessageSender>();

            services.AddSingleton<IDocumentNoSchemeService, DocumentNoSchemeService>();

            services.AddSingleton<IKeyValueParser, KeyValueParser>();
            services.AddSingleton<IImageResizer, ImageResizer>();
        }
    }
}