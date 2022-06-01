using Domain.Entities;

namespace Domain.Common.Repositories;

public interface IEventUserRepository : IRepository<EventUser>
{
    Task<IReadOnlyCollection<EventUser>> GetAllUsersAsync(int eventId);
    Task<IReadOnlyCollection<EventUser>> GetAllEventsAsync(int userId);
}