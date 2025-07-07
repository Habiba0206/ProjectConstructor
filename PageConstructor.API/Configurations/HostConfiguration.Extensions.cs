using FluentValidation;
using FluentValidation.AspNetCore;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Mvc;
using PageConstructor.API.Configurations;
using PageConstructor.Domain.Common.Serializers;
using PageConstructor.Infrastructure.Common.Serializers;
using PageConstructor.Infrastructure.Common.EventBus.Brokers;
using PageConstructor.Infrastructure.Common.Caching;
using PageConstructor.Infrastructure.Common.EventBus.Extensions;
using PageConstructor.Application.Common.Settings;
using PageConstructor.Persistence.Caching.Brokers;
using PageConstructor.Application.Common.EventBus.Brokers;
using PageConstructor.Domain.Constants;
using PageConstructor.Persistence.DataContexts;
using PageConstructor.Persistence.Repositories.Interfaces;
using PageConstructor.Persistence.Repositories;
using PageConstructor.Application.Fonts.Services;
using PageConstructor.Application.Metas.Services;
using PageConstructor.Application.Pages.Services;
using PageConstructor.Application.Scripts.Services;
using PageConstructor.Infrastructure.Fonts.Services;
using PageConstructor.Infrastructure.Metas.Services;
using PageConstructor.Infrastructure.Pages.Services;
using PageConstructor.Infrastructure.Scripts.Services;
using Microsoft.OpenApi.Models;
using PageConstructor.API.Middlewares;
using MediatR;
using PageConstructor.API.Behaviours;
using PageConstructor.Application.Projects.Services;
using PageConstructor.Infrastructure.Projects.Services;
using PageConstructor.Application.Blocks.Services;
using PageConstructor.Infrastructure.Blocks.Services;
using BookManagement.Api.Data;

namespace PageConstructor.API.Configurations;

public static partial class HostConfiguration
{
    private static readonly ICollection<Assembly> Assemblies;

    static HostConfiguration()
    {
        Assemblies = Assembly.GetExecutingAssembly().GetReferencedAssemblies().Select(Assembly.Load).ToList();
        Assemblies.Add(Assembly.GetExecutingAssembly());
    }

    private static WebApplicationBuilder AddSerializers(this WebApplicationBuilder builder)
    {
        builder.Services.AddSingleton<IJsonSerializationSettingsProvider, JsonSerializationSettingsProvider>();

        return builder;
    }

    private static WebApplicationBuilder AddMappers(this WebApplicationBuilder builder)
    {
        builder.Services.AddAutoMapper(Assemblies);

        return builder;
    }

    private static WebApplicationBuilder AddValidators(this WebApplicationBuilder builder)
    {
        // register configurations 
        builder.Services.Configure<ValidationSettings>(builder.Configuration.GetSection(nameof(ValidationSettings)));

        // register fluent validation
        builder.Services.AddValidatorsFromAssemblies(Assemblies).AddFluentValidationAutoValidation();

        return builder;
    }

    private static WebApplicationBuilder AddCaching(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<CacheSettings>(builder.Configuration.GetSection(nameof(CacheSettings)));

        builder.Services.AddLazyCache();

        builder.Services.AddSingleton<ICacheBroker, LazyMemoryCacheBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddEventBus(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddMassTransit(configuration =>
            {
                var serviceProvider = configuration.BuildServiceProvider();
                var jsonSerializerSettingsProvider = serviceProvider.GetRequiredService<IJsonSerializationSettingsProvider>();

                configuration.RegisterAllConsumers(Assemblies);
                configuration.UsingInMemory((context, cfg) =>
                {
                    cfg.ConfigureEndpoints(context);

                    cfg.UseNewtonsoftJsonSerializer();
                    cfg.UseNewtonsoftJsonDeserializer();

                    cfg.ConfigureNewtonsoftJsonSerializer(settings => jsonSerializerSettingsProvider.ConfigureForEventBus(settings));
                    cfg.ConfigureNewtonsoftJsonDeserializer(settings => jsonSerializerSettingsProvider.ConfigureForEventBus(settings));
                });
            });

        builder.Services.AddSingleton<IEventBusBroker, MassTransitEventBusBroker>();

        return builder;
    }

    private static WebApplicationBuilder AddPersistence(this WebApplicationBuilder builder)
    {
        var dbConnectionString =
            builder.Configuration.GetConnectionString(DataAccessConstants.DbConnectionString) ??
            Environment.GetEnvironmentVariable(DataAccessConstants.DbConnectionString);

        var logger = builder.Services.BuildServiceProvider().GetService<ILogger<Program>>();

        logger?.LogInformation("Environment: {Environment}", builder.Environment.EnvironmentName);
        logger?.LogInformation("Connection String Present: {HasConnection}", !string.IsNullOrEmpty(dbConnectionString));
        logger?.LogDebug("Connection String: {ConnectionString}", dbConnectionString);

        builder.Services.AddDbContext<AppDbContext>(options => { options.UseNpgsql(dbConnectionString); });

        return builder;
    }

    private static WebApplicationBuilder AddInfrastructure(this WebApplicationBuilder builder)
    {
        builder.Services.AddHttpContextAccessor();

        //registering repositories
        builder
            .Services
            .AddScoped<IBlockRepository, BlockRepository>()
            .AddScoped<IFontRepository, FontRepository>()
            .AddScoped<IFontWeightRepository, FontWeightRepository>()
            .AddScoped<IMetaRepository, MetaRepository>()
            .AddScoped<IPageRepository, PageRepository>()
            .AddScoped<IProjectRepository, ProjectRepository>()
            .AddScoped<IScriptRepository, ScriptRepository>();

        //registering services
        builder
            .Services
            .AddScoped<IBlockService, BlockService>()
            .AddScoped<IFontService, FontService>()
            .AddScoped<IFontWeightService, FontWeightService>()
            .AddScoped<IMetaService, MetaService>()
            .AddScoped<IPageService, PageService>()
            .AddScoped<IProjectService, ProjectService>()
            .AddScoped<IScriptService, ScriptService>()
            .AddScoped<IFileUploadService, FileUploadService>();
        //registering google fonts service
        builder.Services.AddHttpClient<IGoogleFontsService, GoogleFontsService>();

        return builder;
    }

    private static WebApplicationBuilder AddMediatR(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddMediatR(conf => { conf.RegisterServicesFromAssemblies(Assemblies.ToArray()); });

        builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        return builder;
    }

    private static WebApplicationBuilder AddCors(this WebApplicationBuilder builder)
    {
        builder.Services.AddCors(
            options =>
            {
                options.AddDefaultPolicy(
                    policyBuilder =>
                    {
                        policyBuilder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    }
                );
            }
        );

        return builder;
    }

    private static async ValueTask<WebApplication> SeedDataAsync(this WebApplication app)
    {
        var serviceScope = app.Services.CreateScope();
        await serviceScope.ServiceProvider.InitializeSeedAsync();

        return app;
    }

    private static WebApplicationBuilder AddDevTools(this WebApplicationBuilder builder)
    {
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen(options =>
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Page Constructor API",
                Version = "v1"
            });
        });

        return builder;
    }

    private static WebApplicationBuilder AddExposers(this WebApplicationBuilder builder)
    {
        builder.Services.Configure<ApiBehaviorOptions>
            (options => { options.SuppressModelStateInvalidFilter = true; });

        builder.Services.AddRouting(options => options.LowercaseUrls = true);
        builder.Services.AddControllers();

        return builder;
    }

    private static async ValueTask<WebApplication> MigratedataBaseSchemasAsync(this WebApplication app)
    {
        var serviceScopeFactory = app.Services.GetRequiredKeyedService<IServiceScopeFactory>(null);

        await serviceScopeFactory.MigrateAsync<AppDbContext>();

        return app;
    }

    private static WebApplication UseDevTools(this WebApplication app)
    {
        app.UseStaticFiles();
        app.UseMiddleware<ExceptionHandlingMiddleware>();
        app.UseSwagger();
        app.UseSwaggerUI();

        return app;
    }

    private static WebApplication UseExposers(this WebApplication app)
    {
        app.MapControllers();

        return app;
    }

    private static WebApplication UseIdentityInfrustructure(this WebApplication app)
    {
        app.UseAuthentication();
        app.UseAuthorization();

        return app;
    }
}