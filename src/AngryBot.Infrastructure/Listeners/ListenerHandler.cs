using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Mappers;
using Discord.WebSocket;
using Microsoft.Extensions.Hosting;

namespace AngryBot.Infrastructure.Listeners;

internal sealed class ListenerHandler : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly IGuildRepository _guildRepository;
    private readonly IUserRepository _userRepository;

    public ListenerHandler(DiscordSocketClient client, IGuildRepository guildRepository, IUserRepository userRepository)
    {
        _client = client;
        _guildRepository = guildRepository;
        _userRepository = userRepository;
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        _client.JoinedGuild += JoinedGuild;
        _client.UserJoined += UserJoined;

        return Task.CompletedTask;
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private async Task JoinedGuild(SocketGuild guild)
    {
        await _guildRepository.Add(guild.AsModel());
    }

    private async Task UserJoined(SocketGuildUser guildUser)
    {
        var user = await _userRepository.GetUserById(guildUser.Id);
        if (user is null)
        {
            await _userRepository.Add(guildUser.AsModel());
        }
    }
}
