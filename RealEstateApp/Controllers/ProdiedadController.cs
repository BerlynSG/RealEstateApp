using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;

namespace RealEstateApp.Controllers
{
    public class PropiedadController : Controller
    {
        private List<PropiedadViewModel> propiedades;
        private List<AgenteViewModel> agentes;
        private List<MejoraViewModel> mejoras;
        private List<TipoPropiedadViewModel> tiposPropiedad;
        private List<TipoVentaViewModel> tiposVenta;
        private int tipoUsuario = 0;
        private int idAgente = 0;
        //los tipos de propiedad y venta serán tablas y no enums
        public PropiedadController()
        {
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
                new(), new(){ Nombre = "Apartamento" }, new(){ Nombre = "Casa" }
            };
            tiposVenta = new List<TipoVentaViewModel>()
            {
                new(), new(){ Nombre = "Alquiler" }, new(){ Nombre = "Venta" }
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

        public IActionResult Index()
        {
            ListaPropiedadViewModel vm = new ListaPropiedadViewModel();
            var prop = propiedades.ToList();
            if (tipoUsuario == 2)
            {
                prop = prop.Where(p => p.Agente.Id == idAgente).ToList();
            }
            vm.propiedades = prop;
            vm.tiposPropiedad = tiposPropiedad.ToList();
            vm.Cliente = tipoUsuario == 1;

            return View(vm);
        }

        [HttpPost]
        public IActionResult Index(ListaPropiedadViewModel vm)
        {
            if (vm != null)
            {
                var prop = propiedades.ToList();
                if (tipoUsuario == 2)
                {
                    prop = prop.Where(p => p.Agente.Id == idAgente).ToList();
                }
                vm.propiedades = prop.Where(p => (tiposPropiedad[vm.TipoPropiedad] == p.TipoPropiedad || vm.TipoPropiedad == 0)
                && (vm.Habitaciones == p.Habitaciones || vm.Habitaciones == 0) && (vm.Baños == p.Baños || vm.Baños == 0)
                && (vm.PrecioMinimo <= p.Valor || vm.PrecioMinimo == 0) && (vm.PrecioMaximo >= p.Valor || vm.PrecioMaximo == 0)
                && (vm.Codigo == null || vm.Codigo == "" || p.Codigo.Contains(vm.Codigo))).ToList();
                vm.tiposPropiedad = tiposPropiedad.ToList();
                vm.Cliente = tipoUsuario == 1;
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
    }
}
