using Application.Contracts;
using Application.Contracts.Authorization;
using Application.Dto;
using Application.Dto.Requests.CreateUser;
using Application.Wrappers;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

public class AccountController : ApiControllerBase
{
    private readonly IAccountService _accountService;
    private readonly IClientService _clientService;

    public AccountController(IAccountService accountService, IClientService clientService)
    {
        _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    [HttpPost]
    public async Task<Response<int>> CreateUser([FromBody] CreateUserRequest createUserRequest)
    {
        var userAsync = await _accountService.CreateUserAsync(createUserRequest.UserName, createUserRequest.Password);

        if (userAsync.Data == null || string.IsNullOrWhiteSpace(userAsync.Data.UserId))
            return Application.Wrappers.Response.Fail<int>(userAsync.Errors ?? Array.Empty<ResponseError>());
        
        var clientId = await _clientService.CreateNew(new ClientDto
        {
            UserId = userAsync.Data.UserId,
            Email = createUserRequest.UserName,
            LastName = createUserRequest.LastName,
            FirstName = createUserRequest.FirstName,
            PatrName = createUserRequest.PatrName
        });

        return Application.Wrappers.Response.Success(clientId.Data);

    }
}