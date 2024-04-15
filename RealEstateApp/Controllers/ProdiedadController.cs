using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using RealEstateApp.Core.Domain.Entities;
using System.Security.Claims;

namespace RealEstateApp.Controllers
{
    public class PropiedadController : Controller
    {
        private readonly IPropiedadService _propiedadService;
        private readonly ITipoPropiedadService _tipoPropiedadService;
        private readonly ITipoVentaService _tipoVentaService;
        private readonly IMejoraService _mejoraService;
        //private List<AgenteViewModel> agentes;
        //private int tipoUsuario = 1;
        //private int idAgente = 0;

        public PropiedadController(IPropiedadService propiedadRepository, ITipoPropiedadService tipoPropiedadService,
            ITipoVentaService tipoVentaService, IMejoraService mejoraService)
        {
            _propiedadService = propiedadRepository;
            _tipoPropiedadService = tipoPropiedadService;
            _tipoVentaService = tipoVentaService;
            _mejoraService = mejoraService;
            /*agentes = new List<AgenteViewModel>()
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
            };*/
        }

        public async Task<IActionResult> Index(int id, int messageType, string message)
        {
            ListaPropiedadViewModel vm = new ListaPropiedadViewModel();
            vm.Filtros = new FiltroPropiedadViewModel();
            if (User.Identity.IsAuthenticated)
            {
                if (User.IsInRole(Roles.Cliente.ToString()))
                {
                    vm.Modo = 1;
                    vm.Filtros.TipoFiltroUsuario = id == 0 ? 0 : 1;
                }
                else if (User.IsInRole(Roles.Agente.ToString()))
                {
                    vm.Modo = id == 0 ? 0 : 2;
                    vm.Filtros.TipoFiltroUsuario = 2;
                }
                //vm.Filtros.TipoFiltroUsuario = tipoUsuario;
                //vm.Modo = id;
            }
            
            vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
            vm.tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.MessageType = messageType;
            vm.Message = message;

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

        [Authorize(Roles = "Agente")]
        public async Task<IActionResult> CrearPropiedad()
        {
            List<TipoPropiedadViewModel> tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            List<MejoraViewModel> mejoras = await _mejoraService.GetAllViewModel();
            List<TipoVentaViewModel> tiposVenta = await _tipoVentaService.GetAllViewModel();
            if (tiposPropiedad.Count == 0 || tiposVenta.Count == 0 || mejoras.Count == 0)
            {
                TempData["ErrorMessage"] = "No se pueden crear propiedades porque no existen tipos de propiedad, tipos de venta o mejoras.";
                return RedirectToAction("Index", new { id = 1,
                    messageType = 2,
                    message = "No se puede crear una propiedad porque no hay mejoras, tipos de propiedad y de venta."
                });
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

        [Authorize(Roles = "Agente")]
        [HttpPost]
        public async Task<IActionResult> CrearPropiedad(SavePropiedadViewModel vm)
        {
            if (ModelState.IsValid && vm.ImagenesFiles != null && vm.ImagenesFiles.Count > 0)
            {
                vm.AgenteId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                SavePropiedadViewModel svm = await _propiedadService.Add(vm);
                if (vm.ImagenesFiles != null && svm != null && svm.Id != 0)
                {
                    List<ImagenPropiedad> imagenes = vm.ImagenesFiles
                        .Select(i => new ImagenPropiedad() { Path = UploadFile(i, svm.Id), PropiedadId = svm.Id })
                        .ToList();
                    await _propiedadService.AddImages(imagenes);
                }

                TempData["SuccessMessage"] = "La propiedad se creó correctamente.";
                return RedirectToAction("Index", new { id = 1,
                    messageType = 1,
                    message = "Se ha creado la propiedad correctamente."
                });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();
            return View("Crear", vm);
        }

        [Authorize(Roles = "Agente")]
        public async Task<IActionResult> EditarPropiedad(string codigo)
        {
            SavePropiedadViewModel vm = await _propiedadService.GetByCodigoSaveViewModel(codigo);

            if (vm == null)
            {
                return RedirectToAction("Index", new { id = 1,
                    messageType = 2,
                    message = "No se ha encontrado la propiedad para editarla."
                });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();

            return View("Crear", vm);
        }

        [Authorize(Roles = "Agente")]
        [HttpPost]
        public async Task<IActionResult> EditarPropiedad(SavePropiedadViewModel vm)
        {
            if (ModelState.IsValid)
            {
                PropiedadViewModel propiedad = await _propiedadService.GetByCodigoViewModel(vm.Codigo);

                if (propiedad == null)
                {
                    return RedirectToAction("Index", new { id = 1,
                        messageType = 2,
                        message = "No se ha encontrado la propiedad para editarla."
                    });
                }

                await _propiedadService.Update(vm, propiedad.Id);
                if (vm.ImagenesFiles != null)
                {
                    List<ImagenPropiedad> imagenes = vm.ImagenesFiles
                        .Select(i => new ImagenPropiedad() { Path = UploadFile(i, propiedad.Id), PropiedadId = propiedad.Id })
                        .ToList();
                    await _propiedadService.AddImages(imagenes);
                }

                TempData["SuccessMessage"] = "La propiedad se editó correctamente.";
                return RedirectToAction("Index", new { id = 1,
                    messageType = 1,
                    message = "Se ha editado la propiedad correctamente."
                });
            }

            vm.ListaTipoPropiedad = await _tipoPropiedadService.GetAllViewModel();
            vm.ListaTipoVenta = await _tipoVentaService.GetAllViewModel();
            vm.ListaMejora = await _mejoraService.GetAllViewModel();
            return View("Crear", vm);
        }

        [Authorize(Roles = "Agente")]
        [HttpPost]
        public async Task<IActionResult> Eliminar(ListaPropiedadViewModel vm)
        {
            await _propiedadService.Delete(vm.EliminarId);
            vm.MessageType = 1;
            vm.Message = "Se ha eliminado la propiedad correctamente.";
            vm.propiedades = await _propiedadService.GetAllFilteredViewModel(vm.Filtros);
            vm.tiposPropiedad = await _tipoPropiedadService.GetAllViewModel();
            return View("Index", vm);
        }

        public string UploadFile(IFormFile file, int id, bool isEditing = false, string photoUrl = "")
        {
            if (isEditing && file == null)
            {
                return photoUrl;
            }
            //get directory path
            string basePath = $"/img/Propiedades/{id}";
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
