using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.Models
{
    public class Cliente
    {
        [Key]
        public Guid ClienteId { get; set; }
        [Required]
        public required string Nome { get; set; }
        [Required]
        public required string Cognome { get; set; }
        [Required]
        public required string Email { get; set; }
        [Required]
        public required string Telefono { get; set; }

        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
