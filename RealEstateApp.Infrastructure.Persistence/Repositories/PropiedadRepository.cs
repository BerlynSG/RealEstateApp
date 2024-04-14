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
                .ToListAsync();
        }

        public async Task<List<Propiedad>> GetAllByAgente(string agenteId)
        {
            return await _context.Set<Propiedad>()
                .Include(p => p.TipoPropiedad)
                .Include(p => p.TipoVenta)
                .Include(p => p.Mejoras)
                .Where(p => p.AgenteId == agenteId).ToListAsync();
        }

        public async Task<List<Propiedad>> GetAllFavoritos(string clienteId)
        {
            List<PropiedadFavorita> favoritos = await _context.Set<PropiedadFavorita>()
                .Where(f => f.ClienteId == clienteId)
                .Include(f => f.Propiedad).ThenInclude(p => p.TipoPropiedad)
                .Include(f => f.Propiedad).ThenInclude(p => p.TipoVenta)
                .Include(f => f.Propiedad).ThenInclude(p => p.Mejoras)
                .ToListAsync();
            return favoritos.Select(f => f.Propiedad).ToList();
        }

        public async Task<Propiedad?> GetByCodigo(string codigo)
        {
            Propiedad? propiedad = await _context.Set<Propiedad>()
                .Include(p => p.TipoPropiedad)
                .Include(p => p.TipoVenta)
                .Include(p => p.Mejoras)
                .FirstOrDefaultAsync(p => p.Codigo == codigo);
            return propiedad;
        }

        /*public override async Task<Propiedad> AddAsync(Propiedad entity)
        {
            string codigo = GenerarCodigoUnico();
            while (GetByCodigo(codigo) != null)
            {
                codigo = GenerarCodigoUnico();
            }
            await _context.Set<Propiedad>().AddAsync(entity);
            await _context.Set<ImagenPropiedad>().AddRangeAsync(entity.Imagenes.Select(
                i => new ImagenPropiedad() { PropiedadId = entity.Id, Path = i.Path }).ToList());
            await _context.Set<MejoraPropiedad>().AddRangeAsync(entity.Mejoras.Select(
                i => new MejoraPropiedad() { PropiedadId = entity.Id, MejoraId = i.MejoraId }).ToList());
            await _context.SaveChangesAsync();
            return entity;
        }

        private string GenerarCodigoUnico()
        {
            Random random = new Random();
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, 6)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }*/
    }
}
