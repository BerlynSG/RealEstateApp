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

        public AgenteController(IUserService userService, IMapper mapper, IPropiedadService propiedadService, ITipoPropiedadService tipoPropiedadService)
        {
            _mapper = mapper;
            _userService = userService;
            _propiedadService = propiedadService;
            _tipoPropiedadService = tipoPropiedadService;
        }

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

        public async Task<IActionResult> Detalles(string? agenteId)
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
