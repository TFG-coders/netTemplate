using Microsoft.Extensions.DependencyInjection;
using NetTemplate.Application.Interfaces;
using NetTemplate.Infrastructure.Services;

namespace NetTemplate.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherService, WeatherService>();
            return services;
        }
    }
}
