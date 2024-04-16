/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Application.Features.Categories.Commands.CreateCategory;
using StockApp.Core.Application.Features.Categories.Commands.DeleteCategoryById;
using StockApp.Core.Application.Features.Categories.Commands.UpdateCategory;
using StockApp.Core.Application.Features.Categories.Queries.GetAllCategories;
using StockApp.Core.Application.Features.Categories.Queries.GetCategoryById;
using StockApp.Core.Application.ViewModels.Categories;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StockApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    [SwaggerTag("Mantenimiento de categorias")]
    public class CategoryController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
          Summary = "Listado de categorias",
          Description = "Obtiene todas las categorias creadas y la cantidad de productos que estan asociado a la misma"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {

            return Ok(await Mediator.Send(new GetAllCategoriesQuery()));

        }

        [HttpGet("{id}")]
        [SwaggerOperation(
             Summary = "Categoria por id",
             Description = "Obtiene una categoria utilizando el id como filtro"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetCategoryByIdQuery { Id = id }));
        }

        [HttpPost]
        [SwaggerOperation(
                 Summary = "Creacion de categoria",
                 Description = "Recibe los parametros necesarios para crear una nueva categoria"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return Ok(await Mediator.Send(command));
        }

        [HttpPut("{id}")]
        [SwaggerOperation(
                  Summary = "Actualizacion de categoria",
                  Description = "Recibe los parametros necesarios para modificar una categoria existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveCategoryViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateCategoryCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            if (id != command.Id)
            {
                return BadRequest();
            }
            return Ok(await Mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(
              Summary = "Eliminar una categoria",
              Description = "Recibe los parametros necesarios para eliminar una categoria existente, al eliminar una categoria " +
              "se elimina los productos asociada con esta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteCategoryByIdCommand { Id = id });
            return NoContent();
        }
    }
}*/
