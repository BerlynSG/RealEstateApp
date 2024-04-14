using AutoMapper;
using RealEstateApp.Core.Application.ViewModels.Mejora;
using RealEstateApp.Core.Application.ViewModels.Propiedad;
using RealEstateApp.Core.Application.ViewModels.TipoPropiedad;
using RealEstateApp.Core.Application.ViewModels.TipoVenta;
using RealEstateApp.Core.Application.ViewModels.User;
using RealEstateApp.Core.Domain.Entities;

namespace RealEstateApp.Core.Application.Mapping
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            /*CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ForMember(d => d.MontoInicial, o => o.Ignore())
                .ReverseMap();

            CreateMap<UpdateRequest, UpdateUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<DataResponse, UserViewModel>()
                .ReverseMap();*/

            CreateMap<Propiedad, PropiedadViewModel>()
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => i.Path).ToList()))
                .ForMember(p => p.Agente, opt => opt.Ignore())
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => p.Mejoras
                    .Select(m => m.Mejora )))
                .ReverseMap()
                .ForMember(p => p.Favoritos, opt => opt.Ignore())
                .ForMember(p => p.AgenteId, opt => opt.Ignore())
                .ForMember(p => p.TipoVentaId, opt => opt.MapFrom(p => p.TipoVenta.Id))
                .ForMember(p => p.TipoPropiedadId, opt => opt.MapFrom(p => p.TipoPropiedad.Id))
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => p.Mejoras
                    .Select(m => new MejoraPropiedad() { MejoraId = m.Id, PropiedadId = p.Id })))
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes
                    .Select(i => new ImagenPropiedad() { PropiedadId = p.Id, Path = i }).ToList()));

            CreateMap<Propiedad, SavePropiedadViewModel>()
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => i.Path)))
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => string.Join(",", p.Mejoras.Select(m => m.MejoraId))))
                .ForMember(p => p.ListaMejora, opt => opt.Ignore())
                .ForMember(p => p.ListaTipoPropiedad, opt => opt.Ignore())
                .ForMember(p => p.ListaTipoVenta, opt => opt.Ignore())
                .ForMember(p => p.ImagenesFiles, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.Favoritos, opt => opt.Ignore())
                .ForMember(p => p.TipoVenta, opt => opt.Ignore())
                .ForMember(p => p.TipoPropiedad, opt => opt.Ignore())
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes
                    .Select(i => new ImagenPropiedad() { PropiedadId = p.Id, Path = i })))
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => p.Mejoras.Split(",", StringSplitOptions.None)
                    .Select(m => new MejoraPropiedad() { PropiedadId = p.Id, MejoraId = int.Parse(m) }).ToList()));

            CreateMap<TipoPropiedad, TipoPropiedadViewModel>()
                .ForMember(p => p.CantidadPropiedades, opt => opt.MapFrom(p => p.Propiedades.Count))
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<TipoVenta, TipoVentaViewModel>()
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<Mejora, MejoraViewModel>()
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<Mejora, SaveMejoraViewModel>()
                .ReverseMap()
                .ForMember(p => p.Id, opt => opt.Ignore())
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            /*CreateMap<TipoVenta, SaveTipoVentaViewModel>()
                .ReverseMap()
                .ForMember(t => t.Id, opt => opt.Ignore())
                .ForMember(t => t.HastaCuenta, opt => opt.Ignore())
                .ForMember(t => t.DesdeCuenta, opt => opt.Ignore());

            CreateMap<Mejora, MejoraViewModel>()
                .ReverseMap();

            CreateMap<Mejora, SaveMejoraViewModel>()
                .ReverseMap()
                .ForMember(b => b.Cuenta, opt => opt.Ignore());*/
        }
    }
}
