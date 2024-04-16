using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Propiedad;

namespace RealEstateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades
{
    /// <summary>
    /// Parámetros para obtener todas las propiedades
    /// </summary>
    public class GetAllPropiedadesQuery : IRequest<IList<PropiedadViewModel>>
    {

    }

    public class GetAllPropiedadesQueryHandler : IRequestHandler<GetAllPropiedadesQuery, IList<PropiedadViewModel>>
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;

        public GetAllPropiedadesQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper)
        {
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
        }

        public async Task<IList<PropiedadViewModel>> Handle(GetAllPropiedadesQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetAllViewModel();
            if (Propiedades == null || Propiedades.Count == 0) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        private async Task<List<PropiedadViewModel>> GetAllViewModel()
        {
            var entityList = await _propiedadRepository.GetAllAsync();

            return _mapper.Map<List<PropiedadViewModel>>(entityList);
        }
    }
}
