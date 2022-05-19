using System.ComponentModel.DataAnnotations;

namespace Application.Features.Events.Commands.SaveEvent;

public class SaveEventRequest
{
    [Required]
    public string Name { get; set; }
    [Required]
    public string PageName { get; set; }
    [Required]
    public DateTime StartDate { get; set; }
    [Required]
    public DateTime EndDate { get; set; }
}