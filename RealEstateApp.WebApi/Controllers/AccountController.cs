using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Sistema de membresia")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpPost("Login")]
        [SwaggerOperation(
         Summary = "Login de usuario",
         Description = "Autentica un usuario en el sistema y le retorna un JWT"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> AuthenticateApiAsync(AuthenticationRequest request)
        {
            return Ok(await _accountService.AuthenticateApiAsync(request));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost("Register-Admin")]
        [SwaggerOperation(
            Summary = "Creacion de usuario administrador",
            Description = "Recibe los parametros necesarios para crear un usuario administrador"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAdminAsync(RegisterAdminsRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterAdminUserAsync(request, origin));
        }

        [HttpPost("Register-Desarrollador")]
        [SwaggerOperation(
            Summary = "Creacion de usuario desarrollador",
            Description = "Recibe los parametros necesarios para crear un usuario desarrollador"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDesarrolladorAsync(RegisterAdminsRequest request)
        {
            var origin = Request.Headers["origin"];
            return Ok(await _accountService.RegisterDesarrolladorUserAsync(request, origin));
        }
    }
}
