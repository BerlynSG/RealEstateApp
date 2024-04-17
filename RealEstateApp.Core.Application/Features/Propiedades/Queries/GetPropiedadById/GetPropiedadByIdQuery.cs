using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById
{
    /// <summary>
    /// Parámetros para obtener una mejora
    /// </summary>
    public class GetMejoraByIdQuery : IRequest<MejoraViewModel>
    {
        /// <example>
        /// 1
        /// </example>
        [SwaggerParameter(Description = "Id para filtrar la propiedad")]
        public int Id { get; set; }
    }

    public class GetMejoraByIdQueryHandler : IRequestHandler<GetMejoraByIdQuery, MejoraViewModel>
    {
        private readonly IPropiedadRepository _mejoraRepository;
        private readonly IMapper _mapper;

        public GetMejoraByIdQueryHandler(IPropiedadRepository mejoraRepository, IMapper mapper)
        {
            _mejoraRepository = mejoraRepository;
            _mapper = mapper;
        }

        public async Task<MejoraViewModel> Handle(GetMejoraByIdQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetByIdViewModel(request.Id);
            if (Propiedades == null) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        public virtual async Task<MejoraViewModel> GetByIdViewModel(int id)
        {
            var entity = await _mejoraRepository.GetByIdAsync(id);

            MejoraViewModel vm = _mapper.Map<MejoraViewModel>(entity);
            return vm;
        }
    }
}
