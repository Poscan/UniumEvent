using Application.Dto.Authorization;
using Application.Wrappers;

namespace Application.Contracts.Authorization;

public interface IAccountService
{
    Task<Response<AuthorizationData>> CreateUserAsync(string userName, string password);
}