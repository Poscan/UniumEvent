using Application.Dto;
using Application.Wrappers;

namespace Application.Contracts;

public interface IEventUserService
{
    Task<Response<IEnumerable<EventUserDto>>> GetAllAsync(int userId, CancellationToken cancellationToken);
    Task<Response<int>> SaveAsync(SignUpEventRequest request, CancellationToken cancellationToken);
}