using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using AngryBot.Domain.Common.Options;
using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Common.Enums;

namespace AngryBot.Application.Commands;

internal sealed class CommandHandler : IHostedService
{
    private readonly CommandService _commands;
    private readonly IConfiguration _configuration;
    private readonly DiscordSocketClient _client;
    private readonly IServiceProvider _services;
    private readonly IMessageResponseRepository _messageResponseRepository;

    public CommandHandler(IServiceProvider services, DiscordSocketClient client, CommandService commands, IConfiguration configuration, IMessageResponseRepository messageResponseRepository)
    {
        _commands = commands;
        _configuration = configuration;
        _client = client;
        _services = services;
        _messageResponseRepository = messageResponseRepository;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _client.MessageReceived += MessageReceived;
        await _commands.AddModulesAsync(typeof(CommandHandler).Assembly, _services);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
    }

    private async Task MessageReceived(SocketMessage rawMessage)
    {
        // Ignore system messages and messages from bots
        if (rawMessage is not SocketUserMessage message) return;
        if (message.Source != MessageSource.User) return;

        var settingsOptions = _configuration.GetSection(SettingsOptions.Settings).Get<SettingsOptions>();

        int argPos = 0;
        var context = new SocketCommandContext(_client, message);

        if (message.HasStringPrefix(settingsOptions!.Prefix, ref argPos))
        {
            var result = await _commands.ExecuteAsync(context, argPos, _services);
            if (result.Error.HasValue)
            {
                await context.Channel.SendMessageAsync(result.ToString());
            }
        }
        else
        {
            var responses = await _messageResponseRepository.GetAllByGuildId(context.Guild.Id);

            foreach (var messageResponse in responses)
            {
                if (rawMessage.Content.Contains(messageResponse.Message, StringComparison.CurrentCultureIgnoreCase))
                {
                    switch (messageResponse.ResponseType)
                    {
                        case ResponseType.Message:
                            await context.Channel.TriggerTypingAsync();
                            await context.Channel.SendMessageAsync(messageResponse.Response);
                            break;                        
                        case ResponseType.Reaction:
                            Emote.TryParse(messageResponse.Response, out var emote);
                            Emoji.TryParse(messageResponse.Response, out var emoji);
                            await context.Message.AddReactionAsync(emote is not null ? emote : emoji);
                            break;
                        case ResponseType.Reply:
                            await context.Channel.TriggerTypingAsync();
                            await context.Message.ReplyAsync(messageResponse.Response);
                            break;
                    }
                }
            }            
        }       
    }
}
