using Domain.Common;

namespace Domain.Entities;

public class EventUser : IEntity
{
    public int Id { get; set; }
    
    public Client Client { get; set; }
    public Event Event { get; set; }
    public Status Status { get; set; }
}