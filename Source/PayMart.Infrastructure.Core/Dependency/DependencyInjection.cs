using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PayMart.Infrastructure.Core.Services;

namespace PayMart.Infrastructure.Core.Dependency;

public static class DependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services, configuration);
    }

    private static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var Urls = configuration.GetSection("AppSettings");
    }
}
