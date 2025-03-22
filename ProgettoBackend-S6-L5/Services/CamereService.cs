using ProgettoBackend_S6_L5.Data;
using ProgettoBackend_S6_L5.ViewModels;
using ProgettoBackend_S6_L5.Models;
using Microsoft.EntityFrameworkCore;

namespace ProgettoBackend_S6_L5.Services
{
    public class CamereService
    {
        private readonly ProgettoBackendDbContext _context;

        public CamereService(ProgettoBackendDbContext context)
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

        public async Task<CamereViewModel> GetAllCamereAsync()
        {
            var camereList = new CamereViewModel();

            try
            {
                camereList.Camere = await _context.Camere.ToListAsync();
            }
            catch (Exception ex)
            {
                camereList.Camere = null;
            }

            return camereList;
        }

        public async Task<bool> AddCameraAsync(AddCameraViewModel addCameraViewModel)
        {
            try
            {
                var camera = new Camera()
                {
                    CameraId = Guid.NewGuid(),
                    Numero = addCameraViewModel.Numero,
                    Prezzo = addCameraViewModel.Prezzo,
                    Tipo = addCameraViewModel.Tipo
                };

                _context.Camere.Add(camera);

                return await SaveAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<Camera?> GetCameraByIdAsync(Guid id)
        {
            try
            {
                var camera = await _context.Camere.FindAsync(id);

                if (camera == null)
                {
                    return null;
                }

                return camera;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<bool> EditCameraAsync(EditCameraViewModel editCameraViewModel)
        {
            try
            {
                var camera = await _context.Camere.FindAsync(editCameraViewModel.CameraId);

                if (camera == null)
                {
                    return false;
                }

                camera.Numero = editCameraViewModel.Numero;
                camera.Tipo = editCameraViewModel.Tipo;
                camera.Prezzo = editCameraViewModel.Prezzo;

                return await SaveAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCameraByIdAsync(Guid id)
        {
            try
            {
                var camera = await _context.Camere.FindAsync(id);

                if (camera == null)
                {
                    return false;
                }

                _context.Camere.Remove(camera);

                return await SaveAsync();
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
