using AngryBot.Application.Commands;
using AngryBot.Application.Interactions;
using AngryBot.Domain.Common.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngryBot.Application;

public static class ApplicationInstaller
{
    public static IServiceCollection AddApplication(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .Configure<AuthenticationOptions>(configuration.GetSection(AuthenticationOptions.Authentication))
            .Configure<SettingsOptions>(configuration.GetSection(SettingsOptions.Settings))
            .AddHostedService<InteractionHandler>()
            .AddHostedService<CommandHandler>();
    }
}
