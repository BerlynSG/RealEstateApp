using Microsoft.AspNetCore.Mvc;
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

        public IActionResult Index(int id)
        {
            ListaPropiedadViewModel vm = new ListaPropiedadViewModel();
            vm.Mantenimiento = id != 0;
            var prop = propiedades.ToList();
            if (tipoUsuario == 2 || vm.Mantenimiento)
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
                if (tipoUsuario == 2 || vm.Mantenimiento)
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

        [HttpPost]
        public IActionResult Eliminar(ListaPropiedadViewModel vm)
        {
            Debug.WriteLine("Código de la propiedad a eliminar: " + vm.Codigo);
            return RedirectToRoute(new { controller = "Propiedad", action = "Index", id="1" });
        }

       
        public IActionResult CrearPropiedad()
        {
            if (tiposPropiedad.Count == 0 || tiposVenta.Count == 0 || mejoras.Count == 0)
            {
                TempData["ErrorMessage"] = "No se pueden crear propiedades porque no existen tipos de propiedad, tipos de venta o mejoras.";
                return RedirectToAction("Index", new { id = 1 });
            }

            SavePropiedadViewModel vm = new SavePropiedadViewModel
            {
                TipoPropiedad = tiposPropiedad.ToList(),
                TipoVenta = tiposVenta.ToList(),
                Mejoras = mejoras.ToList()
            };
            ViewData["editMode"] = false;

            return View("GuardarPropiedad", vm);
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
                    TipoPropiedad = vm.TipoPropiedad.Select(tp => new TipoPropiedadViewModel { Nombre = tp.Nombre }).ToList(),
                    TipoVenta = vm.TipoVenta.Select(tv => new TipoVentaViewModel { Nombre = tv.Nombre }).ToList(),
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

            vm.TipoPropiedad = tiposPropiedad.ToList();
            vm.TipoVenta = tiposVenta.ToList();
            vm.Mejoras = mejoras.ToList();
            return View("GuardarPropiedad", vm);
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
                TipoPropiedad = propiedad.TipoPropiedad,
                TipoVenta = propiedad.TipoVenta,
                Valor = propiedad.Valor,
                Baños = propiedad.Baños,
                Habitaciones = propiedad.Habitaciones,
                Tamaño = propiedad.Tamaño,
                Descripcion = propiedad.Descripcion,
                Mejoras = propiedad.Mejoras,
                Imagenes = propiedad.Imagenes,

            };

            return View("GuardarPropiedad", vm);
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

                propiedad.TipoPropiedad = tiposPropiedad;
                propiedad.TipoVenta = tiposVenta;
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

            vm.TipoPropiedad = tiposPropiedad.ToList();
            vm.TipoVenta = tiposVenta.ToList();
            vm.Mejoras = mejoras.ToList();
            return View("GuardarPropiedad", vm);
        }

        private string GenerarCodigoUnico()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}

  