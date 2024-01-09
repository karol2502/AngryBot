using AngryBot.Domain.Entities;

namespace AngryBot.Domain.Common.Interfaces.Repositories;

public interface IMessageResponseRepository
{
    Task Add(MessageResponse messageResponse, CancellationToken cancellationToken = default);
    Task Delete(int messageResponseId, CancellationToken cancellationToken = default);
    Task<MessageResponse?> GetById(int id, CancellationToken cancellationToken = default);
    Task<IEnumerable<MessageResponse>> GetAllByGuildId(ulong guildId, CancellationToken cancellationToken = default);
}
