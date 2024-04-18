using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using RealEstateApp.Core.Application.Enums;

namespace RealEstateApp.Controllers
{
    
    public class AgenteController : Controller
    {        
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IPropiedadService _propiedadService;
        private readonly ITipoPropiedadService _tipoPropiedadService;

        /*private List<TipoPropiedadViewModel> tiposPropiedad = new()
        {
            new(), new(){ Nombre = "Apartamento" }, new(){ Nombre = "Casa" }, new(){ Nombre = "Terreno" }
        };
        private List<TipoVentaViewModel> tiposVenta = new()
        {
            new (), new (){ Nombre = "Alquiler" }, new (){ Nombre = "Venta" }
        };
        private static List<AgenteViewModel> _agentes;*/

        public AgenteController(IUserService userService, IMapper mapper, IPropiedadService propiedadService, ITipoPropiedadService tipoPropiedadService)
        {
            _mapper = mapper;
            _userService = userService;
            _propiedadService = propiedadService;
            _tipoPropiedadService = tipoPropiedadService;
            /*_agentes = new List<AgenteViewModel>
            {
                new AgenteViewModel { Id = "1", Nombre = "Juan", Apellidos = "Perez", Foto = "/img/Agentes/Agente.jpeg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "001", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 150000, Habitaciones = 3, Baños = 2, Tamaño = 200, Descripcion = "Bonita casa en zona residencial", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } },
                        new PropiedadViewModel { Codigo = "002", TipoPropiedad = tiposPropiedad[1], TipoVenta = tiposVenta[1], Valor = 1000, Habitaciones = 2, Baños = 1, Tamaño = 100, Descripcion = "Acogedor apartamento amueblado", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } }
                    }
                },
                new AgenteViewModel { Id = "2", Nombre = "Maria", Apellidos = "Gonzalez", Foto = "/img/Agentes/Agente.jpeg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "003", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 200000, Habitaciones = 4, Baños = 3, Tamaño = 250, Descripcion = "Amplia casa con jardín", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } }
                    }
                },
                new AgenteViewModel { Id = "3", Nombre = "Carlos", Apellidos = "Lopez", Foto = "/img/Agentes/Agente.jpeg", Propiedades = new List<PropiedadViewModel>
                    {
                        new PropiedadViewModel { Codigo = "004", TipoPropiedad = tiposPropiedad[2], TipoVenta = tiposVenta[2], Valor = 80000, Habitaciones = 1, Baños = 1, Tamaño = 80, Descripcion = "Moderno apartamento con vista al mar", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg", "/img/Propiedades/Apartamento.jpg" } },
                        new PropiedadViewModel { Codigo = "005", TipoPropiedad = tiposPropiedad[3], TipoVenta = tiposVenta[2], Valor = 50000, Tamaño = 500, Descripcion = "Terreno en urbanización cerrada", Imagenes = new List<string> { "/img/Propiedades/Apartamento.jpg" } }
                    }
                }
            };*/
        }

        /*public IActionResult Indexx(string searchTerm)
        {
            var users = await _userService.GetAllUsers();
            users = users.Where(u => u.Roles.Contains(Roles.Agente.ToString())).ToList();

            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users
                    .Where(a => a.FirstName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)
                    || a.LastName.Contains(searchTerm, System.StringComparison.OrdinalIgnoreCase)).ToList();
            }

            List<AgenteViewModel> agentes = _mapper.Map<List<AgenteViewModel>>(users);

            agentes = agentes.OrderBy(a => a.Nombre).ToList();

            return View(new ListaAgenteViewModel { Agentes = agentes, SearchTerm = searchTerm });
        }*/

        public async Task<IActionResult> Index(string searchTerm)
        {
            // Obtén todos los usuarios del servicio _userService.
            var users = await _userService.GetAllUsers();

            // Filtrar por searchTerm si existe
            if (!string.IsNullOrEmpty(searchTerm))
            {
                users = users.Where(u =>
                    u.FirstName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                    u.LastName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            // Devuelve la vista correspondiente con la lista filtrada y el término de búsqueda
            return View(users);
        }


        public async Task <IActionResult> Detalles(string? agenteId)
        {
            if (agenteId == null || agenteId == "")
            {
                return NotFound();
            }
            var agente = new DetallesAgenteViewModel();
            agente.Agente = _mapper.Map<AgenteViewModel>(await _userService.GetUserById(agenteId));
            agente.Filtros = new FiltroPropiedadViewModel()
            {
                TipoFiltroUsuario = 2,
                UsuarioId = agenteId
            };
            agente.Agente.Propiedades = await _propiedadService.GetAllFilteredViewModel(agente.Filtros);
            agente.TiposPropiedad = await _tipoPropiedadService.GetAllViewModel();

            if (agente == null)
            {
                return NotFound();
            }

            return View(agente);
        }

        [HttpPost]
        public async Task<IActionResult> Detalles(DetallesAgenteViewModel vm)
        {
            if (vm != null && vm.Agente != null)
            {
                vm.Filtros.TipoFiltroUsuario = 2;
                vm.Agente.Propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
                vm.TiposPropiedad = await _tipoPropiedadService.GetAllViewModel();

                return View(vm);
            }
            return RedirectToRoute(new { controller = "Agente", action = "Index" });
        }

        /*public IActionResult Propiedades(string? codigoAgente)
        {
            if (codigoAgente == null || codigoAgente == "")
            {
                return NotFound();
            }

            var agente = _agentes.FirstOrDefault(a => a.Id == codigoAgente);
            if (agente == null)
            {
                return NotFound();
            }

            return View(agente.Propiedades);
        }*/

        [Authorize(Roles = "Agente")]
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

        [Authorize(Roles = "Agente")]
        [HttpPost]
        public async Task<IActionResult> MiPerfil(AgenteViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var agente = _mapper.Map<AgenteViewModel>(vm);

            await _userService.UpdateAgentAsync( vm,  vm.Id.ToString());

            return RedirectToAction("Index", "Agente");
        }
    }
}
