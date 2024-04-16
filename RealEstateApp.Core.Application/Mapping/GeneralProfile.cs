using AutoMapper;
using RealEstateApp.Core.Application.Dtos.Account;
using RealEstateApp.Core.Application.ViewModels.Agente;
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
            #region UserProfile
            CreateMap<AuthenticationRequest, LoginViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<RegisterAdminsRequest, SaveAdminsViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<RegisterRequest, SaveUserViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<RegisterAdminsRequest, SaveAdminsViewModel>()
                .ForMember(d => d.Error, o => o.Ignore())
                .ForMember(d => d.HasError, o => o.Ignore())
                .ReverseMap();

            CreateMap<AgenteViewModel, SaveUserViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.Nombre))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.Apellidos))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.Celular))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Correo))
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage))
                .ReverseMap()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Phone))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ProfileImage, opt => opt.MapFrom(src => src.ProfileImage));

            CreateMap<UpdateRequest, AgenteViewModel>()
                .ForMember(dest => dest.Nombre, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.Apellidos, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Correo, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore());

            CreateMap<AuthenticationResponse, SaveUserViewModel>()
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.UserName))
                .ForMember(dest => dest.Password, opt => opt.Ignore()) 
                .ForMember(dest => dest.ConfirmPassword, opt => opt.Ignore()) 
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.Phone, opt => opt.MapFrom(src => src.PhoneNumber))
                .ForMember(dest => dest.Rol, opt => opt.MapFrom(src => src.Rol))
                .ForMember(dest => dest.ProfileImage, opt => opt.Ignore()) 
                .ForMember(dest => dest.ImagePath, opt => opt.MapFrom(src => src.ImagePath))
                .ForMember(dest => dest.EmailConfirmed, opt => opt.MapFrom(src => src.IsVerified))
                .ForMember(dest => dest.HasError, opt => opt.MapFrom(src => src.HasError))
                .ForMember(dest => dest.Error, opt => opt.MapFrom(src => src.Error))
                .ForMember(dest => dest.Id, opt => opt.Ignore());

            #endregion

            CreateMap<Propiedad, PropiedadViewModel>()
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => i.Path).ToList()))
                .ForMember(p => p.Agente, opt => opt.Ignore())
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => p.Mejoras.Select(m => m.Mejora ).ToList()))
                .ForMember(p => p.Favorito, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.Favoritos, opt => opt.Ignore())
                .ForMember(p => p.AgenteId, opt => opt.Ignore())
                .ForMember(p => p.TipoVentaId, opt => opt.MapFrom(p => p.TipoVenta.Id))
                .ForMember(p => p.TipoPropiedadId, opt => opt.MapFrom(p => p.TipoPropiedad.Id))
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => p.Mejoras
                    .Select(m => new MejoraPropiedad() { MejoraId = m.Id, PropiedadId = p.Id }).ToList()))
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes
                    .Select(i => new ImagenPropiedad() { PropiedadId = p.Id, Path = i }).ToList()));

            CreateMap<Propiedad, SavePropiedadViewModel>()
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes.Select(i => i.Path).ToList()))
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => string.Join(",", p.Mejoras.Select(m => m.MejoraId))))
                .ForMember(p => p.ListaMejora, opt => opt.Ignore())
                .ForMember(p => p.ListaTipoPropiedad, opt => opt.Ignore())
                .ForMember(p => p.ListaTipoVenta, opt => opt.Ignore())
                .ForMember(p => p.ImagenesFiles, opt => opt.Ignore())
                .ReverseMap()
                .ForMember(p => p.Favoritos, opt => opt.Ignore())
                .ForMember(p => p.TipoVenta, opt => opt.Ignore())
                .ForMember(p => p.TipoPropiedad, opt => opt.Ignore())
                .ForMember(p => p.Mejoras, opt => opt.MapFrom(p => p.Mejoras.Split(",", StringSplitOptions.None)
                    .Select(m => new MejoraPropiedad() { PropiedadId = p.Id, MejoraId = int.Parse(m) }).ToList()))
                .ForMember(p => p.Imagenes, opt => opt.MapFrom(p => p.Imagenes
                    .Select(i => new ImagenPropiedad() { PropiedadId = p.Id, Path = i }).ToList()));

            CreateMap<TipoPropiedad, TipoPropiedadViewModel>()
                .ForMember(p => p.CantidadPropiedades, opt => opt.MapFrom(p => p.Propiedades.Count))
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<TipoPropiedad, SaveTipoPropiedadViewModel>()
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<TipoVenta, TipoVentaViewModel>()
                .ForMember(p => p.CantidadPropiedades, opt => opt.MapFrom(p => p.Propiedades.Count))
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<TipoVenta, SaveTipoVentaViewModel>()
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<Mejora, MejoraViewModel>()
                .ForMember(p => p.CantidadPropiedades, opt => opt.MapFrom(m => m.Propiedades.Count))
                .ReverseMap()
                .ForMember(p => p.Propiedades, opt => opt.Ignore());

            CreateMap<Mejora, SaveMejoraViewModel>()
                .ReverseMap()
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
