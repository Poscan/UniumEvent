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

    public async Task<IEnumerable<Client>> FindClientsAsync(string searchString)
    {
        var search = searchString.ToUpper().Trim().Split(" ");
        return await DbSet.Where(x => (search.Length > 0 && (x.LastName.ToUpper().Contains(search[0]) ||
                                                                    x.FirstName.ToUpper().Contains(search[0]))) ||
                                             (search.Length > 1 && (x.LastName.ToUpper().Contains(search[0]) &&
                                                                    x.FirstName.ToUpper().Contains(search[1]))) ||
                                             (search.Length > 2 && (x.LastName.ToUpper().Contains(search[0]) &&
                                                                    x.FirstName.ToUpper().Contains(search[1]) &&
                                                                    x.PatrName != null &&
                                                                    x.PatrName.ToUpper().Contains(search[2]))))
            .OrderBy(x => x.LastName.ToUpper().Contains(search[0])).ToListAsync();
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