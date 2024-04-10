using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class MejoraRepository : GenericRepository<Mejora>, IMejoraRepository
    {
        private readonly ApplicationContext _context;
        public MejoraRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
