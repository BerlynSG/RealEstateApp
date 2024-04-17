using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealEstateApp.Core.Application.Features.Mejoras.Commands.DeleteMejora
{
    /// <summary>
    /// Parámetros para eliminar un mejora
    /// </summary>
    public class DeleteMejoraCommand : IRequest<int>
    {
        /// <example>1</example>
        [SwaggerParameter(Description = "Id del mejora a eliminar")]
        public int Id { get; set; }
    }
    public class DeleteMejoraCommandHandler : IRequestHandler<DeleteMejoraCommand, int>
    {
        private readonly IMejoraRepository _repository;
        private readonly IMapper _mapper;

        public DeleteMejoraCommandHandler(IMejoraRepository MejoraRepository, IMapper mapper)
        {
            _repository = MejoraRepository;
            _mapper = mapper;
        }

        public async Task<int> Handle(DeleteMejoraCommand request, CancellationToken cancellationToken)
        {
            Mejora Mejora = await _repository.GetByIdAsync(request.Id);
            if (Mejora == null) throw new Exception("Tipo Venta no found");
            await _repository.DeleteAsync(Mejora);
            return request.Id;
        }
    }
}
