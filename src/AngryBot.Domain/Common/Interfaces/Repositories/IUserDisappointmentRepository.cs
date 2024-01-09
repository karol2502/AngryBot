using AngryBot.Domain.Entities;

namespace AngryBot.Domain.Common.Interfaces.Repositories;

public interface IUserDisappointmentRepository
{
    Task Add(UserDisappointment userDisappointment, CancellationToken cancellationToken = default);
    Task<UserDisappointment?> GetByUserId(ulong userId, CancellationToken cancellationToken = default);
    Task IncrementCounter(ulong userId, CancellationToken cancellationToken = default);
    Task<bool> IsExisting(ulong userId, CancellationToken cancellationToken = default);
}
