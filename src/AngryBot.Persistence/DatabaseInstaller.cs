using AngryBot.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngryBot.Persistence;

public static class DatabaseInstaller
{
    public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddDbContext<AngryBotDbContext>(options =>
            {
                var connectionString = configuration.GetConnectionString("db");
                options.UseNpgsql(connectionString!, builder =>
                {
                    builder.MigrationsAssembly(typeof(DatabaseInstaller).Assembly.FullName);
                });
            }, ServiceLifetime.Singleton, ServiceLifetime.Singleton)
            .AddRepositories()
            .AddHostedService<DatabaseSeeder>();
    }
}
