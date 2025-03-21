using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class LogInViewModel
    {
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Password { get; set; }
    }
}
