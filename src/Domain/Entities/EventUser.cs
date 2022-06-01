using Domain.Common;

namespace Domain.Entities;

public class EventUser : IEntity
{
    public int Id { get; set; }
    
    public int ClientId { get; set; }
    public Client? Client { get; set; }
    
    public int EventId { get; set; }
    public Event? Event { get; set; }
    
    public int StatusId { get; set; }
    public Status Status { get; set; }
}