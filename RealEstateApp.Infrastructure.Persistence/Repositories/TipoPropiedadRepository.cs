using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class TipoPropiedadRepository : GenericRepository<TipoPropiedad>, ITipoPropiedadRepository
    {
        private readonly ApplicationContext _context;
        public TipoPropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TipoPropiedad>> GetAllWithIncludeAsync()
        {
            return await _context.Set<TipoPropiedad>()
                .Include(t => t.Propiedades)
                .ToListAsync();
        }
    }
}
