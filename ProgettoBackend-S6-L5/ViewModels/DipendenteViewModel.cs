using EcommerceLiveEfCore.Models;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class DipendenteViewModel
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public DateOnly? BirthDate { get; set; }
        public string? Ruolo { get; set; }

    }
}
