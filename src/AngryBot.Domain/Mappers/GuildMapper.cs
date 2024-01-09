using AngryBot.Domain.Entities;
using Discord.WebSocket;
using Riok.Mapperly.Abstractions;

namespace AngryBot.Domain.Mappers;

[Mapper]
public static partial class GuildMapper
{
    public static partial IEnumerable<Guild> AsModel(this IEnumerable<SocketGuild> guilds);
    public static partial Guild AsModel(this SocketGuild guilds);
}
