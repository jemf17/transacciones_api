using Cobros.Crypto.Controller;
using Cobros.Crypto.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Cobros.Crypto;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddGestorBinance(this IServiceCollection services)
    {

        services.AddSingleton<GestorBinance>();
        services.AddScoped<IGestorBinance, GestorBinance>();
        return services;
    }
}
