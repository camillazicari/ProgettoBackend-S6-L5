﻿using ProgettoBackend_S6_L5.Models;
using System.ComponentModel.DataAnnotations;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class AddCameraViewModel
    {
        [Required]
        public int Numero { get; set; }
        public required string Tipo { get; set; }

        [Required]
        public decimal Prezzo { get; set; }

    }
}
