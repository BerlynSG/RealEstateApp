using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropiedadService : IGenericService<SavePropiedadViewModel, PropiedadViewModel, Propiedad>
    {
        Task<List<PropiedadViewModel>> GetAllFilteredViewModel(FiltroPropiedadViewModel filtro);
        Task<PropiedadViewModel> GetByCodigoViewModel(string codigo);
        Task<SavePropiedadViewModel> GetByCodigoSaveViewModel(string codigo);
        Task DeleteAllByAgenteId(string agenteId);
        Task AddImages(List<ImagenPropiedad> imagenes);
        Task<int> AddFavorito(string codigo, string clientId);
    }
}
