using Discord.Interactions;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using AngryBot.Shared.Logging;

namespace AngryBot.Application.Interactions;

internal sealed class InteractionHandler : IHostedService
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly IServiceProvider _services;
    private readonly ILogger<InteractionService> _logger;

    public InteractionHandler(
            DiscordSocketClient client,
            InteractionService interactions,
            IServiceProvider services,
            ILogger<InteractionService> logger)
    {
        _client = client;
        _interactions = interactions;
        _services = services;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _client.Ready += () => _interactions.RegisterCommandsGloballyAsync(true);
        _client.InteractionCreated += OnInteractionAsync;

        _interactions.Log += msg => LogHelper.OnLogAsync(_logger, msg);
        await _interactions.AddModulesAsync(typeof(InteractionHandler).Assembly, _services);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        _interactions.Dispose();
        await Task.CompletedTask;
    }

    private async Task OnInteractionAsync(SocketInteraction interaction)
    {
        try
        {
            var context = new SocketInteractionContext(_client, interaction);
            var result = await _interactions.ExecuteCommandAsync(context, _services);

            if (!result.IsSuccess)
            {
                await context.Channel.SendMessageAsync(result.ToString());
            }
        }
        catch
        {
            if (interaction.Type == InteractionType.ApplicationCommand)
            {
                await interaction.GetOriginalResponseAsync().ContinueWith(msg => msg.Result.DeleteAsync());
            }
        }
    }
}
