using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora
{
    public class UpdateMejoraResponse
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id de la mejora a editar")]
        public int Id { get; set; }

        /// <example>Piscina</example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }

        /// <example>Area de recreación donde se agrupa una gran cantidad de agua para pasar el tiempo junto a otra persona o solo.</example>
        [SwaggerParameter(Description = "Descripción de la mejora")]
        public string Descripcion { get; set; }
    }
}
