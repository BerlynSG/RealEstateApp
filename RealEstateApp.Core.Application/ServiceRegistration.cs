﻿using Microsoft.Extensions.DependencyInjection;
using RealEstateApp.Core.Application.Interfaces.Services;
using RealEstateApp.Core.Application.Services;
using System.Reflection;

namespace RealEstateApp.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            #region Services
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IPropiedadService, PropiedadService>();
            services.AddTransient<ITipoPropiedadService, TipoPropiedadService>();
            services.AddTransient<ITipoVentaService, TipoVentaService>();
            services.AddTransient<IMejoraService, MejoraService>();
            #endregion
        }
    }
}
