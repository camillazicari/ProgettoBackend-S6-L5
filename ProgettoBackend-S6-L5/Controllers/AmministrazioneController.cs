using Microsoft.AspNetCore.Mvc;
using ProgettoBackend_S6_L5.Services;
using ProgettoBackend_S6_L5.ViewModels;

namespace ProgettoBackend_S6_L5.Controllers
{
    public class AmministrazioneController : Controller
    {
        private readonly AmministrazioneService _amministrazioneService;

        public AmministrazioneController(AmministrazioneService amministrazioneService)
        {
            _amministrazioneService = amministrazioneService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("dipendenti/get-all")]
        public async Task<IActionResult> ListaDipendenti()
        {
            var dipendentiList = await _amministrazioneService.GetAllDipendentiAsync();
            return PartialView("_DipendentiList", dipendentiList);
        }

        [Route("Dipendente/Add")]
        public IActionResult Add()
        {
            return PartialView("_AddDipendenteForm");
        }

        [HttpPost("Dipendente/Add")]
        public async Task<IActionResult> Add(AddDipendenteViewModel addDipendenteViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false
                });
            }

            var result = await _amministrazioneService.AddDipendenteAsync(addDipendenteViewModel);

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
