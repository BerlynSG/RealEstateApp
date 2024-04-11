using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        private readonly ApplicationContext _context;
        public PropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
