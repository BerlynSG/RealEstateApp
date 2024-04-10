using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Repositories;
using RealEstateApp.Infrastructure.Persistence.Contexts;
using RealEstateApp.Infrastructure.Persistence.Repositories;

namespace RealEstateApp.Infrastructure.Persistence
{//decorator pattern - Extension method
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration config)
        {
            #region "Context"
            if (config.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationContext>(options => options.UseInMemoryDatabase("AppDb"));
            }
            else
            {
                string? connectionString = config.GetConnectionString("DefaultConnection");
                services.AddDbContext<ApplicationContext>(options => options.UseSqlServer(connectionString,
                    m => m.MigrationsAssembly(typeof(ApplicationContext).Assembly.FullName)));
            }
            #endregion

            #region "Repositories"
            services.AddTransient<IPropiedadRepository, PropiedadRepository>();
            services.AddTransient<ITipoPropiedadRepository, TipoPropiedadRepository>();
            services.AddTransient<ITipoVentaRepository, TipoVentaRepository>();
            services.AddTransient<IMejoraRepository, MejoraRepository>();
            #endregion
        }
    }
}
