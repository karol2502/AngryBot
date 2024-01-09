using AngryBot.Domain.Entities;
using Discord.WebSocket;
using Riok.Mapperly.Abstractions;

namespace AngryBot.Domain.Mappers;

[Mapper]
public static partial class UserMapper
{
    public static partial IEnumerable<User> AsModel(this IEnumerable<SocketGuildUser> users);
    public static partial User AsModel(this SocketGuildUser users);

    public static DateTime MapDateTimeOffset(DateTimeOffset time)
    {
        return time.UtcDateTime;
    }
}