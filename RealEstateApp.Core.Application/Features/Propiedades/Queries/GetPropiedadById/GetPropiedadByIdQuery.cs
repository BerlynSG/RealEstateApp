using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Propiedades.Queries.GetPropiedadById
{
    /// <summary>
    /// Parámetros para obtener una Propiedad
    /// </summary>
    public class GetPropiedadByIdQuery : IRequest<PropiedadViewModel>
    {
        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "Id para filtrar la propiedad")]
        public int Id { get; set; }
    }

    public class GetPropiedadByIdQueryHandler : IRequestHandler<GetPropiedadByIdQuery, PropiedadViewModel>
    {
        private readonly IPropiedadRepository _PropiedadRepository;
        private readonly IMapper _mapper;

        public GetPropiedadByIdQueryHandler(IPropiedadRepository PropiedadRepository, IMapper mapper)
        {
            _PropiedadRepository = PropiedadRepository;
            _mapper = mapper;
        }

        public async Task<PropiedadViewModel> Handle(GetPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetByIdViewModel(request.Id);
            if (Propiedades == null) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        public virtual async Task<PropiedadViewModel> GetByIdViewModel(int id)
        {
            var entity = await _PropiedadRepository.GetByIdAsync(id);

            PropiedadViewModel vm = _mapper.Map<PropiedadViewModel>(entity);
            return vm;
        }
    }
}
