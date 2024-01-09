using Discord;
using Discord.Commands;

namespace AngryBot.Application.Commands;

public sealed class TestModule : ModuleBase<SocketCommandContext>
{
    [Command("ping")]
    [Summary("Pong!")]
    public async Task PongAsync()
    {
        var reference = new MessageReference(Context.Message.Id, Context.Channel.Id, Context.Guild.Id, false);
        await ReplyAsync("Pong!", messageReference: reference);
    }

    [Command("hello")]
    [Summary("Hello World!")]
    public async Task HelloWorldAsync()
        => await ReplyAsync("Hello World!");
}
