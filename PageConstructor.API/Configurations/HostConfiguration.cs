namespace PageConstructor.API.Configurations;

public static partial class HostConfiguration
{
    public static ValueTask<WebApplicationBuilder> ConfigureAsync(this WebApplicationBuilder builder)
    {
        builder
            .AddSerializers()
            .AddMappers()
            .AddValidators()
            .AddCaching()
            .AddEventBus()
            .AddPersistence()
            .AddInfrastructure()
            .AddMediatR()
            .AddCors()
            .AddDevTools()
            .AddExposers();

        return new(builder);
    }

    public static async ValueTask<WebApplication> ConfigureAsync(this WebApplication app)
    {
        await app
            .MigratedataBaseSchemasAsync();
        
        app
            .UseCors();

        app
            .UseDevTools()
            .UseExposers()
            .UseIdentityInfrustructure();

        return app;
    }
}
