﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Infrastructure.Identity.Services;
using RealEstateApp.Middlewares;

namespace RealEstateApp.Controllers
{
    public class AdminsController : Controller
    {
        private readonly IUserService _userService;
        private readonly IAccountService _accountService;

        public AdminsController(IUserService userService, IAccountService accountService)
        {
            _userService = userService;
            _accountService = accountService;
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
        public async Task<IActionResult> DeleteUser(string id)
        {
            var response = await _userService.DeleteUserAsync(id);

            if (response.HasError)
            {
                return RedirectToAction("Index", "Home");
            }

            return RedirectToAction("Agentes");
        }
        public async Task<IActionResult> EditAdmin(string id)
        {
            var user = await _accountService.GetUserById(id);

            if (user == null || user.Rol != (int)Roles.Administrador)
            {
                return NotFound();
            }

            var model = new SaveAdminsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Cedula = user.Cedula,
                Email = user.Email,
                Username = user.UserName,
            };

            return View("RegisterAdmin", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditAdmin(SaveAdminsViewModel model)
        {
            var origin = Request.Headers["origin"];

            if (!ModelState.IsValid)
            {
                return View("RegisterAdmin", model);
            }

            var updateRequest = new UpdateRequest
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Cedula = model.Cedula,
                Email = model.Email,
                UserName = model.Username
            };

            var response = await _accountService.UpdateUserAsync(updateRequest, model.Id);

            if (response.HasError)
            {
                ModelState.AddModelError(string.Empty, response.Error);
                return View("RegisterAdmin", model); 
            }

            return RedirectToAction("Index"); 
        }
        public async Task<IActionResult> EditDesarrollador(string id)
        {
            var user = await _accountService.GetUserById(id);

            if (user == null || user.Rol != (int)Roles.Desarrollador)
            {
                return NotFound();
            }

            var model = new SaveAdminsViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Cedula = user.Cedula,
                Email = user.Email,
                Username = user.UserName,
            };

            return View("RegisterDesarrollador", model);
        }

        [HttpPost]
        public async Task<IActionResult> EditDesarrollador(SaveAdminsViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("RegisterDesarrollador", model);
            }

            var updateRequest = new UpdateRequest
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Cedula = model.Cedula,
                Email = model.Email,
                UserName = model.Username
            };

            var response = await _accountService.UpdateUserAsync(updateRequest, model.Id);

            if (response.HasError)
            {
                ModelState.AddModelError(string.Empty, response.Error);
                return View("RegisterDesarrollador", model);
            }

            return RedirectToAction("DesarrolladorIndex", "Admins");
        }


    }
}
