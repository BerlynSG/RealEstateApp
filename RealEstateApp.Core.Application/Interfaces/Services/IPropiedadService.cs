using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface IPropiedadService : IGenericService<PropiedadViewModel, SavePropiedadViewModel, Propiedad>
    {

    }
}
