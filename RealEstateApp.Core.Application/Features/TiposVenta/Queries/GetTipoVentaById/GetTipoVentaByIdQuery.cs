using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposVenta.Queries.GetTipoVentaById
{
    /// <summary>
    /// Parámetros para obtener un tipo de venta
    /// </summary>
    public class GetTipoVentaByIdQuery : IRequest<TipoVentaViewModel>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id para filtrar la propiedad")]
        public int Id { get; set; }
    }

    public class GetTipoVentaByIdQueryHandler : IRequestHandler<GetTipoVentaByIdQuery, TipoVentaViewModel>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public GetTipoVentaByIdQueryHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<TipoVentaViewModel> Handle(GetTipoVentaByIdQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetByIdViewModel(request.Id);
            if (Propiedades == null) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        public virtual async Task<TipoVentaViewModel> GetByIdViewModel(int id)
        {
            var entity = await _tipoVentaRepository.GetByIdAsync(id);

            var vm = _mapper.Map<TipoVentaViewModel>(entity);
            return vm;
        }
    }
}
