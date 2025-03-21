using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.Models
{
    public class Camera
    {
        [Key]
        public Guid CameraId { get; set; }
        [Required]
        public int Numero { get; set; }
        [Required]
        public required string Tipo { get; set; } 
        [Required]
        public decimal Prezzo { get; set; }

        public ICollection<Prenotazione> Prenotazioni { get; set; }
    }
}
