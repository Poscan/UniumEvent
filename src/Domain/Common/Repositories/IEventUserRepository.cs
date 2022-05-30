using Domain.Entities;

namespace Domain.Common.Repositories;

public interface IEventUserRepository : IRepository<EventUser>
{
    Task<IReadOnlyCollection<EventUser>> GetAllAsync(int userId);
}