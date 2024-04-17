using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposPropiedad.Commands.CreateTipoPropiedad
{
    /// <summary>
    /// Parámetros para crear un tipo de propiedad
    /// </summary>
    public class CreateTipoPropiedadCommand : IRequest<int>
    {
        /// <example>Apartamento</example>
        [SwaggerParameter(Description = "Nombre del tipo de propiedad")]
        public string Nombre { get; set; }

        /// <example>Vivienda de diferentes tamaños ubicada en un edificio rodeado de otras viviendas similares.</example>
        [SwaggerParameter(Description = "Descripción del tipo de propiedad")]
        public string Descripcion { get; set; }
    }
    public class CreateTipoPropiedadCommandHandler : IRequestHandler<CreateTipoPropiedadCommand, int>
    {
        private readonly ITipoPropiedadRepository _repository;
        private readonly IMapper _mapper;

        public CreateTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _repository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            TipoPropiedad tipoPropiedad = _mapper.Map<TipoPropiedad>(request);
            await _repository.AddAsync(tipoPropiedad);
            return tipoPropiedad.Id;
        }
    }
}
