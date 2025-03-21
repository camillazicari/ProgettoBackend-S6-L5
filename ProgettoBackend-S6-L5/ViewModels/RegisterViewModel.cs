using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        public required string Nome { get; set; }

        [Required]
        public required string Cognome { get; set; }

        [Required]
        public required DateOnly DataNascita { get; set; }

        [Required]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Le password non corrispondono")]
        public required string ConfermaPassword { get; set; }
    }
}
