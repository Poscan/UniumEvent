using System.ComponentModel.DataAnnotations;

namespace Application.Dto.Requests.CreateUser
{
    public class CreateUserRequest
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        public string Password { get; set; } = null!;

        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string? PatrName { get; set; }
    }
}