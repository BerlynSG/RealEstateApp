using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class TipoVentaRepository : GenericRepository<TipoVenta>, ITipoVentaRepository
    {
        private readonly ApplicationContext _context;
        public TipoVentaRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<TipoVenta>> GetAllWithIncludeAsync()
        {
            return await _context.Set<TipoVenta>()
                .Include(t => t.Propiedades)
                .ToListAsync();
        }
    }
}
