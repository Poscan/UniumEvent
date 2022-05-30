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

    [HttpGet("{userId:int}")]
    public async Task<ActionResult<EventDto>> GetAll([FromRoute] int userId, CancellationToken cancellationToken)
    {
        return Ok( await _eventUserService.GetAllAsync(userId, cancellationToken));
    }
    
    [HttpPost]
    public async Task<ActionResult<int>> SaveEvent([FromBody] SignUpEventRequest signUpEventRequest, CancellationToken cancellationToken)
    {
        return Ok(await _eventUserService.SaveAsync(signUpEventRequest, cancellationToken));
    }
}