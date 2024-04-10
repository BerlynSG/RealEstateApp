using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class TipoPropiedadRepository : GenericRepository<TipoPropiedad>, ITipoPropiedadRepository
    {
        private readonly ApplicationContext _context;
        public TipoPropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
