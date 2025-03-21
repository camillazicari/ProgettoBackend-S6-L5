using EcommerceLiveEfCore.Models;
using ProgettoBackend_S6_L5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class AddDipendenteViewModel
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        [Required]
        public DateOnly BirthDate { get; set; }

        [Required]
        public required string Email { get; set; }

        [Required]
        public required string Password { get; set; }

        [Required]
        public required string Role { get; set; }

    }
}
