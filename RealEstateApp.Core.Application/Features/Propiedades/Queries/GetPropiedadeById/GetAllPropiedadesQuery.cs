using AutoMapper;
using MediatR;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.ViewModels.Propiedad;

namespace RealEstateApp.Core.Application.Features.Propiedades.Queries.GetAllPropiedades
{
    public class GetPropiedadByIDQuery : IRequest<SavePropiedadViewModel>
    {
        //public int? CategoryId { get; set; }
        public string Id { get; set; }
    }

    public class GetPropiedadByIDQueryHandler : IRequestHandler<GetPropiedadByIDQuery, SavePropiedadViewModel>
    {
        private readonly IPropiedadRepository _propiedadRepository;
        private readonly IMapper _mapper;

        public GetPropiedadByIDQueryHandler(IPropiedadRepository propiedadRepository, IMapper mapper)
        {
            _propiedadRepository = propiedadRepository;
            _mapper = mapper;
        }

        public async Task<SavePropiedadViewModel> Handle(GetPropiedadByIDQuery request, CancellationToken cancellationToken)
        {
            var Propiedades = await GetByIdSaveViewModel(request.Id);
            if (Propiedades == null) throw new Exception("Propiedades not found");
            return Propiedades;
        }

        public virtual async Task<SavePropiedadViewModel> GetByIdSaveViewModel(string id)
        {
            var entity = await _propiedadRepository.GetByCodigo(id);

            SavePropiedadViewModel vm = _mapper.Map<SavePropiedadViewModel>(entity);
            return vm;
        }
    }
}
