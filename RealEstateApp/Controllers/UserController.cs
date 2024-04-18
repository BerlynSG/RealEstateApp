using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Application.Helpers;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Middlewares;
using RealEstateApp.Core.Application.Enums;

using RealEstateApp.Core.Application.Interfaces.Services;

namespace RealEstateApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Index(LoginViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            AuthenticationResponse userVm = await _userService.LoginAsync(vm);
            if (userVm != null && userVm.HasError != true)
            {
                HttpContext.Session.Set<AuthenticationResponse>("user", userVm);
                return RedirectToRoute(new { controller = "Home", action = "Index" });
            }   
            else
            {
                vm.HasError = userVm.HasError;
                vm.Error = userVm.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Register()
        {
            return View(new SaveUserViewModel());
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        [HttpPost]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];

            if (vm.Rol == 2)
            {
                RegisterResponse response = await _userService.RegisterAgenteAsync(vm, origin, vm.ProfileImage);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
            }
            else
            {
                RegisterResponse response = await _userService.RegisterClienteAsync(vm, origin, vm.ProfileImage);
                if (response.HasError)
                {
                    vm.HasError = response.HasError;
                    vm.Error = response.Error;
                    return View(vm);
                }
            }

            return RedirectToRoute(new { controller = "User", action = "Index" });
        }

        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.ConfirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }



        public string UploadFile(IFormFile file, string id, bool isEditing = false, string photoUrl = "")
        {
            if (isEditing && file == null)
            {
                return photoUrl;
            }
            //get directory path
            string basePath = $"/img/Agentes/{id}";
            string path = Path.Combine(Directory.GetCurrentDirectory(), $"wwwroot{basePath}");
            //create directory path
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            //generade file name
            FileInfo fileInfo = new FileInfo(file.FileName);
            string filename = Guid.NewGuid() + fileInfo.Extension;
            //get final path
            string finalfilePath = Path.Combine(path, filename);
            //copy the uploaded file
            using (var stream = new FileStream(finalfilePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }
            if (isEditing)
            {
                string[] oldImagePart = photoUrl.Split('/');
                string oldFileName = oldImagePart[^1];
                string oldFilePath = Path.Combine(path, oldFileName);
                if (System.IO.File.Exists(oldFilePath))
                {
                    System.IO.File.Delete(oldFilePath);
                }
            }
            return $"{basePath}/{filename}";
        }
    }
}

