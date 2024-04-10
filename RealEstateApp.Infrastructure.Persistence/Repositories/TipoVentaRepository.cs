using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class TipoVentaRepository : GenericRepository<TipoVenta>, ITipoVentaRepository
    {
        private readonly ApplicationContext _context;
        public TipoVentaRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }
    }
}
