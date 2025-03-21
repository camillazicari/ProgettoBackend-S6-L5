using EcommerceLiveEfCore.Models;
using ProgettoBackend_S6_L5.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProgettoBackend_S6_L5.ViewModels
{
    public class AddDipendenteViewModel
    {
        public Guid RoleId { get; set; }
        public ApplicationUser User { get; set; }
        public ApplicationRole Role { get; set; }
        public List<ApplicationRole> Roles { get; set; }
        public string Password { get; set; }

    }
}
