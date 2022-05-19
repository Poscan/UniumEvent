namespace Application.Dto;

public class EventDto
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    public string PageName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}