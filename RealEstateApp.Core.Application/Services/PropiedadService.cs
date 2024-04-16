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
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        public PropiedadService(IPropiedadRepository repository, IMapper mapper, IAccountService accountService) : base(repository, mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _accountService = accountService;
        }

        public override async Task<SavePropiedadViewModel> Add(SavePropiedadViewModel vm)
        {
            vm.Codigo = GenerarCodigoUnico();
            vm.Imagenes = null;
            return await base.Add(vm);
        }

        public async Task AddImages(List<ImagenPropiedad> imagenes)
        {
            await _repository.AddImages(imagenes);
        }

        public async Task DeleteAllByAgenteId(string agenteId)
        {
            await _repository.DeleteAllByAgenteIdAsync(agenteId);
        }

        public async Task<List<PropiedadViewModel>> GetAllFilteredViewModel(FiltroPropiedadViewModel filtro)
        {
            List<Propiedad> propiedades;
            //prop = await _propiedadService.GetAllViewModel();
            if (filtro.TipoFiltroUsuario == 2)
            {
                propiedades = await _repository.GetAllByAgente(filtro.UsuarioId);
            }
            else if (filtro.TipoFiltroUsuario == 1)
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

            List<PropiedadViewModel> result = _mapper.Map<List<PropiedadViewModel>>(propiedades);
            result.ForEach(p => p.Favorito = propiedades.FirstOrDefault(p2 => p2.Id == p.Id)
                .Favoritos.FirstOrDefault(f => f.ClienteId == filtro.UsuarioId) != null);

            return result;
        }

        public async Task<PropiedadViewModel> GetByCodigoViewModel(string codigo)
        {
            PropiedadViewModel propiedad = _mapper.Map<PropiedadViewModel>(await _repository.GetByCodigo(codigo));
            return propiedad;
        }

        public async Task<SavePropiedadViewModel> GetByCodigoSaveViewModel(string codigo)
        {
            return _mapper.Map<SavePropiedadViewModel>(await _repository.GetByCodigo(codigo));
        }

        public async Task<int> AddFavorito(string codigo, string clientId)
        {
            return await _repository.AddFavorito(codigo, clientId);
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
