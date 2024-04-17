using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposPropiedad.Commands.UpdateTipoPropiedad
{
    /// <summary>
    /// Parámetros para editar un tipo de propiedad
    /// </summary>
    public class UpdateTipoPropiedadCommand : IRequest<UpdateTipoPropiedadResponse>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del tipo de propiedad a editar")]
        public int Id { get; set; }

        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        /// <example>Vivienda de diferentes tamaños ubicada en un edificio rodeado de otras viviendas similares.</example>
        [SwaggerParameter(Description = "Descripción del tipo de propiedad")]
        public string Descripcion { get; set; }
    }
    public class UpdateTipoPropiedadCommandHandler : IRequestHandler<UpdateTipoPropiedadCommand, UpdateTipoPropiedadResponse>
    {
        private readonly ITipoPropiedadRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _repository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<UpdateTipoPropiedadResponse> Handle(UpdateTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            TipoPropiedad tipoPropiedad = _mapper.Map<TipoPropiedad>(request);
            if (tipoPropiedad == null) throw new Exception("Tipo Producto no found");
            await _repository.UpdateAsync(tipoPropiedad, request.Id);
            return _mapper.Map<UpdateTipoPropiedadResponse>(tipoPropiedad);
        }
    }
}
