using AngryBot.Domain.Entities;
using AngryBot.Shared.Extensions;
using Discord;
using Discord.Commands;
using Discord.Interactions;
using Discord.WebSocket;
using System.Text;

namespace AngryBot.Application.Interactions;

public sealed class HelpModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly DiscordSocketClient _client;
    private readonly InteractionService _interactions;
    private readonly CommandService _commands;

    public HelpModule(DiscordSocketClient client, InteractionService interactions, CommandService commands)
    {
        _client = client;
        _interactions = interactions;
        _commands = commands;
    }

    [SlashCommand("help", "Show all available commands")]
    public async Task Help()
    {
        var interactionModules = _interactions.Modules;

        var embed = new EmbedBuilder()
           .WithTitle("Available commands")
           .WithColor(Context.Guild.CurrentUser.GetTopRole().GetColor());

        
        foreach (var module in interactionModules)
        {
            var commands = new StringBuilder();
            foreach (var command in module.SlashCommands)
            {
                if(commands.Length > 0)
                {
                    commands.Append(", ");
                }
                commands.Append($"`/{command.ToString()}`");
            }
            embed.AddField(module.Name.Replace("Module", ""), commands);
        }

        await RespondAsync(embed: embed.Build(), ephemeral: true);
    }
}
