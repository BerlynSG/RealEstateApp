using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;

namespace RealEstateApp.Core.Application.Features.TiposPropiedad.Queries.GetAllTiposPropiedad
{
    /// <summary>
    /// Parámetros para obtener todas los tipos de propiedades
    /// </summary>
    public class GetAllTipoPropiedadesQuery : IRequest<IList<TipoPropiedadViewModel>>
    {

    }

    public class GetAllTipoPropiedadesQueryHandler : IRequestHandler<GetAllTipoPropiedadesQuery, IList<TipoPropiedadViewModel>>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;

        public GetAllTipoPropiedadesQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<IList<TipoPropiedadViewModel>> Handle(GetAllTipoPropiedadesQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetAllViewModel();
            if (Propiedades == null || Propiedades.Count == 0) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        private async Task<List<TipoPropiedadViewModel>> GetAllViewModel()
        {
            var entityList = await _tipoPropiedadRepository.GetAllAsync();

            return _mapper.Map<List<TipoPropiedadViewModel>>(entityList);
        }
    }
}
