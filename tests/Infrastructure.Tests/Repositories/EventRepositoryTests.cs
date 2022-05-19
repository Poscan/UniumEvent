using System.Linq;
using System.Threading.Tasks;
using Domain.Common.Repositories;
using Domain.Entities;
using FluentAssertions;
using GenerationRules.Rules;
using Infrastructure.Persistence.Repositories;
using Xunit;

namespace Infrastructure.Tests.Repositories;

public class EventRepositoryTests : RepositoryTests<Event>
{
    private readonly EventGenerationRule _eventGenerationRule;
    private readonly IEventRepository _eventRepository;

    public EventRepositoryTests(EventGenerationRule eventEventGenerationRule) : base(eventEventGenerationRule)
    {
        _eventGenerationRule = eventEventGenerationRule;
        _eventRepository = new EventRepository(Context);
    }

    [Fact]
    public async Task GetByIdAsync_ShouldReturnAllEventsByGenre_IfSuchEventsExist()
    {
        var events = _eventGenerationRule.GenerateLazy(5).ToArray();
        await SaveEntitiesAsync(events);

        var firstEvent = events.First();

        var foundEvent = await _eventRepository.GetByIdAsync(firstEvent.Id);
        foundEvent.Should().BeEquivalentTo(new[] {firstEvent});
    }

    [Fact]
    public async Task Update_ShouldUpdateEventProperties()
    {
        var @event = _eventGenerationRule.Generate();
        await SaveEntitiesAsync(@event);

        var newEvent = _eventGenerationRule.Generate();
        @event.Name = newEvent.Name;
        
        _eventRepository.Update(@event);
        await UnitOfWork.CommitAsync();

        @event = await GetEntity<Event>(@event.Id);
        
        @event.Name.Should().Be(newEvent.Name);
    }
}