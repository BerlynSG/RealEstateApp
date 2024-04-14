using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class MejoraRepository : GenericRepository<Mejora>, IMejoraRepository
    {
        private readonly ApplicationContext _context;
        public MejoraRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Mejora>> GetAllWithIncludeAsync()
        {
            return await _context.Set<Mejora>()
                .Include(t => t.Propiedades)
                .ToListAsync();
        }
    }
}
