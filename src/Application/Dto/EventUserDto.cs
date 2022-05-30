namespace Application.Dto;

public class EventUserDto
{
    public int Id { get; set; }
    
    public ClientDto User { get; set; }
    public EventDto Event { get; set; }
}