using Domain.Entities;

namespace Domain.Common.Repositories;

public interface IClientRepository : IRepository<Client>
{
    Task<Client> CreateNew(Client client);
    Task<Client> GetByUserIdAsync(string userId, CancellationToken cancellationToken);
}