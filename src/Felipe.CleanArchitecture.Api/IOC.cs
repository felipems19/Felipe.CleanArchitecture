using Felipe.CleanArchitecture.Api.Swagger;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Felipe.CleanArchitecture.Api;

public static class IOC
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        return services;
    }

    public static IServiceCollection AddAuthenticationConfig(this IServiceCollection services, IConfiguration configuration)
    {
        //TODO: Add Authentication/Authorization
        return services;
    }
}
