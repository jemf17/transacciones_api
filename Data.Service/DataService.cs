using DBModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Data.Service
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddDataService(this IServiceCollection services, IConfiguration conf)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseNpgsql(conf["DATABASE_CONNECTION"]));

            return services;
        }
    }
}