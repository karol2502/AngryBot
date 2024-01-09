using AngryBot.Domain.Entities;

namespace AngryBot.Domain.Common.Interfaces.Repositories;

public interface IGuildRepository
{
    Task Add(Guild guild, CancellationToken cancellationToken = default);
    Task Delete(ulong guildId, CancellationToken cancellationToken = default);
}
