using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class EditClientViewModel
    {
        public required Guid Id { get; set; }

        public required string Nome { get; set; }
        public required string Cognome { get; set; }
        public required string Email { get; set; }
        public required string Telefono { get; set; }
    }
}
