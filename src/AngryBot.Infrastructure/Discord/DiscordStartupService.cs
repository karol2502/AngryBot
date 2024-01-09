using AngryBot.Domain.Common.Options;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AngryBot.Shared.Logging;

namespace AngryBot.Infrastructure.Discord;

internal sealed class DiscordStartupService : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly IConfiguration _configuration;
    private readonly ILogger<DiscordSocketClient> _logger;

    public DiscordStartupService(DiscordSocketClient client, IConfiguration configuration, ILogger<DiscordSocketClient> logger)
    {
        _client = client;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _client.Log += msg => LogHelper.OnLogAsync(_logger, msg);
        var authenticationOptions = _configuration.GetSection(AuthenticationOptions.Authentication).Get<AuthenticationOptions>();
        await _client.LoginAsync(TokenType.Bot, authenticationOptions!.ApiToken);
        await _client.StartAsync();
        await _client.SetGameAsync("/help", type: ActivityType.Listening);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await _client.LogoutAsync();
        await _client.StopAsync();
    }
}
