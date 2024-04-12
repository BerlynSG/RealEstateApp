using AutoMapper;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Services
{
    public class PropiedadService : GenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>, IPropiedadService
    {
        private readonly IPropiedadRepository _repository;
        private readonly IMapper _mapper;
        public PropiedadService(IPropiedadRepository repository, IMapper mapper) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<PropiedadViewModel>> GetAllFilteredViewModel(FiltroPropiedadViewModel filtro)
        {
            List<Propiedad> propiedades = await _repository.GetAllAsync();

            propiedades.Where(p => (filtro.TipoPropiedad == p.TipoPropiedadId || filtro.TipoPropiedad == 0)
                && (filtro.Habitaciones == p.Habitaciones || filtro.Habitaciones == 0) && (filtro.Baños == p.Baños || filtro.Baños == 0)
                && (filtro.PrecioMinimo <= p.Valor || filtro.PrecioMinimo == 0) && (filtro.PrecioMaximo >= p.Valor || filtro.PrecioMaximo == 0)
                && (filtro.Codigo == null || filtro.Codigo == "" || p.Codigo.Contains(filtro.Codigo))).ToList();
            return null;
        }

        public SavePropiedadViewModel convertir(PropiedadViewModel model)
        {
            Propiedad propiedad = _mapper.Map<Propiedad>(model);
            return _mapper.Map<SavePropiedadViewModel>(propiedad);
        }
    }
}
