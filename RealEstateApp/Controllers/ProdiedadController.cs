using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;

namespace RealEstateApp.Controllers
{
    public class PropiedadController : Controller
    {
        private readonly IPropiedadService _propiedadService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _tipoVentaService;
        private readonly IMejoraService _mejoraService;
        //private List<PropiedadViewModel> propiedades;
        private List<AgenteViewModel> agentes;
        //private List<MejoraViewModel> mejoras;
        //private List<TipoPropiedadViewModel> tiposPropiedad;
        //private List<TipoVentaViewModel> tiposVenta;
        private int tipoUsuario = 0;
        private int idAgente = 0;
        //los tipos de propiedad y venta serán tablas y no enums
        public PropiedadController(IPropiedadService propiedadRepository, ITipoPropiedadService tipoPropiedadService,
            ITipoVentaService tipoVentaService, IMejoraService mejoraService)
        {
            _propiedadService = propiedadRepository;
            _tipoPropiedadService = tipoPropiedadService;
            _tipoVentaService = tipoVentaService;
            _mejoraService = mejoraService;
            agentes = new List<AgenteViewModel>()
            {
                new()
                    {
                        Id = 0,
                        Nombre = "José Antonio",
                        Apellidos = "Fernandez Ramirez",
                        Foto = "/img/Agentes/Agente.jpeg",
                        Celular = "829 254 3687",
                        Correo = "JoséFernandez@email.com"
                    },
                new()
                    {
                        Id = 1,
                        Nombre = "Pedro",
                        Apellidos = "De La Cruz",
                        Foto = "/img/Agentes/Agente.jpeg",
                        Celular = "829 254 3687",
                        Correo = "JoséFernandez@email.com"
                    }
            };
        }

        public async Task<IActionResult> Index(int id, int messageType, string message)
        {
            ListaPropiedadViewModel vm = new ListaPropiedadViewModel();
            vm.Filtros = new FiltroPropiedadViewModel();
            vm.Filtros.TipoFiltroUsuario = tipoUsuario * 2 + id;
            vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
            vm.tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.MessageType = messageType;
            vm.Message = message;
            if (vm.Filtros.TipoFiltroUsuario == 1) vm.Filtros.TipoFiltroUsuario = 5;

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ListaPropiedadViewModel vm)
        {
            if (vm != null)
            {
                int tt = vm.Filtros.TipoFiltroUsuario;
                vm.Filtros.TipoFiltroUsuario = 0;
                vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
                vm.Filtros.TipoFiltroUsuario = tt;
                vm.tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();

                return View(vm);
            }
            return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
        }

        public async Task<IActionResult> Detalles(string codigo)
        {
            PropiedadViewModel vm = await _propiedadService.GetByCodigoViewModel(codigo);
            if (vm == null)
            {
                return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
            }
            return View(vm);
        }

        public async Task<IActionResult> CrearPropiedad()
        {
            List<TipoPropiedadViewModel> tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            List<MejoraViewModel> mejoras = await _mejoraService.GetAllViewModel();
            List<TipoVentaViewModel> tiposVenta = await _tipoVentaService.GetAllViewModel();
            if (tiposPropiedad.Count == 0 || tiposVenta.Count == 0 || mejoras.Count == 0)
            {
                TempData["ErrorMessage"] = "No se pueden crear propiedades porque no existen tipos de propiedad, tipos de venta o mejoras.";
                return RedirectToAction("Index", new { id = 1,
                    messageType = 2,
                    message = "No se puede crear una propiedad porque no hay mejoras, tipos de propiedad y de venta."
                });
            }

            SavePropiedadViewModel vm = new SavePropiedadViewModel
            {
                ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel(),
                ListaTipoVenta = await _tipoVentaService.GetAllViewModel(),
                ListaMejora = await _mejoraService.GetAllViewModel()
            };
            ViewData["editMode"] = false;

            return View("Crear", vm);
        }

        [HttpPost]
        public async Task<IActionResult> CrearPropiedad(SavePropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                await _propiedadService.Add(vm);

                TempData["SuccessMessage"] = "La propiedad se creó correctamente.";
                return RedirectToAction("Index", new { id = 1,
                    messageType = 1,
                    message = "Se ha creado la propiedad correctamente."
                });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();
            return View("Crear", vm);
        }

        public async Task<IActionResult> EditarPropiedad(string codigo)
        {
            SavePropiedadViewModel vm = await _propiedadService.GetByCodigoSaveViewModel(codigo);

            if (vm == null)
            {
                return RedirectToAction("Index", new { id = 1,
                    messageType = 2,
                    message = "No se ha encontrado la propiedad para editarla."
                });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();

            return View("Crear", vm);
        }

        [HttpPost]
        public async Task<IActionResult> EditarPropiedad(SavePropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                PropiedadViewModel propiedad = await _propiedadService.GetByCodigoViewModel(vm.Codigo);

                if (propiedad == null)
                {
                    return RedirectToAction("Index", new { id = 1,
                        messageType = 2,
                        message = "No se ha encontrado la propiedad para editarla."
                    });
                }

                await _propiedadService.Update(vm, propiedad.Id);

                TempData["SuccessMessage"] = "La propiedad se editó correctamente.";
                return RedirectToAction("Index", new { id = 1,
                    messageType = 1,
                    message = "Se ha editado la propiedad correctamente."
                });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();
            return View("Crear", vm);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(ListaPropiedadViewModel vm)
        {
            await _propiedadService.Delete(vm.EliminarId);
            vm.MessageType = 1;
            vm.Message = "Se ha eliminado la propiedad correctamente.";
            int tt = vm.Filtros.TipoFiltroUsuario;
            vm.Filtros.TipoFiltroUsuario = 0;
            vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
            vm.Filtros.TipoFiltroUsuario = tt;
            vm.tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            return View("Index", vm);
        }
    }
}
