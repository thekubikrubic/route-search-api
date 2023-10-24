using RouteSearchApi.Services;
using RouteSearchApi.Services.Abstractions;

namespace NotificationsTemplateService
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection InitService(this IServiceCollection services, IConfiguration config)
        {
            services.AddServices(config);
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddTransient<ISearchService, SearchService>();
            services.AddTransient<IIntegrationService, ProviderOneIntegrationService>();
            services.AddTransient<IIntegrationService, ProviderTwoIntegrationService>();
            return services;
        }
    }
}
