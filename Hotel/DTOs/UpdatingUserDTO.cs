using System.ComponentModel.DataAnnotations;

namespace Hotel.DTOs
{
    public class UpdatingUserDTO
    {
        [StringLength(100)]
        public string? FirstName { get; set; }

        [StringLength(100)]
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }

        [RegularExpression(@"^\+?[0-9]{9,15}$")]
        public string? Phone { get; set; }

        [StringLength(50, MinimumLength = 8), RegularExpression(@"^(?=.*[A-Z])(?=.*\d)(?=.*[\W_]).+$",
            ErrorMessage = "Password must contain uppercase, number and special character.")]
        public string? Password { get; set; }
    }
}