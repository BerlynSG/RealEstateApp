using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Middlewares;

namespace RealEstateApp.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IUserService _userService;

        public AdminsController(IUserService userService)
        {
            _userService = userService;
        }
        public async Task<IActionResult> Home()
        {
            var dashboardData = await _userService.GetAdminDashboardData();

            ViewBag.TotalPropertiesCount = dashboardData["TotalPropertiesCount"];
            ViewBag.ActiveAgentsCount = dashboardData["ActiveAgentsCount"];
            ViewBag.InactiveAgentsCount = dashboardData["InactiveAgentsCount"];
            ViewBag.ActiveClientsCount = dashboardData["ActiveClientsCount"];
            ViewBag.InactiveClientsCount = dashboardData["InactiveClientsCount"];
            ViewBag.ActiveDevelopersCount = dashboardData["ActiveDevelopersCount"];
            ViewBag.InactiveDevelopersCount = dashboardData["InactiveDevelopersCount"];

            return View(); 
        }

        public async Task<IActionResult> Index()
        {
            ViewBag.Users = await _userService.GetAllUsers();

            return View();
        }
        public async Task<IActionResult> DesarrolladorIndex()
        {
            ViewBag.Users = await _userService.GetAllUsers();
            return View();
        }
        public async Task<IActionResult> Agentes()
        {
            ViewBag.Users = await _userService.GetAllUsers();
            return View();
        }
        public IActionResult RegisterAdmin()
        {
            var vm = new SaveAdminsViewModel(); // Modelo para el registro de un nuevo administrador
            return View(vm); // Devuelve la vista de registro para administradores
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAdmin(SaveAdminsViewModel vm)
        {
            // Lógica para registrar un nuevo administrador utilizando _userService.RegisterAdminAsync(vm, origin);

            var origin = Request.Headers["origin"];

            if (vm.Rol == 3)
            {
                RegisterAdminsResponse response = await _userService.RegisterAdminAsync(vm, origin);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
            }
            else
            {
                RegisterAdminsResponse response = await _userService.RegisterDesarrolladorAsync(vm, origin);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
            }

            return RedirectToRoute(new { controller = "Admins", action = "Index" });
        }
        public async Task<IActionResult> ActivateUser(string id)
        {
            return View("ActivateUser", await _userService.GetUserByAdminId(id));
        }

        [HttpPost]
        public async Task<IActionResult> ActivateUser(SaveAdminsViewModel vm)
        {
            await _userService.ActivateUserAsync(vm.Id);
            return RedirectToRoute(new { controller = "Admins", action = "Index" });
        }
        public IActionResult RegisterDesarrollador()
        {
            var vm = new SaveAdminsViewModel(); // Modelo para el registro de un nuevo administrador
            return View(vm); // Devuelve la vista de registro para administradores
        }

        [HttpPost]
        public async Task<IActionResult> RegisterDesarrollador(SaveAdminsViewModel vm)
        {
            // Lógica para registrar un nuevo administrador utilizando _userService.RegisterAdminAsync(vm, origin);

            var origin = Request.Headers["origin"];

                 RegisterAdminsResponse response = await _userService.RegisterDesarrolladorAsync(vm, origin);
                 if (response.HasError)
                 {
                     vm.HasError = response.HasError;
                     vm.Error = response.Error;
                     return View(vm);
                 }

            return RedirectToRoute(new { controller = "Admins", action = "Index" });
        }
    }
}
