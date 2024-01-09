using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngryBot.Persistence.Repositories;

internal sealed class UserRepository : IUserRepository
{
    private readonly AngryBotDbContext _dbContext;

    public UserRepository(AngryBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(User user, CancellationToken cancellationToken = default)
    {
        await _dbContext.Users.AddAsync(user, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<User?> GetUserById(ulong id, CancellationToken cancellationToken = default)
    {
        return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }
}
