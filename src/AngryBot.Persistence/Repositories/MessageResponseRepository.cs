using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngryBot.Persistence.Repositories;

internal sealed class MessageResponseRepository : IMessageResponseRepository
{
    private readonly AngryBotDbContext _dbContext;

    public MessageResponseRepository(AngryBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(MessageResponse messageResponse, CancellationToken cancellationToken = default)
    {
        await _dbContext.MessageResponses.AddAsync(messageResponse, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Delete(int messageResponseId, CancellationToken cancellationToken = default)
    {
        var messageResponse = await _dbContext.MessageResponses.FirstOrDefaultAsync(x => x.Id == messageResponseId, cancellationToken);
        if (messageResponse is not null)
        {
            _dbContext.MessageResponses.Remove(messageResponse);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<MessageResponse>> GetAllByGuildId(ulong guildId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MessageResponses.AsNoTracking().Where(x => x.GuildId == guildId).ToListAsync(cancellationToken);
    }

    public async Task<MessageResponse?> GetById(int id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.MessageResponses.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
