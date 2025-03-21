using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoBackend_S6_L5.Models
{
    public class Prenotazione
    {
        [Key]
        public Guid PrenotazioneId { get; set; }
        [Required]
        public DateOnly DataInizio { get; set; }
        [Required]
        public DateOnly DataFine { get; set; }
        [Required]
        public required string Stato { get; set; }
        public Guid ClienteId { get; set; }

        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        public Guid CameraId { get; set; }

        [ForeignKey("CameraId")]
        public Camera Camera { get; set; }
    }
}
