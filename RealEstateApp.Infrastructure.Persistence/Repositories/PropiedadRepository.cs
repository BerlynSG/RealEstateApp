using RealEstateApp.Core.Domain.Entities;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

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
                .Include(p => p.Imagenes)
                .Include(p => p.Favoritos)
                .ToListAsync();
        }

        public async Task<List<Propiedad>> GetAllByAgente(string agenteId)
        {
            return await _context.Set<Propiedad>()
                .Include(p => p.TipoPropiedad)
                .Include(p => p.TipoVenta)
                .Include(p => p.Mejoras)
                    .ThenInclude(m => m.Mejora)
                .Include(p => p.Imagenes)
                .Include(p => p.Favoritos)
                .Where(p => p.AgenteId == agenteId).ToListAsync();
        }

        public async Task<List<Propiedad>> GetAllFavoritos(string clienteId)
        {
            List<PropiedadFavorita> favoritos = await _context.Set<PropiedadFavorita>()
                .Where(f => f.ClienteId == clienteId)
                .Include(f => f.Propiedad).ThenInclude(p => p.TipoPropiedad)
                .Include(f => f.Propiedad).ThenInclude(p => p.TipoVenta)
                .Include(f => f.Propiedad).ThenInclude(p => p.Imagenes)
                .Include(f => f.Propiedad).ThenInclude(p => p.Mejoras)
                    .ThenInclude(m => m.Mejora)
                .Include(f => f.Propiedad).ThenInclude(p => p.Favoritos)
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
                .Include(p => p.Imagenes)
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

        public async Task DeleteAllByAgenteIdAsync(string agenteId)
        {
            List<Propiedad> propiedades = await _context.Set<Propiedad>()
                .Where(p => p.AgenteId == agenteId).ToListAsync();
            _context.Set<Propiedad>().RemoveRange(propiedades);
            await _context.SaveChangesAsync();
        }

        public async Task AddImages(List<ImagenPropiedad> imagenes)
        {
            if (imagenes != null && imagenes.Count > 0)
            {
                List<ImagenPropiedad> eliminar = await _context.Set<ImagenPropiedad>()
                    .Where(i => i.PropiedadId == imagenes[0].PropiedadId).ToListAsync();
                if (eliminar != null && eliminar.Count > 0)
                    _context.Set<ImagenPropiedad>().RemoveRange(eliminar);
                await _context.Set<ImagenPropiedad>().AddRangeAsync(imagenes);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<int> AddFavorito(string codigo, string clientId)
        {
            Propiedad? propiedad = await _context.Set<Propiedad>().FirstOrDefaultAsync(p => p.Codigo == codigo);
            if (propiedad != null)
            {
                List<PropiedadFavorita> favoritas = await _context.Set<PropiedadFavorita>()
                    .Where(f => f.PropiedadId == propiedad.Id && f.ClienteId == clientId).ToListAsync();
                if (favoritas == null || favoritas.Count == 0)
                {
                    await _context.Set<PropiedadFavorita>()
                        .AddAsync(new PropiedadFavorita() { ClienteId = clientId, PropiedadId = propiedad.Id });
                    await _context.SaveChangesAsync();
                    return 2;
                }
                else
                {
                    _context.Set<PropiedadFavorita>().RemoveRange(favoritas);
                    await _context.SaveChangesAsync();
                    return 1;
                }
            }
            return 0;
        }
        public async Task<int> GetTotalPropertiesCountAsync()
        {
            // Obtener el total de propiedades registradas en el sistema
            int totalPropertiesCount = await _context.Propiedades.CountAsync();

            return totalPropertiesCount;
        }
    }
}
