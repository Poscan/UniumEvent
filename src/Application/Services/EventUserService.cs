using Application.Contracts;
using Application.Dto;
using Application.Wrappers;
using Domain.Common;
using Domain.Common.Repositories;
using Domain.Entities;
using MapsterMapper;

namespace Application.Services;

public class EventUserService : IEventUserService
{
    public EventUserService(IEventUserRepository eventUserUserRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _eventUserRepository = eventUserUserRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private readonly IEventUserRepository _eventUserRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public async Task<Response<IEnumerable<EventUserDto>>> GetAllAsync(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var foundEventUsers = await _eventUserRepository.GetAllAsync(userId);
            var eventUsersDto = _mapper.Map<IEnumerable<EventUserDto>>(foundEventUsers);

            return Response.Success(eventUsersDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<int>> SaveAsync(SignUpEventRequest request, CancellationToken cancellationToken)
    {
        var eventForSave = new EventUser
                           {
                               Client = new Client() { Id = request.UserId },
                               Event = new Event() { Id = request.EventId },
                               Status = new Status { Id = 0, Name = "Ученик" }
                           };
        
        await _eventUserRepository.SaveAsync(eventForSave, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Response.Success(eventForSave.Id);
    }
}