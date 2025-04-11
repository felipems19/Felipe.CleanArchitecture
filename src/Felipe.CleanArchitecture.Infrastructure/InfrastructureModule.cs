using Felipe.CleanArchitecture.Domain.Interfaces.Repositories;
using Felipe.CleanArchitecture.Infrastructure.Data;
using Felipe.CleanArchitecture.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Felipe.CleanArchitecture.Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.AddScoped<ITruckRepository, TruckRepository>();
    }

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetValue<string>("DatabaseConnectionString");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(databaseConnectionString, ConfigureOptions);
        });

        return services;
    }

    static void ConfigureOptions(SqlServerDbContextOptionsBuilder options)
    {
        options.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName);
        options.EnableRetryOnFailure(maxRetryCount: 15, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
    }
}
