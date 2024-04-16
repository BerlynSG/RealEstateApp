using Asp.Versioning;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealEstateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades;
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
          Summary = "Listado de categorias",
          Description = "Obtiene todas las categorias creadas y la cantidad de productos que estan asociado a la misma"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<PropiedadViewModel>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            return Ok(await Mediator.Send(new GetAllPropiedadesQuery()));
        }
    }
}
