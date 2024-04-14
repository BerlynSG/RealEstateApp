using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface ITipoVentaRepository : IGenericRepository<TipoVenta>
    {
        Task<List<TipoVenta>> GetAllWithIncludeAsync();
    }
}
