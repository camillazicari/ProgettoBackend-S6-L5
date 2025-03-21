using EcommerceLiveEfCore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S6_L5.Data;
using ProgettoBackend_S6_L5.Models;
using ProgettoBackend_S6_L5.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;


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

        private async Task<bool> SaveAsync()
        {
            try
            {
                var rowsAffected = await _context.SaveChangesAsync();

                if (rowsAffected > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<DipendenteViewModel> GetAllDipendentiAsync()
        {
            var dipendentiList = new DipendenteViewModel();

            try
            {
                dipendentiList.Dipendenti = await _context.ApplicationUserRoles
                                                        .Include(ur => ur.User)
                                                        .Include(ur => ur.Role)
                                                        .Where(ur => ur.Role.Name == "DipendenteBase" || ur.Role.Name == "DipendenteAdmin")
                                                        .Select(ur => new ApplicationUserRole
                                                        {
                                                            User = ur.User,
                                                            Role = ur.Role
                                                        })
                                                        .ToListAsync();
            }
            catch (Exception ex)
            {
                dipendentiList.Dipendenti = null;
            }

            return dipendentiList;
        }

        public async Task<bool> AddDipendenteAsync(AddDipendenteViewModel addDipendenteViewModel)
        {
            try
            {
                var dipendente = new ApplicationUser()
                {
                    UserName = addDipendenteViewModel.User.UserName,
                    Email = addDipendenteViewModel.User.Email,
                    FirstName = addDipendenteViewModel.User.FirstName,
                    LastName = addDipendenteViewModel.User.LastName,
                    BirthDate = addDipendenteViewModel.User.BirthDate
                };

                var result = await _userManager.CreateAsync(dipendente, addDipendenteViewModel.Password);
                if (!result.Succeeded)
                {
                    return false;
                }

                var role = addDipendenteViewModel.Role.Name; 
                var roleExists = await _roleManager.RoleExistsAsync(role);
                if (!roleExists)
                {
                    return false;
                }

                await _userManager.AddToRoleAsync(dipendente, role);

                return true;
            }
            catch (Exception ex)
            {
                
                return false;
            }
        }

    }
}
