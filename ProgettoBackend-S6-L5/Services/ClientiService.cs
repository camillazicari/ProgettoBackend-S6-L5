using ProgettoBackend_S6_L5.Data;
using ProgettoBackend_S6_L5.ViewModels;
using ProgettoBackend_S6_L5.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgettoBackend_S6_L5.Services
{
    public class ClientiService
    {
        private readonly ProgettoBackendDbContext _context;

        public ClientiService(ProgettoBackendDbContext context)
        {
            _context = context;
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

        public async Task<ClientiViewModel> GetAllClientsAsync()
        {
            var clientiList = new ClientiViewModel();

            try
            {
                clientiList.Clienti = await _context.Clienti.ToListAsync();
            }
            catch (Exception ex)
            {
                clientiList.Clienti = null;
            }

            return clientiList;
        }

        public async Task<bool> AddClientAsync(AddClientViewModel addClientViewModel)
        {
            try
            {
                var cliente = new Cliente()
                {
                    ClienteId = Guid.NewGuid(),
                    Nome = addClientViewModel.Nome,
                    Cognome = addClientViewModel.Cognome,
                    Email = addClientViewModel.Email,
                    Telefono = addClientViewModel.Telefono
                };

                _context.Clienti.Add(cliente);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Cliente?> GetClientByIdAsync(Guid id)
        {
            try
            {
                var cliente = await _context.Clienti.FindAsync(id);

                if (cliente == null)
                {
                    return null;
                }

                return cliente;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> EditClientAsync(EditClientViewModel editClientViewModel)
        {
            try
            {
                var cliente = await _context.Clienti.FindAsync(editClientViewModel.Id);

                if (cliente == null)
                {
                    return false;
                }

                cliente.Nome = editClientViewModel.Nome;
                cliente.Cognome = editClientViewModel.Cognome;
                cliente.Email = editClientViewModel.Email;
                cliente.Telefono = editClientViewModel.Telefono;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteClientByIdAsync(Guid id)
        {
            try
            {
                var cliente = await _context.Clienti.FindAsync(id);

                if (cliente == null)
                {
                    return false;
                }

                _context.Clienti.Remove(cliente);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
