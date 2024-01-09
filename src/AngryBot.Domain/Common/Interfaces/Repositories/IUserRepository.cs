using AngryBot.Domain.Entities;

namespace AngryBot.Domain.Common.Interfaces.Repositories;

public interface IUserRepository
{
    Task Add(User user, CancellationToken cancellationToken = default);
    Task<User?> GetUserById(ulong id, CancellationToken cancellationToken = default);
}
