using AngryBot.Domain.Common.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AngryBot.Persistence.Repositories;
internal static class RepositoryInstaller
{
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
            .AddTransient<IUserRepository, UserRepository>()
            .AddTransient<IUserDisappointmentRepository, UserDisappointmentRepository>()
            .AddTransient<IGuildRepository, GuildRepository>()
            .AddTransient<IDisappointmentRepository, DisappointmentRepository>()
            .AddTransient<IMessageResponseRepository, MessageResponseRepository>();
    }
}
