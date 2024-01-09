using AngryBot.Domain.Common.Interfaces.Repositories;
using AngryBot.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AngryBot.Persistence.Repositories;

internal sealed class UserDisappointmentRepository : IUserDisappointmentRepository
{
    private readonly AngryBotDbContext _dbContext;

    public UserDisappointmentRepository(AngryBotDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task Add(UserDisappointment userDisappointment, CancellationToken cancellationToken = default)
    {
        await _dbContext.UserDisappointments.AddAsync(userDisappointment, cancellationToken);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<UserDisappointment?> GetByUserId(ulong userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserDisappointments
            .Where(x => x.UserId == userId)
            .Include(x => x.Disappointments)
            .FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken);
    }

    public async Task IncrementCounter(ulong userId, CancellationToken cancellationToken = default)
    {
        var userDisappointment = _dbContext.UserDisappointments.FirstOrDefault(x => x.UserId == userId) ?? throw new Exception("UserDisappointment does not exist");
        userDisappointment.Counter++;
        _dbContext.UserDisappointments.Update(userDisappointment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExisting(ulong userId, CancellationToken cancellationToken = default)
    {
        return await _dbContext.UserDisappointments.AnyAsync(x => x.UserId == userId, cancellationToken);
    }
}
