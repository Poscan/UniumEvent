using Application.Dto;
using Application.Wrappers;

namespace Application.Contracts;

public interface IEventUserService
{
    Task<Response<IEnumerable<EventUserDto>>> GetAllEventsAsync(int userId, CancellationToken cancellationToken);
    Task<Response<IEnumerable<EventUserDto>>> GetAllUsersAsync(int eventId, CancellationToken cancellationToken);
    Task<Response<EventUserDto>> SaveAsync(SubscribeEventRequest request, CancellationToken cancellationToken);
    Task<Response<int>> DeleteAsync(int id, CancellationToken cancellationToken);
}