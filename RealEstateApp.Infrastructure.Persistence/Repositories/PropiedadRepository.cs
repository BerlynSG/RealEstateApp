using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Xml.Linq;

namespace RealEstateApp.Infrastructure.Persistence.Repositories
{
    public class PropiedadRepository : GenericRepository<Propiedad>, IPropiedadRepository
    {
        private readonly ApplicationContext _context;
        public PropiedadRepository(ApplicationContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Propiedad>> GetAllWithInclude()
        {
            return await _context.Set<Propiedad>()
                .Include(p => p.TipoPropiedad)
                .Include(p => p.TipoVenta)
                .Include(p => p.Mejoras)
                    .ThenInclude(m => m.Mejora)
                .ToListAsync();
        }

        public async Task<List<Propiedad>> GetAllByAgente(string agenteId)
        {
            return await _context.Set<Propiedad>()
                .Include(p => p.TipoPropiedad)
                .Include(p => p.TipoVenta)
                .Include(p => p.Mejoras)
                    .ThenInclude(m => m.Mejora)
                .Where(p => p.AgenteId == agenteId).ToListAsync();
        }

        public async Task<List<Propiedad>> GetAllFavoritos(string clienteId)
        {
            List<PropiedadFavorita> favoritos = await _context.Set<PropiedadFavorita>()
                .Where(f => f.ClienteId == clienteId)
                .Include(f => f.Propiedad).ThenInclude(p => p.TipoPropiedad)
                .Include(f => f.Propiedad).ThenInclude(p => p.TipoVenta)
                .Include(f => f.Propiedad).ThenInclude(p => p.Mejoras)
                    .ThenInclude(m => m.Mejora)
                .ToListAsync();
            return favoritos.Select(f => f.Propiedad).ToList();
        }

        public async Task<Propiedad?> GetByCodigo(string codigo)
        {
            Propiedad? propiedad = await _context.Set<Propiedad>()
                .Include(p => p.TipoPropiedad)
                .Include(p => p.TipoVenta)
                .Include(p => p.Mejoras)
                    .ThenInclude(m => m.Mejora)
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
            return propiedad;
        }

        public override async Task UpdateAsync(Propiedad entity, int id)
        {
            List<MejoraPropiedad> mejoras = await _context.Set<MejoraPropiedad>().Where(p => p.PropiedadId == id).ToListAsync();
            _context.Set<MejoraPropiedad>().RemoveRange(mejoras);
            await _context.Set<MejoraPropiedad>().AddRangeAsync(entity.Mejoras);
            List<ImagenPropiedad> imagenes = await _context.Set<ImagenPropiedad>().Where(p => p.PropiedadId == id).ToListAsync();
            _context.Set<ImagenPropiedad>().RemoveRange(imagenes);
            await _context.Set<ImagenPropiedad>().AddRangeAsync(entity.Imagenes);
            await _context.SaveChangesAsync();
            await base.UpdateAsync(entity, id);
        }
    }
}
