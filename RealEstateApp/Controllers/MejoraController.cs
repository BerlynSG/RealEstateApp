using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Mejora;

namespace RealEstateApp.Controllers
{
    public class MejoraController : Controller
    {
        private readonly IMejoraService _mejoraService;

        public MejoraController(IMejoraService mejoraService)
        {
            _mejoraService = mejoraService;
        }

        public async Task<ActionResult> Index(int messageType, string message)
        {
            ListaMejoraViewModel vm = new();
            vm.Mejoras = await _mejoraService.GetAllViewModel();
            vm.Message = message;
            vm.MessageType = messageType;
            return View(vm);
        }

        public ActionResult Crear()
        {
            SaveMejoraViewModel vm = new();
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(SaveMejoraViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _mejoraService.Add(vm);
                TempData["SuccessMessage"] = "La propiedad se creó correctamente.";

                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha creado el tipo de Venta correctamente."
                });
            }
            return View(vm);
        }

        public async Task<ActionResult> Editar(int id)
        {
            SaveMejoraViewModel vm = await _mejoraService.GetByIdSaveViewModel(id);
            if (vm == null)
            {
                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha eliminado el tipo de Venta correctamente."
                });
            }

            return View("Crear", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(SaveMejoraViewModel vm)
        {
            if (ModelState.IsValid)
            {
                SaveMejoraViewModel tipo = await _mejoraService.GetByIdSaveViewModel(vm.Id);

                if (tipo == null)
                {
                    return RedirectToAction("Index");
                }

                await _mejoraService.Update(vm, vm.Id);

                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha editado el tipo de Venta correctamente."
                });
            }

            return View("Crear", vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(ListaMejoraViewModel vm)
        {
            await _mejoraService.Delete(vm.IdDelete);
            return RedirectToAction("Index", new { messageType = 1,
                message = "Se ha eliminado el tipo de Venta correctamente."
            });
        }
    }
}
