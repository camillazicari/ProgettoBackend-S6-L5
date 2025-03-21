using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProgettoBackend_S6_L5.Services;
using ProgettoBackend_S6_L5.ViewModels;

namespace ProgettoBackend_S6_L5.Controllers
{
    public class ClientiController : Controller
    {
        private readonly ClientiService _clientiService;

        public ClientiController(ClientiService clientiService)
        {
            _clientiService = clientiService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("clienti/get-all")]
        public async Task<IActionResult> ListaClienti()
        {
            var clientiList = await _clientiService.GetAllClientsAsync();

            return PartialView("_ClientiList", clientiList);
        }

        [Authorize(Roles = "Admin,DipendenteAdmin")]
        [Route("Client/Add")]
        public IActionResult Add()
        {
            return PartialView("_AddClientForm");
        }

        [HttpPost("Client/Add")]
        public async Task<IActionResult> Add(AddClientViewModel addClientViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new
                {
                    success = false,
                    message = "Error while saving entity to database"
                });
            }

            var result = await _clientiService.AddClientAsync(addClientViewModel);

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
        [Route("Client/Edit/{id:guid}")]
        public async Task<IActionResult> Edit(Guid id)
        {
            var cliente = await _clientiService.GetClientByIdAsync(id);

            if (cliente == null)
            {
                return RedirectToAction("Index");
            };

            var editClientViewModel = new EditClientViewModel()
            {
                Id = cliente.ClienteId,
                Nome = cliente.Nome,
                Cognome = cliente.Cognome,
                Email = cliente.Email,
                Telefono = cliente.Telefono
            };

            return PartialView("_EditClientForm", editClientViewModel);
        }

        [HttpPost("client/edit/save")]
        public async Task<IActionResult> Edit(EditClientViewModel editClientViewModel)
        {
            var result = await _clientiService.EditClientAsync(editClientViewModel);

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

        [HttpPost("client/delete/{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var result = await _clientiService.DeleteClientByIdAsync(id);

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
