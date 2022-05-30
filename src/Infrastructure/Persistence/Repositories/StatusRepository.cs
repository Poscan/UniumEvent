using Domain.Common.Repositories;
using Domain.Entities;

namespace Infrastructure.Persistence.Repositories;

public class StatusRepository : Repository<Status>, IStatusRepository
{
    public StatusRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }
}