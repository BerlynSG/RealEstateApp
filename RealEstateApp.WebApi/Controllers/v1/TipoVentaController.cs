using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TiposVenta.Queries.GetAllTiposVenta;
using RealEstateApp.Core.Application.Features.TiposVenta.Queries.GetTipoVentaById;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    //[Authorize(Roles = "Admin")]
    [SwaggerTag("Mantenimiento de tipos de ventas")]
    public class TipoVentaController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de tipos de ventas",
          Description = "Obtiene todas los tipos de ventas creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TipoVentaViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllTipoVentasQuery()));
        }

        [HttpGet("GetById/{id}")]
        [SwaggerOperation(
            Summary = "Tipo de propiedad por id",
            Description = "Obtiene un tipo de venta filtrado por el id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(TipoVentaViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok(await Mediator.Send(new GetTipoVentaByIdQuery { Id = id }));
        }
    }
}
