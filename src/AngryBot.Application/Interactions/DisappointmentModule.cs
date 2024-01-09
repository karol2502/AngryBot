using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;
using AngryBot.Shared.Extensions;
using Discord;
using Discord.Interactions;
using Discord.WebSocket;
using System.Text;

namespace AngryBot.Application.Interactions;

[Group("toilet", "Commands when someone lets you down")]
public sealed class DisappointmentModule : InteractionModuleBase<SocketInteractionContext>
{
    private readonly IUserDisappointmentRepository _userDisappointmentRepository;
    private readonly IDisappointmentRepository _disappointmentRepository;

    public DisappointmentModule(IUserDisappointmentRepository userDisappointmentRepository, IDisappointmentRepository disappointmentRepository)
    {
        _userDisappointmentRepository = userDisappointmentRepository;
        _disappointmentRepository = disappointmentRepository;
    }

    [SlashCommand("add", "Use if someone lets you down")]
    public async Task Add([Summary("user", "Someone that let you down")] SocketGuildUser guildUser, [Summary("description", "what he did")] string description)
    {
        var userDisappointment = await _userDisappointmentRepository.GetByUserId(guildUser.Id);
        if (userDisappointment is null)
        {
            userDisappointment = new UserDisappointment(guildUser.Id);
            await _userDisappointmentRepository.Add(userDisappointment);
        }

        var newDisappointment = new Disappointment(description, userDisappointment, Context.User.Id);
        await _disappointmentRepository.Add(newDisappointment);

        await _userDisappointmentRepository.IncrementCounter(guildUser.Id);
        await RespondAsync($"New toilet added!");
    }

    [SlashCommand("list", "Sends all the disappointments of a user")]
    public async Task List([Summary("user", "someone that let you down")] SocketGuildUser guildUser)
    {
        var userDisappointment = await _userDisappointmentRepository.GetByUserId(guildUser.Id);
        if (userDisappointment is null)
        {
            await RespondAsync("No added responses!");
            return;
        }

        var embed = new EmbedBuilder()
            .WithAuthor(guildUser)
            .WithColor(Context.Guild.CurrentUser.GetTopRole().GetColor());

        var disappointments = new StringBuilder();
        foreach (var disappointment in userDisappointment.Disappointments)
        {
            disappointments.AppendLine($"{disappointment.AddedAt.ToLocalTime()} - **{disappointment.Description}**");            
        }
        embed.AddField("Disappointment list 🚽", disappointments.ToString());
        await RespondAsync(embed: embed.Build());
    }
}
