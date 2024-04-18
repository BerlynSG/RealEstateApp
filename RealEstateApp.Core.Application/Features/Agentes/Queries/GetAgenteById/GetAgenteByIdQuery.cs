using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Agentes.Queries.GetAgenteById
{
    /// <summary>
    /// Parámetros para obtener una Agente
    /// </summary>
    public class GetAgenteByIdQuery : IRequest<AgenteViewModel>
    {
        /// <example>a3de603e-1dbb-4232-aead-30825a48d3d0</example>
        [SwaggerParameter(Description = "Id para filtrar la Agente")]
        public string Id { get; set; }
    }

    public class GetAgenteByIdQueryHandler : IRequestHandler<GetAgenteByIdQuery, AgenteViewModel>
    {
        private readonly IUserService _AgenteRepository;
        private readonly IMapper _mapper;

        public GetAgenteByIdQueryHandler(IUserService AgenteRepository, IMapper mapper)
        {
            _AgenteRepository = AgenteRepository;
            _mapper = mapper;
        }

        public async Task<AgenteViewModel> Handle(GetAgenteByIdQuery request, CancellationToken cancellationToken)
        {
            var Agentes = await GetByIdViewModel(request.Id);
            if (Agentes == null) throw new Exception("Agentes not found");
            return Agentes;
        }

        public virtual async Task<AgenteViewModel> GetByIdViewModel(string id)
        {
            var entity = await _AgenteRepository.GetUserById(id);

            AgenteViewModel vm = _mapper.Map<AgenteViewModel>(entity);
            return vm;
        }
    }
}
