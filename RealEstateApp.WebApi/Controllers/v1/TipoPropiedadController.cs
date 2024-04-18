using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.TiposPropiedad.Commands.CreateTipoPropiedad;
using RealEstateApp.Core.Application.Features.TiposPropiedad.Commands.DeleteTipoPropiedad;
using RealEstateApp.Core.Application.Features.TiposPropiedad.Commands.UpdateTipoPropiedad;
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            try
            {
                return Ok(await Mediator.Send(new GetAllTiposPropiedadesQuery()));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{id}")]
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
            try
            {
                return Ok(await Mediator.Send(new GetTipoPropiedadByIdQuery { Id = id }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPost]
        [SwaggerOperation(
          Summary = "Crear un tipo de propiedad",
          Description = "Crea un tipo de propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] CreateTipoPropiedadCommand command)
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

        [HttpPut("Update/{id}")]
        [SwaggerOperation(
          Summary = "Actualizar un tipo de propiedad",
          Description = "Actualizar un tipo de propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UpdateTipoPropiedadResponse))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] UpdateTipoPropiedadCommand command, int id)
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

        [HttpDelete("Delete/{id}")]
        [SwaggerOperation(
          Summary = "Eliminar un tipo de propiedad",
          Description = "Eliminar un tipo de propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await Mediator.Send(new DeleteTipoPropiedadCommand() { Id = id });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
