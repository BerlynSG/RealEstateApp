using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Agentes.Queries.GetAgenteById
{
    /// <summary>
    /// Parámetros para obtener una Agente
    /// </summary>
    public class ChangeStatusAgenteQuery : IRequest<string>
    {
        /// <example>a3de603e-1dbb-4232-aead-30825a48d3d0</example>
        [SwaggerParameter(Description = "Id para filtrar el Agente")]
        public string Id { get; set; }

        /// <example>true</example>
        [SwaggerParameter(Description = "Estatus del Agente")]
        public bool Status { get; set; }
    }

    public class ChangeStatusAgenteQueryHandler : IRequestHandler<ChangeStatusAgenteQuery, string>
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public ChangeStatusAgenteQueryHandler(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<string> Handle(ChangeStatusAgenteQuery request, CancellationToken cancellationToken)
        {
            var Agentes = await ChangeStatusAgente(request.Id, request.Status);
            return Agentes;
        }

        public virtual async Task<string> ChangeStatusAgente(string id, bool status)
        {
            var Agente = await _userService.GetUserById(id);
            if (Agente.EmailConfirmed != status)
            {
                await _userService.ActivateUserAsync(id);
            }
            return "";
        }
    }
}
