using Domain.Common.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EventRepository : Repository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<Event>> GetByIdAsync(int id)
    {
        return await DbSet.Where(x => x.Id == id).ToListAsync();
    }
}