using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Ordering.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add your infrastructure services here, e.g. DbContext, Repositories, etc.
        // Example:
        // services.AddDbContext<OrderingDbContext>(options => ...);
        // services.AddScoped<IOrderRepository, OrderRepository>();

        var connectionString = configuration.GetConnectionString("Database");

        return services;
    }
}
