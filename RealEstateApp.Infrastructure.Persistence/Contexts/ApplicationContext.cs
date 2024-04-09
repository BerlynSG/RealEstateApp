using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstateApp.Infrastructure.Persistence.Contexts
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        /*public DbSet<User> Users { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<LabTest> LabTests { get; set; }
        public DbSet<Patient> Patients { get; set; }
        public DbSet<LabResult> LabResults { get; set; }
        public DbSet<Cita> Citas { get; set; }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            /*#region "Tables"
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<Medico>().ToTable("Medicos");
            modelBuilder.Entity<LabTest>().ToTable("LabTests");
            modelBuilder.Entity<Patient>().ToTable("Patients");
            modelBuilder.Entity<LabResult>().ToTable("LabResults");
            modelBuilder.Entity<Cita>().ToTable("Citas");
            #endregion

            #region "Primary Keys"
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<Medico>().HasKey(m => m.Id);
            modelBuilder.Entity<LabTest>().HasKey(m => m.Id);
            modelBuilder.Entity<Patient>().HasKey(m => m.Id);
            modelBuilder.Entity<LabResult>().HasKey(m => m.Id);
            modelBuilder.Entity<Cita>().HasKey(m => m.Id);
            #endregion


            #region "Relationships"
            modelBuilder.Entity<Patient>().HasMany<Cita>(p => p.Citas).WithOne(c => c.Patient)
                .HasForeignKey(c => c.PatientId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Medico>().HasMany<Cita>(m => m.Citas).WithOne(c => c.Medico)
                .HasForeignKey(c => c.MedicoId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Cita>().HasMany<LabResult>(c => c.LabResults).WithOne(r => r.Cita)
                .HasForeignKey(r => r.CitaId).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<LabTest>().HasMany<LabResult>(l => l.LabResults).WithOne(l => l.LabTest)
                .HasForeignKey(l => l.LabTestId).OnDelete(DeleteBehavior.Cascade);
            #endregion

            #region "Properties configuration"

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
