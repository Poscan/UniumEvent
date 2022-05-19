using Application.Dto;
using Application.Features.Events.Commands.SaveEvent;
using Application.Wrappers;

namespace Application.Contracts;

public interface IEventService
{
    Task<Response<EventDto>> GetAsync(int eventId, CancellationToken cancellationToken);
    Task<Response<IEnumerable<EventDto>>> GetAllAsync(CancellationToken cancellationToken);
    Task<Response<int>> SaveAsync(SaveEventRequest request, CancellationToken cancellationToken);
}