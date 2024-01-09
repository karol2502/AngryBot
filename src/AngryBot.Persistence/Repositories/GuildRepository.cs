using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngryBot.Persistence.Repositories;

internal sealed class GuildRepository : IGuildRepository
{
    private readonly AngryBotDbContext _dbContext;

    public GuildRepository(AngryBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Guild guild, CancellationToken cancellationToken = default)
    {
        await _dbContext.Guilds.AddAsync(guild, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(ulong guildId, CancellationToken cancellationToken = default)
    {
        var guild = await _dbContext.Guilds.FirstOrDefaultAsync(x => x.Id == guildId, cancellationToken);
        if (guild is not null)
        {
            _dbContext.Guilds.Remove(guild);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
