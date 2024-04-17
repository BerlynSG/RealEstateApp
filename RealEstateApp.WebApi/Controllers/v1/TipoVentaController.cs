using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TiposVenta.Commands.CreateTipoVenta;
using RealEstateApp.Core.Application.Features.TiposVenta.Commands.DeleteTipoVenta;
using RealEstateApp.Core.Application.Features.TiposVenta.Commands.UpdateTipoVenta;
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
            Summary = "Tipo de venta por id",
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

        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear un tipo de venta",
          Description = "Crea un tipo de venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTipoVentaCommand command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await Mediator.Send(command);
                return StatusCode(StatusCodes.Status201Created);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [SwaggerOperation(
          Summary = "Actualizar un tipo de venta",
          Description = "Actualizar un tipo de venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTipoVentaResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateTipoVentaCommand command, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                command.Id = id;
                return Ok(await Mediator.Send(command));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpDelete]
        [SwaggerOperation(
          Summary = "Eliminar un tipo de venta",
          Description = "Eliminar un tipo de venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTipoVentaCommand() { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
