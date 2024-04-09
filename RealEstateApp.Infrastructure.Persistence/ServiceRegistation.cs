using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Infrastructure.Persistence.Contexts;

namespace RealEstateApp.Infrastructure.Persistence
{//decorator pattern - Extension method
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
        {
            #region "Context"
            if (false/*config.GetValue<bool>("UseInMemoryDB")*/)
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                string? connectionString = config.GetConnectionString("Default");
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region "Repositories"
            /*services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<ILabTestRepository, LabTestRepository>();
            services.AddTransient<IPatientRepository, PatientRepository>();
            services.AddTransient<ILabResultRepository, LabResultRepository>();
            services.AddTransient<ICitaRepository, CitaRepository>();*/
            #endregion
        }
    }
}
