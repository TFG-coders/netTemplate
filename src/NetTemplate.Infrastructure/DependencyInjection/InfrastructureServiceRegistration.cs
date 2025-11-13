using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NetTemplate.Application.Abstractions;
using NetTemplate.Application.Interfaces;
using NetTemplate.Infrastructure.Persistence;
using NetTemplate.Infrastructure.Repositories;
using NetTemplate.Infrastructure.Services;

namespace NetTemplate.Infrastructure.DependencyInjection
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
        {
            var cs = config.GetConnectionString("DefaultConnection");

            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(cs, sql =>
                {
                    sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
                }));

            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddSingleton<IWeatherService, WeatherService>();

            return services;
        }
    }
}
