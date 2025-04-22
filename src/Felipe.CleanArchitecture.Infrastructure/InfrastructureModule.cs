using Felipe.CleanArchitecture.Domain.SeedWork;
using Felipe.CleanArchitecture.Infrastructure.Data;
using Felipe.CleanArchitecture.Infrastructure.Data.Interceptors;
using Felipe.CleanArchitecture.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Felipe.CleanArchitecture.Infrastructure;

public static class InfrastructureModule
{
    public static void AddInfrastructureModule(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContexts(configuration);
        services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
    }

    public static IServiceCollection AddDbContexts(this IServiceCollection services, IConfiguration configuration)
    {
        var databaseConnectionString = configuration.GetValue<string>("DatabaseConnectionString");

        services.AddDbContext<AppDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
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
