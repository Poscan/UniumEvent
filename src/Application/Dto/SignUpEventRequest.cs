using System.ComponentModel.DataAnnotations;

namespace Application.Dto;

public class SignUpEventRequest
{
    [Required]
    public int UserId { get; set; }
    [Required]
    public int EventId { get; set; }
}