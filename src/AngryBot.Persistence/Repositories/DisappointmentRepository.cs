using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;

namespace AngryBot.Persistence.Repositories;

internal sealed class DisappointmentRepository : IDisappointmentRepository
{
    private readonly AngryBotDbContext _dbContext;

    public DisappointmentRepository(AngryBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(Disappointment disappointment, CancellationToken cancellationToken = default)
    {
        await _dbContext.Disappointments.AddAsync(disappointment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
