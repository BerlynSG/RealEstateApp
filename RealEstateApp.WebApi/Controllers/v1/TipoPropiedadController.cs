using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TiposPropiedad.Queries.GetAllTiposPropiedad;
using RealEstateApp.Core.Application.Features.TiposPropiedad.Queries.GetTipoPropiedadById;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize(Roles = "Admin")]
    [SwaggerTag("Mantenimiento de tipos de propiedades")]
    public class TipoPropiedadController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de tipos de propiedades",
          Description = "Obtiene todas los tipos de propiedades creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TipoPropiedadViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllTipoPropiedadesQuery()));
        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Tipo de propiedad por id",
            Description = "Obtiene un tipo de propiedad filtrado por el id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoPropiedadViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id }));
        }
    }
}
