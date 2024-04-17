using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposPropiedad.Queries.GetTipoPropiedadById
{
    /// <summary>
    /// Parámetros para obtener un tipo de propiedad
    /// </summary>
    public class GetTipoPropiedadByIdQuery : IRequest<TipoPropiedadViewModel>
    {
        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "Id para filtrar la propiedad")]
        public int Id { get; set; }
    }

    public class GetTipoPropiedadByIdQueryHandler : IRequestHandler<GetTipoPropiedadByIdQuery, TipoPropiedadViewModel>
    {
        private readonly ITipoPropiedadRepository _tipoPropiedadRepository;
        private readonly IMapper _mapper;

        public GetTipoPropiedadByIdQueryHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _tipoPropiedadRepository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<TipoPropiedadViewModel> Handle(GetTipoPropiedadByIdQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetByIdViewModel(request.Id);
            if (Propiedades == null) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        public virtual async Task<TipoPropiedadViewModel> GetByIdViewModel(int id)
        {
            var entity = await _tipoPropiedadRepository.GetByIdAsync(id);

            var vm = _mapper.Map<TipoPropiedadViewModel>(entity);
            return vm;
        }
    }
}
