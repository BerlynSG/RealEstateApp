using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposVenta.Commands.CreateTipoVenta
{
    /// <summary>
    /// Parámetros para crear un tipo de venta
    /// </summary>
    public class CreateTipoVentaCommand : IRequest<int>
    {
        /// <example>Alquiler</example>
        [SwaggerParameter(Description = "Nombre del tipo de venta")]
        public string Nombre { get; set; }

        /// <example>Paga un precio accesible pero constante para obtener una vivienda temporal.</example>
        [SwaggerParameter(Description = "Descripción del tipo de venta")]
        public string Descripcion { get; set; }
    }
    public class CreateTipoVentaCommandHandler : IRequestHandler<CreateTipoVentaCommand, int>
    {
        private readonly ITipoVentaRepository _repository;
        private readonly IMapper _mapper;

        public CreateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _repository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateTipoVentaCommand request, CancellationToken cancellationToken)
        {
            TipoVenta tipoVenta = _mapper.Map<TipoVenta>(request);
            await _repository.AddAsync(tipoVenta);
            return tipoVenta.Id;
        }
    }
}
