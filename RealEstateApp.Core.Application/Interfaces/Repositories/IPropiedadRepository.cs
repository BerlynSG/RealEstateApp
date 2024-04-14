using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Interfaces.Repositories
{
    public interface IPropiedadRepository : IGenericRepository<Propiedad>
    {
        Task<List<Propiedad>> GetAllWithInclude();
        Task<List<Propiedad>> GetAllByAgente(string agenteId);
        Task<List<Propiedad>> GetAllFavoritos(string clienteId);
        Task<Propiedad?> GetByCodigo(string codigo);
        Task DeleteAllByAgenteIdAsync(string agenteId);
    }
}
