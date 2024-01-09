using AngryBot.Infrastructure.Discord;
using Microsoft.Extensions.DependencyInjection;

namespace AngryBot.Infrastructure;

public static class InfrastructureInstaller
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        return services
            .AddHostedService<DiscordStartupService>();
    }
}
