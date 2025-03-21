using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class AddClientViewModel
    {
        [Required]
        [StringLength(20)]
        public required string Nome { get; set; }
        [Required]
        [StringLength(20)]
        public required string Cognome { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        [StringLength(10)]
        public required string Telefono { get; set; }
    }
}
