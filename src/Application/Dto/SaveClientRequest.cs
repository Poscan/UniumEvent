using System.ComponentModel.DataAnnotations;

namespace Application.Dto;

public class SaveClientRequest
{
    [Required]
    public int Id { get; set; }

    public string LastName { get; set; }
    public string FirstName { get; set; }
    public string? PatrName { get; set; }
    public string? MobilePhone { get; set; }
    public string Email { get; set; }
    public string? SchoolNumber { get; set; }
    public DateTime Birthday { get; set; }

    public string ParentLastName { get; set; }
    public string ParentFirstName { get; set; }
    public string? ParentPatrName { get; set; }
    public string? ParentMobilePhone { get; set; }
}