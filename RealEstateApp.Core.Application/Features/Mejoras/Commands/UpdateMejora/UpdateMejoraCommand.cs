using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Mejoras.Commands.UpdateMejora
{
    /// <summary>
    /// Parámetros para editar un mejora
    /// </summary>
    public class UpdateMejoraCommand : IRequest<UpdateMejoraResponse>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del tipo de la mejora")]
        public int Id { get; set; }

        /// <example>Piscina</example>
        [SwaggerParameter(Description = "Nombre de la mejora")]
        public string Nombre { get; set; }

        /// <example>Area de recreación donde se agrupa una gran cantidad de agua para pasar el tiempo junto a otra persona o solo.</example>
        [SwaggerParameter(Description = "Descripción de la mejora")]
        public string Descripcion { get; set; }
    }
    public class UpdateMejoraCommandHandler : IRequestHandler<UpdateMejoraCommand, UpdateMejoraResponse>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public UpdateMejoraCommandHandler(IMejoraRepository MejoraRepository, IMapper mapper)
        {
            _repository = MejoraRepository;
            _mapper = mapper;
        }

        public async Task<UpdateMejoraResponse> Handle(UpdateMejoraCommand request, CancellationToken cancellationToken)
        {
            Mejora Mejora = _mapper.Map<Mejora>(request);
            if (Mejora == null) throw new Exception("Tipo Venta no found");
            await _repository.UpdateAsync(Mejora, request.Id);
            return _mapper.Map<UpdateMejoraResponse>(Mejora);
        }
    }
}
