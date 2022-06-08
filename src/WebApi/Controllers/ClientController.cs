using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ClientsResponse = Application.Wrappers.Response<System.Collections.Generic.IEnumerable<Application.Dto.ClientDto>>;

namespace WebApi.Controllers;

[Authorize]
public class ClientController : ApiControllerBase
{
    public ClientController(IClientService clientService)
    {
        _clientService = clientService ?? throw new ArgumentNullException(nameof(clientService));
    }

    private readonly IClientService _clientService;

    [HttpGet("find")]
    public async Task<ActionResult<IEnumerable<ClientDto>>> FindClients(string searchString)
    {
        return Ok( await _clientService.FindUserAsync(searchString));
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<ClientDto>> GetClient([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok( await _clientService.GetAsync(id, cancellationToken));
    }
    
    [HttpGet]
    public async Task<ActionResult<ClientsResponse>> GetAllClients(CancellationToken cancellationToken)
    {
        return Ok(await _clientService.GetAllAsync(cancellationToken));
    }

    [HttpGet("current-user")]
    public async Task<ActionResult<ClientsResponse>> GetCurrentClient(CancellationToken cancellationToken)
    {
        return Ok(await _clientService.GetByUserIdAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<int>> SaveClient([FromBody] SaveClientRequest saveEventRequest, CancellationToken cancellationToken)
    {
        return Ok(await _clientService.SaveAsync(saveEventRequest, cancellationToken));
    }
}