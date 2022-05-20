using System.Reflection;
using Application.Contracts;
using Application.Services;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ServicesRegistration
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var executingAssembly = Assembly.GetExecutingAssembly();
        
        services.AddMapster(executingAssembly);
        services.AddScoped<IEventService, EventService>();

        return services;
    }

    private static IServiceCollection AddMapster(this IServiceCollection services, params Assembly[] assemblies)
    {
        var config = TypeAdapterConfig.GlobalSettings;
        var registers = config.Scan(assemblies);

        config.Apply(registers);

        services.AddSingleton(config);
        services.AddScoped<IMapper, ServiceMapper>();
        
        return services;
    }
}