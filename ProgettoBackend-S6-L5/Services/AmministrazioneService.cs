using EcommerceLiveEfCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S6_L5.Data;
using ProgettoBackend_S6_L5.ViewModels;


namespace ProgettoBackend_S6_L5.Services
{
    public class AmministrazioneService
    {
        private readonly ProgettoBackendDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<ApplicationRole> _roleManager;

        public AmministrazioneService(ProgettoBackendDbContext context, UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            _userManager = userManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<List<DipendenteViewModel>> GetAllDipendentiAsync()
        {
            try
            {
                var dipendenti = await _context.ApplicationUserRoles
                    .Include(ur => ur.User)
                    .Include(ur => ur.Role)
                    .Where(ur => ur.Role.Name == "DipendenteBase" || ur.Role.Name == "DipendenteAdmin")
                    .Select(ur => new DipendenteViewModel
                    {
                        FirstName = ur.User.FirstName,
                        LastName = ur.User.LastName,
                        Email = ur.User.Email,
                        BirthDate = ur.User.BirthDate,
                        Ruolo = ur.Role.Name
                    })
                    .Distinct()
                    .ToListAsync();

                return dipendenti;
            }
            catch (Exception)
            {
                return new List<DipendenteViewModel>();
            }
        }

        public async Task<bool> AddDipendenteAsync(AddDipendenteViewModel addDipendenteViewModel)
        {
            try
            {
                var dipendente = new ApplicationUser
                {
                    UserName = addDipendenteViewModel.Email,
                    Email = addDipendenteViewModel.Email,
                    FirstName = addDipendenteViewModel.FirstName,
                    LastName = addDipendenteViewModel.LastName,
                    BirthDate = addDipendenteViewModel.BirthDate
                };

                var result = await _userManager.CreateAsync(dipendente, addDipendenteViewModel.Password);
                if (!result.Succeeded)
                {
                    return false;
                }

                if (addDipendenteViewModel.Role != "DipendenteBase" && addDipendenteViewModel.Role != "DipendenteAdmin")
                {
                    return false;
                }

                await _userManager.AddToRoleAsync(dipendente, addDipendenteViewModel.Role);
                return true;
            }
            catch (Exception)
            {

                return false;
            }
        }

    }
}
