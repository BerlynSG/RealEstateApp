using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Controllers
{
    public class TipoPropiedadController : Controller
    {
        private readonly ITipoPropiedadService _tipoPropiedadService;

        public TipoPropiedadController(ITipoPropiedadService tipoPropiedadService)
        {
            _tipoPropiedadService = tipoPropiedadService;
        }

        // GET: TipoPropiedadController
        public async Task<ActionResult> Index(int messageType, string message)
        {
            ListaTipoPropiedadViewModel vm = new();
            vm.TiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.Message = message;
            vm.MessageType = messageType;
            return View(vm);
        }

        // GET: TipoPropiedadController/Create
        public ActionResult Crear()
        {
            SaveTipoPropiedadViewModel vm = new();
            return View(vm);
        }

        // POST: TipoPropiedadController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Crear(SaveTipoPropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _tipoPropiedadService.Add(vm);
                TempData["SuccessMessage"] = "La propiedad se creó correctamente.";

                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha creado el tipo de propiedad correctamente."
                });
            }
            return View(vm);
        }

        // GET: TipoPropiedadController/Edit/5
        public async Task<ActionResult> Editar(int id)
        {
            SaveTipoPropiedadViewModel vm = await _tipoPropiedadService.GetByIdSaveViewModel(id);
            if (vm == null)
            {
                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha eliminado el tipo de propiedad correctamente."
                });
            }

            return View("Crear", vm);
        }

        // POST: TipoPropiedadController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Editar(SaveTipoPropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                SaveTipoPropiedadViewModel tipo = await _tipoPropiedadService.GetByIdSaveViewModel(vm.Id);

                if (vm == null)
                {
                    return RedirectToAction("Index");
                }

                await _tipoPropiedadService.Update(vm, vm.Id);

                return RedirectToAction("Index", new {
                    messageType = 1,
                    message = "Se ha editado el tipo de propiedad correctamente."
                });
            }

            return View("Crear", vm);
        }

        // POST: TipoPropiedadController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Eliminar(ListaTipoPropiedadViewModel vm)
        {
            await _tipoPropiedadService.Delete(vm.IdDelete);
            return RedirectToAction("Index", new { messageType = 1,
                message = "Se ha eliminado el tipo de propiedad correctamente."
            });
        }
    }
}
