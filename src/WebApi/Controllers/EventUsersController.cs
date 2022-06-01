using Application.Contracts;
using Application.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Authorize]
public class EventUsersController : ApiControllerBase
{
    public EventUsersController(IEventUserService eventUserService)
    {
        _eventUserService = eventUserService ?? throw new ArgumentNullException(nameof(eventUserService));
    }

    private readonly IEventUserService _eventUserService;

    [HttpGet("/client-id/{clientId:int}")]
    public async Task<ActionResult<EventDto>> GetAllEvents([FromRoute] int clientId, CancellationToken cancellationToken)
    {
        return Ok( await _eventUserService.GetAllEventsAsync(clientId, cancellationToken));
    }
    
    [HttpGet("/event-id/{eventId:int}")]
    public async Task<ActionResult<EventDto>> GetAllUsers([FromRoute] int eventId, CancellationToken cancellationToken)
    {
        return Ok( await _eventUserService.GetAllUsersAsync(eventId, cancellationToken));
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> Subscribe([FromBody] SubscribeEventRequest subscribeEventRequest, CancellationToken cancellationToken)
    {
        return Ok(await _eventUserService.SaveAsync(subscribeEventRequest, cancellationToken));
    }
    
    [HttpDelete("{id:int}")]
    public async Task<ActionResult<int>> Unsubscribe([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok(await _eventUserService.DeleteAsync(id, cancellationToken));
    }
}