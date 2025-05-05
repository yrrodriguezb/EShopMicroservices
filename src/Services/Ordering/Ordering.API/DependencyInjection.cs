namespace Ordering.API;

public static class DependencyInjection
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        // Add your API services here, e.g. controllers, filters, etc.
        // Example:
        // services.AddControllers();
        // services.AddSwaggerGen(c => ...);

        return services;
    }

    public static WebApplication UseApiServices(this WebApplication app)
    {
        // Configure your API middleware here, e.g. Swagger, CORS, etc.
        // Example:
        // app.UseSwagger();
        // app.UseSwaggerUI(c => ...);

        return app;
    }
}