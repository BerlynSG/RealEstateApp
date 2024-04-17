using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Mejoras.Queries.GetAllMejoras;
using RealEstateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize(Roles = "Admin")]
    [SwaggerTag("Mantenimiento de mejoras")]
    public class MejoraController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de mejoras",
          Description = "Obtiene todas las mejoras creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<MejoraViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllMejorasQuery()));
        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Mejora por id",
            Description = "Obtiene una mejora filtrado por el id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(MejoraViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetMejoraByIdQuery { Id = id }));
        }
    }
}
