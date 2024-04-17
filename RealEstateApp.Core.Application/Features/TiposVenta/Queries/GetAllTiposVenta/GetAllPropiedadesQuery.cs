using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;

namespace RealEstateApp.Core.Application.Features.TiposVenta.Queries.GetAllTiposVenta
{
    /// <summary>
    /// Parámetros para obtener todos los tipos de ventas
    /// </summary>
    public class GetAllTipoVentasQuery : IRequest<IList<TipoVentaViewModel>>
    {
    }

    public class GetAllTipoVentasQueryHandler : IRequestHandler<GetAllTipoVentasQuery, IList<TipoVentaViewModel>>
    {
        private readonly ITipoVentaRepository _tipoVentaRepository;
        private readonly IMapper _mapper;

        public GetAllTipoVentasQueryHandler(ITipoVentaRepository tipoVentaRepository, IMapper mapper)
        {
            _tipoVentaRepository = tipoVentaRepository;
            _mapper = mapper;
        }

        public async Task<IList<TipoVentaViewModel>> Handle(GetAllTipoVentasQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetAllViewModel();
            if (Propiedades == null || Propiedades.Count == 0) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        private async Task<List<TipoVentaViewModel>> GetAllViewModel()
        {
            var entityList = await _tipoVentaRepository.GetAllAsync();

            return _mapper.Map<List<TipoVentaViewModel>>(entityList);
        }
    }
}
