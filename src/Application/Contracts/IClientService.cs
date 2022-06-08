using Application.Dto;
using Application.Wrappers;

namespace Application.Contracts;

public interface IClientService
{
    Task<Response<IEnumerable<ClientDto>>> FindUserAsync(string searchString);
    Task<Response<ClientDto>> GetByUserIdAsync(CancellationToken cancellationToken);
    Task<Response<ClientDto>> GetAsync(int clientId, CancellationToken cancellationToken);
    Task<Response<IEnumerable<ClientDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Response<int>> SaveAsync(SaveClientRequest request, CancellationToken cancellationToken);
    Task<Response<int>> CreateNew(ClientDto clientDto);
}