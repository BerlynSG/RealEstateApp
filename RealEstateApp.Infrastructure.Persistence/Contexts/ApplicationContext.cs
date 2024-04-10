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
                .HasForeignKey(p => p.Mejora).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Propiedad>().HasMany<PropiedadFavorita>(p => p.Favoritos).WithOne(f => f.Propiedad)
                .HasForeignKey(f => f.PropiedadId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Propiedad>().HasMany<ImagenPropiedad>(p => p.Imagenes).WithOne(i => i.Propiedad)
                .HasForeignKey(i => i.PropiedadId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            /*#region "Properties configuration"

            #region "User"
            modelBuilder.Entity<User>().Property(u => u.UserName)
                .IsRequired().HasMaxLength(100);
            modelBuilder.Entity<User>().Property(u => u.Password)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Name)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<User>().Property(u => u.LastName)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<User>().Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<User>().Property(u => u.Phone)
                .IsRequired();
            #endregion

            #region "Medico"
            modelBuilder.Entity<Medico>().Property(u => u.Name)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Medico>().Property(u => u.LastName)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Medico>().Property(u => u.Cedula)
                .IsRequired();
            modelBuilder.Entity<Medico>().Property(u => u.Email)
                .IsRequired();
            modelBuilder.Entity<Medico>().Property(u => u.Phone)
                .IsRequired();
            modelBuilder.Entity<Medico>().Property(u => u.Image)
                .IsRequired();
            #endregion

            #region "LabTest"
            modelBuilder.Entity<LabTest>().Property(u => u.Name)
                .IsRequired().HasMaxLength(150);
            #endregion

            #region "Patient"
            modelBuilder.Entity<Patient>().Property(u => u.Name)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Patient>().Property(u => u.LastName)
                .IsRequired().HasMaxLength(150);
            modelBuilder.Entity<Patient>().Property(u => u.Phone)
                .IsRequired();
            modelBuilder.Entity<Patient>().Property(u => u.Address)
                .IsRequired();
            modelBuilder.Entity<Patient>().Property(u => u.Cedula)
                .IsRequired();
            modelBuilder.Entity<Patient>().Property(u => u.Birthdate)
                .IsRequired();
            modelBuilder.Entity<Patient>().Property(u => u.Image)
                .IsRequired();
            #endregion

            #region "LabResult"
            modelBuilder.Entity<LabResult>().Property(u => u.Result)
                .IsRequired();
            modelBuilder.Entity<LabResult>().Property(u => u.CitaId)
                .IsRequired();
            #endregion

            #region "Cita"
            modelBuilder.Entity<Cita>().Property(u => u.Date)
                .IsRequired();
            modelBuilder.Entity<Cita>().Property(u => u.Cause)
                .IsRequired();
            modelBuilder.Entity<Cita>().Property(u => u.MedicoId)
                .IsRequired();
            modelBuilder.Entity<Cita>().Property(u => u.PatientId)
                .IsRequired();
            #endregion

            #endregion
            
            #region "Tables"
            modelBuilder.Entity<Serie>().ToTable("Series");
            modelBuilder.Entity<Productor>().ToTable("Productores");
            modelBuilder.Entity<Genero>().ToTable("Generos");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<Serie>().HasKey(s => s.Id);
            modelBuilder.Entity<Productor>().HasKey(p => p.Id);
            modelBuilder.Entity<Genero>().HasKey(g => g.Id);
            #endregion

            #region "Relationships"
            modelBuilder.Entity<Productor>().HasMany<Serie>(p => p.series).WithOne(s => s.Productor)
                .HasForeignKey(s => s.IdProductor).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Genero>().HasMany<Serie>(g => g.SeriesP).WithOne(s => s.GeneroPrimario)
                .HasForeignKey(s => s.IdGeneroPrimario).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Genero>().HasMany<Serie>(g => g.SeriesS).WithOne(s => s.GeneroSecundario)
                .HasForeignKey(s => s.IdGeneroSecundario).OnDelete(DeleteBehavior.NoAction);
            #endregion

            #region "Properties configuration"

            #region "Series"
            modelBuilder.Entity<Serie>().Property(s => s.Name)
                .IsRequired().HasMaxLength(150);
            #endregion

            #region "Productores"
            modelBuilder.Entity<Productor>().Property(p => p.Name)
                .IsRequired().HasMaxLength(150);
            #endregion

            #region "Generos"
            modelBuilder.Entity<Genero>().Property(p => p.Name)
                .IsRequired().HasMaxLength(150);
            #endregion

            #endregion*/
        }
    }
}
