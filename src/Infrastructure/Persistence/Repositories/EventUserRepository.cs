using Domain.Common.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EventUserRepository : Repository<EventUser>, IEventUserRepository
{
    public EventUserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<EventUser>> GetAllEventsAsync(int userId)
    {
        return await DbSet.Where(x => x.Client.Id == userId).ToListAsync();
    }
    
    public async Task<IReadOnlyCollection<EventUser>> GetAllUsersAsync(int eventId)
    {
        return await DbSet.Where(x => x.Event.Id == eventId).ToListAsync();
    }
}