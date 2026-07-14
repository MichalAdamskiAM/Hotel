using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class RegisterDto
    {
        [Required]
        [StringLength(100)]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public required string LastName { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [RegularExpression(@"^\+?[0-9]{9,15}$")]
        public required string Phone { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 8)]
        [RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain uppercase, number and special character.")]
        public required string Password { get; set; }
    }
}