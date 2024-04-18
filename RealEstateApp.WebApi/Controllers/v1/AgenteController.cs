using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Agentes.Queries.GetAgenteById;
using RealEstateApp.Core.Application.Features.Agentes.Queries.GetAgentePropiedades;
using RealEstateApp.Core.Application.Features.Agentes.Queries.GetAllAgentes;
using RealEstateApp.Core.Application.ViewModels.Agente;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealEstateApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Administrador,Desarrollador")]
    [SwaggerTag("Mantenimiento de Agentes")]
    public class AgenteController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de Agentes",
          Description = "Obtiene todas las Agentes creadas"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<AgenteViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllAgentesQuery()));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Agente por id",
            Description = "Obtiene una Agente filtrado por el id"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(AgenteViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string id)
        {
            return Ok(await Mediator.Send(new GetAgenteByIdQuery { Id = id }));
        }

        [HttpGet("Propiedades/{id}")]
        [SwaggerOperation(
          Summary = "Listado de propiedades de un agente",
          Description = "Obtiene todas las propiedades de un agente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PropiedadViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await Mediator.Send(new GetAgentePropiedadesQuery() { Id = id }));
        }

        [Authorize(Roles = "Administrador")]
        [HttpPatch]
        [SwaggerOperation(
          Summary = "Cambiar el estado del Agente",
          Description = "Cambiar el estado del Agente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus([FromBody] ChangeStatusAgenteQuery command)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
                await Mediator.Send(command);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
