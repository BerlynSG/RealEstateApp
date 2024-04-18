using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Agentes.Queries.GetAgentePropiedades
{
    /// <summary>
    /// Parámetros para obtener todas las propiedades del agente
    /// </summary>
    public class GetAgentePropiedadesQuery : IRequest<IList<PropiedadViewModel>>
    {
        /// <example>a3de603e-1dbb-4232-aead-30825a48d3d0</example>
        [SwaggerParameter(Description = "Id para filtrar la Agente")]
        public string Id { get; set; }
    }

    public class GetAgentePropiedadesQueryHandler : IRequestHandler<GetAgentePropiedadesQuery, IList<PropiedadViewModel>>
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;

        public GetAgentePropiedadesQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper)
        {
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
        }

        public async Task<IList<PropiedadViewModel>> Handle(GetAgentePropiedadesQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetPropiedadesViewModel(request);
            if (Propiedades == null || Propiedades.Count == 0) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        private async Task<List<PropiedadViewModel>> GetPropiedadesViewModel(GetAgentePropiedadesQuery request)
        {
            var entityList = await _propiedadRepository.GetAllByAgente(request.Id);

            return _mapper.Map<List<PropiedadViewModel>>(entityList);
        }
    }
}
