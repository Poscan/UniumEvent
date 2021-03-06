using Domain.Common;

namespace Domain.Entities;

public class Event : IEntity
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string PageName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}