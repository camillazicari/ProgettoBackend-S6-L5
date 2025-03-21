using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S6_L5.Data;
using ProgettoBackend_S6_L5.Models;
using ProgettoBackend_S6_L5.ViewModels;

namespace ProgettoBackend_S6_L5.Services
{
    public class PrenotazioniService
    {
        private readonly ProgettoBackendDbContext _context;

        public PrenotazioniService(ProgettoBackendDbContext context)
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

        public async Task<PrenotazioniViewModel> GetAllPrenotazioniAsync()
        {
            var prenotazioniList = new PrenotazioniViewModel();

            try
            {
                prenotazioniList.Prenotazioni = await _context.Prenotazioni.Include(p => p.Cliente).Include(p => p.Camera).ToListAsync();
            }
            catch (Exception ex)
            {
                prenotazioniList.Prenotazioni = null;
            }

            return prenotazioniList;
        }

        public async Task<bool> AddPrenotazioneAsync(AddPrenotazioneViewModel addPrenotazioneViewModel)
        {
            try
            {
                var cliente = await _context.Clienti.FirstOrDefaultAsync(c => c.ClienteId == addPrenotazioneViewModel.ClienteId);
                if (cliente == null)
                    return false; 

                var prenotazione = new Prenotazione()
                {
                    PrenotazioneId = Guid.NewGuid(),
                    DataInizio = addPrenotazioneViewModel.DataInizio,
                    DataFine = addPrenotazioneViewModel.DataFine,
                    Stato = addPrenotazioneViewModel.Stato,
                    ClienteId = addPrenotazioneViewModel.ClienteId, 
                    CameraId = addPrenotazioneViewModel.CameraId
                };

                _context.Prenotazioni.Add(prenotazione);
                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<Prenotazione?> GetPrenotazioneByIdAsync(Guid id)
        {
            try
            {
                var prenotazione = await _context.Prenotazioni.FindAsync(id);

                if (prenotazione == null)
                {
                    return null;
                }

                return prenotazione;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<bool> EditPrenotazioneAsync(EditPrenotazioneViewModel editPrenotazioneViewModel)
        {
            try
            {
                var prenotazione = await _context.Prenotazioni.FindAsync(editPrenotazioneViewModel.PrenotazioneId);

                if (prenotazione == null)
                {
                    return false;
                }

                prenotazione.DataInizio = editPrenotazioneViewModel.DataInizio;
                prenotazione.DataFine = editPrenotazioneViewModel.DataFine;
                prenotazione.Stato = editPrenotazioneViewModel.Stato;
                prenotazione.ClienteId = editPrenotazioneViewModel.ClienteId;
                prenotazione.CameraId = editPrenotazioneViewModel.CameraId;

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePrenotazioneByIdAsync(Guid id)
        {
            try
            {
                var prenotazione = await _context.Prenotazioni.FindAsync(id);

                if (prenotazione == null)
                {
                    return false;
                }

                _context.Prenotazioni.Remove(prenotazione);

                return await SaveAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}
