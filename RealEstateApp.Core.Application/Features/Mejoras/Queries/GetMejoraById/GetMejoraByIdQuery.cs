using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Mejoras.Queries.GetMejoraById
{
    /// <summary>
    /// Parámetros para obtener una Mejora
    /// </summary>
    public class GetMejoraByIdQuery : IRequest<MejoraViewModel>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id para filtrar la Mejora")]
        public int Id { get; set; }
    }

    public class GetMejoraByIdQueryHandler : IRequestHandler<GetMejoraByIdQuery, MejoraViewModel>
    {
        private readonly IMejoraRepository _MejoraRepository;
        private readonly IMapper _mapper;

        public GetMejoraByIdQueryHandler(IMejoraRepository MejoraRepository, IMapper mapper)
        {
            _MejoraRepository = MejoraRepository;
            _mapper = mapper;
        }

        public async Task<MejoraViewModel> Handle(GetMejoraByIdQuery request, CancellationToken cancellationToken)
        {
            var Mejoras = await GetByIdViewModel(request.Id);
            if (Mejoras == null) throw new Exception("Mejoras not found");
            return Mejoras;
        }

        public virtual async Task<MejoraViewModel> GetByIdViewModel(int id)
        {
            var entity = await _MejoraRepository.GetByIdAsync(id);

            MejoraViewModel vm = _mapper.Map<MejoraViewModel>(entity);
            return vm;
        }
    }
}
