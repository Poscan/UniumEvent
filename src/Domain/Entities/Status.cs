using Domain.Common;

namespace Domain.Entities;

public class Status : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
}