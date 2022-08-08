using System.ComponentModel.DataAnnotations;

namespace TechSupportHelpSystem.Models.DTO
{
    public class UserAuthenticationDto
    {
        [Required(ErrorMessage = "Email is required.")]
        public string? Username { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        public string? Password { get; set; }
    }
}
