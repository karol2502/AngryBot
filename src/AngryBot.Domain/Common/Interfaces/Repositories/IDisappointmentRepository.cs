using AngryBot.Domain.Entities;

namespace AngryBot.Domain.Common.Interfaces.Repositories;

public interface IDisappointmentRepository
{
    Task Add(Disappointment disappointment, CancellationToken cancellationToken = default);
}
