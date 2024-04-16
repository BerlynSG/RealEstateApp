using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using RealEstateApp.Core.Application.ViewModels.User;
using System.Security.Claims;

namespace RealEstateApp.Controllers
{
    public class AgenteController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private List<TipoPropiedadViewModel> tiposPropiedad = new()
        {
            new(), new(){ Nombre = "Apartamento" }, new(){ Nombre = "Casa" }, new(){ Nombre = "Terreno" }
        };
        private List<TipoVentaViewModel> tiposVenta = new()
        {
            new (), new (){ Nombre = "Alquiler" }, new (){ Nombre = "Venta" }
        };
        private static List<AgenteViewModel> _agentes;

        public AgenteController(IUserService userService, IMapper mapper)
        {
            _mapper = mapper;
            _userService = userService;
            _agentes = new List<AgenteViewModel>
            {
                new AgenteViewModel { Id = 1, Nombre = "Juan", Apellidos = "Perez", Foto = "/img/Agentes/Agente.jpeg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "001", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 150000, Habitaciones = 3, Baños = 2, Tamaño = 200, Descripcion = "Bonita casa en zona residencial", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } },
                        new PropiedadViewModel { Codigo = "002", TipoPropiedad = tiposPropiedad[1], TipoVenta = tiposVenta[1], Valor = 1000, Habitaciones = 2, Baños = 1, Tamaño = 100, Descripcion = "Acogedor apartamento amueblado", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } }
                    }
                },
                new AgenteViewModel { Id = 2, Nombre = "Maria", Apellidos = "Gonzalez", Foto = "/img/Agentes/Agente.jpeg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "003", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 200000, Habitaciones = 4, Baños = 3, Tamaño = 250, Descripcion = "Amplia casa con jardín", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } }
                    }
                },
                new AgenteViewModel { Id = 3, Nombre = "Carlos", Apellidos = "Lopez", Foto = "/img/Agentes/Agente.jpeg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "004", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 80000, Habitaciones = 1, Baños = 1, Tamaño = 80, Descripcion = "Moderno apartamento con vista al mar", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } },
                        new PropiedadViewModel { Codigo = "005", TipoPropiedad = tiposPropiedad[3], TipoVenta = tiposVenta[2], Valor = 50000, Tamaño = 500, Descripcion = "Terreno en urbanización cerrada", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg" } }
                    }
                }
            };
        }

        public IActionResult Index(string searchTerm)
        {
            List<AgenteViewModel> agentes = _agentes;

            if (!string.IsNullOrEmpty(searchTerm))
            {
                agentes = agentes
                    .Where(a => a.Nombre.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase) || a.Apellidos.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            agentes = agentes.OrderBy(a => a.Nombre).ToList();

            return View(new ListaAgenteViewModel { Agentes = agentes, SearchTerm = searchTerm });
        }

        public IActionResult Detalles(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agente = _agentes.FirstOrDefault(a => a.Id == id);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente);
        }

        public IActionResult Propiedades(int? codigoAgente)
        {
            if (codigoAgente == null)
            {
                return NotFound();
            }

            var agente = _agentes.FirstOrDefault(a => a.Id == codigoAgente);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente.Propiedades);
        }
        public async Task<IActionResult> MiPerfil()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var agente = await _userService.GetUserById(userId);

            if (agente == null)
            {
                return NotFound();
            }

            var vm = _mapper.Map<AgenteViewModel>(agente);

            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> MiPerfil(AgenteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var agente = _mapper.Map<AgenteViewModel>(vm);

            await _userService.UpdateUserAsync( vm,  vm.Id.ToString());

            return RedirectToAction("Index", "Agente");
        }

    }
}
