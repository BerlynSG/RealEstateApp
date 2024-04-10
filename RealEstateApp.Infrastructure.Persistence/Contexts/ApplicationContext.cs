using Microsoft.EntityFrameworkCore;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        public DbSet<Propiedad> Propiedades { get; set; }
        public DbSet<TipoPropiedad> TiposPropiedad { get; set; }
        public DbSet<TipoVenta> TiposVenta { get; set; }
        public DbSet<Mejora> Mejoras { get; set; }
        public DbSet<MejoraPropiedad> MejorasPropiedades { get; set; }
        public DbSet<PropiedadFavorita> PropiedadesFavoritas { get; set; }
        public DbSet<ImagenPropiedad> ImagenesPropiedad { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            #region "Tables"
            modelBuilder.Entity<Propiedad>().ToTable("Propiedades");
            modelBuilder.Entity<TipoPropiedad>().ToTable("TiposPropiedad");
            modelBuilder.Entity<TipoVenta>().ToTable("TiposVenta");
            modelBuilder.Entity<Mejora>().ToTable("Mejoras");
            modelBuilder.Entity<MejoraPropiedad>().ToTable("MejorasPropiedades");
            modelBuilder.Entity<PropiedadFavorita>().ToTable("PropiedadesFavoritas");
            modelBuilder.Entity<ImagenPropiedad>().ToTable("ImagenesPropiedad");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Propiedad>().HasKey(p => p.Id);
            modelBuilder.Entity<TipoPropiedad>().HasKey(t => t.Id);
            modelBuilder.Entity<TipoVenta>().HasKey(t => t.Id);
            modelBuilder.Entity<Mejora>().HasKey(m => m.Id);
            modelBuilder.Entity<MejoraPropiedad>().HasKey(m => m.Id);
            modelBuilder.Entity<PropiedadFavorita>().HasKey(f => f.Id);
            modelBuilder.Entity<ImagenPropiedad>().HasKey(i => i.Id);
            #endregion


            #region "Relationships"
            modelBuilder.Entity<TipoPropiedad>().HasMany<Propiedad>(p => p.Propiedades).WithOne(t => t.TipoPropiedad)
                .HasForeignKey(t => t.TipoPropiedadId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TipoVenta>().HasMany<Propiedad>(p => p.Propiedades).WithOne(t => t.TipoVenta)
                .HasForeignKey(t => t.TipoVentaId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Propiedad>().HasMany<MejoraPropiedad>(p => p.Mejoras).WithOne(m => m.Propiedad)
                .HasForeignKey(m => m.PropiedadId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Mejora>().HasMany<MejoraPropiedad>(m => m.Propiedades).WithOne(p => p.Mejora)
                .HasForeignKey(p => p.MejoraId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Propiedad>().HasMany<PropiedadFavorita>(p => p.Favoritos).WithOne(f => f.Propiedad)
                .HasForeignKey(f => f.PropiedadId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Propiedad>().HasMany<ImagenPropiedad>(p => p.Imagenes).WithOne(i => i.Propiedad)
                .HasForeignKey(i => i.PropiedadId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Properties configuration"

            #region "Propiedad"
            modelBuilder.Entity<Propiedad>().Property(u => u.Codigo)
                .IsRequired().HasMaxLength(6);
            modelBuilder.Entity<Propiedad>().Property(u => u.TipoPropiedadId)
                .IsRequired();
            modelBuilder.Entity<Propiedad>().Property(u => u.TipoVentaId)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Propiedad>().Property(u => u.Valor)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Propiedad>().Property(u => u.Habitaciones)
                .IsRequired();
            modelBuilder.Entity<Propiedad>().Property(u => u.Baños)
                .IsRequired();
            modelBuilder.Entity<Propiedad>().Property(u => u.Tamaño)
                .IsRequired();
            modelBuilder.Entity<Propiedad>().Property(u => u.Descripcion)
                .IsRequired();
            modelBuilder.Entity<Propiedad>().Property(u => u.AgenteId)
                .IsRequired();
            #endregion

            #region "TipoPropiedad"
            modelBuilder.Entity<TipoPropiedad>().Property(u => u.Nombre)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<TipoPropiedad>().Property(u => u.Descripcion)
                .IsRequired();
            #endregion

            #region "TipoVenta"
            modelBuilder.Entity<TipoVenta>().Property(u => u.Nombre)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<TipoVenta>().Property(u => u.Descripcion)
                .IsRequired();
            #endregion

            #region "Mejora"
            modelBuilder.Entity<Mejora>().Property(u => u.Nombre)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Mejora>().Property(u => u.Descripcion)
                .IsRequired();
            #endregion

            #region "MejoraPropiedad"
            modelBuilder.Entity<MejoraPropiedad>().Property(u => u.MejoraId)
                .IsRequired();
            modelBuilder.Entity<MejoraPropiedad>().Property(u => u.PropiedadId)
                .IsRequired();
            #endregion

            #region "PropiedadFavorita"
            modelBuilder.Entity<PropiedadFavorita>().Property(u => u.PropiedadId)
                .IsRequired();
            modelBuilder.Entity<PropiedadFavorita>().Property(u => u.ClienteId)
                .IsRequired();
            #endregion

            #region "ImagenPropiedad"
            modelBuilder.Entity<ImagenPropiedad>().Property(u => u.PropiedadId)
                .IsRequired();
            modelBuilder.Entity<ImagenPropiedad>().Property(u => u.Path)
                .IsRequired();
            #endregion

            #endregion
        }
    }
}
