using Application.Contracts;
using Application.Dto;
using Application.Features.Events.Commands.SaveEvent;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using EventsResponse = Application.Wrappers.Response<System.Collections.Generic.IEnumerable<Application.Dto.EventDto>>;

namespace WebApi.Controllers;

[Authorize]
public class EventsController : ApiControllerBase
{
    public EventsController(IEventService eventService)
    {
        _eventService = eventService ?? throw new ArgumentNullException(nameof(eventService));
    }

    private readonly IEventService _eventService;

    [HttpGet("{id:int}")]
    public async Task<ActionResult<EventDto>> GetEvent([FromRoute] int id, CancellationToken cancellationToken)
    {
        return Ok( await _eventService.GetAsync(id, cancellationToken));
    }
    
    [HttpGet]
    public async Task<ActionResult<EventsResponse>> GetAllEvents(CancellationToken cancellationToken)
    {
        return Ok(await _eventService.GetAllAsync(cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult<int>> SaveEvent([FromBody] SaveEventRequest saveEventRequest, CancellationToken cancellationToken)
    {
        return Ok(await _eventService.SaveAsync(saveEventRequest, cancellationToken));
    }
}