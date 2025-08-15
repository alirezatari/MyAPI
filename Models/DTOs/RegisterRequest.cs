using System.ComponentModel.DataAnnotations;

namespace MyApi.Models.DTOs
{
    public class RegisterRequest
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public required string Username { get; set; }

        [Required]
        [StringLength(100, MinimumLength = 8)]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; } // e.g., "Admin" or "User"
    }
}
