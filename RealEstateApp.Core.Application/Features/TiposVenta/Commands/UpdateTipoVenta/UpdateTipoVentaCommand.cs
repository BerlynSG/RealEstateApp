using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposVenta.Commands.UpdateTipoVenta
{
    /// <summary>
    /// Parámetros para editar un tipo de Venta
    /// </summary>
    public class UpdateTipoVentaCommand : IRequest<UpdateTipoVentaResponse>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del tipo de Venta a editar")]
        public int Id { get; set; }

        /// <example>Alquiler</example>
        [SwaggerParameter(Description = "Nombre del tipo de Venta")]
        public string Nombre { get; set; }

        /// <example>Paga un precio accesible pero constante para obtener una vivienda temporal.</example>
        [SwaggerParameter(Description = "Descripción del tipo de Venta")]
        public string Descripcion { get; set; }
    }
    public class UpdateTipoVentaCommandHandler : IRequestHandler<UpdateTipoVentaCommand, UpdateTipoVentaResponse>
    {
        private readonly ITipoVentaRepository _repository;
        private readonly IMapper _mapper;

        public UpdateTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _repository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<UpdateTipoVentaResponse> Handle(UpdateTipoVentaCommand request, CancellationToken cancellationToken)
        {
            TipoVenta tipoVenta = _mapper.Map<TipoVenta>(request);
            if (tipoVenta == null) throw new Exception("Tipo Venta no found");
            await _repository.UpdateAsync(tipoVenta, request.Id);
            return _mapper.Map<UpdateTipoVentaResponse>(tipoVenta);
        }
    }
}
