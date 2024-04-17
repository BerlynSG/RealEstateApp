using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposVenta.Commands.UpdateTipoVenta
{
    public class UpdateTipoVentaResponse
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del tipo de Venta a editar")]
        public int Id { get; set; }

        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "Nombre del tipo de Venta")]
        public string Nombre { get; set; }

        /// <example>Vivienda de diferentes tamaños ubicada en un edificio rodeado de otras viviendas similares.</example>
        [SwaggerParameter(Description = "Descripción del tipo de Venta")]
        public string Descripcion { get; set; }
    }
}
