using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Enums;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Agente;

namespace RealEstateApp.Core.Application.Features.Agentes.Queries.GetAllAgentes
{
    /// <summary>
    /// Parámetros para obtener todas las Agentes
    /// </summary>
    public class GetAllAgentesQuery : IRequest<IList<AgenteViewModel>>
    {

    }

    public class GetAllAgentesQueryHandler : IRequestHandler<GetAllAgentesQuery, IList<AgenteViewModel>>
    {
        private readonly IUserService _AgenteRepository;
        private readonly IMapper _mapper;

        public GetAllAgentesQueryHandler(IUserService AgenteRepository, IMapper mapper)
        {
            _AgenteRepository = AgenteRepository;
            _mapper = mapper;
        }

        public async Task<IList<AgenteViewModel>> Handle(GetAllAgentesQuery request, CancellationToken cancellationToken)
        {
            var Agentes = await GetAllViewModel();
            if (Agentes == null || Agentes.Count == 0) throw new Exception("Agentes not found");
            return Agentes;
        }

        private async Task<List<AgenteViewModel>> GetAllViewModel()
        {
            var entityList = await _AgenteRepository.GetAllUsers();
            entityList = entityList.Where(a => a.Roles.Contains(Roles.Agente.ToString())).ToList();

            return _mapper.Map<List<AgenteViewModel>>(entityList);
        }
    }
}
