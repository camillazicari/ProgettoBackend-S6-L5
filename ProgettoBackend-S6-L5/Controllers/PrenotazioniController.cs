using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProgettoBackend_S6_L5.Services;
using ProgettoBackend_S6_L5.ViewModels;

namespace ProgettoBackend_S6_L5.Controllers
{
    public class PrenotazioniController : Controller
    {
        private readonly PrenotazioniService _amministrazioneService;
        private readonly CamereService _camereService;
        private readonly ClientiService _clientiService;

        public PrenotazioniController(PrenotazioniService amministrazioneService, CamereService camereService, ClientiService clientiService)
        {
            _amministrazioneService = amministrazioneService;
            _camereService = camereService;
            _clientiService = clientiService;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("prenotazioni/get-all")]
        public async Task<IActionResult> ListaPrenotazioni()
        {
            var prenotazioni = await _amministrazioneService.GetAllPrenotazioniAsync();

            return PartialView("_PrenotazioniList", prenotazioni);
        }

        [Authorize(Roles = "Admin,DipendenteAdmin")]
        [Route("Prenotazione/Add")]
        public async Task<IActionResult> Add()
        {
            var camereViewModel = await _camereService.GetAllCamereAsync();
            var clientiViewModel = await _clientiService.GetAllClientsAsync();

            ViewBag.Camere = camereViewModel.Camere;  
            ViewBag.Clienti = clientiViewModel.Clienti;  

            return PartialView("_AddPrenotazioneForm");
        }

        [HttpPost("Prenotazione/Add")]
        public async Task<IActionResult> Add(AddPrenotazioneViewModel addPrenotazioneViewModel)
        {
            Console.WriteLine($"ClienteId ricevuto: {addPrenotazioneViewModel.ClienteId}");

            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _amministrazioneService.AddPrenotazioneAsync(addPrenotazioneViewModel);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                });
            }

            return Json(new
            {
                success = true,
            });
        }

        [Authorize(Roles = "Admin,DipendenteAdmin")]
        [HttpGet("Prenotazione/Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var prenotazione = await _amministrazioneService.GetPrenotazioneByIdAsync(id);
                                              

            if (prenotazione == null)
            {
                return NotFound();
            }

            var editPrenotazioneViewModel = new EditPrenotazioneViewModel
            {
                PrenotazioneId = prenotazione.PrenotazioneId,
                DataInizio = prenotazione.DataInizio,
                DataFine = prenotazione.DataFine,
                Stato = prenotazione.Stato,
                ClienteId = prenotazione.ClienteId,
                CameraId = prenotazione.CameraId
            };

            var camereViewModel = await _camereService.GetAllCamereAsync();
            var clientiViewModel = await _clientiService.GetAllClientsAsync();

            ViewBag.Camere = camereViewModel.Camere;
            ViewBag.Clienti = clientiViewModel.Clienti;

            return PartialView("_EditPrenotazioneForm", editPrenotazioneViewModel);
        }

        [HttpPost("prenotazione/edit/save")]
        public async Task<IActionResult> Edit(EditPrenotazioneViewModel editPrenotazioneViewModel)
        {
            var result = await _amministrazioneService.EditPrenotazioneAsync(editPrenotazioneViewModel);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                });
            }

            return Json(new
            {
                success = true,
            }); ;
        }

        [HttpPost("prenotazione/delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _amministrazioneService.DeletePrenotazioneByIdAsync(id);

            if (!result)
            {
                return Json(new
                {
                    success = false,
                });
            }

            return Json(new
            {
                success = true,
            });
        }
    }
}
