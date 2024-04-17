using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.TiposPropiedad.Commands.DeleteTipoPropiedad
{
    /// <summary>
    /// Parámetros para eliminar un tipo de propiedad
    /// </summary>
    public class DeleteTipoPropiedadCommand : IRequest<int>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del tipo de propiedad a eliminar")]
        public int Id { get; set; }
    }
    public class DeleteTipoPropiedadCommandHandler : IRequestHandler<DeleteTipoPropiedadCommand, int>
    {
        private readonly ITipoPropiedadRepository _repository;
        private readonly IMapper _mapper;

        public DeleteTipoPropiedadCommandHandler(ITipoPropiedadRepository tipoPropiedadRepository, IMapper mapper)
        {
            _repository = tipoPropiedadRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteTipoPropiedadCommand request, CancellationToken cancellationToken)
        {
            TipoPropiedad tipoPropiedad = await _repository.GetByIdAsync(request.Id);
            if (tipoPropiedad == null) throw new Exception("Tipo propiedad no found");
            await _repository.DeleteAsync(tipoPropiedad);
            return request.Id;
        }
    }
}
