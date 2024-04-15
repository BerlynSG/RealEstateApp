using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IMejoraRepository : IGenericRepository<Mejora>
    {
        Task<List<Mejora>> GetAllWithIncludeAsync();
    }
}
