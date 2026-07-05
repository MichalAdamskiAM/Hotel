using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class LoginDto
    {
        [Required]
        public required string Email { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        public required string Password { get; set; }
    }
}