using Application.Contracts;
using Application.Dto;
using Application.Features.Events.Commands.SaveEvent;
using Application.Wrappers;
using Domain.Common;
using Domain.Common.Repositories;
using Domain.Entities;
using MapsterMapper;

namespace Infrastructure.Services;

public class EventService : IEventService
{
    public EventService(IEventRepository eventRepository, IUnitOfWork unitOfWork, IMapper mapper)
    {
        _eventRepository = eventRepository;
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private readonly IEventRepository _eventRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public async Task<Response<EventDto>> GetAsync(int eventId, CancellationToken cancellationToken)
    {
        try
        {
            var foundEvent = await _eventRepository.GetAsync(eventId, cancellationToken);
            var eventDto = _mapper.Map<EventDto>(foundEvent);

            return Response.Success(eventDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<IEnumerable<EventDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        try
        {
            var foundEvents = await _eventRepository.GetAllAsync(cancellationToken);
            var eventsDto = _mapper.Map<IEnumerable<EventDto>>(foundEvents);

            return Response.Success(eventsDto);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Response<int>> SaveAsync(SaveEventRequest request, CancellationToken cancellationToken)
    {
        var eventForSave = _mapper.Map<Event>(request);
        await _eventRepository.SaveAsync(eventForSave, cancellationToken);
        await _unitOfWork.CommitAsync(cancellationToken);

        return Response.Success(eventForSave.Id);
    }
}