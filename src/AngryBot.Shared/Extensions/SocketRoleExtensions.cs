using Discord;
using Discord.WebSocket;
using AngryBot.Shared.Consts;

namespace AngryBot.Shared.Extensions;

public static class SocketRoleExtensions
{
    public static Color GetColor(this SocketRole role)
    {
        return role.Color.Equals(Color.Default) ? Colors.DefaultRoleColor : role.Color;
    }
}
