using Discord.Interactions;

namespace AngryBot.Application.Interactions;

public sealed class TestModule : InteractionModuleBase<SocketInteractionContext>
{
    [SlashCommand("ping", "Make the bot hit the ball")]
    public async Task PongAsync()
        => await RespondAsync($"Pong! {Context.Client.Latency}ms");

    [SlashCommand("hello", "Say hello to the World")]
    public async Task HelloWorldAsync()
        => await RespondAsync("Hello World!");
}
