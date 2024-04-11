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
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => i.Path)))
                .ReverseMap()
                .ForMember(p => p.Favoritos, opt => opt.Ignore())
                .ForMember(p => p.AgenteId, opt => opt.Ignore())
                .ForMember(p => p.TipoVentaId, opt => opt.Ignore())
                .ForMember(p => p.TipoPropiedadId, opt => opt.Ignore())
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => new ImagenPropiedad() { Path = i })));

            /*CreateMap<Propiedad, SavePropiedadViewModel>()
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => i.Path)))
                .ReverseMap()
                .ForMember(p => p.Favoritos, opt => opt.Ignore())
                .ForMember(p => p.AgenteId, opt => opt.Ignore())
                .ForMember(p => p.TipoVentaId, opt => opt.Ignore())
                .ForMember(p => p.TipoPropiedadId, opt => opt.Ignore())
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => new ImagenPropiedad() { Path = i })));

            CreateMap<TipoPropiedad, TipoPropiedadViewModel>()
                .ForMember(vm => vm.Entrada, opt => opt.Ignore())
                .ForMember(vm => vm.Salida, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.Fecha, opt => opt.Ignore())
                .ForMember(p => p.DesdeCuenta, opt => opt.Ignore())
                .ForMember(p => p.HastaCuenta, opt => opt.Ignore())
                .ForMember(p => p.DesdeCuentaId, opt => opt.Ignore())
                .ForMember(p => p.HastaCuentaId, opt => opt.Ignore());

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()
                .ReverseMap();

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
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
