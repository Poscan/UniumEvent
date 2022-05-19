using Domain.Entities;

namespace GenerationRules.Rules;

public sealed class EventGenerationRule : GenerationRuleBase<Event>
{
    public EventGenerationRule()
    {
        RuleFor(x => x.Name, f => f.Random.String());
        RuleFor(x => x.PageName, f => f.Random.String());
        RuleFor(x => x.StartDate, f => f.Date.Past());
        RuleFor(x => x.EndDate, f => f.Date.Future());
    }
}