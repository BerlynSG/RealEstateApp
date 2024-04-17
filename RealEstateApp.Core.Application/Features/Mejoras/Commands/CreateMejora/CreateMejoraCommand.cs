using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Mejoras.Commands.CreateMejora
{
    /// <summary>
    /// Parámetros para crear un mejora
    /// </summary>
    public class CreateMejoraCommand : IRequest<int>
    {
        /// <example>Piscina</example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }

        /// <example>Area de recreación donde se agrupa una gran cantidad de agua para pasar el tiempo junto a otra persona o solo.</example>
        [SwaggerParameter(Description = "Descripción de la mejora")]
        public string Descripcion { get; set; }
    }
    public class CreateMejoraCommandHandler : IRequestHandler<CreateMejoraCommand, int>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public CreateMejoraCommandHandler(IMejoraRepository MejoraRepository, IMapper mapper)
        {
            _repository = MejoraRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(CreateMejoraCommand request, CancellationToken cancellationToken)
        {
            Mejora Mejora = _mapper.Map<Mejora>(request);
            await _repository.AddAsync(Mejora);
            return Mejora.Id;
        }
    }
}
