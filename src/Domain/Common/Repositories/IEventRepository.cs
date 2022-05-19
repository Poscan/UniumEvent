using Domain.Entities;

namespace Domain.Common.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<IReadOnlyCollection<Event>> GetByIdAsync(int id);
}