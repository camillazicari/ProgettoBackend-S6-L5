using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class AddPrenotazioneViewModel
    {
        [Required]
        public DateOnly DataInizio { get; set; }

        [Required]
        public DateOnly DataFine { get; set; }

        [Required]
        public string Stato { get; set; }

        [Required]
        public Guid ClienteId { get; set; }

        [Required]
        public Guid CameraId { get; set; }
    }
}
