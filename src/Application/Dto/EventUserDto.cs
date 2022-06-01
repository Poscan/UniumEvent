namespace Application.Dto;

public class EventUserDto
{
    public int Id { get; set; }
    
    public ClientDto Client { get; set; }
    public EventDto Event { get; set; }
}