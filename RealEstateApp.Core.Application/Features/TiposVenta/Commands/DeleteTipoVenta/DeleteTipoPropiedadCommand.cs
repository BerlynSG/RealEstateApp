using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposVenta.Commands.DeleteTipoVenta
{
    /// <summary>
    /// Parámetros para eliminar un tipo de Venta
    /// </summary>
    public class DeleteTipoVentaCommand : IRequest<int>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del tipo de Venta a eliminar")]
        public int Id { get; set; }
    }
    public class DeleteTipoVentaCommandHandler : IRequestHandler<DeleteTipoVentaCommand, int>
    {
        private readonly ITipoVentaRepository _repository;
        private readonly IMapper _mapper;

        public DeleteTipoVentaCommandHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _repository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteTipoVentaCommand request, CancellationToken cancellationToken)
        {
            TipoVenta tipoVenta = await _repository.GetByIdAsync(request.Id);
            if (tipoVenta == null) throw new Exception("Tipo Venta no found");
            await _repository.DeleteAsync(tipoVenta);
            return request.Id;
        }
    }
}
