using Domain.Common.Exceptions;
using Domain.Common.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class ClientRepository : Repository<Client>, IClientRepository
{
    public ClientRepository(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<Client> CreateNew(Client client)
    {
        await DbSet.AddAsync(client);

        return client;
    }

    public async Task<Client> GetByUserIdAsync(string userId, CancellationToken cancellationToken)
    {
        var client = await DbSet.FirstOrDefaultAsync(x => x.UserId == userId, cancellationToken) ?? throw new EntityNotFoundException(nameof(Client), 0);

        return client;
    }
}