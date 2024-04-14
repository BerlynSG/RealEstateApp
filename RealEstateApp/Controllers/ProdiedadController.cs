using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using System.Diagnostics;

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
        private int tipoUsuario = 2;
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
            /*mejoras = new List<MejoraViewModel>()
            {
                new(), new(){ Id = 1, Nombre = "Balcon" }, new(){ Id = 2, Nombre = "Sala/Comedor" }, new(){ Id = 3, Nombre = "Cocina" }, new(){ Id = 4,Nombre = "Piscina" }
            };
            tiposPropiedad = new List<TipoPropiedadViewModel>()
            {
                new(), new(){ Id = 1, Nombre = "Apartamento" }, new(){ Id = 2, Nombre = "Casa" }
            };
            tiposVenta = new List<TipoVentaViewModel>()
            {
                new(), new(){ Id = 1, Nombre = "Alquiler" }, new(){ Id = 2, Nombre = "Venta" }
            };*/
            /*propiedades = new List<PropiedadViewModel>()
            {
                new()
                {
                    Codigo = "153843",
                    TipoPropiedad = tiposPropiedad[1],
                    Imagenes = new() { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" },
                    TipoVenta = tiposVenta[1],
                    Valor = 59.99,
                    Baños = 0,
                    Habitaciones = 2,
                    Tamaño = 50,
                    Descripcion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" +
                    " incididunt ut labore et dolore magna aliqua.",
                    Agente = agentes[1],
                    Mejoras = new() { mejoras[1], mejoras[3] }
                },
                new()
                {
                    Codigo = "157832",
                    TipoPropiedad = tiposPropiedad[2],
                    Imagenes = new() { "/img/Propiedades/Apartamento.jpg" },
                    TipoVenta = tiposVenta[1],
                    Valor = 129.99,
                    Baños = 3,
                    Habitaciones = 4,
                    Tamaño = 100,
                    Descripcion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" +
                    " incididunt ut labore et dolore magna aliqua.",
                    Agente = agentes[0],
                    Mejoras = new() { mejoras[2], mejoras[3], mejoras[4] }
                },
                new()
                {
                    Codigo = "953782",
                    TipoPropiedad = tiposPropiedad[1],
                    Imagenes = new() { "/img/Propiedades/Apartamento.jpg" },
                    TipoVenta = tiposVenta[2],
                    Valor = 33.99,
                    Baños = 1,
                    Habitaciones = 1,
                    Tamaño = 45,
                    Descripcion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" +
                    " incididunt ut labore et dolore magna aliqua.",
                    Agente = agentes[0],
                    Mejoras = new() { mejoras[1], mejoras[4] }
                },
                new()
                {
                    Codigo = "775262",
                    TipoPropiedad = tiposPropiedad[2],
                    Imagenes = new() { "/img/Propiedades/Apartamento.jpg" },
                    TipoVenta = tiposVenta[2],
                    Valor = 89.99,
                    Baños = 2,
                    Habitaciones = 2,
                    Tamaño = 60,
                    Descripcion = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor" +
                    " incididunt ut labore et dolore magna aliqua.",
                    Agente = agentes[1],
                    Mejoras = new() { mejoras[1], mejoras[2], mejoras[3], mejoras[4] }
                }
            };*/
        }

        public async Task<IActionResult> Index(int id)
        {
            ListaPropiedadViewModel vm = new ListaPropiedadViewModel();
            vm.Filtros = new FiltroPropiedadViewModel();
            vm.Filtros.TipoFiltroUsuario = tipoUsuario * 2 + id;
            vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
            vm.tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ListaPropiedadViewModel vm)
        {
            if (vm != null)
            {
                vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
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

        [HttpPost]
        public IActionResult Eliminar(ListaPropiedadViewModel vm)
        {
            Debug.WriteLine("Código de la propiedad a eliminar: " + vm.Filtros.Codigo);
            return RedirectToRoute(new { controller = "Propiedad", action = "Index", id="1" });
        }

       
        public async Task<IActionResult> CrearPropiedad()
        {
            List<TipoPropiedadViewModel> tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            List<MejoraViewModel> mejoras = await _mejoraService.GetAllViewModel();
            List<TipoVentaViewModel> tiposVenta = await _tipoVentaService.GetAllViewModel();
            if (tiposPropiedad.Count == 0 || tiposVenta.Count == 0 || mejoras.Count == 0)
            {
                TempData["ErrorMessage"] = "No se pueden crear propiedades porque no existen tipos de propiedad, tipos de venta o mejoras.";
                return RedirectToAction("Index", new { id = 1 });
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
                return RedirectToAction("Index", new { id = 1 });
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
                return RedirectToAction("Index", new { id = 1 });
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
                    return RedirectToAction("Index", new { id = 1 });
                }

                await _propiedadService.Update(vm, propiedad.Id);

                /*propiedad.TipoPropiedad = tiposPropiedad.Where(v => v.Id == vm.TipoPropiedadId).First();
                propiedad.TipoVenta = tiposVenta.Where(v => v.Id == vm.TipoVentaId).First();
                propiedad.Valor = vm.Valor;
                propiedad.Baños = vm.Baños;
                propiedad.Habitaciones = vm.Habitaciones;
                propiedad.Tamaño = vm.Tamaño;
                propiedad.Descripcion = vm.Descripcion;
                propiedad.Mejoras = vm.Mejoras.Split(",").Select(ms => mejoras.FirstOrDefault(m => m.Id == int.Parse(ms))).ToList();
                propiedad.Imagenes = vm.Imagenes;*/

                TempData["SuccessMessage"] = "La propiedad se editó correctamente.";
                return RedirectToAction("Index", new { id = 1 });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();
            return View("Crear", vm);
        }
    }
}

  