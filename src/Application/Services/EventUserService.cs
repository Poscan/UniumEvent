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
    public EventUserService(IEventUserRepository eventUserUserRepository, IClientRepository clientRepository, IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper, ICurrentUser currentUser)
    {
        _eventUserRepository = eventUserUserRepository;
        _clientRepository = clientRepository;
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _currentUser = currentUser;
    }

    private readonly IEventUserRepository _eventUserRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly ICurrentUser _currentUser;

    public async Task<Response<IEnumerable<EventUserDto>>> GetAllEventsAsync(int userId, CancellationToken cancellationToken)
    {
        try
        {
            var foundEventUsers = await _eventUserRepository.GetAllEventsAsync(userId);
            
            foreach (var foundEventUser in foundEventUsers)
            {
                var clientFromStore = await _clientRepository.GetAsync(foundEventUser.ClientId);
                var eventFromStore = await _eventRepository.GetAsync(foundEventUser.EventId);
                
                foundEventUser.Client = clientFromStore;
                foundEventUser.Event = eventFromStore;
            }
            
            var eventUsersDto = _mapper.Map<IEnumerable<EventUserDto>>(foundEventUsers);

            return Response.Success(eventUsersDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Response<IEnumerable<EventUserDto>>> GetAllUsersAsync(int eventId, CancellationToken cancellationToken)
    {
        try
        {
            var foundEventUsers = await _eventUserRepository.GetAllUsersAsync(eventId);
            
            foreach (var foundEventUser in foundEventUsers)
            {
                var clientFromStore = await _clientRepository.GetAsync(foundEventUser.ClientId);
                var eventFromStore = await _eventRepository.GetAsync(foundEventUser.EventId);
                
                foundEventUser.Client = clientFromStore;
                foundEventUser.Event = eventFromStore;
            }
            
            var eventUsersDto = _mapper.Map<IEnumerable<EventUserDto>>(foundEventUsers);

            return Response.Success(eventUsersDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<EventUserDto>> SaveAsync(SubscribeEventRequest request, CancellationToken cancellationToken)
    {
        var eventForSave = new EventUser
                           {
                               ClientId = request.UserId,
                               EventId = request.EventId,
                               StatusId = 1
                           };
        
        await _eventUserRepository.SaveAsync(eventForSave, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        eventForSave.Client = await _clientRepository.GetAsync(request.UserId);
        eventForSave.Event = await _eventRepository.GetAsync(request.EventId);
        
        var eventUserDto = _mapper.Map<EventUserDto>(eventForSave);

        return Response.Success(eventUserDto);
    }
    
    public async Task<Response<int>> DeleteAsync(int id, CancellationToken cancellationToken)
    {
        var userId = _currentUser.UserId;
        var client = await _clientRepository.GetByUserIdAsync(userId, cancellationToken);
        
        var events = await _eventUserRepository.GetAllUsersAsync(id);
        var eventForDelete = events.FirstOrDefault(x => x.ClientId == client.Id);
        
        _eventUserRepository.Delete(eventForDelete);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Response.Success(eventForDelete.Id);
    }
}