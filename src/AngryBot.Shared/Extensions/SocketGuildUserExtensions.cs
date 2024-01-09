using Discord.WebSocket;

namespace AngryBot.Shared.Extensions;

public static class SocketGuildUserExtensions
{
    public static SocketRole GetTopRole(this SocketGuildUser socketGuildUser)
    {
        return socketGuildUser.Roles.MaxBy(x => x.Position) ?? socketGuildUser.Guild.EveryoneRole;
    }
}
