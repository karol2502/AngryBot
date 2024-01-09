using AngryBot.Domain.Common.Enums;
using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;
using AngryBot.Shared.Extensions;
using Discord;
using Discord.Interactions;

namespace AngryBot.Application.Interactions;

[Group("response", "Message response commands")]
public sealed class MessageResponseModule : InteractionModuleBase<SocketInteractionContext>
{
    private const int MAX_RESPONSE_LENGTH = 200;

    private readonly IMessageResponseRepository _messageResponseRepository;

    public MessageResponseModule(IMessageResponseRepository messageResponseRepository)
    {
        _messageResponseRepository = messageResponseRepository;
    }

    [SlashCommand("add", "Autoresponse on message")]
    [RequireContext(ContextType.Guild)]
    public async Task Add([Summary("message", "message I will reply to")] string message, [Summary("response_type")] ResponseType responseType, [Summary("response", "if 'Reaction' is chosen, the response must be an single emoji")] string response)
    {
        if (responseType == ResponseType.Reaction)
        {
            if (!Emote.TryParse(response, out var emote) && !Emoji.TryParse(response, out var emoji))
            {
                await RespondAsync($"Invalid emoji {response}");
                return;
            }
        }
        else
        {
            if (response.Length > MAX_RESPONSE_LENGTH)
            {
                await RespondAsync($"Response too long!");
                return;
            }
        }

        var messageResponse = new MessageResponse(message, response, responseType, Context.Interaction.GuildId!.Value, Context.User.Id);
        await _messageResponseRepository.Add(messageResponse);
        await RespondAsync($"Response added!");
    }

    [SlashCommand("list", "Send list of all responses")]
    [RequireContext(ContextType.Guild)]
    public async Task MessageResponseList()
    {
        var messageResponses = await _messageResponseRepository.GetAllByGuildId(Context.Guild.Id);

        if (!messageResponses.Any())
        {
            await RespondAsync("No added responses!");
            return;
        }

        var embed = new EmbedBuilder()
            .WithTitle("Added responses list")
            .WithColor(Context.Guild.CurrentUser.GetTopRole().GetColor());

        foreach (var mr in messageResponses)
        {
            embed.AddField($"ID: {mr.Id}", $"Message: {mr.Message}\nResponse: {mr.Response}");
        }

        await RespondAsync(embed: embed.Build());
    }

    [SlashCommand("delete", "Delete response")]
    [RequireContext(ContextType.Guild)]
    public async Task Delete([Summary("response_id", "Use 'response list' to check all id list")] int responseId)
    {
        var messageResponse = await _messageResponseRepository.GetById(responseId);

        if (messageResponse is null || (messageResponse is not null && messageResponse.GuildId != Context.Guild.Id))
        {
            await RespondAsync("Response does not exist");
            return;
        }

        await _messageResponseRepository.Delete(messageResponse!.Id);
        await RespondAsync("Response deleted!");
    }
}
