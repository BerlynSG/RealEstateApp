using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadByCodigo
{
    /// <summary>
    /// Parámetros para obtener una propiedad
    /// </summary>
    public class GetPropiedadByCodigoQuery : IRequest<PropiedadViewModel>
    {
        /// <example>
        /// 238456
        /// </example>
        [SwaggerParameter(Description = "Código para filtrar la propiedad")]
        public string Codigo { get; set; }
    }

    public class GetPropiedadByCodigoQueryHandler : IRequestHandler<GetPropiedadByCodigoQuery, PropiedadViewModel>
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;

        public GetPropiedadByCodigoQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper)
        {
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
        }

        public async Task<PropiedadViewModel> Handle(GetPropiedadByCodigoQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetByIdViewModel(request.Codigo);
            if (Propiedades == null) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        public virtual async Task<PropiedadViewModel> GetByIdViewModel(string codigo)
        {
            var entity = await _propiedadRepository.GetByCodigo(codigo);

            PropiedadViewModel vm = _mapper.Map<PropiedadViewModel>(entity);
            return vm;
        }
    }
}
