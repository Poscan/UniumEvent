using Domain.Common.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class EventUserRepository : Repository<EventUser>, IEventUserRepository
{
    public EventUserRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<IReadOnlyCollection<EventUser>> GetAllAsync(int userId)
    {
        return await DbSet.Where(x => x.Client.Id == userId).ToListAsync();
    }
}