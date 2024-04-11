using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Services
{
    public interface ITipoVentaService : IGenericService<SaveTipoVentaViewModel, TipoVentaViewModel, TipoVenta>
    {

    }
}
