using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
using RealEstateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadByCodigo;
using RealEstateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize(Roles = "Admin")]
    [SwaggerTag("Mantenimiento de propiedades")]
    public class PropiedadController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de propiedades",
          Description = "Obtiene todas las propiedades creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PropiedadViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPropiedadesQuery()));
        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Propiedad por id",
            Description = "Obtiene una propiedad filtrado por el id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetPropiedadByIdQuery { Id = id }));
        }

        [HttpGet("GetByCodigo/{codigo}")]
        [SwaggerOperation(
            Summary = "Propiedad por código",
            Description = "Obtiene una propiedad filtrado por el código"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetByCodigo(string codigo)
        {
            return Ok(await Mediator.Send(new GetPropiedadByCodigoQuery { Codigo = codigo }));
        }
    }
}
