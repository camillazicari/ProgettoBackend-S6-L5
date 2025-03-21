using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoBackend_S6_L5.Services;
using ProgettoBackend_S6_L5.ViewModels;

namespace ProgettoBackend_S6_L5.Controllers
{
    public class CamereController : Controller
    {
        private readonly CamereService _camereService;

        public CamereController(CamereService camereService)
        {
            _camereService = camereService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("camere/get-all")]
        public async Task<IActionResult> ListaClienti()
        {
            var camereList = await _camereService.GetAllCamereAsync();

            return PartialView("_CamereList", camereList);
        }

        [Authorize(Roles = "Admin,DipendenteAdmin")]
        [Route("Camera/Add")]
        public IActionResult Add()
        {
            return PartialView("_AddCameraForm");
        }

        [HttpPost("Camera/Add")]
        public async Task<IActionResult> Add(AddCameraViewModel addCameraViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while saving entity to database"
                });
            }

            var result = await _camereService.AddCameraAsync(addCameraViewModel);

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
        [Route("Camera/Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var camera = await _camereService.GetCameraByIdAsync(id);

            if (camera == null)
            {
                return RedirectToAction("Index");
            };

            var editCameraViewModel = new EditCameraViewModel()
            {
                CameraId = camera.CameraId,
                Numero = camera.Numero,
                Prezzo = camera.Prezzo,
                Tipo = camera.Tipo,
            };

            return PartialView("_EditCameraForm", editCameraViewModel);
        }

        [HttpPost("camera/edit/save")]
        public async Task<IActionResult> Edit(EditCameraViewModel editCameraViewModel)
        {
            var result = await _camereService.EditCameraAsync(editCameraViewModel);

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

        [HttpPost("camera/delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _camereService.DeleteCameraByIdAsync(id);

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
