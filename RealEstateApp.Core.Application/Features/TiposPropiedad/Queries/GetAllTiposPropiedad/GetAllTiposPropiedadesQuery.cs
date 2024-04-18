using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Core.Application.Features.TiposPropiedad.Queries.GetAllTiposPropiedad
{
    /// <summary>
    /// Parámetros para obtener todas los tipos de propiedades
    /// </summary>
    public class GetAllTiposPropiedadesQuery : IRequest<IList<TipoPropiedadViewModel>>
    {

    }

    public class GetAllTipoPropiedadesQueryHandler : IRequestHandler<GetAllTiposPropiedadesQuery, IList<TipoPropiedadViewModel>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;

        public GetAllTipoPropiedadesQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<IList<TipoPropiedadViewModel>> Handle(GetAllTiposPropiedadesQuery request, CancellationToken cancellationToken)
        {
            var TiposPropiedad = await GetAllViewModel();
            if (TiposPropiedad == null || TiposPropiedad.Count == 0) throw new Exception("Tipos Propiedad not found");
            return TiposPropiedad;
        }

        private async Task<List<TipoPropiedadViewModel>> GetAllViewModel()
        {
            var entityList = await _tipoPropiedadRepository.GetAllAsync();

            return _mapper.Map<List<TipoPropiedadViewModel>>(entityList);
        }
    }
}
