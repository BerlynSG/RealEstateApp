/*using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Core.Application.Enums;
using StockApp.Core.Application.Features.Products.Commands.CreateProduct;
using StockApp.Core.Application.Features.Products.Commands.DeleteProductById;
using StockApp.Core.Application.Features.Products.Commands.UpdateProduct;
using StockApp.Core.Application.Features.Products.Queries.GetAllProducts;
using StockApp.Core.Application.Features.Products.Queries.GetProductById;
using StockApp.Core.Application.Interfaces.Services;
using StockApp.Core.Application.ViewModels.Products;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace StockApp.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Basic")]
    [SwaggerTag("Mantenimiento de productos")]
    public class ProductController : BaseApiController
    {
        [HttpGet]
        [SwaggerOperation(
           Summary = "Listado de productos",
           Description = "Obtiene todos los productos filtrado por categoria"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ProductViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get([FromQuery] GetAllProductsParameter filters)
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery() { CategoryId = filters.CategoryId }));
        }

        [HttpGet("{id}")]
        [SwaggerOperation(
            Summary = "Producto por id",
            Description = "Obtiene un producto utilizando el id como filtro"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveProductViewModel))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await Mediator.Send(new GetProductByIdQuery { Id = id }));
        }

        [HttpPost]
        [SwaggerOperation(
          Summary = "Creacion de producto",
          Description = "Recibe los parametros necesarios para crear un nuevo producto"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post(CreateProductCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            
            return Ok(await Mediator.Send(command));
        }


        [HttpPut("{id}")]
        [SwaggerOperation(
               Summary = "Actualizacion de producto",
               Description = "Recibe los parametros necesarios para modificar un producto existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveProductViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Put(int id, UpdateProductCommand command)
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
            Summary = "Eliminar un producto",
            Description = "Recibe los parametros necesarios para eliminar un producto existente"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteProductByIdCommand { Id = id });
            return NoContent();
        }
    }
}*/
