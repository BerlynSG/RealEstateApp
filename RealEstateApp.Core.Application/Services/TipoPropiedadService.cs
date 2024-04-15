using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class TipoPropiedadService : GenericService<SaveTipoPropiedadViewModel, TipoPropiedadViewModel, TipoPropiedad>, ITipoPropiedadService
    {
        private readonly ITipoPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public TipoPropiedadService(ITipoPropiedadRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public override async Task<List<TipoPropiedadViewModel>> GetAllViewModel()
        {
            var entityList = await _repository.GetAllWithIncludeAsync();

            return _mapper.Map<List<TipoPropiedadViewModel>>(entityList);
        }
    }
}
