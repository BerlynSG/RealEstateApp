using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;

namespace RealEstateApp.Controllers
{
    public class TipoVentaController : Controller
    {
        private readonly ITipoVentaService _tipoVentaService;

        public TipoVentaController(ITipoVentaService tipoVentaService)
        {
            _tipoVentaService = tipoVentaService;
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Index(int messageType, string message)
        {
            ListaTipoVentaViewModel vm = new();
            vm.TiposVenta = await _tipoVentaService.GetAllViewModel();
            vm.Message = message;
            vm.MessageType = messageType;
            return View(vm);
        }

        [Authorize(Roles = "Administrador")]
        public ActionResult Crear()
        {
            SaveTipoVentaViewModel vm = new();
            return View(vm);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(SaveTipoVentaViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _tipoVentaService.Add(vm);
                TempData["SuccessMessage"] = "El tipo de venta se creó correctamente.";

                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha creado el tipo de Venta correctamente."
                });
            }
            return View(vm);
        }

        [Authorize(Roles = "Administrador")]
        public async Task<ActionResult> Editar(int id)
        {
            SaveTipoVentaViewModel vm = await _tipoVentaService.GetByIdSaveViewModel(id);
            if (vm == null)
            {
                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha eliminado el tipo de Venta correctamente."
                });
            }

            return View("Crear", vm);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(SaveTipoVentaViewModel vm)
        {
            if (ModelState.IsValid)
            {
                SaveTipoVentaViewModel tipo = await _tipoVentaService.GetByIdSaveViewModel(vm.Id);

                if (tipo == null)
                {
                    return RedirectToAction("Index");
                }

                await _tipoVentaService.Update(vm, vm.Id);

                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha editado el tipo de Venta correctamente."
                });
            }

            return View("Crear", vm);
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(ListaTipoVentaViewModel vm)
        {
            await _tipoVentaService.Delete(vm.IdDelete);
            return RedirectToAction("Index", new { messageType = 1,
                message = "Se ha eliminado el tipo de Venta correctamente."
            });
        }
    }
}
