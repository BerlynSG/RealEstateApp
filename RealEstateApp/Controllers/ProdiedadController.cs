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
        private List<PropiedadViewModel> propiedades;
        private List<AgenteViewModel> agentes;
        private List<MejoraViewModel> mejoras;
        private List<TipoPropiedadViewModel> tiposPropiedad;
        private List<TipoVentaViewModel> tiposVenta;
        private int tipoUsuario = 2;
        private int idAgente = 0;
        //los tipos de propiedad y venta serán tablas y no enums
        public PropiedadController(IPropiedadService propiedadRepository)
        {
            _propiedadService = propiedadRepository;
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
            mejoras = new List<MejoraViewModel>()
            {
                new(), new(){ Nombre = "Balcon" }, new(){ Nombre = "Sala/Comedor" }, new(){ Nombre = "Cocina" }, new(){ Nombre = "Piscina" }
            };
            tiposPropiedad = new List<TipoPropiedadViewModel>()
            {
                new(), new(){ Id = 1, Nombre = "Apartamento" }, new(){ Id = 2, Nombre = "Casa" }
            };
            tiposVenta = new List<TipoVentaViewModel>()
            {
                new(), new(){ Id = 1, Nombre = "Alquiler" }, new(){ Id = 2, Nombre = "Venta" }
            };
            propiedades = new List<PropiedadViewModel>()
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
            };
        }

        public async Task<IActionResult> Index(int id)
        {
            ListaPropiedadViewModel vm = new ListaPropiedadViewModel();
            vm.Filtros = new FiltroPropiedadViewModel();
            vm.Filtros.TipoFiltroUsuario = tipoUsuario * 2 + id;
            vm.propiedades = GetListaPropiedades(vm.Filtros.TipoFiltroUsuario);
            vm.tiposPropiedad = tiposPropiedad.ToList();

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ListaPropiedadViewModel vm)
        {
            if (vm != null)
            {
                List<PropiedadViewModel> prop = GetListaPropiedades(vm.Filtros.TipoFiltroUsuario);

                vm.propiedades = prop.Where(p => (tiposPropiedad[vm.Filtros.TipoPropiedad] == p.TipoPropiedad || vm.Filtros.TipoPropiedad == 0)
                && (vm.Filtros.Habitaciones == p.Habitaciones || vm.Filtros.Habitaciones == 0) && (vm.Filtros.Baños == p.Baños || vm.Filtros.Baños == 0)
                && (vm.Filtros.PrecioMinimo <= p.Valor || vm.Filtros.PrecioMinimo == 0) && (vm.Filtros.PrecioMaximo >= p.Valor || vm.Filtros.PrecioMaximo == 0)
                && (vm.Filtros.Codigo == null || vm.Filtros.Codigo == "" || p.Codigo.Contains(vm.Filtros.Codigo))).ToList();
                vm.tiposPropiedad = tiposPropiedad.ToList();

                return View(vm);
            }
            return RedirectToRoute(new { controller = "Propiedad", action = "Index" });
        }

        public IActionResult Detalles(string codigo)
        {
            PropiedadViewModel vm = propiedades.FirstOrDefault(p => p.Codigo == codigo);
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

       
        public IActionResult CrearPropiedad()
        {
            if (tiposPropiedad.Count == 0 || tiposVenta.Count == 0 || mejoras.Count == 0)
            {
                TempData["ErrorMessage"] = "No se pueden crear propiedades porque no existen tipos de propiedad, tipos de venta o mejoras.";
                return RedirectToAction("Index", new { id = 1 });
            }

            tiposPropiedad.ToList();

            SavePropiedadViewModel vm = new SavePropiedadViewModel
            {
                ListaTipoPropiedad = tiposPropiedad.ToList(),
                ListaTipoVenta = tiposVenta.ToList(),
                ListaMejora = mejoras.ToList()
            };
            ViewData["editMode"] = false;

            return View("Crear", vm);
        }

        [HttpPost]
        public IActionResult CrearPropiedad(SavePropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                string codigo = GenerarCodigoUnico();

                PropiedadViewModel nuevaPropiedad = new PropiedadViewModel
                {
                    Codigo = codigo,
                    TipoPropiedad = tiposPropiedad.Where(v => v.Id == vm.TipoPropiedad).First(),
                    TipoVenta = tiposVenta.Where(v => v.Id == vm.TipoVenta).First(),
                    Baños = vm.Baños,
                    Habitaciones = vm.Habitaciones,
                    Tamaño = vm.Tamaño,
                    Descripcion = vm.Descripcion,
                    Agente = agentes.FirstOrDefault(a => a.Id == idAgente),
                    Mejoras = vm.Mejoras.Select(m => new MejoraViewModel { Nombre = m.Nombre }).ToList(),
                    Imagenes = vm.Imagenes
                };


                propiedades.Add(nuevaPropiedad);

                TempData["SuccessMessage"] = "La propiedad se creó correctamente.";
                return RedirectToAction("Index", new { id = 1 });
            }

            vm.ListaTipoPropiedad = tiposPropiedad.ToList();
            vm.ListaTipoVenta = tiposVenta.ToList();
            vm.ListaMejora = mejoras.ToList();
            return View("Crear", vm);
        }

        public IActionResult EditarPropiedad(string codigo)
        {
            PropiedadViewModel propiedad = propiedades.FirstOrDefault(p => p.Codigo == codigo);

            if (propiedad == null)
            {
                return RedirectToAction("Index", new { id = 1 });
            }

            SavePropiedadViewModel vm = new SavePropiedadViewModel
            {
                Codigo = propiedad.Codigo,
                TipoPropiedad = propiedad.TipoPropiedad.Id,
                TipoVenta = propiedad.TipoVenta.Id,
                Valor = propiedad.Valor,
                Baños = propiedad.Baños,
                Habitaciones = propiedad.Habitaciones,
                Tamaño = propiedad.Tamaño,
                Descripcion = propiedad.Descripcion,
                Mejoras = propiedad.Mejoras,
                Imagenes = propiedad.Imagenes,

            };

            vm.ListaTipoPropiedad = tiposPropiedad.ToList();
            vm.ListaTipoVenta = tiposVenta.ToList();
            vm.ListaMejora = mejoras.ToList();

            return View("Crear", vm);
        }

        [HttpPost]
        public IActionResult EditarPropiedad(SavePropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                PropiedadViewModel propiedad = propiedades.FirstOrDefault(p => p.Codigo == vm.Codigo);

                if (propiedad == null)
                {
                    return RedirectToAction("Index", new { id = 1 });
                }

                propiedad.TipoPropiedad = tiposPropiedad.Where(v => v.Id == vm.TipoPropiedad).First();
                propiedad.TipoVenta = tiposVenta.Where(v => v.Id == vm.TipoVenta).First();
                propiedad.Valor = vm.Valor;
                propiedad.Baños = vm.Baños;
                propiedad.Habitaciones = vm.Habitaciones;
                propiedad.Tamaño = vm.Tamaño;
                propiedad.Descripcion = vm.Descripcion;
                propiedad.Mejoras = vm.Mejoras;
                propiedad.Imagenes = vm.Imagenes;

                TempData["SuccessMessage"] = "La propiedad se editó correctamente.";
                return RedirectToAction("Index", new { id = 1 });
            }

            vm.ListaTipoPropiedad = tiposPropiedad.ToList();
            vm.ListaTipoVenta = tiposVenta.ToList();
            vm.ListaMejora = mejoras.ToList();
            return View("Crear", vm);
        }

        private string GenerarCodigoUnico()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        private List<PropiedadViewModel> GetListaPropiedades(int TipoFiltroUsuario)
        {
            List<PropiedadViewModel> prop = propiedades.ToList();
            //prop = await _propiedadService.GetAllViewModel();
            if (TipoFiltroUsuario == 4 || TipoFiltroUsuario == 5)
            {
                prop = prop.Where(p => p.Agente.Id == idAgente).ToList();
            }
            else if (TipoFiltroUsuario == 2)
            {
                //lista de propiedades favoritas
            }
            return prop;
        }
    }
}

  