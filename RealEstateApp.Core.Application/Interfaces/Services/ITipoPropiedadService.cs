using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITipoPropiedadService : IGenericService<SaveTipoPropiedadViewModel, TipoPropiedadViewModel, TipoPropiedad>
    {

    }
}
