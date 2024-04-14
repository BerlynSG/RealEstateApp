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

        public override Task<SavePropiedadViewModel> Add(SavePropiedadViewModel vm)
        {
            vm.Codigo = GenerarCodigoUnico();
            vm.Imagenes = null;
            vm.Mejoras = null;
            return base.Add(vm);
        }

        public async Task<List<PropiedadViewModel>> GetAllFilteredViewModel(FiltroPropiedadViewModel filtro)
        {
            List<Propiedad> propiedades;
            //prop = await _propiedadService.GetAllViewModel();
            if (filtro.TipoFiltroUsuario == 4 || filtro.TipoFiltroUsuario == 5)
            {
                propiedades = await _repository.GetAllByAgente(filtro.UsuarioId);
            }
            else if (filtro.TipoFiltroUsuario == 3)
            {
                propiedades = await _repository.GetAllFavoritos(filtro.UsuarioId);
            }
            else
            {
                propiedades = await _repository.GetAllWithInclude();
            }

            propiedades = propiedades.Where(p => (filtro.TipoPropiedad == p.TipoPropiedadId || filtro.TipoPropiedad == 0)
                && (filtro.Habitaciones == p.Habitaciones || filtro.Habitaciones == 0) && (filtro.Baños == p.Baños || filtro.Baños == 0)
                && (filtro.PrecioMinimo <= p.Valor || filtro.PrecioMinimo == 0) && (filtro.PrecioMaximo >= p.Valor || filtro.PrecioMaximo == 0)
                && (filtro.Codigo == null || filtro.Codigo == "" || p.Codigo.Contains(filtro.Codigo))).ToList();
            
            return _mapper.Map<List<PropiedadViewModel>>(propiedades);
        }

        public async Task<PropiedadViewModel> GetByCodigoViewModel(string codigo)
        {
            return _mapper.Map<PropiedadViewModel>(await _repository.GetByCodigo(codigo));
        }

        public async Task<SavePropiedadViewModel> GetByCodigoSaveViewModel(string codigo)
        {
            return _mapper.Map<SavePropiedadViewModel>(await _repository.GetByCodigo(codigo));
        }

        public SavePropiedadViewModel convertir(PropiedadViewModel model)
        {
            Propiedad propiedad = _mapper.Map<Propiedad>(model);
            return _mapper.Map<SavePropiedadViewModel>(propiedad);
        }

        private string GenerarCodigoUnico()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
